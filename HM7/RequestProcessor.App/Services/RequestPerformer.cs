using System;
using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>

        private readonly IRequestHandler _requestHandler;
        private readonly IResponseHandler _responseHandler;
        private readonly ILogger _logger;

        public RequestPerformer(
            IRequestHandler requestHandler,
            IResponseHandler responseHandler,
            ILogger logger)
        {
            _requestHandler = requestHandler;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(
            IRequestOptions requestOptions,
            IResponseOptions responseOptions)
        {
            bool result = true;

            _logger.Log($"Option '{requestOptions.Name}' handle is started.");

            try
            {
                _logger.Log($"Request '{requestOptions.Name}' handle is started.");
                var response = (_requestHandler.HandleRequestAsync(requestOptions)).Result;
                _logger.Log($"Request '{requestOptions.Name}' handle is finished.");

                _logger.Log($"Response '{requestOptions.Name}' handle is started.");
                await _responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
                _logger.Log($"Response '{requestOptions.Name}' handle is finished.");
            }
            catch (ArgumentNullException ex)
            {
                _logger.Log(ex, $"Request {requestOptions.Name} property {ex.ParamName} is null.");
                result = false;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.Log(ex, $"Request {requestOptions.Name} property {ex.ParamName} out of range.");
                result = false;
            }
            catch (TaskCanceledException ex)
            {
                _logger.Log(ex, "HTTP-client Timeout.");
                _logger.Log($"Unsuccessful response handle is started.");
                await _responseHandler.HandleResponseAsync(new Response { Handled = false }, requestOptions, responseOptions);
                _logger.Log($"Unsuccessful response handle is finished.");
                result = false;
            }
            catch (HttpRequestException ex)
            {
                _logger.Log(ex, "Internal client exception.");
                result = false;
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(ex, "Invalid client state.");
                result = false;
            }
            catch (Exception)
            {
                throw new PerformException();
            }

            _logger.Log($"Option '{requestOptions.Name}' handle is finished.");

            return result;
        }
    }
}
