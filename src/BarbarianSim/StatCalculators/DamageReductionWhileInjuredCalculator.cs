﻿namespace BarbarianSim.StatCalculators;

public class DamageReductionWhileInjuredCalculator
{
    public DamageReductionWhileInjuredCalculator(MaxLifeCalculator maxLifeCalculator)
    {
        _maxLifeCalculator = maxLifeCalculator;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public virtual double Calculate(SimulationState state)
    {
        var damageReduction = state.Config.GetStatTotalMultiplied(g => 1 - (g.DamageReductionWhileInjured / 100.0));

        return state.Player.IsInjured(_maxLifeCalculator.Calculate(state)) ? damageReduction : 1;
    }
}
