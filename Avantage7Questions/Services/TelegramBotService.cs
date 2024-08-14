using NuGet.Protocol.Plugins;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Avantage7Questions.Services
{
    public class TelegramBotService
    {

        private readonly ITelegramBotClient _bot;

        public TelegramBotService(string botToken)
        {
            _bot = new TelegramBotClient(botToken);
        }

        public async Task SendMessageAsync(long chatId, string message)
        {
            try
            {
                await _bot.SendTextMessageAsync(chatId, message);

                Console.WriteLine($"Повідомлення '{message}' було успішно надіслано."); // Повідомлення в консоль після успіху
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При надсиланні повідомлення виникла помилка: {ex.Message}"); // Логування помилок
            }
        }
    }
}