using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBot
{
    internal class HeadsAndTails
    {


        public async static Task heads_and_tails(ITelegramBotClient botClient, Update update)
        {
            Random random = new Random();
            string heads;
            string tails;
            int value = random.Next(0, 1);
            if (value == 0)
            {

            }
            if (value == 1)
            {

            }
        }
    }
}
