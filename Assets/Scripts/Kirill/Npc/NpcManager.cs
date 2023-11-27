using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcManager : MonoBehaviour
{
    [Header("Npc")]
    [SerializeField] private List<GameObject> _npcList;
    [SerializeField] private Transform _pncParent;
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private Vector2Int _countRange;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _spawnOffset = 3;
    
    public static NpcManager Instance;

    private Transform _player;
        
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
        
        _player = Movement.Instance.transform;
    }

    public void SpawnAllNpc(int direction)
    {
        int count = Random.Range(_countRange.x, _countRange.y + 1);

        for (int i = 0; i < count; i++)
        {
            SpawnNpc(Random.Range(_speedRange.x, _speedRange.y + 1), direction);
        }
    }

    private void SpawnNpc(float speed, int direction)
    {
        float posX = Random.Range(-2.5f, 2.5f);
        float posZ = _player.position.z - _spawnOffset;
        GameObject npc = Instantiate(_npcList[Random.Range(0, _npcList.Count)], new Vector3(posX, 0, posZ), Quaternion.identity, _pncParent);

        NpcMovement movement = npc.GetComponent<NpcMovement>();
        movement.Speed = speed;
        movement.Directon = direction;

        StartCoroutine(RemoveNpc(npc));
    }

    private IEnumerator RemoveNpc(GameObject npc)
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(npc);
    }
}
