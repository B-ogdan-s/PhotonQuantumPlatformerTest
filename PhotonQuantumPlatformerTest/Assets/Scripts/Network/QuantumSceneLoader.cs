using Photon.Realtime;
using Quantum;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuantumSceneLoader : QuantumNetworkCallbacks
{
	private Scene _oldScene;
	
    public async Task LoadScene(RuntimeConfig config, RealtimeClient client)
    {
		_oldScene = SceneManager.GetActiveScene();

		var preloadMap = false;
		if (config != null
		  && config.Map.Id.IsValid
		  && config.SimulationConfig.Id.IsValid)
		{
			if (QuantumUnityDB.TryGetGlobalAsset(config.SimulationConfig, out Quantum.SimulationConfig simulationConfigAsset))
			{
				preloadMap = simulationConfigAsset.AutoLoadSceneFromMap == SimulationConfig.AutoLoadSceneFromMapMode.Disabled;
			}
		}

		if (preloadMap)
		{
			if (QuantumUnityDB.TryGetGlobalAsset(config.Map, out Quantum.Map map))
			{
				using (new ConnectionServiceScope(client))
				{
					await SceneManager.LoadSceneAsync(map.Scene, LoadSceneMode.Additive);
					SceneManager.SetActiveScene(SceneManager.GetSceneByName(map.Scene));
				}
			}
		}
	}

	public async void UnloadOldScene()
	{
		await SceneManager.UnloadSceneAsync(_oldScene);
	}
}
