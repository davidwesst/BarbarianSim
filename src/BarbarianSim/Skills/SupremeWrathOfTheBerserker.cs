﻿using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class SupremeWrathOfTheBerserker
{
    // While Wrath of the Berserker is active, every 50 Fury you spend increases Berserk's damage bonus by 25%[x]
    public const double DAMAGE_BONUS = 1.25;

    public virtual double GetBerserkDamageBonus(SimulationState state)
    {
        if (state.Player.Auras.Contains(Aura.WrathOfTheBerserker) &&
            state.Player.Auras.Contains(Aura.Berserking) &&
            state.Config.HasSkill(Skill.SupremeWrathOfTheBerserker))
        {
            var startTime = state.ProcessedEvents.OrderBy(e => e.Timestamp).Last(e => e is WrathOfTheBerserkerEvent).Timestamp;
            var totalFurySpent = state.ProcessedEvents.OfType<FurySpentEvent>().Where(e => e.Timestamp >= startTime).Sum(e => e.FurySpent);

            return Math.Pow(DAMAGE_BONUS, Math.Floor(totalFurySpent / 50));
        }

        return 1.0;
    }
}
