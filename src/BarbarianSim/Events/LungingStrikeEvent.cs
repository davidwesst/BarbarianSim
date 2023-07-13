﻿using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class LungingStrikeEvent : EventInfo
{
    public LungingStrikeEvent(double timestamp, EnemyState target) : base(timestamp)
    {
        Target = target;
    }

    public EnemyState Target { get; init; }
    public DamageEvent DamageEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public AuraAppliedEvent BerserkingAuraAppliedEvent { get; set; }
    public BleedAppliedEvent BleedAppliedEvent { get; set; }
    public LuckyHitEvent LuckyHitEvent { get; set; }
    public AuraAppliedEvent WeaponCooldownAuraAppliedEvent { get; set; }

    private const double FURY_GENERATED = 10.0;

    public override void ProcessEvent(SimulationState state)
    {
        var weaponDamage = (LungingStrike.Weapon.MinDamage + LungingStrike.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = LungingStrike.GetSkillMultiplier(state);
        var damageMultiplier = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, Target, SkillType.Basic, DamageSource.LungingStrike);

        var damage = weaponDamage * skillMultiplier * damageMultiplier;
        var damageType = DamageType.Direct;

        var critChance = CritChanceCalculator.Calculate(state, DamageType.Physical);
        var critRoll = RandomGenerator.Roll(RollType.CriticalStrike);

        if (critRoll <= critChance)
        {
            damage *= CritDamageCalculator.Calculate(state, LungingStrike.Weapon.Expertise);
            damageType = DamageType.DirectCrit;

            if (state.Config.Skills.ContainsKey(Skill.CombatLungingStrike))
            {
                BerserkingAuraAppliedEvent = new AuraAppliedEvent(Timestamp, 1.5, Aura.Berserking);
                state.Events.Add(BerserkingAuraAppliedEvent);
            }
        }

        DamageEvent = new DamageEvent(Timestamp, damage, damageType, DamageSource.LungingStrike, SkillType.Basic, Target);
        state.Events.Add(DamageEvent);

        FuryGeneratedEvent = new FuryGeneratedEvent(Timestamp, FURY_GENERATED);
        state.Events.Add(FuryGeneratedEvent);

        var weaponSpeed = 1 / LungingStrike.Weapon.AttacksPerSecond;
        weaponSpeed *= AttackSpeedCalculator.Calculate(state);
        WeaponCooldownAuraAppliedEvent = new AuraAppliedEvent(Timestamp, weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(WeaponCooldownAuraAppliedEvent);

        var luckyRoll = RandomGenerator.Roll(RollType.LuckyHit);

        if (luckyRoll <= (LungingStrike.LUCKY_HIT_CHANCE + LuckyHitChanceCalculator.Calculate(state)))
        {
            LuckyHitEvent = new LuckyHitEvent(Timestamp, SkillType.Basic, Target);
            state.Events.Add(LuckyHitEvent);
        }
    }
}
