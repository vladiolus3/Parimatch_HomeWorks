using System;
using System.Linq;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Services;

namespace RequestProcessor.App.Menu
{
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>

        private readonly IRequestPerformer _performer;
        private readonly IOptionsSource _options;
        private readonly ILogger _logger;

        public MainMenu(
            IRequestPerformer performer,
            IOptionsSource options,
            ILogger logger)
        {
            _performer = performer;
            _options = options;
            _logger = logger;
            _logger.ToString();
        }

        public async Task<int> StartAsync()
        {
            try
            {
                Console.WriteLine("Getting options is started.");               
                var arrayOptions = (await _options.GetOptionsAsync()).ToArray();               
                Console.WriteLine("Getting options is finished.");

                Console.WriteLine("Option filtering is started.");
                arrayOptions = arrayOptions
                    .Where(opt => new RequestOption(opt.Item1, opt.Item2).IsValid == true)
                    .Select(opt => opt)
                    .ToArray();
                Console.WriteLine("Option filtering is finished.");

                Console.WriteLine("Execution of requests is started.");
                var tasks = arrayOptions
                    .Select(opt => _performer.PerformRequestAsync(opt.Item1, opt.Item2))
                    .ToArray();
                _ = Task.WhenAll(tasks);
                Console.WriteLine("Execution of requests is finished.");
            }
            catch (PerformException)
            {
                Console.WriteLine("Something went wrong.");
                return -1;
            }

            return 0;
        }
    }
}
