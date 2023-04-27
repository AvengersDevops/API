﻿namespace AvengersAPI.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<TaskToUser> TaskToUsers { get; } = new List<TaskToUser>();
}
