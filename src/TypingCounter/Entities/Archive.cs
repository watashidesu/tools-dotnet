using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Forms;

namespace TypingCounter.Entities
{
    public partial class Archive : EntityBase
    {
        [Key]
        [ForeignKey(nameof(ArchiveDate))]
        public long ArchiveId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Keys Code { get; set; }

        public long Count { get; set; }

        public virtual ArchiveDate ArchiveDate { get; set; }
    }
}