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
    [SerializeField] private int _maxTerrainCount = 10;

    private Vector3 _currentPosition = Vector3.zero;
    private List<GameObject> _terrainList = new();
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnRandomTerrain();
        }
    }

    public void SpawnRandomTerrain()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                SpawnTerrain(GetRandomTerrain(_obstacleTerrains));
                break;
            case 1:
                SpawnTerrain(GetRandomTerrain(_panHittingTerrains));
                break;
            case 2:
                SpawnTerrain(GetRandomTerrain(_gravitationTerrains));
                break;
        }
        
        SpawnTerrain(_middleTerrain);
    }

    private GameObject GetRandomTerrain(List<GameObject> terrains)
    {
        return terrains[Random.Range(0, terrains.Count)];
    }
    
    private void SpawnTerrain(GameObject terrain)
    {
        if (_terrainList.Count > 0)
            _currentPosition.x += _terrainList[^1].transform.localScale.x / 2 + terrain.transform.localScale.x / 2;
        
        GameObject terrainObject = Instantiate(terrain, _currentPosition, Quaternion.identity, _terrainParent);
        
        _terrainList.Add(terrainObject);

        if (_terrainList.Count > _maxTerrainCount)
        {
            Destroy(_terrainList[0]);
            _terrainList.RemoveAt(0);
        }
    }

    // TODO: надо сделать распознавание террейна под которым игрок находится чтобы заменить его на обгон или наоборот
    public void SpawnPlayerOvertookTerrain()
    {
        SpawnTerrain(_playerOvertookTerrain);
        SpawnTerrain(_middleTerrain);
    }
    
    public void SpawnEnemyOvertookTerrain()
    {
        SpawnTerrain(_enemyOvertookTerrain);
        SpawnTerrain(_middleTerrain);
    }
}
