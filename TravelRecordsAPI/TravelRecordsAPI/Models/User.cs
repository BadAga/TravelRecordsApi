using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelRecordsAPI.Models
{
    public partial class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string? ImageId { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Username { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [StringLength(200)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? Bio { get; set; }
    }
}
