using Foniste.Models.Exception;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foniste.Models.Ventures{
    [Table("ventures_img")]
    public class VenturesImg
    {
        [Key]
        [Column("img_id")]
        public int ImgId { get; set; }

        [Column("img_name")]
        public string? ImgName { get; set; }

        [Column("img_file")]
        public byte[]? ImgFile { get; set; }

        [Column("org_id")]
        public int OrgId { get; set; }

        [Column("creation_date")]
        public DateTime? CreationDate { get; set; }
    }
}