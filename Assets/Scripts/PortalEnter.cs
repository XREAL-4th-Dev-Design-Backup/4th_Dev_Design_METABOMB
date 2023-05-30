using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnter : MonoBehaviour
{
    Vector3 destination = new Vector3(-254.5f, 236.9f, 252.0f);
    // Update is called once per frame
    void Update()
    {
        transform.position =  Vector3.MoveTowards(transform.position, destination, 1);
    }
}
