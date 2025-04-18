﻿namespace NorthwindApp.Domain;

public class Role : BaseEntity
{
    public string Abrv { get; set; }
    public string Name { get; set; }
    public ICollection<User> Users { get; set; }
}