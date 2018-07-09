using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ApproovSDK
{
    /**
     * Service point configuration.
     * 
     * Adds simple pinning scheme to service point manager.
     * 
     * FOR DEMONSTRATION PURPOSES ONLY
     */
    public static class ServicePointConfiguration
    {
        private static string PinnedPublicKey = null;

        public static void SetUp(string key = null)
        {
            PinnedPublicKey = key;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertficate;
        }

        private static bool ValidateServerCertficate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors
        )
        {
            if (PinnedPublicKey == null || PinnedPublicKey.Length <= 0) return true;

            //Console.WriteLine("Expected: " + PinnedPublicKey);
            //Console.WriteLine("Found   : " + certificate?.GetPublicKeyString());

            return String.Equals(PinnedPublicKey, certificate?.GetPublicKeyString(),
                                 StringComparison.OrdinalIgnoreCase);
        }
    }
}
