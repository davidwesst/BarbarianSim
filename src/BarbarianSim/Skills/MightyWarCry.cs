﻿using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class MightyWarCry : IHandlesEvent<WarCryEvent>
{
    // War Cry grants you 15%[x] Base Life (15%[x] HP) as Fortify
    public const double FORTIFY = 0.15;

    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.MightyWarCry) && state.Config.Skills[Skill.MightyWarCry] > 0)
        {
            state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, "Mighty War Cry", FORTIFY * state.Player.BaseLife));
        }
    }
}
