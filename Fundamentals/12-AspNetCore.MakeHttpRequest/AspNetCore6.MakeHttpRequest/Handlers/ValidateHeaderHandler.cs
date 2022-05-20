namespace AspNetCore6.MakeHttpRequest.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateHeaderHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("X-API-KEY"))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("The API key header X-API-KEY is required.")
                };
            }
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
