using System;

/*
 * This is a sample file. Copy the contents of this file into an actual
 * Secrets.cs file. 
 * 
 * NOTES:
 * 
 * 1) The public key string is expected in hexadecimal format. It is more
 *    common to see this string in Base64 format, so convert as necessary.
 * 
 * 2) In the Xamarin project, this file's build action is set to 'None'. 
 *    Make sure the build action for the actual Secrets.cs file is set
 *    to 'Compile'.

namespace DemoApp
{
    public static class Secrets
    {
        /*
         * Place the public key for teh demo site here in hexadecimal format.
         */
        public const string DemoPublicKey =
            "3082010a0282010100c0b9798b24ff33fb176b305a5e04e9a638eedb8322e380" +
            "0c8e35a18a0b1f6130032a22254b324357f35b3a2b11e8b2474fc1b4c3227b71" +
            "3a6fbc7837d683e0065790901873c70fa18a80be5c21830514084154f95a279b" + 
            "1f131cd8fa00b38cc396b204d6ef7e2c8bb9ea697714c0797a1a66f0cedc6d5a" +
            "28d3fcc637b0db0b333220c5eab659858e6b781bd92a76690fc5476ba8e23156" + 
            "139eef5b22606fbcbbcd5489e735550faac4123f25e6dcfefafc5c7ebe8b01d1" + 
            "0dc56178945f2c64e08aa7920fa0bbfc3fe2c2f4ce051cd9b4aa98eb4a502a47" + 
            "bd8e06b0a911e3d4fa57ea2cee45819e1bdbd68771cc8b3b0032621c0b39094d" + 
            "96c1f77c005d653b470203010001";
    }
}