using System;
using System.Threading.Tasks;

using ApproovSDK;
using ApproovSDK.iOS.Bind;

namespace ApproovSDK.iOS
{

    public class IosApproov : Approov
    {
        private const string TAG = "Approov";

        /**
         * Create an iOS Approov object.
         * 
         * At application startup, create an iOSApproov object and assign 
         * it as the Approov Shared singleton. In the AppDelegate's
         * OnFinished() method, add:
         * 
         *     Approov.Shared = new IosApproov();
         */
        public IosApproov() : base()
        {
            // Initialize the Approov SDK
        }

        public override string FetchToken(string url)
        {
            string token = ErrorToken;

            ApproovAttestee attestee = ApproovAttestee.SharedAttestee();

            ApproovTokenFetchData tokenFetchData = attestee.FetchApproovTokenAndWait(url);
            if (tokenFetchData.Result == ApproovTokenFetchResult.Successful)
            {
                token = tokenFetchData.ApproovToken;
            }

            return token;
        }

        public override Task<string> FetchTokenAsync(string url = null)
        {
            var tcs = new TaskCompletionSource<string>();

            string token = ErrorToken;

            ApproovAttestee attestee = ApproovAttestee.SharedAttestee();

            attestee.FetchApproovToken((ApproovTokenFetchData tokenFetchData) =>
            {
                if (tokenFetchData.Result == ApproovTokenFetchResult.Successful)
                {
                    token = tokenFetchData.ApproovToken;
                }

                try
                {
                    tcs.SetResult(token);
                }
                catch (Exception exc)
                {
                    tcs.SetException(exc);
                }
            }, url);

            return tcs.Task;
        }

        public override void SetTokenPayloadValue(string value)
        {
            ApproovAttestee.SharedAttestee().SetTokenPayloadValue(value);
        }

        public override byte[] GetCert(string url)
        {
            return ApproovAttestee.SharedAttestee().GetCert(url).ToArray();
        }

        public override void ClearCerts()
        {
            ApproovAttestee.SharedAttestee().ClearCerts();
        }
    }
}
