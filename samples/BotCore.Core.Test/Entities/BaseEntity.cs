using System.ComponentModel.DataAnnotations;

namespace BotCore.Core.Test.Entities
{
    public class BaseEntity
    {
        [Key] public int Id { get; set; }
    }
}