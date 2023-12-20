using Application.Interfaces;
using Application.Interfaces.Repositories;
using Infrastructure.Data.Interceptors;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            //services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            // For Entity Framework  
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString,
                providerOptions => providerOptions.EnableRetryOnFailure()).UseLazyLoadingProxies());

            // For Identity  
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // services
            services.AddTransient<IUserManagerService, UserManagerService>();
            services.AddScoped<IUser, CurrentUser>();

            // repo
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IRepositoryManager, RepositoryManager>();
            services.AddTransient<ITodoItemRepository, TodoItemRepository>();

            return services;
        }
    }
}
