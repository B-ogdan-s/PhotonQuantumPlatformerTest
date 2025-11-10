using UnityEditor;
using UnityEngine;
using Zenject;

public class MainMenuState_Main : MainMenuState_Base
{
	private CharacterPreviewHandler _previewHandler;
	private UI_Manager _manager;
	private MainMenu_UI _mainMenu;

	private PlayerGameData _playerGameData;

	[Inject]
	public MainMenuState_Main(MainMenuState_Data data) : base(data)
	{
		_manager = data.UIManager;
		_previewHandler = data.CharacterPreviewHandler;
		_playerGameData = data.GameData;

		_mainMenu = _manager.GetPanel<MainMenu_UI>();

		if (string.IsNullOrEmpty(_playerGameData.Player.PlayerNickname))
		{
			string newName = $"Player_{Random.Range(0, 10000).ToString()}";
			_mainMenu.SetName(newName);
			_playerGameData.Player.PlayerNickname = newName;
		}
		else
		{
			_mainMenu.SetName(_playerGameData.Player.PlayerNickname);
		}
	}

	public override void Enter()
	{
		base.Enter();

		_mainMenu = _manager.GetPanel<MainMenu_UI>();
		_mainMenu.OnStartGame += () => _sm.ChangeState(typeof(MainMenuState_Lobby));
		_mainMenu.OnOpenCharacters += () => _sm.ChangeState(typeof(MainMenuState_CharacterSelector));
		_mainMenu.PreviewPanel.OnDragAction += _previewHandler.RotateCharacter;


		_manager.ClearStack();
	}

	public override void Exit()
	{
		base.Exit();
		_mainMenu.OnStartGame = null;
		_mainMenu.OnOpenGameMode = null;
	}

	public void UpdateName(string name)
	{
		_playerGameData.Player.PlayerNickname = name;
	}
}
