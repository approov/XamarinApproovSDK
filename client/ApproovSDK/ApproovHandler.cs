using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace ApproovSDK
{
    public class ApproovHandler : DelegatingHandler
    {
        /** The default Approov token header name: "Approov-Token". */
        public const string DefaultHeaderName = "Approov-Token";

        private string headerName;

        private bool checkCerts;

        /**
         * Intercepts client requests and adds a request header set to the
         * current Approov token string.
         * 
         * @param headerName   The header name or "Approov-Token" if the 
         *                     header is null or not provided.
         * @param checkCerts   If true or not specified, check that the 
         *                     requested domain's TLS certificates seen by
         *                     the client match those seen by Approov.
         * @param innerHandler Inner handler to pass to. If null or not 
         *                     specified, the default Http handler is used
         *                     which terminates the chain of handlers.
         */
        public ApproovHandler(string headerName = null, bool checkCerts = true,
                              HttpMessageHandler innerHandler = null)
        {
            this.headerName = (headerName != null) ? headerName : DefaultHeaderName;
            this.checkCerts = checkCerts;
            InnerHandler = (innerHandler != null) ? innerHandler : new HttpClientHandler();
        }

        /**
         * Adds the current Approov token to the request.
         */
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string url = checkCerts? request.RequestUri.ToString() : null;

            // fetch approov token
            string token = await Approov.Shared.FetchTokenAsync(url);

            // add approov token header to request
            request.Headers.Add(headerName, token);

            // pass down to inner handler
            var response = await base.SendAsync(request, cancellationToken);

            // pass up to outer handler
            return response;
        }
    }
}