using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launch : MonoBehaviour
{
    public Transform launchPoint;
    public Transform target;    
    public GameObject projectile;
    public float speed = 1;


    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, target.position, speed*Time.deltaTime);
        
    }
}
