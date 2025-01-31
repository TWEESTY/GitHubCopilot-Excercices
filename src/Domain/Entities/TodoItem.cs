﻿namespace Copilot.Domain.Entities;

public class TodoItem
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    public bool Done { get; set; }
}
