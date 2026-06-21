using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempleERP.Models
{
    public class PoojaMaster
    {
        [Key]
        public int PoojaId { get; set; }

        [Required]
        [StringLength(150)]
        public string PoojaName { get; set; }

        [StringLength(150)]
        public string MalayalamName { get; set; }

        [StringLength(50)]
        public string PoojaCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductCost { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriestDonation { get; set; } = 0;

        public int MaxNos { get; set; } = 0;

        public int? CategoryId { get; set; }

        public bool IsSingle { get; set; } = false;

        public bool PrintReceiptSeparate { get; set; } = false;

        [StringLength(150)]
        public string EditablePoojaName { get; set; }

        [StringLength(150)]
        public string AccountHead { get; set; }

        [StringLength(150)]
        public string PrintCategory { get; set; }

        public bool IsRateEditable { get; set; } = false;

        public bool IsCoupon { get; set; } = false;

        public bool IsOnlyQty { get; set; } = false;

        public bool SmsRequired { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Property
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
