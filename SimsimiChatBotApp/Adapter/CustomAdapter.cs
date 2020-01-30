using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Github.Library.Bubbleview;
using SimsimiChatBotApp.Models;

namespace SimsimiChatBotApp.Adapter
{
    public class CustomAdapter : BaseAdapter
    {
        private List<ChatModel> listChatModel;
        private Context context;
        private LayoutInflater inflater;
        

        public CustomAdapter (List<ChatModel> listChatModel,Context context)
        {
            this.context = context;
            this.listChatModel = listChatModel;
            inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
        }

        public override int Count { 
            get 
            { return listChatModel.Count; }
        
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if(view==null)
            {
                if (listChatModel[position].IsSend)
                    view = inflater.Inflate(Resource.Layout.List_item_message_send, null);
                else
                    view = inflater.Inflate(Resource.Layout.List_item_message_recv, null);

                BubbleTextView bubbleTextView = view.FindViewById<BubbleTextView>(Resource.Id.text_message);
                bubbleTextView.Text =(listChatModel[position].ChatMessage);
            }
            return view;
        }
    }
}