using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI2PostMan.Models;

namespace WebAPI2PostMan.Controllers
{
    /// <summary>
    ///     产品服务
    /// </summary>
    [RoutePrefix("Product")]
    public class ProductController : ApiController
    {
        private static readonly List<Product> Products = new List<Product>
        {
            new Product{Id = Guid.NewGuid(), Description = "产品描述",Name = "产品名称",Price = 123},
            new Product{Id = Guid.NewGuid(), Description = "产品描述",Name = "产品名称",Price = 124},
            new Product{Id = Guid.NewGuid(), Description = "产品描述",Name = "产品名称",Price = 125},
            new Product{Id = Guid.NewGuid(), Description = "产品描述",Name = "产品名称",Price = 126}
        };

        /// <summary>
        ///     获取所有产品
        /// </summary>
        [HttpGet, Route("All")]
        public IEnumerable<Product> Get()
        {
            return Products;
        }

        /// <summary>
        ///     获取产品
        /// </summary>
        /// <param name="id">产品编号</param>
        [HttpGet, Route("{id}")]
        public string Get(Guid id)
        {
            return "value";
        }

        /// <summary>
        ///     添加产品
        /// </summary>
        /// <param name="request">产品请求</param>
        [HttpPost, Route("")]
        public string Post(Product request)
        {
            Products.Add(request);
            return "ok";
        }
        /// <summary>
        ///     编辑产品
        /// </summary>
        /// <param name="id">产品编号</param>
        /// <param name="request">编辑后的产品</param>
        [HttpPut, Route("{id}")]
        public void Put(int id, Product request)
        {
        }
        /// <summary>
        ///     删除产品
        /// </summary>
        /// <param name="id">产品编号</param>
        [HttpDelete, Route("{id}")]
        public string Delete(Guid id)
        {
            var model = Products.FirstOrDefault(x => x.Id.Equals(id));
            Products.Remove(model);
            var result = string.Format("编号为{0}的产品删除成功！", id);
            return result;
        }
    }
}
