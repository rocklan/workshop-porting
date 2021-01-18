using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dummy.db.entity
{
    public partial class Post
    {
        public Post()
        {
            PostComment = new HashSet<PostComment>();
        }

        [Required]
        [StringLength(255)]
        public string PostDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PostDate { get; set; }
        public string PostText { get; set; }
        [Column("PostTypeID")]
        public int PostTypeId { get; set; }
        [Required]
        [StringLength(255)]
        public string PostTitle { get; set; }
        [Key]
        [Column("PostID")]
        public int PostId { get; set; }
        [StringLength(255)]
        public string PostUrl { get; set; }
        [Required]
        public bool? Published { get; set; }

        [ForeignKey(nameof(PostTypeId))]
        [InverseProperty("Post")]
        public virtual PostType PostType { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<PostComment> PostComment { get; set; }
    }
}
