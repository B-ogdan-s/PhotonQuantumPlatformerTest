using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectorHandler
{
	private List<CharacterData> _characters;

	CharacterData _currentCharacter;
	public List<CharacterData> Characterds => _characters;
	public CharacterData CurrentCharacter => _currentCharacter;

	public Action<CharacterData> OnChangeCharacter;

	public CharacterSelectorHandler(List<CharacterData> characters)
	{
		_characters = characters;
		_currentCharacter = _characters[0];
	}

	public void SetCurrentCharacter(CharacterData character)
	{
		_currentCharacter = character;
		OnChangeCharacter?.Invoke(character);

	}
}
