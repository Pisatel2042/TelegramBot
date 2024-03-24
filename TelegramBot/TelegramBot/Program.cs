using System;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Channels;

partial class Program
{


    private static ReceiverOptions _receiverOptions;
    static void Main(string[] args)
    {
        Start();
        Console.ReadLine();

    }

    static async void Start()
    {

        CancellationTokenSource cts = new();
        _receiverOptions = new();
        var client = new TelegramBotClient("7082038598:AAHqMCO_r1LPZlid_Bv09XWoKgovjPFiPME");
        client.StartReceiving(
     updateHandler: UpdateHandler,
     pollingErrorHandler: ErrorHandler,
     receiverOptions: _receiverOptions,
     cancellationToken: cts.Token);


    }



    async static Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {

        var message = update.Message;
        var ChatId = message!.Chat!.Id!;
        var user = message.From;
  

        //if (message != null && message.Text != null)
        //{

        //    if (message.Text == "/start" )
        //    {   
        //        var keyboard = new InlineKeyboardMarkup(new[]
        //            {
        //                new [] // first row
        //                {
        //                    InlineKeyboardButton.WithUrl("1 2","www.google.com"),
        //                    InlineKeyboardButton.WithCallbackData("☢️☣️что сегодня в столовке☣️☢️"),
        //                },
        //                new [] // second row
        //                {
        //                    InlineKeyboardButton.WithCallbackData("2.1"),
        //                    InlineKeyboardButton.WithCallbackData("2.2"),
        //                }
        //            });
        //        await botClient.SendTextMessageAsync(ChatId, "Жамкни!", replyMarkup: keyboard);
        //     //  await botClient.SendTextMessageAsync(ChatId, $"Привет {user!.FirstName}");
            
               
        //    }
        //     if (message.Text == "/inline")
        //    {
        //        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        //        {
        //                    new KeyboardButton[] { "Понедельник" },
        //                    new KeyboardButton[] { "Вторник" },
        //                    new KeyboardButton[] { "Среда" },
        //                    new KeyboardButton[] { "Четверг" },
        //                    new KeyboardButton[] { "Пятница" },
        //                    });
        //        await botClient.SendTextMessageAsync(ChatId, "Привет", replyMarkup: replyKeyboardMarkup);
        //        await ShowButton(botClient, update);
                

            
        //    }
        //     if (message.Text == "привет")
        //    {
        //        await botClient.SendTextMessageAsync(ChatId, "Привет ");
        //        return;
        //    }
        //     return;
            
        //}
        
      if(message != null && message.Text != null) 
        {
            if (message.Text == "/start")
            {
                
                //await botClient.SendTextMessageAsync(ChatId, "Выбире \n /Inline \n /Reply");
                await ShowInlineButton(botClient, update);
                return;
            }
           else if (message.Text == "/Inline")
            {
                await ShowReplyButton(botClient, update);
                return;
               
            }
            await ShowReplyButtonData(botClient, update);
            if (message.Text == "/Reply")
            {
                await botClient.SendTextMessageAsync(ChatId, "cat");
                return;
            }
        }


        Console.WriteLine($"{user!.FirstName} ({user.Id}) Время: {message!.Date} написал сообщение: {message!.Text}");

            






    }

    public async static  Task inlineButton(ITelegramBotClient botClient, Update update)
    {
        var message = update.Message;
        var ChatId = message!.Chat.Id;
        var user = message.From;

        var keyboard = new InlineKeyboardMarkup(new[]
                    {
                        new [] // first row
                        {
                            InlineKeyboardButton.WithUrl("1 2","www.google.com"),
                            InlineKeyboardButton.WithCallbackData("☢️☣️что сегодня в столовке☣️☢️"),
                        },
                        new [] // second row
                        {
                            InlineKeyboardButton.WithCallbackData("2.1"),
                            InlineKeyboardButton.WithCallbackData("2.2"),
                        }
                    });
        await botClient.SendTextMessageAsync(ChatId, "Жамкни!", replyMarkup: keyboard);
        return;
    }
     async static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
   
    public async static Task ShowReplyButtonData(ITelegramBotClient botClient, Update update)
    {
        var message = update.Message;
        var ChatId = message!.Chat.Id;

       
        switch (message.Text)
        {

            case "Понедельник":
                await botClient.SendTextMessageAsync(ChatId, "Расписание на Понедельник:\n Практика \n Практика \n Практика ");
                break;
            case "Вторник":
                await botClient.SendTextMessageAsync(ChatId, "Расписание Вторник:\n Плактика \n Плактика \n Плактика ");
                break;
            case "Среда":
                await botClient.SendTextMessageAsync(ChatId, "Расписание на Среда:\n ПрактиОН \n ПрактикОН \n ПрактикОН ");
                break;
            case "Четверг":
                await botClient.SendTextMessageAsync(ChatId, "Расписание на Четверг:\n Практика \n Практика \n Практика ");
                break;
            case "Пятница":
                await botClient.SendTextMessageAsync(ChatId, "Расписание на Пятница:\n Практика \n Практика \n Практика ");
                break;


        }
    }
    public async static Task ShowReplyButton(ITelegramBotClient botClient, Update update)
    {
        var message = update.Message;
        var ChatId = message!.Chat.Id;


        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                                   {
                            new KeyboardButton[] { "Понедельник" },
                            new KeyboardButton[] { "Вторник" },
                            new KeyboardButton[] { "Среда" },
                            new KeyboardButton[] { "Четверг" },
                            new KeyboardButton[] { "Пятница" },
                            });
        await botClient.SendTextMessageAsync(ChatId, "Привет", replyMarkup: replyKeyboardMarkup);
    }
    public async static Task ShowInlineButton(ITelegramBotClient botClient, Update update)
    {
        var message = update.Message;
        var ChatId = message!.Chat.Id;
        var user = message.From;


        var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {   
            
                
                 new [] // first row
                        {
                            InlineKeyboardButton.WithUrl("1 2","www.google.com"),
                            InlineKeyboardButton.WithCallbackData("что сегодня в столовке"),
                        },
                 new [] // second row
                        {
                            InlineKeyboardButton.WithCallbackData("2.1"),
                            InlineKeyboardButton.WithCallbackData("2.2"),
                        }
            });
        //var inlineKeyboard = new InlineKeyboardMarkup(new[]
        //    {
        //        InlineKeyboardButton.WithUrl("Go url 1", "https://www.google.com/"),
        //        InlineKeyboardButton.WithUrl("Go url 2", "https://www.bing.com/")
        //    });
        
        await botClient.SendTextMessageAsync(ChatId, "Жамкни!", replyMarkup: inlineKeyboard);
        return;
    }

}


 





//var keyboard = new List<List<KeyboardButton>>()
//        {
//            new List<KeyboardButton>{ new KeyboardButton(TEXT_1), new KeyboardButton(TEXT_2), },
//            new List<KeyboardButton>{ new KeyboardButton(TEXT_3), new KeyboardButton(TEXT_4), },
//    };
//return new ReplyKeyboardMarkup(keyboard);