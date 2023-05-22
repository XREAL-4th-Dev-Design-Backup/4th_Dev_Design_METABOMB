using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject firePos;  //총알 생성 위치
    [SerializeField] private GameObject hitFx; //총알 맞았다는 효과  
    [SerializeField] private Vector3 gunPosition;
    //[SerializeField] private GameObject gun;
    //private Transform swanPoint; //ray 선 끝 위치
    Color rayColor = Color.red; //ray 선 색깔

    void Start()
    {
        //gun.transform.position = gunPosition;
        //gunPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }

    // Update is called once per frame
    void Update()
    {
        //gun.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        DrawRay();
        //trigger 누를 때
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            TriggerShoot();
        }
    }

    public void TriggerShoot()
    {
        Ray ray = new Ray(firePos.transform.position, firePos.transform.forward);
        RaycastHit hitInfo;
        //크리스털에 총알이 맞았으면
        if(Physics.Raycast(ray, out hitInfo))
        {
            if(hitInfo.collider.tag == "crystal")
            {
                BulletImpact(hitInfo);
                Destroy(hitInfo.transform.gameObject);
                //크리스털 깨질 때 효과 넣을 거면 함수 생성해서 넣기
            }
        }

    }

    public void BulletImpact(RaycastHit hitInfo)
    {
        ParticleSystem ps = hitFx.GetComponent<ParticleSystem>();
        ps.Play();  //효과 생성
        //Instantiate(hitFx, hitInfo.point, Quaternion.identity); 
    }
    
    void DrawRay()
    {
        Debug.DrawRay(firePos.transform.position, firePos.transform.forward * 30, rayColor);
    }
}
