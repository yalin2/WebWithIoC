using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebWithIoC;
using WebWithIoC.Controllers;
using WebWithIoC.Models;

namespace WebWithIoC.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ResolveRegisteredTypeShouldSucceed()
        {
            MyIocContainer.MyIocContainer.Register<IInventoryManagable, InventoryManagableMock>(MyIocContainer.MyIocContainer.LifestyleType.Transient);
            IInventoryManagable inventoryService = MyIocContainer.MyIocContainer.Resolve<IInventoryManagable>();
            Assert.IsNotNull(inventoryService);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ResolveUnregisteredTypeShouldReturnInvalidOperationException()
        {
            var inventoryService = MyIocContainer.MyIocContainer.Resolve<String>();
        }
    }
}
