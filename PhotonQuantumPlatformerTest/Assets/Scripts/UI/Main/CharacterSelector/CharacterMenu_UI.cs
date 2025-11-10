using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu_UI : UI_Panel
{
	[SerializeField] private Button _backButton;
	[SerializeField] private Button _usedButton;
	[SerializeField] private CharacterPreviewPanel _previewPanel;
	[SerializeField] private TextMeshProUGUI _nameText;

	public CharacterPreviewPanel PreviewPanel => _previewPanel;

	private CharacterData _curentData;

	public Action OnBack;
	public Action<CharacterData> OnUsed;

	private void Awake()
	{
		_backButton.onClick.AddListener(() => OnBack?.Invoke());
		_usedButton.onClick.AddListener(() => OnUsed?.Invoke(_curentData));
	}

	public void Initialize(CharacterData data)
	{
		_curentData = data;
		_nameText.text = data.Name;
	}


	private void OnDestroy()
	{
		_backButton.onClick.RemoveAllListeners();
		_usedButton.onClick.RemoveAllListeners();
	}
}
