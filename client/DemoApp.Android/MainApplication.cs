using System;

using Android.App;
using Android.Runtime;
using Android.Util;

using ApproovSDK;
using ApproovSDK.Droid;

namespace DemoApp.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Set up shared Android Approover
            Approov.Shared = new AndroidApproov(this.ApplicationContext);
            Log.Info("Approov", "set Approov shared instance");
        }
    }
}