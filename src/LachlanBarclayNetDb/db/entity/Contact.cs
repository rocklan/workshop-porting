using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LachlanBarclayNet.DAO.Standard
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [Column("ContactID")]
        public int ContactId { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }
    }
}
