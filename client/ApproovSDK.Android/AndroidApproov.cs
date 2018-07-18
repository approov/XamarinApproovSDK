using System;
using System.Threading.Tasks;

using Android.Content;
using Android.Util;

using ApproovSDK;
using Com.Criticalblue.Attestationlibrary;

namespace ApproovSDK.Droid
{

    public class AndroidApproov : Approov
    {
        private const string TAG = "Approov";

        /**
         * Create an Android Approov object.
         * 
         * At application startup, create an AndroidApproov object and assign 
         * it as the Approov Shared singleton. In the MainApplication's
         * OnCreate() method, add:
         * 
         *     Approov.Shared = new AndroidApproov(this.ApplicationContext);
         * 
         * @param context The application context.
         */
        public AndroidApproov(Context context) : base()
        {
            // Initialize the Approov SDK
            try
            {
                // Creates the configuration object for the Approov SDK based
                // on the Android application context
                ApproovConfig config = ApproovConfig.GetDefaultConfig(context);
                ApproovAttestation.Initialize(config);
            }
            catch (Exception ex)
            {
                Log.Error(TAG, ex.Message);
            }
        }

        public override string FetchToken(string url)
        {
            TokenInterfaceApproovResults
                              results = ApproovAttestation.Shared().FetchApproovTokenAndWait(url);

            if (results.Result == ApproovAttestation.AttestationResult.Success)
            {
                Log.Info(TAG, "Fetched Approov token");
                return results.Token;
            }
            else
            {
                Log.Info(TAG, "Failed to fetch Approov token");
                return ErrorToken;
            }
        }

        public class TokenCB : Java.Lang.Object, ITokenInterface
        {
            private TaskCompletionSource<string> tcs;

            public TokenCB(TaskCompletionSource<string> tcs)
            {
                this.tcs = tcs;
            }

            public void ApproovTokenFetchResult(TokenInterfaceApproovResults results)
            {
                string token = ErrorToken;

                ApproovAttestation.AttestationResult result = results.Result;

                if (results.Result == ApproovAttestation.AttestationResult.Success)
                {
                    token = results.Token;
                    Log.Info(TAG, "Fetched Approov token (async)");
                }
                else{
                    Log.Info(TAG, "Failed to fetch Approov token (async)");
                }

                try
                {
                    tcs.SetResult(token);
                }
                catch (Exception exc)
                {
                    tcs.SetException(exc);
                }
            }
        }

        public override Task<string> FetchTokenAsync(string url = null)
        {
            var tcs = new TaskCompletionSource<string>();
            var cb = new TokenCB(tcs);

            ApproovAttestation.Shared().FetchApproovToken(cb, url);

            return tcs.Task;
        }

        public override void SetTokenPayloadValue(string value)
        {
            ApproovAttestation.Shared().SetTokenPayloadValue(value);
        }

        public override byte[] GetCert(string url)
        {
            return ApproovAttestation.Shared().GetCert(url);
        }

        public override void ClearCerts()
        {
            ApproovAttestation.Shared().ClearCerts();
            Log.Info(TAG, "Cleared certificate cache");
        }
    }
}
