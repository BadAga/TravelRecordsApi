using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelRecordsAPI.Models
{
    [Table("Post")]
    public partial class Post
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int TripId { get; set; }
        public int StageId { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string ImageId { get; set; } = null!;
        [StringLength(200)]
        [Unicode(false)]
        public string Story { get; set; } = null!;
        public int? LocalizationId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }
    }
}
