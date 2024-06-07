using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DLFeedBack360.Models
{
    public partial class LanguageDetail
    {
        [Key]
        [Column("LanguageID")]
        public int LanguageId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LanguageName { get; set; }
    }
}
