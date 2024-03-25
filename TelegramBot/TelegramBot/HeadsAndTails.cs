using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    internal class HeadsAndTails
    {
        
        

        
        public async static Task InlineButtonHeadsAndTails()
        {
            var keyboard = new InlineKeyboardMarkup(new[]
                   {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Орел","button1"),
                            InlineKeyboardButton.WithCallbackData("Решка","2"),
                        } 
                    });
        }




        public async static Task HeadsAndTailsGame(ITelegramBotClient botClient, Update update)
        {
            
            //int heads;
            //int tails;

            Random random = new Random();
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
