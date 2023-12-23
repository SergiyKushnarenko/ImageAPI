using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ImageApi.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjMTA4YTAzNS01Mzk3LTQ2N2UtYjgwNC04YzA5ODllYjY2ZjIiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsIkRhdGVPZkpvaW5pbmciOiIzMS4wOC4yMDIzIDk6MDk6MDkiLCJlbWFpbCI6InRlc3RAZ21haWwuY29tIiwiRnVsbE5hbWUiOiJTYW0iLCJleHAiOjE2OTM0ODM3NDksImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcxMTIvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzExMi8ifQ.uEO-b72dTctRr3x7VeKyVBbuydb7GgN874jfNP--9Sw\"\"",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }

}