using System;
using Azure.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace SAMLTester
{
    public static class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        private static void Configure(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            IWebHostEnvironment environment = context.HostingEnvironment;
            builder.SetBasePath(environment.ContentRootPath);
            builder.AddAppConfiguration();
            // builder.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true);
            builder.AddJsonFile("appsettings.json", optional: false);
            builder.AddEnvironmentVariables();
        }

        private static IConfigurationBuilder AddAppConfiguration(this IConfigurationBuilder builder)
        {
            IConfigurationRoot builtConfig = builder.Build();

            string appConfigurationName = builtConfig["AppConfigurationName"];
            if (string.IsNullOrWhiteSpace(appConfigurationName))
            {
                return builder;
            }

            var azureAppConfiguration = builder.AddAzureAppConfiguration(options =>
            {
                options.Connect(new Uri($"https://{appConfigurationName}.azconfig.io"),
                        new ChainedTokenCredential(new ManagedIdentityCredential(), new DefaultAzureCredential()))
                    .Select(KeyFilter.Any, "ScreeningUI");
            }).Build();

            builder.AddConfiguration(azureAppConfiguration);

            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                    azureServiceTokenProvider.KeyVaultTokenCallback));

            var keyVaultConfigurationName = builtConfig["KeyVaultName"];

            if (string.IsNullOrWhiteSpace(keyVaultConfigurationName))
            {
                return builder;
            }

            var keyVault = builder.AddAzureKeyVault(
                $"https://{keyVaultConfigurationName}.vault.azure.net/",
                keyVaultClient,
                new DefaultKeyVaultSecretManager()).Build();

            builder.AddConfiguration(keyVault);
            return builder;
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(Configure).ConfigureLogging((context, logBuilder) => { })
                .UseStartup<Startup>();
    }
}