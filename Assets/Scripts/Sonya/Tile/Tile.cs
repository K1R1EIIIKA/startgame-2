
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "startgame-2/Tile", order = 0)]
public class Tile : ScriptableObject {
     [SerializeField] private string _name;
    public string Name {  get => _name; }
   [SerializeField] private float _lenght;
    public float Length {  get => _lenght; }
 [SerializeField] private GameObject _scene;
    public GameObject Scene {  get => _scene; }

}


