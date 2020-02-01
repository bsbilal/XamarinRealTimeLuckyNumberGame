using Java.Lang;
using static EngineIO.Emitter;

namespace XamarinRealTimeLuckyNumberGame
{
    internal class MyLoseEventListener : Java.Lang.Object, IListener
    {
        private MainActivity mainActivity;

        public MyLoseEventListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void Call(params Object[] args)
        {
            int money = Integer.ParseInt(args[0].ToString());

            mainActivity.RunOnUiThread(() => {
                mainActivity.txtResult.SetBackgroundResource(Resource.Drawable.LoseTextView);
                mainActivity.txtResult.Text = $"Result: {mainActivity.resultNumber} | Your lose {money} EUR";
            });
        }
    }
}