﻿namespace BarbarianSim;

public class EnemyState
{
    public int Life { get; set; }
    public int MaxLife { get; set; }
    public ISet<Aura> Auras { get; init; } = new HashSet<Aura>();

    public bool IsSlowed() => Auras.Contains(Aura.Slow);

    public bool IsCrowdControlled() => Auras.Any(a => a is Aura.Chill or Aura.Daze or Aura.Fear or Aura.Freeze or Aura.Immobilize or Aura.Knockback or Aura.Knockdown or Aura.Slow or Aura.Stagger or Aura.Stun or Aura.Taunt or Aura.Tether);

    public bool IsInjured() => ((double)Life / MaxLife) <= 0.35;

    public bool IsVulnerable() => Auras.Contains(Aura.Vulnerable);
}
