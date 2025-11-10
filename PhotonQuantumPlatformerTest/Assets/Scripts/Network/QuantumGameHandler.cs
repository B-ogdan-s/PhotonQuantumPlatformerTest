using Photon.Deterministic;
using Photon.Realtime;
using Quantum;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class QuantumGameHandler
{
	private AssetRef<SimulationConfig> _simulationConfig;
	private AssetRef<SystemsConfig> _systemsConfig;

	//private QuantumRunner _runner;
	private RealtimeClient _client;

	private QuantumClientUpdater _updater;
	private PlayerGameData _data;

	//public QuantumRunner Runner => _runner;
	public RealtimeClient Client => _client;

	[Inject]
	public QuantumGameHandler(PlayerGameData data, GameModeData Mod, 
		AssetRef<SimulationConfig> simulationConfig,
		AssetRef<SystemsConfig> systemsConfig)
	{
		_data = data;
		_data.Mod = Mod;
		_simulationConfig = simulationConfig;
		_systemsConfig = systemsConfig;
	}


	public async Task<RealtimeClient> CreateClient(RealtimeClient client)
	{
		if(client == null)
			client = new RealtimeClient();

		_client = client;

		var args = new MatchmakingArguments
		{
			PhotonSettings = PhotonServerSettings.Global.AppSettings,
			RoomName = null,
			CanOnlyJoin = false,
			MaxPlayers = _data.Mod.MaxPlayers,
			PluginName = "QuantumPlugin",
			UserId = Guid.NewGuid().ToString(),
		};
		_client = await MatchmakingExtensions.ConnectToRoomAsync(_client, args);

		return _client;
	}

	public async Task CreateGame()
	{
		Map map = _data.Mod.GetMap(_data.MapId);
		_data.MapId++;
		RuntimeConfig config = new();

		config.SimulationConfig = _simulationConfig;
		config.SystemsConfig = _systemsConfig;
		config.Map = map;

		SessionRunner.Arguments arg = new()
		{
			RunnerFactory = QuantumRunnerUnityFactory.DefaultFactory,
			GameParameters = QuantumRunnerUnityFactory.CreateGameParameters,
			ClientId = _client.UserId,
			RuntimeConfig = config,
			SessionConfig = QuantumDeterministicSessionConfigAsset.DefaultConfig,
			GameMode = DeterministicGameMode.Multiplayer,
			PlayerCount = _data.Mod.MaxPlayers,
			StartGameTimeoutInSeconds = 10,
			Communicator = new QuantumNetworkCommunicator(_client),
		};

		var Runner = await QuantumRunner.StartGameAsync(arg);

		Runner.Game.AddPlayer(_data.Player);

		//var scene = SceneManager.GetSceneByName("MainMenu");
		//if (scene.IsValid() && scene.isLoaded)
		//{
		//	Test.Instance.Print("Scene Maby Unload?");
		//	await SceneManager.UnloadSceneAsync(scene);
		//}
		//else
		//{
		//	Test.Instance.Print("Scene Is Null");
		//	Debug.LogWarning("MainMenu scene not loaded or invalid in build");
		//}

		//await SceneManager.UnloadSceneAsync("MainMenu");

	}
	public void CreateUpdater(RealtimeClient client)
	{
		var go = new GameObject("QuantumClientUpdater");
		_updater = go.AddComponent<QuantumClientUpdater>();
		_updater.Client = client;
	}

	public void DestroyUpdater()
	{
		GameObject.Destroy(_updater.gameObject);
		_updater = null;
	}
}
