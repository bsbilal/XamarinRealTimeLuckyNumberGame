using Java.Lang;
using static EngineIO.Emitter;

namespace XamarinRealTimeLuckyNumberGame
{
    internal class MyRewardEventListener : Java.Lang.Object, IListener
    {
        private MainActivity mainActivity;

        public MyRewardEventListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void Call(params Object[] args)
        {
            mainActivity.RunOnUiThread(() =>
            {

                mainActivity.txtResult.Text = $"Result: {mainActivity.resultNumber} | Congratulations ! You WON {args[0].ToString()} EUR";
                mainActivity.txtResult.SetBackgroundResource(Resource.Drawable.WinTextView);
                Common.score += Integer.ParseInt(args[0].ToString());
                mainActivity.txtScore.Text = Common.score.ToString();

            });
        }
    }
}