namespace Quantum
{
    using Photon.Deterministic;

	public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter> //, ISignalOnPlayerAdded
    {
		public override void Update(Frame f, ref Filter filter)
        {
			var input = f.GetPlayerInput(filter.PlayerLink->Player);

			var direction = input->Direction;

            if(direction.Magnitude > 1)
            {
                direction = direction.Normalized;
            }

			var forward = input->CameraForward;
			var right = input->CameraRight;

			var moveDir = (forward * direction.Y + right * direction.X).Normalized;

			var velocity = moveDir * filter.Moving->Speed * f.DeltaTime;

			filter.KCC->Move(f, filter.Entity, velocity.XOZ);

			if (moveDir.SqrMagnitude > FP._0)
			{
				var targetRot = FPQuaternion.LookRotation(moveDir, FPVector3.Up);

				FP rotationSpeed = FP._10 * f.DeltaTime; 

				filter.Transform->Rotation = FPQuaternion.Slerp(
					filter.Transform->Rotation,
					targetRot,
					rotationSpeed
				);
			}
		}

        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public CharacterController3D* KCC;
            public PlayerLink* PlayerLink;
			public PlayerMoving* Moving;
        }
	}
}
