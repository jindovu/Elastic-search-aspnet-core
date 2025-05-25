using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Elastic.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddElasticSearch
            (this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetSection("Elastic:Url").Value;
            var index = configuration.GetSection("Elastic:Index").Value;
            var username = configuration.GetSection("Elastic:Username").Value;
            var password = configuration.GetSection("Elastic:Password").Value;

            var settings = new ConnectionSettings(new Uri(url!))
                .DefaultIndex(index)
                .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
                .BasicAuthentication(username, password);

            services.AddSingleton<IElasticClient>(new ElasticClient(settings));
            return services;
        }
    }
}
