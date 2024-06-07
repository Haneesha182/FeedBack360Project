using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DLFeedBack360.Models
{
    public partial class CountryDetail
    {
        [Key]
        [Column("CountryID")]
        public int CountryId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CountryName { get; set; }
    }
}
