namespace Quantum.Platform
{
    using Photon.Deterministic;
	using Quantum.Collections;

    public unsafe class MovementPlatformSystem : SystemMainThreadFilter<MovementPlatformSystem.Filter>
    {
        public override void Update(Frame f, ref Filter filter)
        {
			var positions = f.ResolveList(filter.Movement->Position);

			FPVector3 currentPos = filter.Transform->Position;
			FPVector3 target = positions[filter.Movement->CurrentIndex];

			FPVector3 direction = (target - currentPos);
			FP distance = direction.Magnitude;
			FP moveStep = filter.Movement->Speed * f.DeltaTime;

			if (moveStep > distance)
			{
				moveStep = distance;
				ChangeTarget(filter.Movement, positions);
			}

			filter.Transform->Position += direction.Normalized * moveStep;

			var triggers = f.ResolveList(filter.Movement->Triggers);
			foreach (var trigger in triggers)
			{
				var triggerTransform = f.Unsafe.GetPointer<Transform3D>(trigger);
				triggerTransform->Position += direction.Normalized * moveStep;
			}
		}

        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public PlatformMoving* Movement;
        }

		private void ChangeTarget(PlatformMoving* Movement, QList<FPVector3> positions)
		{
			if(!Movement->revers)
			{
				if(Movement->CurrentIndex == positions.Count -1)
				{
					Movement->revers = true;
					Movement->CurrentIndex--;
				}
				else
				{
					Movement->CurrentIndex++;
				}
			}
			else
			{
				if (Movement->CurrentIndex == 0)
				{
					Movement->revers = false;
					Movement->CurrentIndex++;
				}
				else
				{
					Movement->CurrentIndex--;
				}
			}
		}
    }
}
