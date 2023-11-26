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
    [SerializeField] private GameObject _winTerrain;
    [SerializeField] private Transform _terrainParent;
    
    [Header("Other")]
    [SerializeField] private int _maxTerrainCount = 5;
    [SerializeField] private Transform _player;

    private Vector3 _currentPosition = Vector3.zero;
    private float _offset;
    private List<float> _offsetList = new();
    private List<GameObject> _terrainList = new();

    private Vector3 _startReplacedPos;
    
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
        // Debug.Log(string.Join(" ", _offsetList.Select(x => x.ToString())));
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
            var bigObjectOffset = GetObjectOffset(^1, terrain);
            Transform road = Array.Find(_terrainList[^1].GetComponentsInChildren<Transform>(), x => x.name == "Road");

            _offset = road.localScale.y / 50 - bigObjectOffset;
            _currentPosition.z += _offset;
        }

        GameObject terrainObject = Instantiate(terrain, _currentPosition, Quaternion.identity, _terrainParent);
        
        _terrainList.Add(terrainObject);
        _offsetList.Add(_offset);

        if (_terrainList.Count > _maxTerrainCount)
            RemoveTerrain();
    }

    private void ReplaceTerrain(GameObject terrain)
    {
        var bigObjectOffset = GetObjectOffset(2, terrain);

        Vector3 spawnPos = _startReplacedPos;
        spawnPos.z -= bigObjectOffset;
        _currentPosition.z -= bigObjectOffset * 2;
        
        for (int i = 2; i < _terrainList.Count; i++)
        {
            Vector3 pos = _terrainList[i].transform.position;
            _terrainList[i].transform.position = new Vector3(pos.x, pos.y, pos.z - bigObjectOffset * 2);
        }

        GameObject newTerrain = Instantiate(_enemyOvertookTerrain, spawnPos, Quaternion.identity, _terrainParent);
        _terrainList.Insert(2, newTerrain);
    }

    private float GetObjectOffset(Index listIndex, GameObject terrain)
    {
        Transform road = Array.Find(_terrainList[listIndex].GetComponentsInChildren<Transform>(), x => x.name == "Road");
        Transform newRoad = Array.Find(terrain.GetComponentsInChildren<Transform>(), x => x.name == "Road");
        float bigObjectOffset = (road.localScale.y - newRoad.localScale.y) / 100;
        
        return bigObjectOffset;
    }

    private void RemoveTerrain(int index = 0)
    {
        if (index != 0)
            _startReplacedPos = _terrainList[index].transform.position;
        
        Destroy(_terrainList[index]);
        _terrainList.RemoveAt(index);
        _offsetList.RemoveAt(index);
    }

    public void SpawnPlayerOvertookTerrain()
    {
        RemoveTerrain(2);
        ReplaceTerrain(_playerOvertookTerrain);
    }
    
    public void SpawnEnemyOvertookTerrain()
    {
        RemoveTerrain(2);
        ReplaceTerrain(_enemyOvertookTerrain);
    }

    public void SpawnWinTerrain()
    {
        for (int i = 2; i < _terrainList.Count; i++)
            RemoveTerrain(i);
        
        ReplaceTerrain(_winTerrain);
    }
}
