using Microsoft.Extensions.DependencyInjection;

namespace PageRank.Core.Dependency
{
    public static class Dependencies
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services;
        }
    }
}