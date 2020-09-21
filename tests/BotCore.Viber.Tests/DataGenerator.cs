using Viber.Bot;

namespace BotCore.Viber.Tests
{
    public static class DataGenerator
    {
        public static class Viber
        {
            public static CallbackData GetDefaultViberCallbackData(string text)
            {
                return new CallbackData
                {
                    Message = new TextMessage
                    {
                        Sender = new User
                        {
                            Id = "1",
                            Avatar = "1"
                        },
                        Receiver = "1",
                        Text = text,
                    },
                    Sender = new User
                    {
                        Id = "1",
                        Avatar = "1"
                    },
                    Event = EventType.Message
                };
            }
        }
    }
}