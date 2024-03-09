using Microsoft.OpenApi.Models;

namespace ToDoAPI.Extentions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
             {
                 options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     Description =
                     "JWT Authorization Header Using the Bearer Scheme. \r\n\r\n " +
                     "Enter 'Bearer'[Space] and then your token in the text input below. \r\n\r\n" +
                     "Example: \"Bearer 12345abcdef\"",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     Scheme = "Bearer"

                 });
                 options.AddSecurityRequirement(new OpenApiSecurityRequirement()
             {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },

                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header

                    },
                    new List<string>()
                }
             });
                     });




            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDe", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization Header using the Bearer scheme. Enter 'Bearer'[Space] and then your token in the text input below. Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header
                    },
                     //new string[] {}

                     new List<string>()

                }
            });
            });
        }
    }
}
