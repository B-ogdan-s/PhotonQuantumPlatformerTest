using Zenject;

public class MainMenuState_Data
{
    private StateMachine<MainMenuState_Base> _stateMachine;
	private UI_Manager _uiManager;
	private QuantumSessionManager _sessionManager;

	public StateMachine<MainMenuState_Base> StateMachine => _stateMachine;
	public UI_Manager UIManager => _uiManager;
	public QuantumSessionManager SessionManager => _sessionManager;

	[Inject]
	public MainMenuState_Data(StateMachine<MainMenuState_Base> stateMachine,
		UI_Manager ui_Manager,
		QuantumSessionManager sessionManager)
	{
		_stateMachine = stateMachine;
		_uiManager = ui_Manager;
		_sessionManager = sessionManager;
	}
}
