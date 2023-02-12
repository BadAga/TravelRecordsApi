using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelRecordsAPI.Models
{
    [Table("Attraction")]
    public partial class Attraction
    {
        [Key]
        [Column("AttractionID")]
        public int AttractionId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string AttractionName { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string AttractionDesc { get; set; } = null!;
        public int? Score { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string? Popularity { get; set; }
    }
}
