using Java.Lang;
using static EngineIO.Emitter;

namespace XamarinRealTimeLuckyNumberGame
{
    internal class MyMoneySendEventListener : Java.Lang.Object, IListener
    {
        private MainActivity mainActivity;

        public MyMoneySendEventListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void Call(params Object[] args)
        {
            mainActivity.RunOnUiThread(() => {
               
                mainActivity.txtMoney.Text = args[0].ToString();

            });
        }
    }
}