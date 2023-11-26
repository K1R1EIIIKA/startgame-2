using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tile[] tileprefabs;
    [SerializeField] private float zSpawn = 0;
    [SerializeField] private float tileLength = 30;
    [SerializeField] private int numberOfTiles = 5;
    [SerializeField] private Transform playerTransform;
    private List<GameObject> activeTiles = new List<GameObject>();
    
    void Start()
    {
        for(int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tileprefabs.Length));
        }
    }

    
    void Update()
    {
        if (playerTransform.position.z-35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tileprefabs.Length));
            DeleteTile();
        }
    }
    public void SpawnTile(int tileIndex)
    {
        GameObject go=Instantiate(tileprefabs[tileIndex].Scene, transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        tileLength=tileprefabs[tileIndex].Length;
        zSpawn += tileprefabs[tileIndex].Length;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    } 
}
