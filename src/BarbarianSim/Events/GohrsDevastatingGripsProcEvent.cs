﻿using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class GohrsDevastatingGripsProcEvent : EventInfo
{
    public GohrsDevastatingGripsProcEvent(double timestamp, double damage) : base(timestamp) => Damage = damage;

    public double Damage { get; init; }
    public IList<DamageEvent> DamageEvents { get; init; } = new List<DamageEvent>();

    public override void ProcessEvent(SimulationState state)
    {
        foreach (var enemy in state.Enemies)
        {
            var damageEvent = new DamageEvent(Timestamp, Damage, DamageType.Fire, DamageSource.GohrsDevastatingGrips, SkillType.None, enemy);
            DamageEvents.Add(damageEvent);
            state.Events.Add(damageEvent);
        }
    }
}
