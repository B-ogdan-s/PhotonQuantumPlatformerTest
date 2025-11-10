namespace Quantum.Finish
{
    using Photon.Deterministic;

	public unsafe class FinishTriggerSystem : SystemSignalsOnly, ISignalOnTriggerEnter3D, ISignalOnTriggerExit3D
	{
		int _count = 0;

		public override void OnInit(Frame f)
		{
			_count = 0;
			base.OnInit(f);
		}

		public void OnTriggerEnter3D(Frame f, TriggerInfo3D info)
		{
			if (!f.Has<FinishLink>(info.Entity) 
				|| !f.Has<PlayerLink>(info.Other))
			{
				return;
			}
			if (!f.IsVerified)
				return;

			_count++;

			if(_count == f.MaxPlayerCount)
			{
				_count = 0;
				f.Events.OnFinish(f);
			}
		}

		public void OnTriggerExit3D(Frame f, ExitInfo3D info)
		{
			if (!f.Has<FinishLink>(info.Entity)
				|| !f.Has<PlayerLink>(info.Other))
			{
				return;
			}
			if (!f.IsVerified)
				return;


			_count--;
		}
	}
}
