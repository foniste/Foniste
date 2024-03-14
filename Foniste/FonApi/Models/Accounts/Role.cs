using FonApi.Models.Exception;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FonApi.Models.Accounts{
    [Table("role")]
    public class Role{
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("role_name")]
        public string? RoleName { get; set; }

        [Column("role_description")]
        public string? RoleDescription { get; set; }
    }
}