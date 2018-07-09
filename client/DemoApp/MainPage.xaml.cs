using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;

using ApproovSDK;
using System.Net;

namespace DemoApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (Constants.UseCertificatePinning) 
            {
                ServicePointConfiguration.SetUp(Secrets.DemoPublicKey);
            }
        }

        /*
         * OnHello() demonstrates an unprotected API call to the demo server. 
         * 
         * No Approov token is required. 
         * 
         * A successful response is 'Hello World!"
         */

        private readonly HttpClient helloClient = new HttpClient();


        private async void OnHello(object sender, EventArgs e)
        {
            HttpResponseMessage response;

            // make hello request (not protected by approov)

            try
            {
                response = await helloClient.GetAsync(Constants.HelloUrl);
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException is WebException we && we.Status ==
                    WebExceptionStatus.TrustFailure)
                {
                    // Pinning failure
                    response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.ReasonPhrase = "NOT AUTHORIZED";
                }
                else
                {
                    // Other network issues  
                    response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.ReasonPhrase = "NETWORK EXCEPTION";
                }
            }
            catch (Exception)
            {
                // All other exceptions  
                response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.ReasonPhrase = "OTHER EXCEPTION";
            }

            string message;
            string imageSource;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                message = content;
                imageSource = "hello.png";
            }
            else
            {
                message = response.ReasonPhrase;
                imageSource = "confused.png";
            }

            statusLabel.Text = message;
            statusImage.Source = imageSource;
        }

        /*
         * OnShape() demonstrates an Approov-protected API call to the demo server. 
         * 
         * An Approov interceptor is used to automatically fetch an Approov token
         * and add it to the Approov-Token header for every API call made by the
         * shapeClient. 
         * 
         * A successful response is a random shape name.
         */

        private readonly HttpClient shapeClient = new HttpClient(
            new ApproovHandler(checkCerts: true));

        private async void OnShape(object sender, EventArgs e)
        {
            HttpResponseMessage response;

            // make shape request (protected by approov)

            try
            {
                response = await shapeClient.GetAsync(Constants.ShapeUrl);
            }
            catch (HttpRequestException ex)
            {
                if (ex.InnerException is WebException we && we.Status ==
                    WebExceptionStatus.TrustFailure)
                {
                    // Pinning failure
                    response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.ReasonPhrase = "NOT AUTHORIZED";
                }
                else
                {
                    // Other network issues  
                    response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.ReasonPhrase = "NETWORK EXCEPTION";
                }
            }
            catch (Exception)
            {
                // Other exceptions  
                response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.ReasonPhrase = "OTHER EXCEPTION";
            }

            string message;
            string imageSource;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                message = content;
                imageSource = content.ToLower() + ".png";
            }
            else
            {
                message = response.ReasonPhrase;
                imageSource = "confused.png";
            }

            statusLabel.Text = message;
            statusImage.Source = imageSource;
        }
    }
}
