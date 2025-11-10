namespace Quantum.Platform
{
    using Photon.Deterministic;

    public unsafe class PlatformRiderSystem : SystemMainThreadFilter<PlatformRiderSystem.Filter>
    {
        public override void Update(Frame f, ref Filter filter)
        {
			var rider = filter.Rider;

			if (rider->PlatformEntity == EntityRef.None)
				return;

			if (!f.Exists(rider->PlatformEntity))
				return;

			var platformTransform = f.Unsafe.GetPointer<Transform3D>(rider->PlatformEntity);

			FPVector3 currentPlatformPos = platformTransform->Position;
			FPVector3 delta = currentPlatformPos - rider->LastPlatformPos;

			filter.Transform->Position += delta;
			rider->LastPlatformPos = currentPlatformPos;
		}

		public struct Filter
		{
			public EntityRef Entity;
			public Transform3D* Transform;
			public PlatformRider* Rider;
		}
	}
}
