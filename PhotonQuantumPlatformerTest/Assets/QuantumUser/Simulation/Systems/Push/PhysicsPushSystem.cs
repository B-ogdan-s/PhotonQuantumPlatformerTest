namespace Quantum
{
    using Photon.Deterministic;
	using UnityEngine;

    public unsafe class PhysicsPushSystem : SystemMainThreadFilter<PhysicsPushSystem.Filter>
    {
        public override void Update(Frame f, ref Filter filter)
		{
			//filter.PhysicsBody->AddForce(filter.PushDirection->Dir);
			filter.PhysicsBody->AddLinearImpulse(filter.PushDirection->Dir);
			f.Remove(filter.Entity, typeof(PushDirection));
		}

        public struct Filter
		{
			public EntityRef Entity;
			public PushDirection* PushDirection;
            public PhysicsBody3D* PhysicsBody;
		}
    }
}
