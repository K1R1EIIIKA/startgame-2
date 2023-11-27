using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    private bool done;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        //if (PlayerManager.playerIsUp&&!done){
//transform.position.y=transform.position.y+
        //  }
        Vector3 newPosition =
            new Vector3(offset.x + target.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 1f);
    }
}