using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    public static TerrainGenerator Instance;
    
    [Header("Terrains")]
    [SerializeField] private List<GameObject> _obstacleTerrains;
    [SerializeField] private List<GameObject> _panHittingTerrains;
    [SerializeField] private List<GameObject> _gravitationTerrains;
    [SerializeField] private GameObject _playerOvertookTerrain;
    [SerializeField] private GameObject _middleTerrain;
    [SerializeField] private GameObject _enemyOvertookTerrain;
    [SerializeField] private Transform _terrainParent;
    
    [Header("Other")]
    [SerializeField] private int _maxTerrainCount = 5;
    // [SerializeField] private float _terrainOffset = 1;
    // [SerializeField] private float _middleTerrainOffset = 1;
    

    private Vector3 _currentPosition = Vector3.zero;
    private List<GameObject> _terrainList = new List<GameObject>();
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < _maxTerrainCount; i++)
        {
            SpawnRandomTerrain();
            SpawnTerrain(_middleTerrain);
        }
    }

    private void SpawnRandomTerrain()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                SpawnTerrain(_obstacleTerrains[Random.Range(0, _obstacleTerrains.Count)]);
                break;
            case 1:
                SpawnTerrain(_panHittingTerrains[Random.Range(0, _obstacleTerrains.Count)]);
                break;
            case 2:
                SpawnTerrain(_gravitationTerrains[Random.Range(0, _obstacleTerrains.Count)]);
                break;
        }
    }
    
    private void SpawnTerrain(GameObject terrain)
    {
        if (_terrainList.Count > 0)
            _currentPosition.x += _terrainList[^1].transform.localScale.x / 2 + terrain.transform.localScale.x / 2;
        
        GameObject terrainObject = Instantiate(terrain, _currentPosition, Quaternion.identity, _terrainParent);
        
        _terrainList.Add(terrainObject);
    }
}
