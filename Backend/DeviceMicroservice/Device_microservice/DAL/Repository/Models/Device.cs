using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Models;

[Table("Device")]
public partial class Device
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("description")]
    [StringLength(40)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("address")]
    [StringLength(25)]
    [Unicode(false)]
    public string Address { get; set; } = null!;

    [Column("max_hourly_consumption")]
    public double MaxHourlyConsumption { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Devices")]
    public virtual User? User { get; set; }
}
