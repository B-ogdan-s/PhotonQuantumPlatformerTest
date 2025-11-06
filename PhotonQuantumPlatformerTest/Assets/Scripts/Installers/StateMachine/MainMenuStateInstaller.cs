using System;
using System.Collections.Generic;
using Zenject;

public class MainMenuStateInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
		Container.Bind<StateMachine<MainMenuState_Base>>().AsSingle();
		Container.Bind<MainMenuState_Data>().AsSingle();

		var data = Container.Resolve<MainMenuState_Data>();
		var machine = Container.Resolve<StateMachine<MainMenuState_Base>>();

		machine.Initialize(CreateStates(data));
		machine.ChangeState(typeof(MainMenuState_Main));
	}

	private Dictionary<Type, MainMenuState_Base> CreateStates(MainMenuState_Data data)
	{
		Dictionary<Type, MainMenuState_Base> states = new()
		{
			{typeof(MainMenuState_Main), new MainMenuState_Main(data) },
			{typeof(MainMenuState_Lobby), new MainMenuState_Lobby(data) },
		};

		return states;
	}
}