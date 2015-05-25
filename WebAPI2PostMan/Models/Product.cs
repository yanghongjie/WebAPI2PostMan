using System;

namespace WebAPI2PostMan.Models
{
    /// <summary>
    ///     产品
    /// </summary>
    public class Product
    {
        /// <summary>
        ///     编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }
    }
}