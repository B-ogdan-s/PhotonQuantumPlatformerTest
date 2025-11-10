namespace Quantum.DeadZone
{
    using Photon.Deterministic;

	public unsafe class DeadZoneSystem : SystemSignalsOnly, ISignalOnTriggerEnter3D
	{
		public void OnTriggerEnter3D(Frame f, TriggerInfo3D info)
		{
			if (!f.Has<DeadZoneLink>(info.Entity) || !f.Has<PlayerLink>(info.Other))
				return;

			if (f.Unsafe.TryGetPointer<Transform3D>(info.Other, out var transform))
			{
				if (!f.Unsafe.TryGetPointer<PlayerLink>(info.Other, out var player))
					return;

				transform->Position = new FPVector3(-1 + player->Player * 2, 0, 0);
			}
		}
	}
}
