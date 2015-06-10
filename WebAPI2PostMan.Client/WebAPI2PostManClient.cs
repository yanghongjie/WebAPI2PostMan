using System.Collections.Generic;
using WebAPI2PostMan.WebModel;

namespace WebAPI2PostMan.Client
{
    /// <summary>
    ///     WebApi2PostMan 客户端
    /// </summary>
    public class WebApi2PostManClient
    {
        /// <summary>
        ///     获取所有产品
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns>产品列表</returns>
        public static IEnumerable<Product> GetAllProduct(int? timeout = 10)
        {
            return WebApiHelper.CallGetWebApi<IEnumerable<Product>>(WebApi2PostManStatic.RouteProductGetAll,WebApi2PostManStatic.ServiceUrl,timeout);
        }
        /// <summary>
        ///     添加产品
        /// </summary>
        /// <param name="request">添加的产品</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>添加结果</returns>
        public static string AddProduct(Product request,int? timeout = 10)
        {
            return WebApiHelper.CallPostWebApi<string, Product>(WebApi2PostManStatic.RouteProductAdd, request,WebApi2PostManStatic.ServiceUrl, timeout);
        }
    }
}