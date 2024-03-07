using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FonApi.Models.Ventures{
    [Table("ventures_header")]
    public class VenturesHeader
    {
        [Key]
        [Column("venture_id")]
        public int VentureId { get; set; }

        [Column("venture_header")]
        public string? VentureHeader { get; set; }

        [Column("target_fund")]
        public decimal TargetFund { get; set; }

        [Column("header_thumbnail")]
        public string? HeaderThumbnail { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }
    }
}