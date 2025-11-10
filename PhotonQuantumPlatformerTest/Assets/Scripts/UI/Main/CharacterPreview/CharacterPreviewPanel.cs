using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterPreviewPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	private bool _isClick = false;

	public Action<float> OnDragAction;
	public Action OnClick;


	public void OnPointerDown(PointerEventData eventData)
	{
		_isClick = true;
	}

	public void OnDrag(PointerEventData eventData)
	{
		_isClick = false;

		OnDragAction?.Invoke(eventData.delta.x);

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if(_isClick)
		{
			OnClick?.Invoke();
			return;
		}
	}
}
