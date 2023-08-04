﻿namespace BarbarianSim.Events;

public class RaidLeaderProcEvent : EventInfo
{
    public RaidLeaderProcEvent(double timestamp, double duration) : base(timestamp, null) => Duration = duration;

    public double Duration { get; set; }
    public IList<HealingEvent> HealingEvents { get; init; } = new List<HealingEvent>();

    public override string ToString() => $"{base.ToString()} - Healing for X% of max life for {Duration:F2} seconds";
}
