using System;
using RequestProcessor.App.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestProcessor.App.Services
{
    internal class RequestHandler : IRequestHandler
    {
        private readonly HttpClient _httpClient;
        public RequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResponse> HandleRequestAsync(IRequestOptions requestOptions)
        {
            if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));
            if (!requestOptions.IsValid) throw new ArgumentOutOfRangeException(nameof(requestOptions));
            if (requestOptions.ContentType == null && requestOptions.Body != null) throw new ArgumentOutOfRangeException(nameof(requestOptions));

            using var message = new HttpRequestMessage(
                MapRequestMethod(requestOptions.Method),
                new Uri(requestOptions.Address)
                );

            StringContent content = (requestOptions.Method == RequestMethod.Get || requestOptions.Method == RequestMethod.Delete) ?
                new StringContent("") : new StringContent(requestOptions.Body);

            message.Content = content;

            using var response = await _httpClient.SendAsync(message);

            //HttpResponseMessage response = requestOptions.Method switch
            //{
            //    RequestMethod.Get => await _httpClient.GetAsync(requestOptions.Address),
            //    RequestMethod.Post => await _httpClient.PostAsync(requestOptions.Address, content),
            //    RequestMethod.Put => await _httpClient.PutAsync(requestOptions.Address, content),
            //    RequestMethod.Patch => await _httpClient.PatchAsync(requestOptions.Address, content),
            //    RequestMethod.Delete => await _httpClient.DeleteAsync(requestOptions.Address),
            //    _ => throw new ArgumentOutOfRangeException(nameof(RequestMethod)),
            //};

            return new Response
            {
                Code = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };

        }

        private static HttpMethod MapRequestMethod(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Patch:
                    return HttpMethod.Patch;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                case RequestMethod.Undefined:
                default:
                    throw new ArgumentOutOfRangeException(nameof(RequestMethod));
            }
        }
    }
}