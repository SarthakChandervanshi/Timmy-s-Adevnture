using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{


    [SerializeField] private Transform target;


    private void Update()
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 10f);
    }
}