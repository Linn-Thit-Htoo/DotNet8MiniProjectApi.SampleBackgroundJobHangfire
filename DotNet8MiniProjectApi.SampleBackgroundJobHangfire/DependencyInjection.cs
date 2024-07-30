namespace DotNet8MiniProjectApi.SampleBackgroundJobHangfire;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, WebApplicationBuilder builder)
    {
        return services.AddDbContextService(builder)
            .AddDataAccessService()
            .AddBusinessLogicService()
            .AddHangfireService(builder);
    }

    private static IServiceCollection AddDbContextService(this IServiceCollection services, WebApplicationBuilder builder)
    {
        return builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
        }, ServiceLifetime.Transient);
    }

    private static IServiceCollection AddDataAccessService(this IServiceCollection services)
    {
        return services.AddScoped<DL_Setup>();
    }

    private static IServiceCollection AddBusinessLogicService(this IServiceCollection services)
    {
        return services.AddScoped<BL_Setup>();
    }

    private static IServiceCollection AddHangfireService(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        builder.Services.AddHangfire(opt =>
        {
            opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("DbConnection"))
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
        });

        builder.Services.AddHangfireServer();
        return services;
    }
}