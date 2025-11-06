using UnityEngine;
using Zenject;

public class MainMenuState_Main : MainMenuState_Base
{
	private UI_Manager _manager;
	private MainMenu_UI _mainMenu;

	[Inject]
	public MainMenuState_Main(MainMenuState_Data data) : base(data)
	{
		_manager = data.UIManager;
	}

	public override void Enter()
	{
		base.Enter();

		_mainMenu = _manager.GetPanel<MainMenu_UI>();
		_mainMenu.OnStartGame += () => _sm.ChangeState(typeof(MainMenuState_Lobby));


		_manager.ClearStack();
	}

	public override void Exit()
	{
		base.Exit();
		_mainMenu.OnStartGame = null;
		_mainMenu.OnOpenGameMode = null;
	}
}
