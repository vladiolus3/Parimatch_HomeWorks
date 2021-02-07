using RequestProcessor.App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RequestProcessor.App.Services
{
    [Serializable]
    internal class RequestOption : IRequestOptions, IResponseOptions
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("method")]
        public RequestMethod Method { get; set; }
        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        public bool IsValid { get; set; } = true;

        public RequestOption() { }

        public RequestOption(IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            Name = requestOptions.Name;
            Address = requestOptions.Address;
            Method = requestOptions.Method;
            ContentType = requestOptions.ContentType;
            Body = requestOptions.Body;
            Path = responseOptions.Path;
            if (string.IsNullOrWhiteSpace(Name)|| string.IsNullOrWhiteSpace(Address) || string.IsNullOrWhiteSpace(Path))
                IsValid = false;
        }
    }
}
