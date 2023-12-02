using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MCMicroservice.Repository.Models;

[Table("Measurement")]
public partial class Measurement
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("timestamp")]
    public long Timestamp { get; set; }

    [Column("device_id")]
    public int DeviceId { get; set; }

    [Column("measurement_value")]
    public double MeasurementValue { get; set; }
}
