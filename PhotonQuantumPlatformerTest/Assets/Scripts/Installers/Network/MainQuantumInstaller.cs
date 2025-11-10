using Quantum;
using UnityEngine;
using Zenject;

public class MainQuantumInstaller : MonoInstaller
{
	//[SerializeField] private GameModeData _mod;
	//[SerializeField] AssetRef<SimulationConfig> _simulationConfig;
	//[SerializeField] AssetRef<SystemsConfig> _systemsConfig;

	public override void InstallBindings()
	{
		Container.Bind<QuantumSessionManager>().AsSingle();
		//Container.Bind<QuantumGameHandler>().AsSingle().WithArguments(_mod, _simulationConfig, _systemsConfig);
	}

}
