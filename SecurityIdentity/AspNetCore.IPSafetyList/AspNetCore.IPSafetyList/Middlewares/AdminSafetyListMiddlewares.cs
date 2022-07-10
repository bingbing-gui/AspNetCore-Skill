using System.Net;

namespace AspNetCore.IPSafetyList.Middlewares
{
    public class AdminSafetyListMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AdminSafetyListMiddlewares> _logger;
        private readonly byte[][] _safetyList;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="safetyList"></param>
        public AdminSafetyListMiddlewares(RequestDelegate next,
            ILogger<AdminSafetyListMiddlewares> logger,
            string safetyList)
        {
            var ips = safetyList.Split(';');
            _safetyList = new byte[ips.Length][];
            for (var i = 0; i < ips.Length; i++)
            {
                _safetyList[i] = IPAddress.Parse(ips[i]).GetAddressBytes();
            }
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethod.Get.Method)
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                _logger.LogDebug("Request from Remote IP address: {RemoteIp}", remoteIp);

                var bytes = remoteIp.GetAddressBytes();
                var badIp = true;
                foreach (var address in _safetyList)
                {
                    if (address.SequenceEqual(bytes))
                    {
                        badIp = false;
                        break;
                    }
                }

                if (badIp)
                {
                    _logger.LogWarning(
                        "Forbidden Request from Remote IP address: {RemoteIp}", remoteIp);
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            await _next(context);
        }
    }
}