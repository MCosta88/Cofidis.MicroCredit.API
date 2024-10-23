using Cofidis.MicroCredit.Services.Interfaces;
using Cofidis.MicroCredit.Data.Models.External;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Cofidis.MicroCredit.Services.Services
{
    public class HttpClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IHttpClientService
    {
        public async Task<User?> GetExternalUserByNIF(string nif)
        {
            var apiUrl = configuration.GetValue<string>("DigitalKeyAPI") + nif;

            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            httpRequestMessage.Headers.Add(HeaderNames.Accept, "application/json");

            try
            {
                var httpClient = httpClientFactory.CreateClient();
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {

                    return null;
                }

                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();


                if (contentStream.Length == 0)
                {
                    return null;
                }

                var user = await JsonSerializer.DeserializeAsync<User>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user;
            }
            catch (HttpRequestException ex)
            {

                throw new Exception($"Error fetching user data: {ex.Message}", ex);
            }
            catch (Exception ex)
            {

                throw new Exception($"An unexpected error occurred: {ex.Message}", ex);
            }
        }
    }
}
