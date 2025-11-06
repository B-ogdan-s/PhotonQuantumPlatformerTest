namespace Quantum
{
    using Photon.Deterministic;

    public unsafe class JumpingSystem : SystemMainThreadFilter<JumpingSystem.Filter>
	{
        public override void Update(Frame f, ref Filter filter)
		{
			var input = f.GetPlayerInput(filter.PlayerLink->Player);


			if (input->Jump.IsDown)
			{
				filter.KCC->Jump(f, impulse: filter.PlayerJumping->JumpPower);
			}

		}

		public struct Filter
		{
			public EntityRef Entity;
			public CharacterController3D* KCC;
			public PlayerLink* PlayerLink;
			public PlayerJumping* PlayerJumping;
		}
	}
}
