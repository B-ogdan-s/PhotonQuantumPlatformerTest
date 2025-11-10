using Quantum;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FinishEvent : QuantumMonoBehaviour
{
	[Inject] private PlayerGameData _playerGameData;


	private void OnEnable()
	{
		QuantumEvent.Subscribe(this, (EventOnFinish callback) => OnFinish(callback));
	}

	public void OnFinish(EventOnFinish callback)
	{
		if(_playerGameData.MapId >= _playerGameData.Mod.MapCount)
		{
			QuantumRunner.ShutdownAll();

			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenu");
			return;
		}

		Map map = _playerGameData.Mod.GetMap(_playerGameData.MapId);
		_playerGameData.MapId++;

		callback.Frame.Map = map;
	}
}
