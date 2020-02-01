using Java.Lang;
using static EngineIO.Emitter;

namespace XamarinRealTimeLuckyNumberGame
{
    internal class MyWaitRestartEventListener : Java.Lang.Object, IListener
    {
        private MainActivity mainActivity;

        public MyWaitRestartEventListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void Call(params Object[] args)
        {
            mainActivity.canPlay = true;
            mainActivity.RunOnUiThread(() => {

                mainActivity.txtResult.Visibility = Android.Views.ViewStates.Gone;
                
            });
        }
    }
}