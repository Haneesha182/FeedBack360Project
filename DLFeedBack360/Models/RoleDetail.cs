using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DLFeedBack360.Models
{
    public partial class RoleDetail
    {
        [Key]
        [Column("RoleID")]
        public int RoleId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RoleName { get; set; }
        [Column("RoleSequenceID")]
        public int RoleSequenceId { get; set; }
    }
}
