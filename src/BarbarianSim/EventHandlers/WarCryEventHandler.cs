﻿using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.Skills;

namespace BarbarianSim.EventHandlers;

public class WarCryEventHandler : EventHandler<WarCryEvent>
{
    public WarCryEventHandler(BoomingVoice boomingVoice) => _boomingVoice = boomingVoice;

    private readonly BoomingVoice _boomingVoice;

    public override void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        e.Duration = WarCry.DURATION * _boomingVoice.GetDurationIncrease(state);

        e.WarCryAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, e.Duration, Aura.WarCry);
        state.Events.Add(e.WarCryAuraAppliedEvent);

        e.WarCryCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, WarCry.COOLDOWN, Aura.WarCryCooldown);
        state.Events.Add(e.WarCryCooldownAuraAppliedEvent);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedWarCry))
        {
            e.BerserkingAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, WarCry.BERSERKING_DURATION_FROM_ENHANCED, Aura.Berserking);
            state.Events.Add(e.BerserkingAuraAppliedEvent);
        }

        if (state.Config.Skills.ContainsKey(Skill.MightyWarCry))
        {
            e.FortifyGeneratedEvent = new FortifyGeneratedEvent(e.Timestamp, WarCry.FORTIFY_FROM_MIGHTY * state.Player.BaseLife);
            state.Events.Add(e.FortifyGeneratedEvent);
        }
    }
}