using AutoMapper;
using GoogleCast;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongoose.Common.Services;
using Mongoose.Common.Services.Contracts;

namespace Mongoose.Common.Utility
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonComponents(this IServiceCollection services, IConfiguration _)
        {
            services.AddSingleton<ICastService, CastService>()
                .AddGoogleCast();
            return services;
        }
    }
}