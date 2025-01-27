﻿using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class BerserkingDamageCalculator
{
    public virtual double Calculate(SimulationState state) => state.Player.Auras.Contains(Aura.Berserking) ? 25.0 : 0.0;
}
