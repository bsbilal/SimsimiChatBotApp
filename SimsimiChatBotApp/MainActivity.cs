using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;
using SimsimiChatBotApp.Models;
using System.Collections.Generic;
using SimsimiChatBotApp.Helper;
using Newtonsoft.Json;
using SimsimiChatBotApp.Adapter;
using System.Net.Http;

namespace SimsimiChatBotApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        ListView listOfMessage;
         EditText userMessage ;
         FloatingActionButton btnSend;

        List<ChatModel> listChat = new List<ChatModel>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
          
            SetContentView(Resource.Layout.activity_main);


            listOfMessage = FindViewById<ListView>(Resource.Id.list_of_message);
            userMessage = FindViewById<EditText>(Resource.Id.user_messages);

            btnSend = FindViewById<FloatingActionButton>(Resource.Id.fab);
            btnSend.Click += delegate
              {
                 string text = userMessage.Text;
                  ChatModel model = new ChatModel();
                  model.ChatMessage = text;
                  model.IsSend = true;

                  listChat.Add(model);

                  new SimsimiAPI(this).Execute(userMessage.Text);
              };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        internal class SimsimiAPI : AsyncTask<string, string, string>
        {
            private MainActivity mainActivity;
            private const string API_KEY = "VUKnVBMGhpVlgcYNa6xQBjr6Z2DumB2sZTDi1+Pq";
        public SimsimiAPI(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }



            protected override string RunInBackground(params string[] @params)
            {
                

                string url = $"http://sandbox.api.simsimi.com/request.p?key={API_KEY}&lc=en&ft=1.0&text={@params[0]}";

                HttpDataHandler dataHandler = new HttpDataHandler();
                return dataHandler.GetHttpData(url);

            }
            protected override void OnPostExecute(string result)
            {
                SimsimiModel simsimiModel = JsonConvert.DeserializeObject<SimsimiModel>(result);

                ChatModel model = new ChatModel();
                model.ChatMessage = simsimiModel.response;
                model.IsSend = false;

                mainActivity.listChat.Add(model);
                CustomAdapter adapter = new CustomAdapter(mainActivity.listChat, mainActivity.BaseContext);
                mainActivity.listOfMessage.Adapter = adapter;
            }

        }
    }

  
}