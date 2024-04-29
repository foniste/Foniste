using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foniste.Models.Accounts
{
	[Table("users")]
    public class Users
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("username")]
        [MaxLength(255)]
        public string? Username { get; set; }

        [Column("name")]
        [MaxLength(255)]
        public string? Name { get; set; }

        [Column("surname")]
        [MaxLength(255)]
        public string? Surname { get; set; }

        [Column("profile_img")]
        public byte[]? ProfileImage { get; set; }
    }
}