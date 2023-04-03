﻿namespace Moonlight.App.Database.Entities;

public class Subscription
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string LimitsJson { get; set; } = "";
}