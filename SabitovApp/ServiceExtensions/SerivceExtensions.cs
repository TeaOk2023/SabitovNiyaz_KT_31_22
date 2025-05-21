using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SabitovApp.Data;
using SabitovApp.Interfaces.WorkloadInterface;

namespace SabitovApp.ServiceExtensions
{
    public static class SerivceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDisciplineService, DisciplineService>();
            services.AddScoped<IWorkloadService, WorkloadService>();
            return services;
        }
    }
}
