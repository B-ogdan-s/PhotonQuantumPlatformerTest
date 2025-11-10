namespace Quantum.Platform
{
    using Photon.Deterministic;

	public unsafe class PlatformTriggerSystem : SystemSignalsOnly,
		ISignalOnTriggerEnter3D,
		ISignalOnTriggerExit3D
	{
		public void OnTriggerEnter3D(Frame f, TriggerInfo3D info)
		{
			if (!f.Has<PlatformLink>(info.Entity))
				return;

			var link = f.Get<PlatformLink>(info.Entity);

			EntityRef platformEntity = link.Platform;
			EntityRef otherEntity = info.Other;

			if (platformEntity == otherEntity || !f.Has<Transform3D>(otherEntity))
				return;


			if (!f.Has<PlatformRider>(otherEntity))
			{
				var rider = new PlatformRider()
				{
					PlatformEntity = platformEntity,
					LastPlatformPos = f.Unsafe.GetPointer<Transform3D>(platformEntity)->Position,
				};

				f.Add(otherEntity, rider);
			}
		}

		public void OnTriggerExit3D(Frame f, ExitInfo3D info)
		{
			if (!f.Has<PlatformLink>(info.Entity))
				return;

			if (f.Has<PlatformRider>(info.Other))
			{
				f.Remove<PlatformRider>(info.Other);
			}
		}
	}
}
