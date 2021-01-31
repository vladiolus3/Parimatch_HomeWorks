using System;
using System.Text.Json.Serialization;

namespace Task_2
{
    [Serializable]
    internal class Settings
    {
        [JsonPropertyName("primesFrom")]
        public int PrimesFrom { get; set; }
        [JsonPropertyName("primesTo")]
        public int PrimesTo { get; set; }
    }

}