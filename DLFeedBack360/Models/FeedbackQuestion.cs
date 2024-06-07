using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DLFeedBack360.Models
{
    public partial class FeedbackQuestion
    {
        [Key]
        [Column("QID")]
        public int Qid { get; set; }
        [Column("QDescription")]
        [StringLength(50)]
        [Unicode(false)]
        public string Qdescription { get; set; }
        public bool IsActive { get; set; }
    }
}
