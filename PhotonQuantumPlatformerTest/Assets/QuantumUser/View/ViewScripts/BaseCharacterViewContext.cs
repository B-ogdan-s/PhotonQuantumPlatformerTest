using UnityEngine;
using Quantum;
using Unity.Cinemachine;

public class BaseCharacterViewContext : MonoBehaviour, IQuantumViewContext
{
	[SerializeField] private CinemachineCamera _camera;

	public CinemachineCamera Camera => _camera;
}
