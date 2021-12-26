﻿using Crpg.Domain.Common;
using Crpg.Domain.Entities.Heroes;

namespace Crpg.Domain.Entities.Battles;

/// <summary>
/// Application to join a <see cref="Battle"/> during the <see cref="BattlePhase.Preparation"/> phase.
/// </summary>
public class BattleFighterApplication : AuditableEntity
{
    public int Id { get; set; }
    public int BattleId { get; set; }
    public int HeroId { get; set; }

    /// <summary>The side the hero is applying to.</summary>
    public BattleSide Side { get; set; }
    public BattleFighterApplicationStatus Status { get; set; }

    public Battle? Battle { get; set; }
    public Hero? Hero { get; set; }
}
