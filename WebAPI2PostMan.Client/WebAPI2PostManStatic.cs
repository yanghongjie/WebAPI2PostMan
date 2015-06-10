using System.Configuration;

namespace WebAPI2PostMan.Client
{
    /// <summary>
    ///     WebApi2PostMan静态资源类
    /// </summary>
    public class WebApi2PostManStatic
    {
        /// <summary>
        ///     服务地址
        /// </summary>
        public static string ServiceUrl = ConfigurationManager.AppSettings["WebAPI2PostManServiceUrl"];
        /// <summary>
        ///     获取所有产品
        /// </summary>
        public static string RouteProductGetAll = "api/Product/All";
        /// <summary>
        ///     添加产品
        /// </summary>
        public static string RouteProductAdd = "api/Product/Add";
    }
}