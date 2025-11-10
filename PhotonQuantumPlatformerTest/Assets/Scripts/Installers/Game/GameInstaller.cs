using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private FinishEvent _event;
    public override void InstallBindings()
    {
        Container.BindInstance(_event).AsSingle(); 
        Container.QueueForInject(_event);
	}
}