namespace Device_microservice.Services
{
    public static class CorsConfigExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsConfigSection = configuration.GetSection(CorsOptions.Cors);
            var corsConfig = corsConfigSection.Get<CorsOptions>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(corsConfig.AllowedOrigins).AllowAnyHeader().AllowAnyMethod();
                });
            });

            return services;
        }
    }

    public class CorsOptions
    {
        public const string Cors = "Cors";
        public string[] AllowedOrigins { get; set; }
    }
}
