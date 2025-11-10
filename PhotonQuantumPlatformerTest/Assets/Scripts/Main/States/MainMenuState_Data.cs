using Zenject;

public class MainMenuState_Data
{
	private CharacterPreviewHandler _characterPreviewHandler;
	private CharacterSelectorHandler _characterSelectorHandler;
    private StateMachine<MainMenuState_Base> _stateMachine;
	private UI_Manager _uiManager;
	private QuantumSessionManager _sessionManager;
	private PlayerGameData _gameData;

	public StateMachine<MainMenuState_Base> StateMachine => _stateMachine;
	public CharacterPreviewHandler CharacterPreviewHandler => _characterPreviewHandler;
	public CharacterSelectorHandler CharacterSelectorHandler => _characterSelectorHandler;
	public UI_Manager UIManager => _uiManager;
	public QuantumSessionManager SessionManager => _sessionManager;
	public PlayerGameData GameData => _gameData;

	[Inject]
	public MainMenuState_Data(StateMachine<MainMenuState_Base> stateMachine,
		CharacterPreviewHandler characterPreviewHandler,
		CharacterSelectorHandler characterSelectorHandler,
		UI_Manager ui_Manager,
		QuantumSessionManager sessionManager,
		PlayerGameData gameData)
	{
		_stateMachine = stateMachine;
		_characterPreviewHandler = characterPreviewHandler;
		_characterSelectorHandler = characterSelectorHandler;
		_uiManager = ui_Manager;
		_sessionManager = sessionManager;
		_gameData = gameData;
	}
}
