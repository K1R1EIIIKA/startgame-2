using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "SuperDuperGame/Tile", order = 0)]
public class Tile : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _lenght;
    [SerializeField] private GameObject _scene;
    
    public string Name => _name;
    public float Length => _lenght;
    public GameObject Scene => _scene;
}