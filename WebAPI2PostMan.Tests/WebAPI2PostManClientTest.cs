using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI2PostMan.Client;
using WebAPI2PostMan.WebModel;

namespace WebAPI2PostMan.Tests
{
    [TestClass]
    public class WebApi2PostManClientTest
    {
        [TestMethod]
        public void GetAllProductTest()
        {
            var response = WebApi2PostManClient.GetAllProduct();
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void AddProductTest()
        {
            var request = new Product
            {
                Id = 999,
                Description = "UnitTest Decription",
                Name = "UnitTest Product",
                Price = 99
            };
            var response = WebApi2PostManClient.AddProduct(request);
            Assert.IsNotNull(response);
        }
    }
}
