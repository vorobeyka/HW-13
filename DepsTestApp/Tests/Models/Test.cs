using DepsTestApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

namespace DepsTestApp.Tests.Models
{
    internal class Test : ITest
    {
        [JsonPropertyName("expectedStatusCode")]
        public int ExpectedStatusCode { get; set; }

        [JsonPropertyName("requestString")]
        public string RequestString { get; set; }

        [JsonPropertyName("headerName")]
        public string HeaderName { get; set; }

        [JsonPropertyName("account")]
        public Account Account { get; set; }

        [JsonPropertyName("requestMethod")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RequestMethod Method { get; set; }
    }
}
