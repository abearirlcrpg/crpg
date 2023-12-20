﻿using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace Crpg.Module.Modes.TrainingGround;

[DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromServer)]
internal sealed class CrpgUpdateTrainingGroundArenaType : GameNetworkMessage
{
    private static readonly CompressionInfo.Integer TroopTypeCompressionInfo = new((int)TroopType.Invalid, (int)TroopType.NumberOfTroopTypes);
    public TroopType PlayerTroopType { get; set; }

    protected override void OnWrite()
    {
        WriteIntToPacket((int)PlayerTroopType, TroopTypeCompressionInfo);
    }

    protected override bool OnRead()
    {
        bool bufferReadValid = true;
        PlayerTroopType = (TroopType)ReadIntFromPacket(TroopTypeCompressionInfo, ref bufferReadValid);
        return bufferReadValid;
    }

    protected override MultiplayerMessageFilter OnGetLogFilter()
    {
        return MultiplayerMessageFilter.Messaging;
    }

    protected override string OnGetLogFormat()
    {
        return "Crpg duel troop type message from server: " + PlayerTroopType;
    }
}
