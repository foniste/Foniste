using FonApi.Models.Exception;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FonApi.Models.Accounts{
    [Table("login_log")]
    public class LoginLog{
        [Key]
        [Column("log_id")]
        public int LogId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("creation_date")]
        public DateTime? CreationDate { get; set; }

        [Column("last_login")]
        public DateTime? LastLogin { get; set; }
    }
}