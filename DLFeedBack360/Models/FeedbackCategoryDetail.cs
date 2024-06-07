using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DLFeedBack360.Models
{
    public partial class FeedbackCategoryDetail
    {
        [Key]
        [Column("FBCDCategoryID")]
        public int FbcdcategoryId { get; set; }
        [Column("FBCDDescription")]
        [StringLength(50)]
        [Unicode(false)]
        public string Fbcddescription { get; set; }
        [Column("FBCDCreatedate", TypeName = "datetime")]
        public DateTime Fbcdcreatedate { get; set; }
    }
}
