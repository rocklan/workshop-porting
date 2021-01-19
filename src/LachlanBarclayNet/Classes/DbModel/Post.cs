namespace LachlanBarclayNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class PostOld
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PostOld()
        {
            PostComments = new HashSet<PostComment>();
        }

        [Required]
        [StringLength(255)]
        public string PostDescription { get; set; }

        public DateTime PostDate { get; set; }

        public string PostText { get; set; }

        public int PostTypeID { get; set; }

        public bool Published { get; set; }

        [Required]
        [StringLength(255)]
        public string PostTitle { get; set; }

        public int PostID { get; set; }

        [StringLength(255)]
        public string PostUrl { get; set; }

        public virtual PostType PostType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostComment> PostComments { get; set; }
    }
}
