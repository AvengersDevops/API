using System;
using System.Collections.Generic;

namespace AvengersAPI.Entities;

public partial class Task
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public bool Done { get; set; }
}
