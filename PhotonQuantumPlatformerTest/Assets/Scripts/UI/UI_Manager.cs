using System;
using System.Collections.Generic;

public class UI_Manager
{
	private UI_Panel _basePanel;

	private Dictionary<Type, UI_Panel> _convertPanels = new();
    private Stack<UI_Panel> _panelStack = new();

	public UI_Manager(UI_Panel basePanel, UI_Panel[] panels)
	{
		foreach (var panel in panels)
		{
			panel.gameObject.SetActive(false);
			_convertPanels.Add(panel.GetType(), panel);
		}
		_basePanel = basePanel;
		_basePanel.SetActive(true);
	}

	public T SetBasePanel<T>() where T : UI_Panel
	{
		if (_basePanel != null)
			_basePanel.SetActive(false);

		var panel = _convertPanels[typeof(T)];
		panel.SetActive(true);
		_basePanel = panel;

		_panelStack.Clear();
		return panel as T;
	}
	public T PushPanel<T>(bool isDisable = true) where T : UI_Panel
	{
		var panel = _convertPanels[typeof(T)];
		if (_panelStack.Count > 0 && _panelStack.Peek() == panel)
			return panel as T;

		if(isDisable)
		{
			if (_panelStack.Count > 0)
				_panelStack.Peek().SetActive(false);
			else
				_basePanel.SetActive(false);
		}


		panel.SetActive(true);
		_panelStack.Push(panel);
		return panel as T;
	}

	public void PopPanel()
	{
		if (_panelStack.Count == 0)
			return;

		var top = _panelStack.Pop();
		top.SetActive(false);

		if (_panelStack.Count > 0)
			_panelStack.Peek().SetActive(true);
		else
			_basePanel.SetActive(true);
	}

	public void ClearStack()
	{
		while (_panelStack.Count > 0)
		{
			var panel = _panelStack.Pop();
			panel.SetActive(false);
		}
		_basePanel.SetActive(true);
	}

	public T GetPanel<T>() where T : UI_Panel
	{
		return _convertPanels[typeof(T)] as T;
	}
}
