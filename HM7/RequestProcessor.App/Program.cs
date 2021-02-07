using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Menu;
using RequestProcessor.App.Services;

namespace RequestProcessor.App
{
    /// <summary>
    /// Entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <returns>Returns exit code.</returns>
        private static async Task<int> Main()
        {
            try
            {
                Console.WriteLine("Request Processor by st. Dovhal Vladylsav");
                const string path = "options.json";

                using HttpClient httpClient = new HttpClient();
                RequestHandler requestHandler = new RequestHandler(httpClient);
                ResponseHandler responseHandler = new ResponseHandler();
                Logger logger = new Logger();

                RequestPerformer requestPerformer = new RequestPerformer(requestHandler, responseHandler, logger);
                OptionsSource optionsSource = new OptionsSource(path);

                var mainMenu = new MainMenu(requestPerformer, optionsSource, logger);

                return await mainMenu.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Critical unhandled exception");
                Console.WriteLine(ex);
                return -1;
            }
        }
    }
}
