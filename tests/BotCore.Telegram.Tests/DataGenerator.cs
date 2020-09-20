using Telegram.Bot.Types;

namespace BotCore.Telegram.Tests
{
    public static class DataGenerator
    {
        public static class Telegram
        {
            public static Update GetDefaultTelegramUpdate(string text)
            {
                return new Update
                {
                    Message = new Message
                    {
                        Chat = new Chat
                        {
                            Id = 1
                        },
                        From = new User
                        {
                            Username = "username",
                            Id = 1
                        },
                        Text = text
                    }
                };
            }

            public static Update GetDefaultTelegramUpdateWithReply(string replyText, string text)
            {
                return new Update
                {
                    Message = new Message
                    {
                        Chat = new Chat
                        {
                            Id = 1
                        },
                        From = new User
                        {
                            Username = "username",
                            Id = 1
                        },
                        ReplyToMessage = new Message
                        {
                            Text = replyText
                        },
                        Text = text
                    }
                };
            }
        }
    }
}