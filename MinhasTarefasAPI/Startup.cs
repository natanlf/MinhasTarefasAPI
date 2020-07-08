using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MinhasTarefasAPI.Database;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Repositories;
using MinhasTarefasAPI.Repositories.Contracts;

namespace MinhasTarefasAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Tira a validação do modo state
            services.Configure<ApiBehaviorOptions>(op =>
            {
                op.SuppressModelStateInvalidFilter = true;
            });
            services.AddDbContext<MinhasTarefasContext>(opt =>
            {
                //UseSqlite é necessário instalar pelo gerenciador NuGet
                opt.UseSqlite("Data Source=Database\\MinhasTarefas.db");
            });

            //Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions( //para não dar loop no json
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            //Autenticação
            /*AddDefaultIdentity traz config de telas, como é uma api rest, não preciso disso*/
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MinhasTarefasContext>()
                .AddDefaultTokenProviders(); //AddDefaultTokenProviders habilita o uso de tokens no Identity

            /* Config a forma de autenticação */
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chave-api-jwt-minhas-tarefas"))
                };
            });

            /*sistema de autenticação é por token*/
            services.AddAuthorization(auth => {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                             .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                             .RequireAuthenticatedUser()
                                             .Build()
                );
            });

            /*Ao redirecionar para o login coloca o status 401 não autorizado*/
            services.ConfigureApplicationCookie(options => {
                options.Events.OnRedirectToLogin = context => {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePages(); //status de erros
            app.UseAuthentication(); //autenticação
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
