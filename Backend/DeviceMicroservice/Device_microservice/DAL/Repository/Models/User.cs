using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Models;

[Table("User")]
public partial class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
