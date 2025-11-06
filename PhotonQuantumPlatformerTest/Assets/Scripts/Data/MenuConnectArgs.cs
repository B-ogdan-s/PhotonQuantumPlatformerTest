using Quantum;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuConnectArgs", menuName = "args/MenuConnectArgs")]
public class MenuConnectArgs : ScriptableObject
{
    public int MaxPLayers;
    public RuntimeConfig Config;
}
