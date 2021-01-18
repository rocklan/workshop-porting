using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dummy.db.entity
{
    public partial class Users
    {
        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(255)]
        public string Password { get; set; }
        [StringLength(255)]
        public string QrCode { get; set; }
        public int Attempts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LockedOutUntil { get; set; }
        [StringLength(255)]
        public string UserEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        [StringLength(50)]
        public string EmailConfirmCode { get; set; }
        [StringLength(50)]
        public string PasswordForgotCode { get; set; }
        public bool IsQuizAdmin { get; set; }
        public bool DoNotEmail { get; set; }
        [StringLength(1000)]
        public string AboutMe { get; set; }
        [Column(TypeName = "decimal(9, 2)")]
        public decimal? NerdCred { get; set; }
        [Column(TypeName = "decimal(9, 2)")]
        public decimal? AvgScore { get; set; }
        [Column(TypeName = "decimal(9, 2)")]
        public decimal? QuizzesCompleted { get; set; }
        [Column("SpeakerID")]
        public int? SpeakerId { get; set; }
        [StringLength(32)]
        public string QrCodeTemp { get; set; }
    }
}
