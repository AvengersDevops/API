using System;
using System.Collections;
using System.Collections.Generic;

namespace AvengersAPI.Entities;

public partial class Task
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public DateTime? DueDate { get; set; }

    public BitArray? Done { get; set; }

    public string? Description { get; set; }
}
