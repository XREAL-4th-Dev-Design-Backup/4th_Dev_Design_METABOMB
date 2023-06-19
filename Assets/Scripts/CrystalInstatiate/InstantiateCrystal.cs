using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//크리스탈 prefab을 생성해주는 스크립트다.

public class InstantiateCrystal : MonoBehaviour
{
    public GameObject crystalPrefab;
    
    //public GameObject crystalExplosionEffect;//플레이어가 맞추지않으면 혼자서 터질 때 발생하는 이펙트

    public GameObject crystalInstantiateEffect; //크리스탈이 발생할 때 실린더에서 발생하는 이펙트

    //public GameObject crystalEffect; //크리스탈이 공중에 있는동안 생성되는 이펙트

    public float destroyTime = 3.0f; //파괴될 시간

    private float fTickTime; //초 세기

    GameObject crystal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        fTickTime += Time.deltaTime;
        
    
       if ( fTickTime >= destroyTime)
       {
            DestroyCrystal();// destroyTime초 뒤에 실행
            print(fTickTime);
       }    
       */
    }


    public void InsCrystal()
    {
        //transform.Rotate(Vector3.back * 45);

   

        //GameObject crystal = Instantiate(crystalPrefab);

        //GameObject insEffect = Instantiate(crystalInstantiateEffect, this.transform.GetChild(0).position, Quaternion.identity); //물체 생성될 때 effect 발생

        crystal = Instantiate(crystalPrefab, this.transform.GetChild(0).position, this.transform.GetChild(0).rotation); //스크립트가 붙어있는 게임 오브젝트의 자식오브젝트의 위치에서 물체가 생성

        //GameObject crysEffect = Instantiate(crystalEffect, crystal.transform.GetChild(0).position, crystal.transform.rotation); //크리스탈이랑 함께 있는 effect
        //Debug.Log(instance.name);


        //crystal.GetComponent<Rigidbody>().AddForce(new Vector3(25, 0, 0), ForceMode.Impulse)

        //Destroy(crysEffect, destroyTime); //크리스탈 이펙트 제거
        //Destroy(crystal, destroyTime); //크리스탈 제거 

    }

    
}
