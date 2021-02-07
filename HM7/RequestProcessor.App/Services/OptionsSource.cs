using Newtonsoft.Json;
using RequestProcessor.App.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RequestProcessor.App.Services
{
    internal class OptionsSource : IOptionsSource
    {
        private readonly string _path;

        public OptionsSource(string path)
        {
            _path = path;
        }

        public async Task<IEnumerable<(IRequestOptions, IResponseOptions)>> GetOptionsAsync()
        {
            //using FileStream fs = File.OpenRead(_path);
            //var r = await JsonConvert.DeserializeObject<>
            //var result = (await JsonSerializer.DeserializeAsync<IEnumerable<ValidationOptions>>(fs, jsonOptions));

            string json = await File.ReadAllTextAsync(_path);
            var result = JsonConvert.DeserializeObject<IEnumerable<RequestOption>>(json);

            return result.Select(opt => ((IRequestOptions)opt, (IResponseOptions)opt));
        }
    }
}
