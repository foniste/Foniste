using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foniste.Models.Ventures
{
    [Table("ventures_all")]
    public class VenturesAll
    {
        [Key]
        [Column("venture_id")]
        public int venture_id { get; set; }

        [Column("organization_id")]
        public int organization_id { get; set; }

        [Column("venture_header")]
        public string? venture_header { get; set; }

        [Column("venture_description")]
        public string? venture_description { get; set; }

        [Column("num_of_investor")]
        public int num_of_investor { get; set; }

        [Column("fund_amount")]
        public float fund_amount { get; set; }

        [Column("target_fund")]
        public float target_fund { get; set; }

        [Column("min_invest_value")]
        public float min_invest_value { get; set; }

        [Column("header_thumbnail")]
        public string? header_thumbnail { get; set; }
    }
}