using Quantum;
using UnityEngine;
using Zenject;

public class MainQuantumInstaller : MonoInstaller
{
	[SerializeField] private MenuConnectArgs _connectArgs;
	[SerializeField] private RuntimePlayer _player;
	public override void InstallBindings()
	{
		Container.Bind<QuantumSessionManager>().AsSingle().WithArguments(_connectArgs, _player);
	}

}
