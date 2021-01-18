using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LachlanBarclayNet.DAO.Standard
{
    public partial class PostType
    {
        public PostType()
        {
            Post = new HashSet<Post>();
        }

        [Key]
        [Column("PostTypeID")]
        public int PostTypeId { get; set; }
        [Required]
        [StringLength(255)]
        public string PostTypeName { get; set; }

        [InverseProperty("PostType")]
        public virtual ICollection<Post> Post { get; set; }
    }
}
