﻿using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace BarbarianSim.Tests.StatCalculators;

public class VulnerableDamageBonusCalculatorTests
{
    private readonly Mock<TwoHandedAxeExpertise> _mockTwoHandedAxeExpertise = TestHelpers.CreateMock<TwoHandedAxeExpertise>();
    private readonly SimulationState _state = new(new SimulationConfig());
    private readonly VulnerableDamageBonusCalculator _calculator;

    public VulnerableDamageBonusCalculatorTests()
    {
        _mockTwoHandedAxeExpertise.Setup(m => m.GetVulnerableDamageMultiplier(It.IsAny<SimulationState>(), It.IsAny<GearItem>())).Returns(1.0);
        _calculator = new(_mockTwoHandedAxeExpertise.Object);
    }

    [Fact]
    public void Base_Vulnerable_Damage_Is_20()
    {
        _state.Enemies.First().Auras.Add(Aura.Vulnerable);

        var result = _calculator.Calculate(_state, _state.Enemies.First(), null);

        result.Should().Be(1.2);
    }

    [Fact]
    public void Includes_Gear_When_Enemy_Is_Vulnerable()
    {
        _state.Config.Gear.Helm.VulnerableDamage = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Vulnerable);

        var result = _calculator.Calculate(_state, _state.Enemies.First(), null);

        result.Should().BeApproximately(1.32, 0.000001);
    }

    [Fact]
    public void Includes_Paragon_When_Enemy_Is_Vulnerable()
    {
        _state.Config.Paragon.VulnerableDamage = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Vulnerable);

        var result = _calculator.Calculate(_state, _state.Enemies.First(), null);

        result.Should().BeApproximately(1.32, 0.000001);
    }

    [Fact]
    public void Includes_TwoHandedAxeExpertise_When_Enemy_Is_Vulnerable()
    {
        _mockTwoHandedAxeExpertise.Setup(m => m.GetVulnerableDamageMultiplier(_state, _state.Config.Gear.TwoHandSlashing)).Returns(1.15);
        _state.Config.Gear.Helm.VulnerableDamage = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Vulnerable);

        var result = _calculator.Calculate(_state, _state.Enemies.First(), _state.Config.Gear.TwoHandSlashing);

        result.Should().Be((1.2 + 0.12) * 1.15);
    }

    [Fact]
    public void Returns_1_When_Enemy_Is_Not_Vulnerable()
    {
        _state.Config.Gear.Helm.VulnerableDamage = 12.0;
        _state.Enemies.First().Auras.Add(Aura.Stun);

        var result = _calculator.Calculate(_state, _state.Enemies.First(), null);

        result.Should().Be(1.0);
    }
}
