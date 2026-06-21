using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TempleERP.Models
{
    public class Items
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [StringLength(150)]
        public string ItemName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation Property
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
