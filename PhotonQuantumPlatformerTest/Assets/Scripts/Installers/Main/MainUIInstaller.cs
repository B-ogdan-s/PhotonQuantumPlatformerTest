using UnityEngine;
using Zenject;

public class MainUIInstaller : MonoInstaller
{
	[SerializeField] private UI_Panel[] _uiPanels;
	[SerializeField] private UI_Panel _basePanel;

	public override void InstallBindings()
	{
		Container.Bind<UI_Manager>().AsSingle().WithArguments(_basePanel, _uiPanels);
	}
}