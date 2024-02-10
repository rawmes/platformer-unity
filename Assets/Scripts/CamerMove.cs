
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float damping;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 movePos = target.position+offset;
        transform.position = Vector3.SmoothDamp(transform.position,movePos,ref velocity, damping); 
    }


}
