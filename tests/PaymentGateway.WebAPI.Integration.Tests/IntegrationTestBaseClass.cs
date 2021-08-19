using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace PaymentGateway.WebAPI.Integration.Tests
{
    public class IntegrationTestBaseClass
    {
        protected HttpClient Client;
        private readonly WebApplicationFactory<Startup> _factory;

        protected IntegrationTestBaseClass()
        {
            _factory = new WebApplicationFactory<Startup>();

            Client = _factory.CreateClient();
        }
    }
}
