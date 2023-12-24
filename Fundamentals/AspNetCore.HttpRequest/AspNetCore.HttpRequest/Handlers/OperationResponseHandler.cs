using AspNetCore.HttpRequest.Models;

namespace AspNetCore.HttpRequest.Handlers
{
    public class OperationResponseHandler : DelegatingHandler
    {
        private readonly IOperationScoped _operationScoped;
        public OperationResponseHandler(IOperationScoped operationScoped)
        => _operationScoped = operationScoped;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage()
            {
                Content = new StringContent(_operationScoped.OperationId)
            });
        }

    }
}
