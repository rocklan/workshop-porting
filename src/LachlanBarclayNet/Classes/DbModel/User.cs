namespace LachlanBarclayNet.DAO
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Users")]
    public partial class UserOld
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string QrCode { get; set; }
        public int Attempts { get; set; }
        public DateTime? LockedOutUntil { get; set; }
    }

}
