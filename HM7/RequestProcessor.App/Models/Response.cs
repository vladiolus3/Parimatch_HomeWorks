using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    internal class Response : IResponse
    {
        public bool Handled { get; set; } = true;

        public int Code { get; set; } = default;

        public string Content { get; set; } = default;
    }
}
