using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Viewmodels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;

        public ProductController(IStoreRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string? category ,int productPage = 1) => View(new ProductsListViewModel
        {
            Products = repository.Products
                .OrderBy(p => p.ProductID)
                .Where(p => category == null || p.Category == category)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),

            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = repository.Products.Count()
            },
            CurrentCategory = category
        });
    }
}
