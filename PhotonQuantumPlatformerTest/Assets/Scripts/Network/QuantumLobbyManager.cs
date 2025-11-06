using Photon.Realtime;
using Quantum;
using UnityEngine;
using System;

public class QuantumLobbyManager : QuantumNetworkCallbacks
{
	public Action OnChangePlayerCount;

	public override void OnJoinedRoom()
	{
		OnChangePlayerCount?.Invoke();
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		OnChangePlayerCount?.Invoke();
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		OnChangePlayerCount?.Invoke();
	}
}
