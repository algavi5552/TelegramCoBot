using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace TelegramCoBot
{
    internal class TelegramBotHelper
    {
       object  fileCreateData;
        private string _token;
        Telegram.Bot.TelegramBotClient _client;
        public TelegramBotHelper(string token)
        {
            this._token = token;
        }
        //string connString = @"Data Source=192.168.0.20;Initial Catalog=nomen;	Persist Security Info=True;User ID=sa;	Password=l14sql170407";
        DataTable dt = new();
        int i = 0;
        
        internal void GetUpdates()
        {
            _client = new(_token);
            var me = _client.GetMeAsync().Result; //получаем инфо  о  боте
            if (me!=null && !string.IsNullOrEmpty(me.Username ))
            {
                int offset = 0;
                while (true)
                {
                    try
                    {
                        var updates = _client.GetUpdatesAsync(offset).Result;
                        if (updates != null && updates.Count() > 0)
                        {
                            foreach (var update in updates)
                            {
                                CheckTheLoad();
                                processUpdate(update);
                                offset = update.Id + 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        private void processUpdate(Telegram.Bot.Types.Update update)
        {
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    var text = update.Message.Text;
                    if (text.Contains("статус"))
                    {
                        _client.SendTextMessageAsync(update.Message.Chat.Id, "OK");
                    }
                    else if (text.Contains("дата"))
                    {
                        _client.SendTextMessageAsync(update.Message.Chat.Id, fileCreateData.ToString());
                    }
                    else
                    {
                        _client.SendTextMessageAsync(update.Message.Chat.Id, "OK " + fileCreateData.ToString());
                    }
 
                    //_client.SendTextMessageAsync(update.Message.Chat.Id, "Binary Data Received: " + text);
                    break;
                default: 
                    Console.WriteLine(update.Type+" Not implemented! ");
                    break;
            }
        }

        public void CheckTheLoad()
        {
            if (i == 0)
            {
                dt.Columns.Add("папка для vipnet");
                i = 1;
            }
            DirectoryInfo dir = new DirectoryInfo(@"\\Srv-ttmf-sql2\единый номенклатор\");
            dt.Clear();
            foreach (var item in dir.GetFiles())
            {
                dt.Rows.Add(item);
                dt.Rows.Add(item.CreationTime);
            }
            fileCreateData = dt.Rows[1][0];

        }
    }
}