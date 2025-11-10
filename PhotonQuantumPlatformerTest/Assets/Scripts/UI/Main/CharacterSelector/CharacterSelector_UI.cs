using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector_UI : UI_Panel
{
	[SerializeField] private Button _closeButton;
	[SerializeField] private Transform _containerTransform;
	[SerializeField] private CharacterPanel _characterPanelPrefab;

	private List<CharacterPanel> _panels = new();

	public Action<CharacterData> OnClick;
	public Action OnClose;

	private void Awake()
	{
		_closeButton.onClick.AddListener(() => OnClose?.Invoke());
	}

	public void Initialize(List<CharacterData> data)
	{
		foreach (CharacterData c in data)
		{
			CharacterPanel panel = Instantiate(_characterPanelPrefab, _containerTransform);
			panel.Initialize(c);
			panel.OnClick += (value) => OnClick?.Invoke(value);
			_panels.Add(panel);
		}
	}
}
