using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCylinder : MonoBehaviour
{
    float x,y,z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        y+=50*Time.deltaTime;
        transform.eulerAngles = new Vector3(0,y,0);
    }
}
