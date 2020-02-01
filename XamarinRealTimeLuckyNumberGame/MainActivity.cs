using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

using Socket= SocketIO.Client.Socket;
using SocketIO.Client;
using Org.Json;

namespace XamarinRealTimeLuckyNumberGame
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public TextView txtCount, txtResult, txtMoney, txtStatus, txtScore;
        public Button btnSubmit, btnDisconnect;
        public EditText edtBetValue, edtMoney;
        public Socket socket;

        public bool isDisconnect = false, isBet = false, canPlay = true;
        public int resultNumber = -1;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            edtBetValue = FindViewById<EditText>(Resource.Id.edtGuessedNumber);
            edtMoney = FindViewById<EditText>(Resource.Id.edtMoney);

            txtCount = FindViewById<TextView>(Resource.Id.txtCount);
            txtScore = FindViewById<TextView>(Resource.Id.txtScore);
            txtResult = FindViewById<TextView>(Resource.Id.txtResult);
            txtMoney = FindViewById<TextView>(Resource.Id.txtMoney);
            txtStatus = FindViewById<TextView>(Resource.Id.txtStatus);

            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmitBet);
            btnSubmit.Click += delegate {

                try
                {
                    if(canPlay)
                    {
                        if(!isBet)
                        {
                            int betValueMoney = Convert.ToInt32(edtMoney.Text.ToString());
                            if(Common.score>=betValueMoney)
                            {
                                JSONObject jsonObject = new JSONObject();
                                jsonObject.Put("money",betValueMoney);
                                jsonObject.Put("betValue", Convert.ToInt32(edtBetValue.Text.ToString()));


                                socket.Emit("client_send_money", jsonObject);

                                Common.score -= betValueMoney;
                                txtScore.Text = Convert.ToString(Common.score);
                                isBet = true;
                            }
                            else
                            {
                                Toast.MakeText(this, "You do not have enough money", ToastLength.Short).Show();

                            }

                        }
                        else
                        {
                            Toast.MakeText(this, "You already bet in this turn.", ToastLength.Short).Show();


                        }

                    }
                    else
                    {
                        Toast.MakeText(this, "Please wait until to next turn.", ToastLength.Short).Show();

                    }
                }
                catch (Exception ex)
                {

                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();

                }



            };

            btnDisconnect = FindViewById<Button>(Resource.Id.btnDisconnect);
            btnDisconnect.Click += delegate {

                if (!isDisconnect) { 
                    socket.Disconnect();
                    btnDisconnect.Text = "CONNECT";
                }
                else
                {

                    socket.Connect();
                    btnDisconnect.Text = "DISCONNECT";
                }

                isDisconnect = !isDisconnect;

            };


            try
            {
                socket = IO.Socket("http://10.0.2.2:3000");
                socket.On(Socket.EventConnect, new MyConnectEventListener(this));
                socket.Connect();
            }
            catch (System.Exception ex)
            {

                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }


            //Register all event from socket
            RegisterListeningEvent();

        }

        private void RegisterListeningEvent()
        {
            socket.On("broadcast",new MyBroadcastEventListener(this));
            socket.On("result", new MyResultEventListener(this));

            socket.On("wait_before_restart", new MyWaitBeforeRestartEventListener(this));
            socket.On("restart", new MyWaitRestartEventListener(this));
            socket.On("money_send", new MyMoneySendEventListener(this));
            socket.On("reward", new MyRewardEventListener(this));
            socket.On("lose", new MyLoseEventListener(this));

            socket.On(Socket.EventDisconnect, new MyDisconnectEventListener(this));

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}