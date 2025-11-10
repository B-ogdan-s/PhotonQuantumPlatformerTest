namespace Quantum
{
    using Photon.Deterministic;
	using System.Collections.Generic;

	public unsafe class PlayerSpawer : SystemSignalsOnly, ISignalOnPlayerAdded, ISignalOnMapChanged
	{
		Dictionary<PlayerRef, EntityRef> _players = new();

		public void OnMapChanged(Frame f, AssetRef<Map> previousMap)
		{
			foreach (var p in _players)
			{
				if (f.Unsafe.TryGetPointer<Transform3D>(p.Value, out var transform))
				{
					transform->Position = new FPVector3(-1 + p.Key * 2, 0, 0);
				}
			}
		}

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
				transform->Position = new FPVector3(-1 + player * 2, 0, 0);
			}
			_players.Add(player, entity);


			if (_players.Count >= f.MaxPlayerCount)
				f.Events.OnReadyPlayers();
		}
	}
}
