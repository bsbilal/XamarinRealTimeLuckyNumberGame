using Java.Lang;
using static EngineIO.Emitter;

namespace XamarinRealTimeLuckyNumberGame
{
    internal class MyBroadcastEventListener: Java.Lang.Object, IListener
    {
        private MainActivity mainActivity;

        public MyBroadcastEventListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void Call(params Object[] args)
        {
            mainActivity.RunOnUiThread(() => {

            mainActivity.txtCount.Text = new StringBuilder("Timer: ").Append(args[0].ToString()).ToString();
                mainActivity.txtStatus.Text = string.Empty;
                mainActivity.txtResult.Text = string.Empty;


            });
        }
    }
}