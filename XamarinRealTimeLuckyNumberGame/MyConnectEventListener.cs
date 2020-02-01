using Android.Widget;
using Java.Lang;
using static EngineIO.Emitter;

namespace XamarinRealTimeLuckyNumberGame
{
    internal class MyConnectEventListener:Java.Lang.Object,IListener
    {
        private MainActivity mainActivity;

        public MyConnectEventListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void Call(params Object[] p0)
        {
            mainActivity.RunOnUiThread(() => {
                Toast.MakeText(mainActivity, "Connected", ToastLength.Short).Show();

            });
        }
    }
}