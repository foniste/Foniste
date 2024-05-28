using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FonApi.Models.Accounts{
    [Table("organization")]    
    public class Organizations{
        [Key]
        [Column("org_id")]
        public int OrgId { get; set; }

        [Column("phonenumber1")]
        [MaxLength(255)]
        public string? PhoneNumber1 { get; set; }

        [Column("phonenumber2")]
        [MaxLength(255)]
        public string? PhoneNumber2 { get; set; }

        [Column("phonenumber3")]
        [MaxLength(255)]
        public string? PhoneNumber3 { get; set; }

        [Column("address")]
        [MaxLength(255)]
        public string? Address { get; set; }

        [Column("iban_no")]
        [MaxLength(32767)]
        public string? IBAN { get; set; }

        [Column("organization_name")]
        [MaxLength(255)]
        public string? OrganizationName { get; set; }
    }
}