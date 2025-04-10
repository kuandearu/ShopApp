using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models
{
    public class Order
    {
        [Key] 
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        public OrderStatus Status { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Note cannot exceed 1000 characters.")]
        public string? Note { get; set; }

        public decimal Total { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
