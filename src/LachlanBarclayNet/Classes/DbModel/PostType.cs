namespace LachlanBarclayNet.DAO
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostType")]
    public partial class PostType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PostType()
        {
            Posts = new HashSet<PostOld>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostTypeID { get; set; }

        [Required]
        [StringLength(255)]
        public string PostTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostOld> Posts { get; set; }

    }
}
