using UnityEngine;

public class CharacterPreviewHandler : MonoBehaviour
{
	[SerializeField] private Transform _characterSpawnContainer;
	[SerializeField] private float _rotateSpeed;

	GameObject _currentCharacter;
	public void ChangeCharacter(GameObject character)
	{
		if(_currentCharacter != null)
			Destroy(_currentCharacter.gameObject);

		_characterSpawnContainer.localEulerAngles = Vector3.zero;

		_currentCharacter = Instantiate(character, _characterSpawnContainer);
	}

	public void RotateCharacter(float delta)
	{
		_characterSpawnContainer.localEulerAngles += new Vector3(0, -delta, 0) * Time.deltaTime * _rotateSpeed;
	}
}
