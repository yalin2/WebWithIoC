using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWithIoC.Models;

namespace WebWithIoC.Controllers
{
    public class HomeController : Controller
    {
        private IInventoryManagable inventoryService;
        private IDataDbContext context;
        public HomeController()
        {
            MyIocContainer.MyIocContainer.Register<IInventoryManagable, InventoryManagableMock>(MyIocContainer.MyIocContainer.LifestyleType.Singleton);
            MyIocContainer.MyIocContainer.Register<IDataDbContext, DataDbContext>(MyIocContainer.MyIocContainer.LifestyleType.Singleton);
            //MyIocContainer.MyIocContainer.Register<IDataDbContext, MockDataDbContext>(MyIocContainer.MyIocContainer.LifestyleType.Singleton);
            inventoryService = MyIocContainer.MyIocContainer.Resolve<IInventoryManagable>();
            context = MyIocContainer.MyIocContainer.Resolve<IDataDbContext>();
        }
        public ActionResult Index()
        {
            var customers = context.Customers.ToList();
            var products = context.Products.ToList();
            var orders = context.Orders.ToList();
            var viewModel = new HomeIndexViewModel()
            {
                Customers = customers,
                Orders = orders,
                Products = products
            };
            return View(viewModel);
        }
        
        public int GetInventory(int id)
        {
            return inventoryService.GetInvetory(id);
        }
        public int SetInventory(int id, int count)
        {
            return inventoryService.SetInventory(id, count);
        }
        public int UseInventory(int id, int count)
        {
            return inventoryService.UseInventory(id, count);
        }
    }
}