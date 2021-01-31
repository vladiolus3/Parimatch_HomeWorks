using System.Text.Json.Serialization;

namespace Task_3
{
    public class Result
    {
        [JsonPropertyName("countSuccess")]
        public int CountSuccess { get; set; }
        [JsonPropertyName("countFail")]
        public int CountFail { get; set; }
        public Result(int cs, int cf)
        {
            CountSuccess = cs;
            CountFail = cf;
        }
    }
}
