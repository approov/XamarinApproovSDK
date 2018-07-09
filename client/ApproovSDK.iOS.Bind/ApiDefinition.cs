using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using WebKit;

namespace ApproovSDK.iOS.Bind
{
    //[Static]
    ////[Verify(ConstantsInterfaceAssociation)]
    //interface Constants
    //{
    //    // extern double ApproovVersionNumber;
    //    [Field("ApproovVersionNumber", "__Internal")]
    //    double ApproovVersionNumber { get; }

    //    // extern const unsigned char [] ApproovVersionString;
    //    [Field("ApproovVersionString", "__Internal")]
    //    /*byte[]*/ IntPtr ApproovVersionString { get; }
    //}

    // @interface ApproovAttestee : NSObject
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface ApproovAttestee
    {
        // +(instancetype _Nullable)sharedAttestee;
        [Static]
        [Export("sharedAttestee")]
        [return: NullAllowed]
        ApproovAttestee SharedAttestee();

        // -(ApproovConfig * _Nonnull)createDefaultConfig;
        [Export("createDefaultConfig")]
        //[Verify(MethodToProperty)]
        //ApproovConfig CreateDefaultConfig { get; }
        ApproovConfig CreateDefaultConfig();

        // -(BOOL)initialise:(ApproovConfig * _Nonnull)config error:(NSError * _Null_unspecified * _Nonnull)error;
        [Export("initialise:error:")]
        bool Initialise(ApproovConfig config, out NSError error);

        // @property (readonly, copy) NSString * _Nonnull approovToken;
        [Export("approovToken")]
        string ApproovToken { get; }

        // -(void)setTokenPayloadValue:(NSString * _Nonnull)value;
        [Export("setTokenPayloadValue:")]
        void SetTokenPayloadValue(string value);

        // -(void)fetchApproovToken:(FetchApproovTokenCompletionHandler _Nonnull)completionHandler __attribute__((deprecated("Use the fetchApproovToken variant with URL specifier")));
        [Export("fetchApproovToken:")]
        void FetchApproovToken(FetchApproovTokenCompletionHandler completionHandler);

        // -(void)fetchApproovToken:(FetchApproovTokenCompletionHandler2 _Nonnull)completionHandler :(NSString * _Nullable)url;
        [Export("fetchApproovToken::")]
        void FetchApproovToken(FetchApproovTokenCompletionHandler2 completionHandler, [NullAllowed] string url);

        // -(ApproovTokenFetchData * _Nonnull)fetchApproovTokenAndWait __attribute__((deprecated("Use the fetchApproovTokenAndWait variant with URL specifier")));
        //[Export("fetchApproovTokenAndWait")]
        //[Verify(MethodToProperty)]
        //ApproovTokenFetchData FetchApproovTokenAndWait { get; }

        // -(ApproovTokenFetchData * _Nonnull)fetchApproovTokenAndWait:(NSString * _Nullable)url;
        [Export("fetchApproovTokenAndWait:")]
        ApproovTokenFetchData FetchApproovTokenAndWait([NullAllowed] string url);

        // -(NSData * _Nullable)getCert:(NSString * _Nonnull)url;
        [Export("getCert:")]
        [return: NullAllowed]
        NSData GetCert(string url);

        // -(void)clearCerts;
        [Export("clearCerts")]
        void ClearCerts();

        // -(BOOL)registerUIWebView:(UIWebView * _Nonnull)webView;
        [Export("registerUIWebView:")]
        bool RegisterUIWebView(UIWebView webView);

        // -(void)unregisterUIWebView:(UIWebView * _Nonnull)webView;
        [Export("unregisterUIWebView:")]
        void UnregisterUIWebView(UIWebView webView);

        // -(BOOL)registerWKWebView:(WKWebView * _Nonnull)webView;
        [Export("registerWKWebView:")]
        bool RegisterWKWebView(WKWebView webView);

        // -(void)unregisterWKWebView:(WKWebView * _Nonnull)webView;
        [Export("unregisterWKWebView:")]
        void UnregisterWKWebView(WKWebView webView);
    }

    // typedef void (^FetchApproovTokenCompletionHandler)(ApproovTokenFetchResult, NSString * _Nonnull);
    delegate void FetchApproovTokenCompletionHandler(ApproovTokenFetchResult arg0, string arg1);

    // typedef void (^FetchApproovTokenCompletionHandler2)(ApproovTokenFetchData * _Nonnull);
    delegate void FetchApproovTokenCompletionHandler2(ApproovTokenFetchData arg0);

    // @interface ApproovConfig : NSObject
    [BaseType(typeof(NSObject))]
    interface ApproovConfig
    {
        // @property NSString * _Nullable customerName;
        [NullAllowed, Export("customerName")]
        string CustomerName { get; set; }

        // @property NSURL * _Nullable attestationURL;
        [NullAllowed, Export("attestationURL", ArgumentSemantic.Assign)]
        NSUrl AttestationURL { get; set; }

        // @property NSURL * _Nullable failoverURL;
        [NullAllowed, Export("failoverURL", ArgumentSemantic.Assign)]
        NSUrl FailoverURL { get; set; }

        // @property NSTimeInterval networkTimeout;
        [Export("networkTimeout")]
        double NetworkTimeout { get; set; }
    }

    // @interface ApproovTokenFetchData : NSObject
    [BaseType(typeof(NSObject))]
    interface ApproovTokenFetchData
    {
        // @property (readonly) ApproovTokenFetchResult result;
        [Export("result")]
        ApproovTokenFetchResult Result { get; }

        // @property (readonly) NSString * _Nonnull approovToken;
        [Export("approovToken")]
        string ApproovToken { get; }
    }
}
