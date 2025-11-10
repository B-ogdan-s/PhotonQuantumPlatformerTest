using Quantum;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeData", menuName = "Data/GameModeData")]
public class GameModeData : ScriptableObject
{
	[SerializeField] private int _maxPlayers;
	[SerializeField] private Map[] _maps;

	public int MaxPlayers => _maxPlayers;

	public int MapCount => _maps.Length;

	public Map GetMap(int id)
	{
		return _maps[id];
	}
}
