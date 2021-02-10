namespace WebApi.Modules
{
    using Autofac;
    using System;
    using System.Net.Http;
    using WebApi.Repositories;

    public class HttpClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(_ => {
                var httpClient = new HttpClient() { BaseAddress = new Uri("https://content.guardianapis.com") };
                httpClient.DefaultRequestHeaders.Add("api-key", "e46b337f-b283-448d-8a64-64d7da8dd011");
                return httpClient;
            })
            .Named<HttpClient>("NewsClient")
            .SingleInstance();
        }
    }
}