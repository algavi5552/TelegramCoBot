using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramCoBot
{
    class Program
    {
        static void Main(string[] args)
        {
            //string token = "1414817182:AAERJcMneZsGNel5-CIGd6tUJjy6vFnBWXk";
            try
            {
                TelegramBotHelper hlp = new TelegramBotHelper(token: "1414817182:AAERJcMneZsGNel5-CIGd6tUJjy6vFnBWXk");
                hlp.GetUpdates();
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
