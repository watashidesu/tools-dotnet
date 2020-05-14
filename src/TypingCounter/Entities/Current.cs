using Core.Utilities;
using MoreLinq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows.Forms;

namespace TypingCounter.Entities
{
    public partial class Current : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Keys Code { get; set; }

        public long Count { get; set; }

        /// <summary>
        /// 初期レコードを生成します。
        /// </summary>
        public static List<Current> CreateInitialKeyList()
        {
            return EnumUtil.GetEnums<Keys>().DistinctBy(code => (int)code).Select(code => new Current() { Code = code, Count = 0 }).ToList();
        }
    }
}