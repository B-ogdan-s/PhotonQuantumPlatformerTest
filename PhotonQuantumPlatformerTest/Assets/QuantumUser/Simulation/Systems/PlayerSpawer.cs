namespace Quantum
{
    using Photon.Deterministic;

	public unsafe class PlayerSpawer : SystemSignalsOnly, ISignalOnPlayerAdded
	{
		public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
		{
			var runtimePlayer = f.GetPlayerData(player);
			var entity = f.Create(runtimePlayer.PlayerAvatar);
			var lilk = new PlayerLink()
			{
				Player = player
			};

			f.Add(entity, lilk);

			if (f.Unsafe.TryGetPointer<Transform3D>(entity, out var transform))
			{
				transform->Position = new FPVector3( -1 + player * 2, 2, 0);
			}
		}
	}
}
