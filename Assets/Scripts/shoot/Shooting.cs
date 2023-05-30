using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject firePos;  //총알 생성 위치
    [SerializeField] private GameObject bulletFx; //총구 효과
    [SerializeField] private GameObject collisionFx; //크리스탈 충돌 효과
    private bool leftIsGrabbed, rightIsGrabbed;
    public bool isGunSetting;

    void Start()
    {
        leftIsGrabbed = false;
        rightIsGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGunSetting = GetComponent<SwitchAnimation>().isGunSetting;
        if (isGunSetting)   //main interaction일때만 작동
        {
            DrawRay();
            //trigger 누를 때
            //if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && rightIsGrabbed)    //오른손, grabbed 구별
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))    //오른손, grabbed 미구별
            {
                FireposFx();
                TriggerShoot();
                StartCoroutine(VibrateControll(1, 0.5f, 0.2f, OVRInput.Controller.RTouch));  //진동 1초
                //OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RTouch);    //진동 2초
            }

            //if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && leftIsGrabbed)    //왼손, grabbed 구별
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))    //왼손, grabbed 미구별
            {
                FireposFx();
                TriggerShoot();
                StartCoroutine(VibrateControll(1, 0.5f, 0.2f, OVRInput.Controller.LTouch));  //진동 1초
                //OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.LTouch);    //진동 2s
            }
        }

    }

    protected IEnumerator VibrateControll(float waitTime, float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(waitTime);
        OVRInput.SetControllerVibration(0, 0, controller);
    }

    public void TriggerShoot()
    {
        Ray ray = new Ray(firePos.transform.position, firePos.transform.forward);
        RaycastHit hitInfo;
        //크리스털에 총알이 맞았으면
        if(Physics.Raycast(ray, out hitInfo, 30))
        {
            if (hitInfo.collider.gameObject.tag == "crystal")
            {
                BulletImpact(hitInfo);
                hitInfo.collider.gameObject.GetComponent<CrystalBlow>().onCollisionEnter(); //격추 시 크리스탈 효과
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
        Instantiate(collisionFx, hitInfo.point, Quaternion.identity); 
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
