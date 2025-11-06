using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_UI : UI_Panel
{
	[SerializeField] private Button _startGameButton;
	[SerializeField] private Button _gameModeButton;

	public Action OnStartGame;
	public Action OnOpenGameMode;

	private void Awake()
	{
		_startGameButton.onClick.AddListener(() => OnStartGame?.Invoke());
		_gameModeButton.onClick.AddListener(() => OnOpenGameMode?.Invoke());
	}

	private void OnDestroy()
	{
		_startGameButton.onClick.RemoveAllListeners();
		_gameModeButton.onClick.RemoveAllListeners();
		OnStartGame = null;
		OnOpenGameMode = null;
	}
}
