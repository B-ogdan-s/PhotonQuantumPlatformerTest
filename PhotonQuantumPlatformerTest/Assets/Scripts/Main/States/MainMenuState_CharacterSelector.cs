using UnityEngine;
using Zenject;

public class MainMenuState_CharacterSelector : MainMenuState_Base
{
	private CharacterSelectorHandler _selectorHandler;
	private UI_Manager _manager;
	private CharacterSelector_UI _selector_UI;
	private CharacterMenu_UI _menu;
	private CharacterPreviewHandler _previewHandler;

	private PlayerGameData _gameData;

	[Inject]
	public MainMenuState_CharacterSelector(MainMenuState_Data data) : base(data)
	{
		_selectorHandler = data.CharacterSelectorHandler;
		_manager = data.UIManager;
		_selector_UI = _manager.GetPanel<CharacterSelector_UI>();
		_selector_UI.Initialize(_selectorHandler.Characterds);
		_previewHandler = data.CharacterPreviewHandler;
		_gameData = data.GameData;

		if(!_gameData.Player.PlayerAvatar.IsValid)
		{
			_selectorHandler.SetCurrentCharacter(_selectorHandler.Characterds[0]); 
			_previewHandler.ChangeCharacter(_selectorHandler.Characterds[0].PreviewModel);
			_gameData.Player.PlayerAvatar = _selectorHandler.Characterds[0].GamePrefab;
		}
		else
		{
			foreach(var c in _selectorHandler.Characterds)
			{
				if(c.GamePrefab == _gameData.Player.PlayerAvatar)
				{
					_selectorHandler.SetCurrentCharacter(c);
					_previewHandler.ChangeCharacter(c.PreviewModel);
					return;
				}
			}
		}
	}

	public override void Enter()
	{
		_manager.PushPanel<CharacterSelector_UI>();

		_selector_UI.OnClick += OpenCharactMenu;
		_selector_UI.OnClose += CloseMenu;
	}
	public override void Exit()
	{
		_selector_UI.OnClick -= OpenCharactMenu;
		_selector_UI.OnClose -= CloseMenu;
	}

	private void OpenCharactMenu(CharacterData data)
	{
		_menu = _manager.PushPanel<CharacterMenu_UI>();
		_menu.Initialize(data);
		_menu.OnBack += CloseCharactMenu;
		_menu.OnUsed += ChangeCharacter;

		_previewHandler.ChangeCharacter(data.PreviewModel);
		_menu.PreviewPanel.OnDragAction += _previewHandler.RotateCharacter;
	}

	private void CloseCharactMenu()
	{
		_manager.PopPanel();

		_menu.OnBack -= CloseCharactMenu;
		_menu.OnUsed -= ChangeCharacter;
		_menu.PreviewPanel.OnDragAction = null;
	}

	private void ChangeCharacter(CharacterData data)
	{
		CloseCharactMenu();

		_selectorHandler.SetCurrentCharacter(data);
		_gameData.Player.PlayerAvatar = data.GamePrefab;
		CloseMenu();
	}

	private void CloseMenu()
	{
		_previewHandler.ChangeCharacter(_selectorHandler.CurrentCharacter.PreviewModel);
		_sm.ChangeState(typeof(MainMenuState_Main));
	}

}
