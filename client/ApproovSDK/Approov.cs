using System;
using System.Threading.Tasks;

namespace ApproovSDK
{
    public abstract class Approov
    {
        /** Error token string: "ERROR_TOKEN". */
        public const string ErrorToken = "ERROR_TOKEN";

        /**
         * Approov singleton.
         * 
         * This instance points to a platform-specific implementation 
         * which should be created when the app starts up.
         */
        public static Approov Shared;

        /**
         * Returns true if token is an error token.
         * 
         * @param   token A token string.
         * @returns       True if token is null or is error token value.
         */
        public static bool IsErrorToken(string token)
        {
            return token == null || token.Equals(ErrorToken);
        }

        /**
         * Creates a concrete Android approover.
         */
        protected Approov()
        {
        }

        /**
         * Fetches a token from the Approov cloud service.
         * 
         * This function blocks until a token is returned.
         *
         * @param   url The URL of the connection to be protected by Approov
         *              or null if no protection is desired.
         * @returns     A token string.
         */
        public abstract string FetchToken(string url);

        /**
         * Fetches a token asynchronously from the Approov cloud service.
         * 
         * This function returns a task which returns a token when complete.
         *
         * @param   url The URL of the connection to be protected by Approov
         *              or null if no protection is desired.
         * @returns     A task which returns a token string once completed.
         */
        public abstract Task<string> FetchTokenAsync(string url = null);

        /**
         * Sets a token payload value.
         * 
         * A base64 encoded SHA256 hash of the payload data will be added as
         * a 'pay' claim in subsequent attestation tokens. 
         *
         * @param   value The payload data as a non-null string value.
         */
        public abstract void SetTokenPayloadValue(string value);

        /**
         * Retrieves the X.509 TLS leaf certificate in DER binary format
         * for the given URL.
         *
         * The fetchToken() or fetchTokenAsync() method should be invoked 
         * with the same URL prior to invoking this method.
         * 
         * @param   url URL of the connection protected by Approov.
         * @returns     The certificate data, or null if the connection to the 
         *              given URL has not been retrieved by Approov.
         */
        public abstract byte[] GetCert(string url);

        /**
         * Clears the internal cache of X.509 TLS leaf certificates retrieved 
         * by Approov. This should be called if you suspect that the 
         * certificate information stored is incorrect, either as a result of 
         * communication with your server or a miss-match in the certificates
         * obtained by calling getCert and comparing the answer to your 
         * connection's certificate.
         * 
         * @returns Nothing.
         */
        public abstract void ClearCerts();
    }
}
