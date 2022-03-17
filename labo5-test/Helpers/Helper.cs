using System.Linq;
using labo5_sneakers.Repositories;
using labo5_test.Fakes.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace labo5_test.Helpers;

public class Helper
{
    public static WebApplicationFactory<Program> CreateApi()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IBrandRepository));
                    services.Remove(descriptor);

                    var fakeBrandRepository = new ServiceDescriptor(typeof(IBrandRepository),
                        typeof(FakeBrandRepository), ServiceLifetime.Transient);
                    services.Add(fakeBrandRepository);

                    var fakeOccasionRepository = new ServiceDescriptor(typeof(IOccasionRepository),
                        typeof(FakeOccasionRepository), ServiceLifetime.Transient);
                    services.Add(fakeOccasionRepository);

                    var fakeOrderRepository = new ServiceDescriptor(typeof(IOrderRepository),
                        typeof(FakeOrderRepository), ServiceLifetime.Transient);
                    services.Add(fakeOrderRepository);

                    var fakeSneakerRepository = new ServiceDescriptor(typeof(ISneakerRepository),
                        typeof(FakeSneakerRepository), ServiceLifetime.Transient);
                    services.Add(fakeSneakerRepository);
                });
            });

        return application;
    }
}