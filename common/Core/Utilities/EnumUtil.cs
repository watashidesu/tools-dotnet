using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Core.Utilities
{
    public static class EnumUtil
    {
        /// <summary>
        /// enum を List に変換します
        /// </summary>
        /// <typeparam name="T">enum class</typeparam>
        /// <returns>enum list</returns>
        public static List<T> GetEnums<T>()
            where T : IComparable, IFormattable, IConvertible
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 列挙子名から enum を取得します
        /// </summary>
        /// <typeparam name="T">enum 型</typeparam>
        /// <param name="propName">列挙子名</param>
        /// <returns>enum</returns>
        public static T GetEnum<T>(string propName)
            where T : IComparable, IFormattable, IConvertible
        {
            return (T)Enum.Parse(typeof(T), propName, true);
        }

        /// <summary>
        /// int 型から enum を取得します
        /// 未定義の数値が渡された場合は0が返却されます
        /// </summary>
        /// <typeparam name="T">enum 型</typeparam>
        /// <param name="intVal">数値</param>
        /// <returns>enum</returns>
        public static T GetEnum<T>(int? intVal)
            where T : IComparable, IFormattable, IConvertible
        {
            return Enum.IsDefined(typeof(T), intVal) ? (T)Enum.ToObject(typeof(T), intVal) : default(T);
        }

        /// <summary>
        /// enum の description 属性値を取得します
        /// </summary>
        /// <typeparam name="T">enum 型</typeparam>
        /// <param name="val">enum</param>
        /// <returns>description</returns>
        public static string GetDescription<T>(this T val)
            where T : IComparable, IFormattable, IConvertible
        {
            FieldInfo fieldInfo = val.GetType().GetField(val.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return val.ToString();
            }
        }

        /// <summary>
        /// 特定の属性を取得する
        /// </summary>
        /// <typeparam name="TAttribute">属性型</typeparam>
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            //リフレクションを用いて列挙体の型から情報を取得
            var fieldInfo = value.GetType().GetField(value.ToString());
            //指定した属性のリスト
            var attributes
                = fieldInfo.GetCustomAttributes(typeof(TAttribute), false)
                .Cast<TAttribute>();
            //属性がなかった場合、空を返す
            if ((attributes?.Count() ?? 0) <= 0)
                return null;
            //同じ属性が複数含まれていても、最初のみ返す
            return attributes.First();
        }
    }
}