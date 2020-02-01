using Java.Lang;
using static EngineIO.Emitter;

namespace XamarinRealTimeLuckyNumberGame
{
    internal class MyResultEventListener : Java.Lang.Object, IListener
    {
    
        private MainActivity mainActivity;

        public MyResultEventListener(MainActivity mainActivity)
        {
            this.mainActivity = mainActivity;
        }

        public void Call(params Object[] args)
        {
            mainActivity.resultNumber = Integer.ParseInt(args[0].ToString());
            mainActivity.RunOnUiThread(() => {

                mainActivity.txtResult.Visibility = Android.Views.ViewStates.Visible;
                mainActivity.txtResult.Text = new StringBuilder("Result: ").Append(args[0].ToString()).ToString();
             

            });

        }
    }
}