using D1_Tech.Core.Interfaces.GenericInterfaces;
using D1_Tech.Core.Interfaces.PageInterfaces;
using D1_Tech.Service.Repositories.GenericRepositories;
using D1_Tech.Service;
using D1_Tech.Core.Interfaces.PageInterfaces.PageRepositoryInterfaces;
using D1_Tech.Service.Repositories.PageRepositories;

namespace D1_Tech.Application.Middleware
{
    public static class AllDependencyInjection
    {
        public static IServiceCollection AddInjections(
                this IServiceCollection services)
        {
            #region Generic Injection
            services.AddTransient(typeof(IGenericCrudRepository<>), typeof(GenericCrudRepository<>));
            #endregion
            #region Service Injection
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlaceDetailService, PlaceDetailService>();
            #endregion
            #region Repository Injection
            services.AddScoped<IPlaceRepository, PlaceServiceRepository>();
            #endregion
            return services;
        }
    }
}