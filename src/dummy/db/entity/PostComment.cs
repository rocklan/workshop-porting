using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dummy.db.entity
{
    public partial class PostComment
    {
        [Key]
        public int PostCommentId { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PostCommentDate { get; set; }
        public string Comment { get; set; }
        public bool? IsVisible { get; set; }
        [Column("PostID")]
        public int? PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        [InverseProperty("PostComment")]
        public virtual Post Post { get; set; }
    }
}
