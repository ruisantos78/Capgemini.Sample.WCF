using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WPF;

public static class RegisterService
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        var servicesTypes = typeof(App).Assembly.GetTypes()
            .Where(x => x.GetCustomAttribute<RegisterServiceAttribute>() is not null)
            .Select(x => new
            {
                Type = x,
                Attribute = x.GetCustomAttribute<RegisterServiceAttribute>()
            });

        foreach (var servicesType in servicesTypes)
        {
            switch (servicesType.Attribute.InstanceType)
            {
                case InstanceType.Singleton:
                    services = servicesType.Attribute.ImplementationType is null
                        ? services.AddSingleton(servicesType.Type)
                        : services.AddSingleton(servicesType.Type, servicesType.Attribute.ImplementationType);
                    break;

                case InstanceType.Transient:
                    services = servicesType.Attribute.ImplementationType is null
                        ? services.AddTransient(servicesType.Type)
                        : services.AddTransient(servicesType.Type, servicesType.Attribute.ImplementationType);
                    break;

                case InstanceType.Scoped:
                    services = servicesType.Attribute.ImplementationType is null
                        ? services.AddScoped(servicesType.Type)
                        : services.AddScoped(servicesType.Type, servicesType.Attribute.ImplementationType);
                    break;
            }
        }

        return services;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class RegisterServiceAttribute: Attribute
{
    public InstanceType InstanceType { get; init; } = InstanceType.Transient;

    public Type? ImplementationType { get; init; } = null;    
}

public enum InstanceType
{
    Singleton,
    Transient,
    Scoped
}
