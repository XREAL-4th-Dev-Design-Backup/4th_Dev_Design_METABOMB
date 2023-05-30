using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCrystal : MonoBehaviour
{
    float x,y,z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x+=50*Time.deltaTime;
        y+=50*Time.deltaTime;
        z+=50*Time.deltaTime;
        transform.eulerAngles = new Vector3(x,y,z);
    }
}
