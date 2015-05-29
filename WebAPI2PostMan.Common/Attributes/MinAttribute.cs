using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI2PostMan.Common.Attributes
{
    /// <summary>
    ///     最小值特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinAttribute : ValidationAttribute
    {
        /// <summary>
        ///     最小值
        /// </summary>
        public int MinimumValue { get; set; }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="minimun"></param>
        public MinAttribute(int minimun)
        {
            MinimumValue = minimun;
        }

        /// <summary>
        ///     验证逻辑
        /// </summary>
        /// <param name="value">需验证的值</param>
        /// <returns>是否通过验证</returns>
        public override bool IsValid(object value)
        {
            int intValue;
            if (value != null && int.TryParse(value.ToString(), out intValue))
            {
                return (intValue >= MinimumValue);
            }
            return false;
        }

        /// <summary>
        ///     格式化错误信息
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns>错误信息</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0} 最小值为 {1}", name, MinimumValue);
        }
    }
}