using Java.Lang;
using static EngineIO.Emitter;

namespace XamarinRealTimeLuckyNumberGame
{
    internal class MyWaitBeforeRestartEventListener : Java.Lang.Object, IListener
    {
        private MainActivity mainActivity;

        public MyWaitBeforeRestartEventListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void Call(params Object[] args)
        {
            mainActivity.canPlay = true;
            mainActivity.RunOnUiThread(()=>{
                mainActivity.txtStatus.Text = $"Please wait for {args[0].ToString()} seconds";
                mainActivity.txtCount.Text = "wait..";
                mainActivity.isBet = false;


            });
        }
    }
}