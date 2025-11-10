using Quantum;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameEvent : QuantumMonoBehaviour
{
	private void Awake()
	{
		QuantumEvent.Subscribe(listener: this, handler: (EventOnReadyPlayers e) => StartGame(e));
	}

	private async void StartGame(EventOnReadyPlayers e)
	{
		await SceneManager.UnloadSceneAsync("MainMenu");
	}
}
