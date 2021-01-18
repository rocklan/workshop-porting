namespace LachlanBarclayNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostComment")]
    public partial class PostComment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostCommentId { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        public DateTime PostCommentDate { get; set; }

        public string Comment { get; set; }

        public bool? IsVisible { get; set; }

        public int? PostID { get; set; }

        public virtual Post Post { get; set; }
    }
}
