using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Data;
using SalesWebMvc.Services;

namespace SalesWebMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request or not.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Alterar para utilizar o MySQL
            services.AddDbContext<SalesWebMvcContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("SalesWebMvcContext"), builder =>
                        builder.MigrationsAssembly("SalesWebMvc"))); // Delegates

            // Registrar os servicos no sistema de Injecao de Dependencia da Aplicacao
            services.AddScoped<SeedingService>(); // Servico "SeedingService"
            services.AddScoped<SellerService>(); // Servico "SellerService"
            services.AddScoped<DepartmentService>(); // Servico "DepartmentService"
            services.AddScoped<SalesRecordService>(); // Servico "SalesRecordService"
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request Pipeline.
        // Metodo "Configure" aceita que sejam colocados outros parametros que foram adicionados com ".AddScoped"
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        {
            // Configuracao para mudar a localizacao padrao para os EUA (enUS)
            var enUS = new CultureInfo("en-US"); // Variavel que recebe a localizacao
            var localizationOptions = new RequestLocalizationOptions // Variavel para receber as configuracoes 
            {
                DefaultRequestCulture = new RequestCulture(enUS), // Indica a configuracao padrao da localizacao do app
                SupportedCultures = new List<CultureInfo> { enUS }, // Locails possiveis do app
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            /*// Configuracao para mudar a localizacao padrao para os EUA (enUS)
            var ptBR = new CultureInfo("pt-BR"); // Variavel que recebe a localizacao
            var localizationOptions = new RequestLocalizationOptions // Variavel para receber as configuracoes 
            {
                DefaultRequestCulture = new RequestCulture(ptBR), // Indica a configuracao padrao da localizacao do app
                SupportedCultures = new List<CultureInfo> { ptBR }, // Locails possiveis do app
                SupportedUICultures = new List<CultureInfo> { ptBR }
            };*/

            app.UseRequestLocalization(localizationOptions); // Efetiva a alteracao das configuracoes localizacao

            // Teste para verificar se esta no perfil de Desenvolvimento ou de Producao (quando o app ja foi publicado)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed(); // Popula a base de dados, caso esteja vazia
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"); // "id?" - "?" Indica um atributo opcional
            });
        }
    }
}
