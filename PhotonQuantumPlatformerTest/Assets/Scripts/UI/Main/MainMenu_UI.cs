using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu_UI : UI_Panel
{
	[SerializeField] private Button _startGameButton;
	[SerializeField] private Button _gameModeButton;
	[SerializeField] private Button _charactersButton;
	[SerializeField] private TMP_InputField _inputField;
	[SerializeField] private CharacterPreviewPanel _previewPanel;
	public CharacterPreviewPanel PreviewPanel => _previewPanel;

	public Action OnStartGame;
	public Action OnOpenGameMode;
	public Action OnOpenCharacters;
	public Action<string> OnUpdateName;

	private void Awake()
	{
		_startGameButton.onClick.AddListener(() => OnStartGame?.Invoke());
		_gameModeButton.onClick.AddListener(() => OnOpenGameMode?.Invoke());
		_charactersButton.onClick.AddListener(() => OnOpenCharacters?.Invoke());

		_inputField.onEndEdit.AddListener((string value) => OnUpdateName?.Invoke(value));
	}

	private void OnDestroy()
	{
		_startGameButton.onClick.RemoveAllListeners();
		_gameModeButton.onClick.RemoveAllListeners();
		_charactersButton.onClick.RemoveAllListeners();

		_inputField.onEndEdit.RemoveAllListeners();
		OnStartGame = null;
		OnOpenGameMode = null;
	}

	public void SetName(string name)
	{
		_inputField.text = name;
	}
}
