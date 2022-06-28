using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPCT.DataSupport
{
    [Table("CompanyProductPriceMargins")]
    public class CompanyProductPriceMargin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyProductPriceMarginId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public double Discount { get; set; }

        [Required]
        public bool IsWithVAT { get; set; }

        [Required]
        public int VATId { get; set; }
    }

    [Table("CompanyProductPriceMargins_History")]
    public class CompanyProductPriceMargin_History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryId { get; set; }
        public int? CompanyProductPriceMarginId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProductId { get; set; }
        public double? Discount { get; set; }
        public bool? IsWithVAT { get; set; }
        public int? VATId { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string ChangeNote { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string CreatedBy { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
    }
}