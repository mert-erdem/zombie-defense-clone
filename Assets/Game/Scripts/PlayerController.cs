using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 dir = Vector3.zero;
    private void Update()
    {
#if UNITY_EDITOR
        dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(dir * (speed * Time.deltaTime), Space.Self);
#endif
        
    }
}
