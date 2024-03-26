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
        //  var ChatId = message.Chat.Id;
        //var user = message.From;





        if (message != null && message.Text != null)
        {
            if (message.Text == "/start")
            {

                //await botClient.SendTextMessageAsync(ChatId, "Выбире \n /Inline \n /Reply");
                await ShowinlineButton(botClient, update);

            
                return;
            }

            await ButtonInlineClickProcessing(botClient, update);
            if (message.Text == "/stop")
            {

            }
            else if (message.Text == "/Inline")
            {
                await ShowReplyButton(botClient, update);
                return;

            }
            await ButtonReplyClickProcessing(botClient, update);
            if (message.Text == "/Reply")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id!, "cat");
                return;
            }

        }


        // Console.WriteLine($"{message!.From.FirstName} ({message.From.Id}) Время: {message.Date} написал сообщение: {message.Text}");








    }

    public async static Task ShowinlineButton(ITelegramBotClient botClient, Update update)
    {
        var message = update.Message;
        var ChatId = message!.Chat.Id;
        var user = message.From;

        var keyboardInline = new InlineKeyboardMarkup(new[]
                    {
                        new [] // first row
                        {
                            InlineKeyboardButton.WithUrl(text:"Расписание на сегодня ","https://clck.ru/39eZDB"),
                            InlineKeyboardButton.WithCallbackData("☣что сегодня в столовке☣",callbackData:"button2"),
                        },
                        new [] // second row
                        {
                            InlineKeyboardButton.WithCallbackData("🎰 Игра Орел Решка 🎰",callbackData:"button3"),

                            InlineKeyboardButton.WithCallbackData("Анекдоты", callbackData: "button4"),

                        }
                    });
        await botClient.SendTextMessageAsync(ChatId, " Привет что хочешь ?", replyMarkup: keyboardInline);
        return;
    }
    public static async Task ButtonInlineClickProcessing(ITelegramBotClient botClient, Update update)
    {   //CancellationToken cancellationToken = new CancellationToken();

        //  var message = update.Message;
        //  var ChatId = message!.Chat.Id;
        //  var user = message.From;

        //switch(update.Type) 
        //{
        //    case UpdateType.CallbackQuery:
        //        {
        //            var callbackQuery = update.CallbackQuery;
        //            var User = callbackQuery!.From;
        //            var chat = callbackQuery.Message!.Chat;
        //            switch (callbackQuery.Data)
        //            {
        //                case "1":
        //                    {
        //                        await botClient.SendTextMessageAsync(ChatId, "Привт зайка");
        //                        return;
        //                    }
        //            }

        //        }
        //        break;
        //}

        //if(update.CallbackQuery!.Data == "button1")
        //{
        //    await botClient.SendTextMessageAsync(update.CallbackQuery.Message!.Chat.Id, "Привет соска" ,cancellationToken: cancellationToken);
        //        return;
        //}

        Console.WriteLine($"нажал на кнопку:");
        switch (update.Type)
        {
            case UpdateType.CallbackQuery:
                var callbackQuery = update.CallbackQuery;
                var user = callbackQuery.From;
                Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");
                var chat = callbackQuery.Message.Chat;
                switch (callbackQuery.Data)
                {
                    case "button1":
                        {
                            await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                            await botClient.SendTextMessageAsync(chat.Id, "ты нажал на кнопку 1");
                            return;
                        }
                    case "button2":
                        {

                            await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Тут может быть ваш текст!");
                            await botClient.SendTextMessageAsync(chat.Id, $"Вы нажали на {callbackQuery.Data}");
                            return;
                        }
                    case "button3":
                        {
                            // А тут мы добавили еще showAlert, чтобы отобразить пользователю полноценное окно
                            await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "А это полноэкранный текст!", showAlert: true);
                            await botClient.SendTextMessageAsync(chat.Id, $"Вы нажали на {callbackQuery.Data}");
                            return;
                        }
                }
                return;
                //if (update.Type == UpdateType.CallbackQuery)
                //{
                //    CallbackQuery callbackQuery = update.CallbackQuery!;
                //    Console.WriteLine("ну Привет ");
                //}
                //else
                //{
                //    return;
                //}

        }

    }
    async static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async static Task ButtonReplyClickProcessing(ITelegramBotClient botClient, Update update)
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


                                return;

}













//var keyboard = new List<List<KeyboardButton>>()
//        {
//            new List<KeyboardButton>{ new KeyboardButton(TEXT_1), new KeyboardButton(TEXT_2), },
//            new List<KeyboardButton>{ new KeyboardButton(TEXT_3), new KeyboardButton(TEXT_4), },
//    };
//return new ReplyKeyboardMarkup(keyboard);

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


//    
//       
//       
//                     

//                               

//                     


//                

//            case UpdateType.CallbackQuery:
//                {
//                    // Переменная, которая будет содержать в себе всю информацию о кнопке, которую нажали
//                    var callbackQuery = update.CallbackQuery;

//                    // Аналогично и с Message мы можем получить информацию о чате, о пользователе и т.д.
//                    var user = callbackQuery.From;

//                    // Выводим на экран нажатие кнопки
//                    Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

//                    // Вот тут нужно уже быть немножко внимательным и не путаться!
//                    // Мы пишем не callbackQuery.Chat , а callbackQuery.Message.Chat , так как
//                    // кнопка привязана к сообщению, то мы берем информацию от сообщения.
//                    var chat = callbackQuery.Message.Chat;

//                    // Добавляем блок switch для проверки кнопок
//                    switch (callbackQuery.Data)
//                    {
//                        // Data - это придуманный нами id кнопки, мы его указывали в параметре
//                        // callbackData при создании кнопок. У меня это button1, button2 и button3

//                        case "button1":
//                            {
//                                // В этом типе клавиатуры обязательно нужно использовать следующий метод
//                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
//                                // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку

//                                await botClient.SendTextMessageAsync(
//                                    chat.Id,
//                                    $"Вы нажали на {callbackQuery.Data}");
//                                return;
//                            }

//                        case "button2":
//                            {
//                                // А здесь мы добавляем наш сообственный текст, который заменит слово "загрузка", когда мы нажмем на кнопку
//                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Тут может быть ваш текст!");

//                                await botClient.SendTextMessageAsync(
//                                    chat.Id,
//                                    $"Вы нажали на {callbackQuery.Data}");
//                                return;
//                            }

//                        case "button3":
//                            {
//                                // А тут мы добавили еще showAlert, чтобы отобразить пользователю полноценное окно
//                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "А это полноэкранный текст!", showAlert: true);

//                                await botClient.SendTextMessageAsync(
//                                    chat.Id,
//                                    $"Вы нажали на {callbackQuery.Data}");
//                                return;
//                            }
//                    }

//                    return;
//                }
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.ToString());
//    }
//}

// Удаление реплей клавиатуры 
//Message sentMessage = await botClient.SendTextMessageAsync(
//    chatId: chatId,
//    text: "Removing keyboard",
//    replyMarkup: new ReplyKeyboardRemove(),
//    cancellationToken: cancellationToken);