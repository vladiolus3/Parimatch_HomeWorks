using System.Text.Json.Serialization;

namespace Task_1
{
    internal class Result
    {
        [JsonPropertyName("sucess")]
        public bool Success { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("duration")]
        public string Duration { get; set; }
        [JsonPropertyName("primes")]
        public int[] Primes { get; set; }
        public Result() 
        {
            Success = true;
            Error = null;
            Duration = null;
            Primes = null;
        }

    }
}
