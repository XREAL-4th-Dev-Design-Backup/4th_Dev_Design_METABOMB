using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject firePos;  //총알 생성 위치
    [SerializeField] private GameObject bulletFx; //총알 효과
    [SerializeField] private Vector3 gunPosition;
    //[SerializeField] private GameObject gun;
    //private Transform swanPoint; //ray 선 끝 위치
    private bool leftIsGrabbed, rightIsGrabbed;

    void Start()
    {
        leftIsGrabbed = false;
        rightIsGrabbed = false;
        //gun.transform.position = gunPosition;
        //gunPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }

    // Update is called once per frame
    void Update()
    {
        //gun.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        DrawRay();
        //trigger 누를 때
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && rightIsGrabbed)    //오른손
        {
            FireposFx();
            TriggerShoot();
            OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RTouch);    //진동
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && leftIsGrabbed)    //왼손
        {
            FireposFx();
            TriggerShoot();
            OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.LTouch);    //진동
        }
    }

    public void TriggerShoot()
    {
        Ray ray = new Ray(firePos.transform.position, firePos.transform.forward);
        RaycastHit hitInfo;
        //크리스털에 총알이 맞았으면
        if(Physics.Raycast(ray, out hitInfo, 30))
        {
            //Debug.Log("hit!!!!!!!!!!!!!!:"+hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.tag == "crystal")
            {
                BulletImpact(hitInfo);
                Destroy(hitInfo.transform.gameObject);
                //크리스털 깨질 때 효과 넣을 거면 함수 생성해서 넣기
            }
        }

    }

    public void FireposFx()
    {
        ParticleSystem ps = bulletFx.GetComponent<ParticleSystem>();
        ps.Play();  //효과 생성
    }

    public void BulletImpact(RaycastHit hitInfo)
    {
        //ParticleSystem ps = bulletFx.GetComponent<ParticleSystem>();
        //ps.Play();  //효과 생성
        Instantiate(bulletFx, hitInfo.point, Quaternion.identity); 
    }
    
    void DrawRay()
    {
        Debug.DrawRay(firePos.transform.position, firePos.transform.forward * 30, Color.red);
    }
    
    public void isGrabbedTrue()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            leftIsGrabbed = true;
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            rightIsGrabbed = true;
        }
    }

    public void isGrabbedFalse()
    {
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            leftIsGrabbed = false;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            rightIsGrabbed = false;
        }
    }   
}
