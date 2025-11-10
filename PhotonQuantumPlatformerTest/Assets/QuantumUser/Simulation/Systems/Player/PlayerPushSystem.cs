namespace Quantum.Player
{
    using Photon.Deterministic;
	using Quantum.Physics3D;

    public unsafe class PlayerPushSystem : SystemMainThreadFilter<PlayerPushSystem.Filter>
    {
		public override void Update(Frame f, ref Filter filter)
		{
			var input = f.GetPlayerInput(filter.PlayerLink->Player);

			if (input->Push.WasPressed && !filter.Push->IsPush)
			{
				UnityEngine.Debug.Log("0002");
				filter.Push->IsPush = true;
				TryPush(f, ref filter);
			}
			else
			{
				filter.Push->IsPush = false;
			}
		}

		private void TryPush(Frame f, ref Filter filter)
		{
			var kcc = filter.KCC;
			var origin = filter.Transform->Position;
			var up = filter.Transform->Up;
			var forward = filter.Transform->Forward;

			FPVector3 rayStart = origin + FP._0_50 * up;
			FP maxDistance = FP._1_50;

			Hit3D? hit = f.Physics3D.Raycast(rayStart, forward, maxDistance);

			if (hit.HasValue)
			{
				if (!f.Has<Pushable>(hit.Value.Entity))
					return;

				PushDirection push = new PushDirection()
				{
					Dir = forward * filter.Push->PushForce
				};

				f.Add(hit.Value.Entity, push);
			}
		}

		public struct Filter
		{
			public EntityRef Entity;
			public CharacterController3D* KCC;
			public Transform3D* Transform;
			public PlayerLink* PlayerLink;
            public PlayerPush* Push;
		}
    }
}
