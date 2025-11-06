using UnityEngine;

public abstract class MainMenuState_Base : BaseState
{
    protected StateMachine<MainMenuState_Base> _sm;
    protected MainMenuState_Base(MainMenuState_Data data)
    {
        _sm = data.StateMachine;
    }
}
