using RequestProcessor.App.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RequestProcessor.App.Services
{
    internal class ResponseHandler : IResponseHandler
    {
        public async Task HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (requestOptions == null) throw new ArgumentNullException(nameof(requestOptions));
            if (responseOptions == null) throw new ArgumentNullException(nameof(responseOptions));
            if (!requestOptions.IsValid) throw new ArgumentOutOfRangeException(nameof(requestOptions));
            if (!responseOptions.IsValid) throw new ArgumentOutOfRangeException(nameof(responseOptions));

            using FileStream fs = new FileStream(responseOptions.Path, FileMode.Append, FileAccess.Write);
            using StreamWriter sw = new StreamWriter(fs);
            await sw.WriteLineAsync(response.Code.ToString() + '\t' + response.Handled.ToString() + '\n' + response.Content.ToString());
        }
    }
}
