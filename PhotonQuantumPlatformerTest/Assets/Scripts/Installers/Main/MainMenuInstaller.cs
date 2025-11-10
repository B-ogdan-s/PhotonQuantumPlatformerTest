using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
	[SerializeField] private List<CharacterData> _characterData;
	[SerializeField] private CharacterPreviewHandler _characterPreviewHandler;
	public override void InstallBindings()
	{
		Container.Bind<CharacterSelectorHandler>().AsSingle().WithArguments(_characterData);
		Container.BindInstances(_characterPreviewHandler);
	}
}
