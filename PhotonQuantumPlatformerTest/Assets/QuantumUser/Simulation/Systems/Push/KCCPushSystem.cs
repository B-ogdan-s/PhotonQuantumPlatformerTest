namespace Quantum
{
    using Photon.Deterministic;

    public unsafe class KCCPushSystem : SystemMainThreadFilter<KCCPushSystem.Filter>
    {
        public override void Update(Frame f, ref Filter filter)
        {
			filter.KCC->Velocity += filter.PushDirection->Dir;

			filter.PushDirection->Dir *= FP.FromRoundedFloat_UNSAFE(0.93f); 

			if (filter.PushDirection->Dir.Magnitude < FP._0_10)
			{
				f.Remove(filter.Entity, typeof(PushDirection));
			}
        }

        public struct Filter
        {
            public EntityRef Entity;
            public PushDirection* PushDirection;
			public CharacterController3D* KCC;
		}
    }
}
