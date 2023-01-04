using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelRecordsAPI.Models
{
    [Table("Stage")]
    public partial class Stage
    {
        [Key]
        public int StageId { get; set; }
        public int UserId { get; set; }
        public int TripId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Title { get; set; } = null!;
        [StringLength(150)]
        [Unicode(false)]
        public string TripDesc { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }
    }
}
