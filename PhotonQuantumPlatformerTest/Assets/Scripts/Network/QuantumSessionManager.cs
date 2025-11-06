using Quantum;
using System;
using Zenject;
using Photon.Realtime;
using UnityEngine;
using Photon.Deterministic;

public class QuantumSessionManager
{
    private readonly QuantumLobbyManager _lobbyManager = new();
	private readonly QuantumSceneLoader _sceneLoader = new();

	private MenuConnectArgs _connectArgs;
	private RuntimePlayer _player;

	private RealtimeClient _client;
	private QuantumRunner _runner;
	private QuantumClientUpdater _updater;

	public Action OnStartSession;
	public Action OnFailedSession;
	public Action OnLeaveSession;

	public Action OnStartGame;

	public Action<int, int> OnChangePlayerCount;

	[Inject]
	public QuantumSessionManager(MenuConnectArgs connectArgs, RuntimePlayer player)
	{
		_connectArgs = connectArgs;
		_player = player;
		_lobbyManager.OnChangePlayerCount += ChangePlayerCount;
	}

	public async void StartSessionAsync()
	{
		var args = new MatchmakingArguments
		{
			PhotonSettings = PhotonServerSettings.Global.AppSettings,
			RoomName = null,
			CanOnlyJoin = false,
			MaxPlayers = _connectArgs.MaxPLayers,
			PluginName = "QuantumPlugin",
			UserId = Guid.NewGuid().ToString(),
		};
		_client = new RealtimeClient();
		_client.AddCallbackTarget(_lobbyManager);
		CreateUpdater();

		_client = await MatchmakingExtensions.ConnectToRoomAsync(_client, args);

		if (_client == null || !_client.InRoom)
		{
			DestroyUpdater();
			OnFailedSession?.Invoke();
			return;
		}

		_client.AddCallbackTarget(_lobbyManager);

		OnStartSession?.Invoke();
	}

	public async void LeaveSessionAsync()
	{
		DestroyUpdater();
		_client?.RemoveCallbackTarget(_lobbyManager);
		await _client?.DisconnectAsync();
		OnLeaveSession?.Invoke();
	}

	private void ChangePlayerCount()
	{
		int currentCount = _client.CurrentRoom.PlayerCount;
		int maxCount = _client.CurrentRoom.MaxPlayers;

		OnChangePlayerCount?.Invoke(currentCount, maxCount);

		if(currentCount == maxCount)
		{
			OnStartGame?.Invoke();
			CreateGame();
		}
	}

	private async void CreateGame()
	{
		DestroyUpdater();

		await _sceneLoader.LoadScene(_connectArgs.Config, _client);

		SessionRunner.Arguments arg = new()
		{
			RunnerFactory = QuantumRunnerUnityFactory.DefaultFactory,
			GameParameters = QuantumRunnerUnityFactory.CreateGameParameters,
			ClientId = _client.UserId,
			RuntimeConfig = _connectArgs.Config,
			SessionConfig = QuantumDeterministicSessionConfigAsset.DefaultConfig,
			GameMode = DeterministicGameMode.Multiplayer,
			PlayerCount = _connectArgs.MaxPLayers,
			StartGameTimeoutInSeconds = 10,
			Communicator = new QuantumNetworkCommunicator(_client),
		};

		_runner = await QuantumRunner.StartGameAsync(arg);

		_runner.Game.AddPlayer(_player);

		_sceneLoader.UnloadOldScene();
	}

	private void CreateUpdater()
	{
		var go = new GameObject("QuantumClientUpdater");
		_updater = go.AddComponent<QuantumClientUpdater>();
		_updater.Client = _client;
	}
	private void DestroyUpdater()
	{
		GameObject.Destroy(_updater.gameObject);
		_updater = null;
	}
}
