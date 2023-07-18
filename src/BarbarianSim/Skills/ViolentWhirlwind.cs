﻿using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class ViolentWhirlwind : IHandlesEvent<WhirlwindSpinEvent>
{
    // After using Whirlwind for 2 seconds, Whirlwind deals 30%[x] increased damage until cancelled
    public const double DELAY = 2.0;
    public const double DAMAGE_MULTIPLIER = 1.3;

    public void ProcessEvent(WhirlwindSpinEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.ViolentWhirlwind))
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp + DELAY, 0, Aura.ViolentWhirlwind));
        }
    }
}
