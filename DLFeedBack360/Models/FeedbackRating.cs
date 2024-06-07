using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DLFeedBack360.Models
{
    public partial class FeedbackRating
    {
        [Key]
        [Column("FRID")]
        public int Frid { get; set; }
        [Column("ToID")]
        public int ToId { get; set; }
        [Column("ByID")]
        public int ById { get; set; }
        [Column("QID")]
        public int Qid { get; set; }
        public int Rating { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
