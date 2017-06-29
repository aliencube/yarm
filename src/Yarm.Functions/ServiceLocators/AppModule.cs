using System.Net.Http;
using System.Net.Http.Formatting;

using Autofac;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Yarm.Extensions;
using Yarm.Functions.Formatters;
using Yarm.Functions.FunctionFactories;
using Yarm.Models.Settings;
using Yarm.Services;

namespace Yarm.Functions.ServiceLocators
{
    /// <summary>
    /// Implements the app module.
    /// </summary>
    public class AppModule : Module
    {
        /// <summary>
        /// Add registrations to the container builder.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        protected override void Load(ContainerBuilder containerBuilder)
        {
            // Settings
            containerBuilder.RegisterTypeAsSingleInstance<FunctionAppSettings, IFunctionAppSettings>();

            var serializerSettings = new JsonSerializerSettings
                                     {
                                         ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                         Converters = { new StringEnumConverter() },
                                         Formatting = Formatting.Indented,
                                         NullValueHandling = NullValueHandling.Ignore,
                                         MissingMemberHandling = MissingMemberHandling.Ignore
                                     };
            containerBuilder.RegisterAsSingleInstance<JsonSerializerSettings, JsonSerializerSettings>(_ => serializerSettings);

            var jsonMediaTypeFormatter = new JsonMediaTypeFormatter { SerializerSettings = serializerSettings };
            var yamlMediaTypeFormatter = new YamlMediaTypeFormatter();
            containerBuilder.RegisterAsSingleInstance<JsonMediaTypeFormatter, JsonMediaTypeFormatter>(_ => jsonMediaTypeFormatter);
            containerBuilder.RegisterAsSingleInstance<YamlMediaTypeFormatter, YamlMediaTypeFormatter>(_ => yamlMediaTypeFormatter);

            var httpClient = new HttpClient();
            containerBuilder.RegisterAsSingleInstance<HttpClient, HttpClient>(_ => httpClient);

            // Functions
            containerBuilder.RegisterAssemblyTypes(typeof(IFunction).Assembly)
                            .Where(t => t.Name.EndsWithEquivalent("Function"))
                            .AsImplementedInterfaces()
                            .InstancePerDependency();

            // Helpers
            //containerBuilder.RegisterTypeAsInstancePerDependency<StorageAccountHelper, IStorageAccountHelper>();

            // Mappers

            // Services
            containerBuilder.RegisterAssemblyTypes(typeof(IService).Assembly)
                            .Where(t => t.Name.EndsWithEquivalent("Service"))
                            .AsImplementedInterfaces()
                            .InstancePerDependency();
        }
    }
}