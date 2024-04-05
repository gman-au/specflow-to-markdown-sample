using Microsoft.Extensions.DependencyInjection;
using My.Test.Library.Domain;
using My.Test.Library.Domain.Services;
using SolidToken.SpecFlow.DependencyInjection;

namespace My.Test.Library.N.Unit.Tests.Injection
{
    public class Startup
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            services
                .AddSingleton<IEventService, MyEventService>()
                .AddSingleton<IRequestValidator, MyRequestValidator>()
                .AddSingleton<IBookingService, MyBookingService>();
            
            return services;
        }
    }
}