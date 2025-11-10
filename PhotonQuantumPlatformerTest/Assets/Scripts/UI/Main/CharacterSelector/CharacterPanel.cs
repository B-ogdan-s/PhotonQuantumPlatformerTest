using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
	[SerializeField] private Button _button;
	[SerializeField] private TextMeshProUGUI _nameText;
	
	public Action<CharacterData> OnClick;

	public void Initialize(CharacterData data)
	{
		_nameText.text = data.name;
		_button.onClick.AddListener(() => OnClick?.Invoke(data));
	}
}
