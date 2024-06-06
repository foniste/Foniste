using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foniste.Models.Accounts{
    [Table("user_auth")]
    public class UserAuth{

        [Column("email")]
        [MaxLength(255)]
        public string? Email { get; set; }

        [MaxLength(255)]
        [Column("password")]
        public string? Password { get; set; }
    }
}