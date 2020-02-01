using Java.Lang;
using static EngineIO.Emitter;

namespace XamarinRealTimeLuckyNumberGame
{
    internal class MyDisconnectEventListener : Java.Lang.Object, IListener
    {
        private MainActivity mainActivity;

        public MyDisconnectEventListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void Call(params Object[] args)
        {
            mainActivity.RunOnUiThread(() => {
                mainActivity.txtCount.Text = "Disconnected...";
                mainActivity.txtMoney.Text = "Disconnected...";



            });
        }
    }
}