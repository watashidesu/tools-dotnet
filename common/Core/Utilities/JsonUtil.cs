using Newtonsoft.Json;

namespace Core.Utilities
{
    public static class JsonUtil
    {
        /// <summary>
        /// オブジェクトを JSON 文字列に変換します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>JSON 文字列</returns>
        public static string Serialize(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.None);
            return json;
        }

        /// <summary>
        /// JSON 文字列をオブジェクトに変換します。
        /// </summary>
        /// <typeparam name="T">オブジェクト型</typeparam>
        /// <param name="json">JSON 文字列</param>
        /// <returns>オブジェクト</returns>
        public static T Deserialize<T>(string json)
        {
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}