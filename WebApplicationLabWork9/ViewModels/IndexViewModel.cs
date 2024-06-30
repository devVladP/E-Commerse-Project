using WebApplicationLabWork9.Data;
using WebApplicationLabWork9.Models;

namespace WebApplicationLabWork9.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(PaginatedList list, IEnumerable<Product> p, string Id) 
        {
            products = p.ToList();
            UserId = Id;
            productsList = list;
        }
        public List<Product> products { get; set; }
        public string UserId { get; set; }
        public string filterString { get; set; } = string.Empty;

        public PaginatedList productsList { get; set; }
    }
}
