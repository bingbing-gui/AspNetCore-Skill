using AspNetCore.UsingHttpVerb.Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.UsingHttpVerb.Practice.Handlers
{
    public class OperationResponseHandler : DelegatingHandler
    {
        private readonly IOperationScoped _operationScoped;
        public OperationResponseHandler(IOperationScoped operationScoped)
        {
            _operationScoped = operationScoped;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // For sample purposes, return the OperationId as the body.
            return Task.FromResult(new HttpResponseMessage
            {
                Content = new StringContent(_operationScoped.OperationId)
            });
        }
    }
}
