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
        var ChatId = message!.Chat.Id;
        var user = message.From;

        if (message != null && message.Text != null)
        {

            if (message.Text == "/start")
            {
                var keyboard = new InlineKeyboardMarkup(new[]
{
                        new [] // first row
                        {
                            InlineKeyboardButton.WithUrl("12","www.google.com"),
                            InlineKeyboardButton.WithCallbackData("☢️☣️что сегодня в столовке☣️☢️"),
                        },
                        new [] // second row
                        {
                            InlineKeyboardButton.WithCallbackData("2.1"),
                            InlineKeyboardButton.WithCallbackData("2.2"),
                        }
                    });
                await botClient.SendTextMessageAsync(ChatId, "Жамкни!", replyMarkup: keyboard);
             //  await botClient.SendTextMessageAsync(ChatId, $"Привет {user!.FirstName}");
                return;
               
            }
            else if (message.Text == "/inline")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                            new KeyboardButton[] { "Понедельник" },
                            new KeyboardButton[] { "Вторник" },
                            new KeyboardButton[] { "Среда" },
                            new KeyboardButton[] { "Четверг" },
                            new KeyboardButton[] { "Пятница" },
                            });
                await botClient.SendTextMessageAsync(ChatId, "Привет", replyMarkup: replyKeyboardMarkup);


                return;
            }
            await ShowButton(botClient, update);
            Console.WriteLine($"{user!.FirstName} ({user.Id}) Время: {message.Date} написал сообщение: {message!.Text}");
        }






    }
     async static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
   
    public async static Task ShowButton(ITelegramBotClient botClient, Update update)
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
    public async static Task Inlinck(ITelegramBotClient botClient, Update update)
    {
        await botClient.SendTextMessageAsync(update.Message!.Chat.Id, "ПОка");   
    }
}


 





//var keyboard = new List<List<KeyboardButton>>()
//        {
//            new List<KeyboardButton>{ new KeyboardButton(TEXT_1), new KeyboardButton(TEXT_2), },
//            new List<KeyboardButton>{ new KeyboardButton(TEXT_3), new KeyboardButton(TEXT_4), },
//    };
//return new ReplyKeyboardMarkup(keyboard);