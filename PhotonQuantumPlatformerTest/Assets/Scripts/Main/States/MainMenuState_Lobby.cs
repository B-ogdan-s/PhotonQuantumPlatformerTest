using UnityEngine;
using Zenject;

public class MainMenuState_Lobby : MainMenuState_Base
{
	private UI_Manager _manager;
	private QuantumSessionManager _sessionManager;
	private Lobby_UI _lobby;

	[Inject]
	public MainMenuState_Lobby(MainMenuState_Data data) : base(data)
	{
		_manager = data.UIManager;
		_sessionManager = data.SessionManager;
	}
	public override void Enter()
	{
		base.Enter();
		_sessionManager.OnStartSession += StartSession;
		_sessionManager.OnFailedSession += FailedSession;
		_sessionManager.OnLeaveSession += LeaveSession;
		_sessionManager.OnStartGame += StartGame;

		_sessionManager.OnChangePlayerCount += UpdateCount;

		_lobby = _manager.PushPanel<Lobby_UI>();
		_lobby.OnLeave += _sessionManager.LeaveSessionAsync;

		_sessionManager.StartSessionAsync();
	}

	public override void Exit()
	{
		base.Exit();
		_sessionManager.OnStartSession -= StartSession;
		_sessionManager.OnFailedSession -= FailedSession;
		_sessionManager.OnLeaveSession -= LeaveSession;
		_sessionManager.OnStartGame -= StartGame;

		_sessionManager.OnChangePlayerCount -= UpdateCount;

		_lobby.OnLeave -= _sessionManager.LeaveSessionAsync;
	}

	private void StartSession()
	{
		_lobby.ActivateShowData(true);
		_lobby.ActivateButtons(true);
	}
	private void StartGame()
	{
		_lobby.ActivateShowData(true);
		_lobby.ActivateButtons(false);
	}
	private void UpdateCount(int count, int maxCount)
	{
		_lobby.UpdatePlayerCount(count, maxCount);
	}
	private void LeaveSession()
	{
		_sm.ChangeState(typeof(MainMenuState_Main));
	}
	private void FailedSession()
	{
		Debug.Log("Failed Start Session");
	}
}
