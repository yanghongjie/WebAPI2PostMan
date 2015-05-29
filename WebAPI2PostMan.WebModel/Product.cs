using System.ComponentModel.DataAnnotations;

namespace WebAPI2PostMan.WebModel
{
    /// <summary>
    ///     产品
    /// </summary>
    public class Product
    {
        /// <summary>
        ///     编号
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        ///     名称
        /// </summary>
        [Required, MaxLength(36)]
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