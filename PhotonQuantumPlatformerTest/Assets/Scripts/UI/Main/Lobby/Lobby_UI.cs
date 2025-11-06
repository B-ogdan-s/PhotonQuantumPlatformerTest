using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_UI : UI_Panel
{
	[SerializeField] private TextMeshProUGUI _playerCountText;
	[SerializeField] private Button _leaveButtom;

	public Action OnLeave;

	private const string _countText = "Player Count: ";

	private void Awake()
	{
		_leaveButtom.onClick.AddListener(() => OnLeave?.Invoke());
	}

	private void OnEnable()
	{
		ActivateShowData(false);
		ActivateButtons(false);
	}

	public void UpdatePlayerCount(int curentCount, int maxCount)
	{
		_playerCountText.text = _countText + $"{curentCount}/{maxCount}";
	}

	public void ActivateShowData(bool value)
	{
		_playerCountText.gameObject.SetActive(value);
	}
	public void ActivateButtons(bool value)
	{
		_leaveButtom.interactable = value;
	}

	private void OnDestroy()
	{
		_leaveButtom.onClick.RemoveAllListeners();
		OnLeave = null;
	}
}
