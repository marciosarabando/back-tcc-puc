using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using TCC.INSPECAO.Api.Config;
using TCC.INSPECAO.Domain.Handlers;
using TCC.INSPECAO.Domain.Repositories;
using TCC.INSPECAO.Infra.Contexts;
using TCC.INSPECAO.Infra.Repositories;


namespace TCC.INSPECAO.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = Configuration.GetSection("Firebase:Authority").Value;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = Configuration.GetSection("Firebase:ValidIssuer").Value,
                            ValidateAudience = true,
                            ValidAudience = Configuration.GetSection("Firebase:ValidAudience").Value,
                            ValidateLifetime = true
                        };
                    });

            //Database InMemory
            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

            //Database SQL Server
            if (_env.IsDevelopment())
                //Docker
                services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            else
                //Aws
                services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlAWSConnection")));

            //Conexao com o MySql
            //services.AddDbContext<DataContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySQLConnection")));

            //DI dos Repository
            services.AddTransient<IEstabelecimentoRepository, EstabelecimentoRepository>();
            services.AddTransient<IInspecaoRepository, InspecaoRepository>();
            services.AddTransient<IInspecaoStatusRepository, InspecaoStatusRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IClaimsRepository, ClaimsRepository>();
            services.AddTransient<IUsuarioClaimsRepository, UsuarioClaimsRepository>();
            services.AddTransient<ITurnoRepository, TurnoRepository>();
            services.AddTransient<ISistemaRepository, SistemaRepository>();
            services.AddTransient<IInspecaoItemRepository, InspecaoItemRepository>();
            services.AddTransient<ISistemaItemRepository, SistemaItemRepository>();
            services.AddTransient<IUnidadeMedidaRepository, UnidadeMedidaRepository>();

            //DI dds Handlers
            services.AddTransient<UsuarioHandler, UsuarioHandler>();
            services.AddTransient<InspecaoHandler, InspecaoHandler>();
            services.AddTransient<SistemaHandler, SistemaHandler>();

            //AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //Swagger
            services.AddSwaggerConfig();

            //Cors
            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST", "DELETE", "PUT", "OPTIONS")
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));

                options.AddPolicy("Production",
                    builder =>
                        builder
                        .WithOrigins("http://tcc-inspec.s3-website-sa-east-1.amazonaws.com").AllowAnyHeader()
                        .WithMethods("GET", "POST", "DELETE", "PUT", "OPTIONS")
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwaggerConfig();

            app.UseRouting();

            if (env.IsDevelopment())
                app.UseCors("Development");
            else
                app.UseCors("Production");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}
