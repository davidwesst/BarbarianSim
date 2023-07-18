﻿using BarbarianSim.Config;
using BarbarianSim.EventHandlers;
using BarbarianSim.Events;
using FluentAssertions;
using Xunit;

namespace BarbarianSim.Tests.EventHandlers;

public class BarrierAppliedEventHandlerTests
{
    private readonly SimulationState _state = new SimulationState(new SimulationConfig());
    private readonly BarrierAppliedEventHandler _handler = new();

    [Fact]
    public void Adds_Barrier_To_Player()
    {
        var barrierAppliedEvent = new BarrierAppliedEvent(123.0, 1000, 10.0);

        _handler.ProcessEvent(barrierAppliedEvent, _state);

        _state.Player.Barriers.Should().Contain(barrierAppliedEvent.Barrier);
        _state.Player.Barriers.Should().HaveCount(1);
        barrierAppliedEvent.Barrier.Amount.Should().Be(1000);
    }

    [Fact]
    public void Creates_BarrierExpiredEvent()
    {
        var barrierAppliedEvent = new BarrierAppliedEvent(123.0, 1000, 10.0);

        _handler.ProcessEvent(barrierAppliedEvent, _state);

        _state.Events.Should().Contain(barrierAppliedEvent.BarrierExpiredEvent);
        _state.Events.Should().ContainSingle(e => e is BarrierExpiredEvent);
        barrierAppliedEvent.BarrierExpiredEvent.Timestamp.Should().Be(133.0);
        barrierAppliedEvent.BarrierExpiredEvent.Barrier.Should().Be(barrierAppliedEvent.Barrier);
    }
}