using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TerrainGenerator : MonoBehaviour
{
    public static TerrainGenerator Instance;
    
    [Header("Terrains")]
    [SerializeField] private GameObject _firstTerrain;
    
    [SerializeField] private List<GameObject> _obstacleTerrains;
    [SerializeField] private List<GameObject> _panHittingTerrains;
    [SerializeField] private List<GameObject> _gravitationTerrains;
    
    [SerializeField] private GameObject _playerOvertookTerrain;
    [SerializeField] private GameObject _enemyOvertookTerrain;
    [SerializeField] private Transform _terrainParent;
    
    [Header("Other")]
    [SerializeField] private int _maxTerrainCount = 5;
    [SerializeField] private Transform _player;

    private Vector3 _currentPosition = Vector3.zero;
    private float _offset;
    private List<float> _offsetList = new();
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
        SpawnTerrain(_firstTerrain);
        for (int i = 0; i < _maxTerrainCount-1; i++)
        {
            SpawnRandomTerrain();
        }
    }

    private void Update()
    {
        Debug.Log(string.Join(" ", _offsetList.Select(x => x.ToString())));
        if (_player.position.z - _offsetList[1] > _terrainList[0].transform.position.z)
        {
            RemoveTerrain();
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
        
        // SpawnTerrain(_middleTerrain);
    }

    private GameObject GetRandomTerrain(List<GameObject> terrains)
    {
        return terrains[Random.Range(0, terrains.Count)];
    }
    
    private void SpawnTerrain(GameObject terrain)
    {
        if (_terrainList.Count > 0)
        {
            Transform road = Array.Find(_terrainList[^1].GetComponentsInChildren<Transform>(), x => x.name == "Road");
            Transform newRoad = Array.Find(terrain.GetComponentsInChildren<Transform>(), x => x.name == "Road");
            float bigObjectOffset = (road.localScale.y - newRoad.localScale.y) / 100;

            _offset = road.localScale.y / 50 - bigObjectOffset;
            _currentPosition.z += _offset;
        }

        GameObject terrainObject = Instantiate(terrain, _currentPosition, Quaternion.identity, _terrainParent);
        
        _terrainList.Add(terrainObject);
        _offsetList.Add(_offset);

        if (_terrainList.Count > _maxTerrainCount)
        {
            RemoveTerrain();
        }
    }

    private void ReplaceTerrain(GameObject terrain)
    {
        Transform road = Array.Find(_terrainList[^1].GetComponentsInChildren<Transform>(), x => x.name == "Road");
        Transform newRoad = Array.Find(terrain.GetComponentsInChildren<Transform>(), x => x.name == "Road");
        float bigObjectOffset = (road.localScale.y - newRoad.localScale.y) / 100;

        float offset = road.localScale.y / 50 - bigObjectOffset;
        Vector3 spawnPos = _terrainList[2].transform.position;
        // spawnPos.z -= offset;

        GameObject newTerrain = Instantiate(_enemyOvertookTerrain, spawnPos, Quaternion.identity, _terrainParent);
        _terrainList.Insert(2, newTerrain);
    }

    
    private void RemoveTerrain(int index = 0)
    {
        Destroy(_terrainList[index]);
        _terrainList.RemoveAt(index);
        _offsetList.RemoveAt(index);
    }

    public void SpawnPlayerOvertookTerrain()
    {
        SpawnTerrain(_playerOvertookTerrain);
        // SpawnTerrain(_middleTerrain);
    }
    
    public void SpawnEnemyOvertookTerrain()
    {
        RemoveTerrain(2);
        ReplaceTerrain(_enemyOvertookTerrain);
        // SpawnTerrain(_middleTerrain);
    }
}
