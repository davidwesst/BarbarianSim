﻿namespace BarbarianSim.StatCalculators;

public class DamageToCrowdControlledCalculator
{
    public virtual double Calculate(SimulationState state, EnemyState enemy) => enemy.IsCrowdControlled() ? state.Config.Gear.GetStatTotal(g => g.DamageToCrowdControlled) : 0.0;
}
