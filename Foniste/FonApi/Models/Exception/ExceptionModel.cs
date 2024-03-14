using System.ComponentModel.DataAnnotations;

namespace FonApi.Models.Exception
{
    public class ExceptionLog
    {
        [Key]
        public int Id { get; set; } 
        public int ExceptionId { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }
    }
}
