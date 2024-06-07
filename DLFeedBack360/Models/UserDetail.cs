using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DLFeedBack360.Models
{
    public partial class UserDetail
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("UserID")]
        [StringLength(50)]
        [Unicode(false)]
        public string UserId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Password { get; set; }
        [Column("EmpID")]
        public int EmpId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Mobile { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Country { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Language { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Role { get; set; }
    }
}
