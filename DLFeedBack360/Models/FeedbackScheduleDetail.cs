using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DLFeedBack360.Models
{
    public partial class FeedbackScheduleDetail
    {
        [Key]
        [Column("FBSID")]
        public int Fbsid { get; set; }
        [Column("FBSUserID")]
        [StringLength(50)]
        [Unicode(false)]
        public string FbsuserId { get; set; }
        [Column("FBSCategoryID")]
        public int FbscategoryId { get; set; }
        [Column("FBSProviderUSerID")]
        [StringLength(50)]
        [Unicode(false)]
        public string FbsproviderUserId { get; set; }
        [Column("FBSLastDate", TypeName = "datetime")]
        public DateTime FbslastDate { get; set; }
        [Column("FBSIsActive")]
        public bool FbsisActive { get; set; }
    }
}
