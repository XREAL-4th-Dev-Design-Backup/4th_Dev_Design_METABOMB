using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBlow : MonoBehaviour
{
    [SerializeField] private GameObject blowVersion;
    //[SerializeField] private GameObject ExplosionVersion;
    //public float ExplosionDelayTime;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("AutoExplosion", ExplosionDelayTime);
//        AutoExplosion();    //물체 생성 스크립트에서 참초해 사용할 거면 지우기
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCollisionEnter()
    {
        Destroy(gameObject);
        Instantiate(blowVersion, transform.position, transform.rotation);
    }

    //물체 자동 파괴 함수 
    //public void AutoExplosion()
    //{
    //    Instantiate(ExplosionVersion, transform.position, transform.rotation);
        
    //    Destroy(gameObject, 1);
    //}
}
