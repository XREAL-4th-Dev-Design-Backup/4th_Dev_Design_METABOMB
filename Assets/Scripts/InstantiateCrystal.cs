using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//크리스탈 prefab을 생성해주는 스크립트다.

public class InstantiateCrystal : MonoBehaviour
{
    public GameObject crystalPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InsCrystal()
    {
        //transform.Rotate(Vector3.back * 45);

        GameObject crystal = Instantiate(crystalPrefab);

        //crystal.GetComponent<Rigidbody>().AddForce(new Vector3(25, 0, 0), ForceMode.Impulse);

        Destroy(crystal, 1f);
    }
}
