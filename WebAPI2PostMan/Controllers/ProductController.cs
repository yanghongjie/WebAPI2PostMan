using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI2PostMan.Common.Attributes;
using WebAPI2PostMan.WebModel;

namespace WebAPI2PostMan.Controllers
{
    /// <summary>
    ///     产品服务
    /// </summary>
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        /// <summary>
        ///     获取所有产品
        /// </summary>
        [HttpGet, Route(""), Route("All")]
        public IEnumerable<Product> Get()
        {
            return _products;
        }
        /// <summary>
        ///     获取前几产品
        /// </summary>
        [HttpGet, Route("Top/{count:min(3):int=3}")]
        public IEnumerable<Product> GetTop(int count)
        {
            return _products.Take(3);
        }
        /// <summary>
        ///     获取产品
        /// </summary>
        /// <param name="id">产品编号</param>
        [HttpGet, Route("")]
        public Product Get(int id)
        {
            return _products.FirstOrDefault(x=>x.Id.Equals(id));
        }
        /// <summary>
        ///     添加产品
        /// </summary>
        /// <param name="request">产品请求</param>
        [HttpPost, Route(""), Route("Add")]
        public string Post(Product request)
        {
            _products.Add(request);
            return "ok";
        }
        /// <summary>
        ///     编辑产品
        /// </summary>
        /// <param name="id">产品编号</param>
        /// <param name="request">编辑后的产品</param>
        [HttpPut, Route(""), Route("{id}")]
        [ValidateModel]
        public string Put(int id, Product request)
        {
            var model = _products.FirstOrDefault(x => x.Id.Equals(id));
            if (model == null) return "未找到该产品";
            model.Name = request.Name;
            model.Price = request.Price;
            model.Description = request.Description;
            return "ok";
        }
        /// <summary>
        ///     删除产品
        /// </summary>
        /// <param name="id">产品编号</param>
        [HttpDelete, Route(""), Route("{id}")]
        public string Delete(int id)
        {
            var model = _products.FirstOrDefault(x => x.Id.Equals(id));
            _products.Remove(model);
            var result = string.Format("编号为{0}的产品删除成功！", id);
            return result;
        }



        /// <summary>
        ///     Mock数据
        /// </summary>
        /// <returns></returns>
        private static List<Product> LoadList()
        {
            var ran = new Random();
            var list = new List<Product>();
            for (var i = 1; i < 10; i++)
            {
                list.Add(new Product {Id = i, Description = i + " 号产品描述", Name = i + " 号产品名称", Price = ran.Next(100,88888)});
            }
            return list;
        }
        /// <summary>
        ///     构造函数
        /// </summary>
        public ProductController()
        {
            if (_products == null)
                _products = LoadList();
        }
        private static  List<Product> _products;
    }
}
