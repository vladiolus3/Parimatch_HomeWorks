using System.Collections.Generic;
using System.Threading.Tasks;
using Task_1.Models;

namespace Task_1.Clients
{
    /// <summary>
    /// Abstract currency rates provider interface.
    /// </summary>
    public interface IRatesProviderClient
    {
        /// <summary>
        /// Requests rates from  currency rates provider.
        /// </summary>
        /// <returns>Returns currency rates.</returns>
        Task<IEnumerable<CurrencyRate>> GetRatesAsync();
    }
}
