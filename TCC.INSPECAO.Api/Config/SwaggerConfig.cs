using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TCC.INSPECAO.Api.Config
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            //Swagger
            services.AddSwaggerGen(options =>
            {
                options.MapType<JObject>(() => new OpenApiSchema { Type = "object" });

                options.SwaggerDoc("v1.0", new OpenApiInfo()
                {
                    Title = "TCC.INSPECAO.API",
                    Version = "1.0",
                    Description = "API INSPEC TCC PUC MINAS - Marcio Sarabando",
                    Contact = new OpenApiContact
                    {
                        Name = "Marcio Sarabando",
                        Email = "marcio.sarabando@gmail.com"
                    }
                });

                // Adiciona header de autentica��o por ApiKey
                /*options.AddSecurityDefinition("apiKey", new OpenApiSecurityScheme
                {
                    Description = Configuration["KeyHandler:value"],
                    Name = Configuration["KeyHandler:key"],
                    In = ParameterLocation.Header
                });*/

                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();

                // integrate xml comments
                //options.IncludeXmlComments(XmlCommentsFilePath);

                options.CustomSchemaIds(x => x.FullName);

                // Adiciona header de autentica��o por ApiKey
                /*
                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "apiKey"
                            },
                            Scheme = "apiKey",
                            Name = "apiKey",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                        }
                    });
                */

                // Adiciona header de autentica��o por Bearer JWT

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securityScheme, new[] { "Bearer " });

                options.AddSecurityRequirement(securityRequirement);


            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            //Swagger
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
                options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{httpReq.PathBase}" }
                    }
                );
            });

            app.UseSwaggerUI(options =>
            {
                var basePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                options.SwaggerEndpoint($"{basePath}/swagger/v1.0/swagger.json", "TCC.INSPECAO.Api");
            });
            //Swagger

            return app;
        }
    }


    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                //var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                var routeInfo = description.RouteInfo;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
            //operation.Responses["400"].Description = "Invalid query parameter(s). Read the response description";
            //operation.Responses["401"].Description = "Authorization has been denied for this request";
        }
    }

}