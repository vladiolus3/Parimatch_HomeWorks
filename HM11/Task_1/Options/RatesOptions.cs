namespace Task_1.Options
{
    /// <summary>
    /// Option for getting rate
    /// </summary>
    public class RatesOptions
    {
        /// <summary>
        /// Base currency
        /// </summary>
        public string BaseCurrency { get; set; }
        /// <summary>
        /// Checking is BaseCurrency empty or not
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseCurrency);
    }
}
