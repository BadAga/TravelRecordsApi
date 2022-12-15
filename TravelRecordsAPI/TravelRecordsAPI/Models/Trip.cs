using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelRecordsAPI.Models
{
    [Table("Trip")]
    public partial class Trip
    {
        [Key]
        public int TripId { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string? ImageId { get; set; }
        public int UserId { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? TripDesc { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Title { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }
    }
}
