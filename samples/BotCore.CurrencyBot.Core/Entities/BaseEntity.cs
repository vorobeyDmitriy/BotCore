using System.ComponentModel.DataAnnotations;

namespace BotCore.Core.CurrencyBot.Entities
{
    public class BaseEntity
    {
        [Key] public int Id { get; set; }
    }
}