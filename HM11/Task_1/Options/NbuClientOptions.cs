using System;

namespace Task_1.Options
{
    /// <summary>
    /// Option for getting from NbuClient
    /// </summary>
    public class NbuClientOptions
    {
        /// <summary>
        /// Base address for HttpClient
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Checking is Base Address empty or not
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseAddress) &&
                               Uri.TryCreate(BaseAddress, UriKind.Absolute, out _);
    }
}
