using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _previewModel;
    [SerializeField] private Quantum.AssetRef<Quantum.EntityPrototype> _gamePrefab;

    public string Name => _name;
    public GameObject PreviewModel => _previewModel;
    public Quantum.AssetRef<Quantum.EntityPrototype> GamePrefab => _gamePrefab;

}
