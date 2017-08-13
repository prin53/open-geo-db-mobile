using System;
using Android.App;
using Android.Runtime;

namespace OpenGeoDB.Droid
{
    [MetaData("com.google.android.maps.v2.API_KEY", Value = "AIzaSyD94WS_0nN4ri5MnnCnZ6r8vIfYNDOGU28")]
    [MetaData("com.google.android.gms.version", Value="@integer/google_play_services_version")]
    [Application(
        Theme = "@style/OpenGeoDB.Theme", 
        Icon = "@mipmap/icon",
        Label = "@string/AppName",
        AllowBackup = true
    )]
    public class OpenGeoDbApplication : Application
    {
        public OpenGeoDbApplication()
        {
            /* Required constructor */
        }

        protected OpenGeoDbApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            /* Required constructor */
        }
    }
}
