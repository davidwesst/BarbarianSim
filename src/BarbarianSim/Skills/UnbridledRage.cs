﻿using BarbarianSim.Enums;

namespace BarbarianSim.Skills;

public class UnbridledRage
{
    // Core skills deal 135%[x] increased damage, but cost 100%[x] more Fury

    public virtual double GetFuryCostReduction(SimulationState state, SkillType skillType)
    {
        if (state.Config.HasSkill(Skill.UnbridledRage))
        {
            if (skillType == SkillType.Core)
            {
                return 2.0;
            }
        }

        return 1.0;
    }

    public virtual double GetDamageBonus(SimulationState state, SkillType skillType)
    {
        if (state.Config.HasSkill(Skill.UnbridledRage))
        {
            if (skillType == SkillType.Core)
            {
                return 2.0;
            }
        }

        return 1.0;
    }
}
