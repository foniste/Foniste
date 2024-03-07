using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FonApi.Models.Accounts{
    [Table("user_auth")]
    public class UserAuth {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("email")]
        [MaxLength(255)]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("role_id")]
        public int? RoleId { get; set; }

        [Column("creation_date")]
        public DateTime? CreationDate { get; set; }
    }
}