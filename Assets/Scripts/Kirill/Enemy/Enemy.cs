using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Enemy", menuName = "SuperDuperGame/Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _prefab;
    
    public float Speed => _speed;
    public GameObject Prefab => _prefab;
}