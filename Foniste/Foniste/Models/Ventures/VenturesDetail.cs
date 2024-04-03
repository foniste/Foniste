using Foniste.Models.Exception;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foniste.Models.Ventures{
    [Table("ventures_detail")]
    public class VenturesDetail
    {
        [Key]
        [Column("venture_id")]
        public int VentureId { get; set; }

        [Column("num_of_investor")]
        public int? NumberOfInvestor { get; set; }

        [Column("fund_amount")]
        public decimal? FundAmount { get; set; }

        [Column("min_invest_value")]
        public decimal? MinInvestValue { get; set; }

        [Column("video_link")]
        public string? VideoLink { get; set; }

        [Column("description")]
        public string? Description { get; set; }
    }
}