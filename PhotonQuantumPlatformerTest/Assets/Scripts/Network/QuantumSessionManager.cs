using Quantum;
using System;
using Zenject;
using Photon.Realtime;
using UnityEngine;
using System.Threading.Tasks;

public class QuantumSessionManager
{
    private readonly QuantumLobbyManager _lobbyManager = new();

	private QuantumGameHandler _gameHandler;
	private PlayerGameData _gameData;

	public Action OnStartSession;
	public Action OnFailedSession;
	public Action OnLeaveSession;

	public Action OnStartGame;

	public Action<int, int> OnChangePlayerCount;

	[Inject]
	public QuantumSessionManager(QuantumGameHandler gameHandler, PlayerGameData gameData)
	{
		_gameHandler = gameHandler;
		_lobbyManager.OnChangePlayerCount += ChangePlayerCount;
		_gameData = gameData;
	}
	public async void StartSessionAsync()
	{
		RealtimeClient  client = new RealtimeClient();
		client.AddCallbackTarget(_lobbyManager);
		_gameHandler.CreateUpdater(client);

		client = await _gameHandler.CreateClient(client);

		if (client == null || !client.InRoom)
		{
			_gameHandler.DestroyUpdater();
			OnFailedSession?.Invoke();
			return;
		}

		OnStartSession?.Invoke();
	}

	public async void LeaveSessionAsync()
	{
		RealtimeClient client = _gameHandler.Client;

		_gameHandler.DestroyUpdater();
		client?.RemoveCallbackTarget(_lobbyManager);
		await client?.DisconnectAsync();
		OnLeaveSession?.Invoke();
	}

	private void ChangePlayerCount()
	{
		Room room = _gameHandler.Client.CurrentRoom;

		int currentCount = room.PlayerCount;
		int maxCount = room.MaxPlayers;

		OnChangePlayerCount?.Invoke(currentCount, maxCount);

		if(currentCount == maxCount)
		{
			OnStartGame?.Invoke();
			CreateGame();
		}
	}

	private async Task CreateGame()
	{
		_gameData.MapId = 0;
		_gameHandler.DestroyUpdater();
		await _gameHandler.CreateGame();
	}
}
