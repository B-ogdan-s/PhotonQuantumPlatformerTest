using Quantum;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
	[SerializeField] private GameModeData _mod;
	[SerializeField] AssetRef<SimulationConfig> _simulationConfig;
	[SerializeField] AssetRef<SystemsConfig> _systemsConfig;
	public override void InstallBindings()
    {
		Container.Bind<PlayerGameData>().AsSingle().NonLazy();
		Container.Bind<QuantumGameHandler>().AsSingle().WithArguments(_mod, _simulationConfig, _systemsConfig);
	}
}