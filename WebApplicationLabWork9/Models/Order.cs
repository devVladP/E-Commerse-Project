using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLabWork9.Models
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("AppUser")]
        public Guid UserId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}
