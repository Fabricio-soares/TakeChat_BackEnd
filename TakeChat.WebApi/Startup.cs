using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using TakeChat.Application;
using TakeChat.TmdbAdapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otc.AspNetCore.ApiBoot;
using Otc.Extensions.Configuration;
using TakeChat.Infra.Repository;
using TakeChat.infra.Repository.Microsoft.Extensions.DependencyInjection;

namespace TakeChat.WebApi
{
    /// <summary>
    /// Este eh o Startup da API. 
    /// <para>
    /// A base <see cref="ApiBootStartup"/> implementa uma serie de requisitos
    /// que consideramos necessarios para qualquer API, como Log, Swagger,
    /// Authorizacao, Versionamento e muito mais.
    /// Veja https://github.com/OleConsignado/otc-aspnetcore-apiboot para maiores
    /// detalhes.
    /// </para>
    /// </summary>
    public class Startup : ApiBootStartup
    {
        protected override ApiMetadata ApiMetadata => new ApiMetadata()
        {
            Name = "TakeChat",
            Description = "true",
            DefaultApiVersion = "1.0"
        };

        public Startup(IConfiguration configuration)
            : base(configuration)
        {

        }

        /// <summary>
        /// Registra os servicos especificos da API.
        /// </summary>
        /// <param name="services"></param>
        [ExcludeFromCodeCoverage]
        protected override void ConfigureApiServices(
            IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(TmdbMapperProfile),
                typeof(WebApiMapperProfile));

            services.AddTmdbAdapter(
                Configuration.SafeGet<TmdbAdapterConfiguration>());

            services.AddApplication
                (Configuration.SafeGet<ApplicationConfiguration>());

            services.AddRepository
            (Configuration.SafeGet<RepositoryConfiguration>());
        }
    }
}
