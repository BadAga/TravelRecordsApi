using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelRecordsAPI.Models
{
    [Table("HasAttraction")]
    public partial class HasAttraction
    {
        [Key]
        [Column("AttractionID")]
        public int AttractionId { get; set; }
        [Key]
        [Column("StageID")]
        public int StageId { get; set; }
    }
}
