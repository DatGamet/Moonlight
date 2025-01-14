﻿using Moonlight.App.Models.Misc;

namespace Moonlight.App.Database.Entities.LogsEntries;

public class AuditLogEntry
{
    public int Id { get; set; }
    public AuditLogType Type { get; set; }
    public string JsonData { get; set; } = "";
    public bool System { get; set; }
    public string Ip { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}