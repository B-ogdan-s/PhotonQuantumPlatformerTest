using Quantum;
using UnityEngine;

public class CameraView : QuantumEntityViewComponent<BaseCharacterViewContext>
{

	public override void OnActivate(Frame frame)
	{
		var link = frame.Get<PlayerLink>(EntityRef);
		if(Game.PlayerIsLocal(link.Player))
			ViewContext.Camera.Target.TrackingTarget = transform;
	}
}
