using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject firePos;  //�Ѿ� ���� ��ġ
    [SerializeField] private GameObject hitFx; //�Ѿ� �¾Ҵٴ� ȿ��  
    [SerializeField] private Vector3 gunPosition;
    //[SerializeField] private GameObject gun;
    //private Transform swanPoint; //ray �� �� ��ġ
    Color rayColor = Color.red; //ray �� ����

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
        //trigger ���� ��
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            TriggerShoot();
        }
    }

    public void TriggerShoot()
    {
        Ray ray = new Ray(firePos.transform.position, firePos.transform.forward);
        RaycastHit hitInfo;
        //ũ�����п� �Ѿ��� �¾�����
        if(Physics.Raycast(ray, out hitInfo))
        {
            if(hitInfo.collider.tag == "crystal")
            {
                BulletImpact(hitInfo);
                Destroy(hitInfo.transform.gameObject);
                //ũ������ ���� �� ȿ�� ���� �Ÿ� �Լ� �����ؼ� �ֱ�
            }
        }

    }

    public void BulletImpact(RaycastHit hitInfo)
    {
        ParticleSystem ps = hitFx.GetComponent<ParticleSystem>();
        ps.Play();  //ȿ�� ����
        //Instantiate(hitFx, hitInfo.point, Quaternion.identity); 
    }
    
    void DrawRay()
    {
        Debug.DrawRay(firePos.transform.position, firePos.transform.forward * 30, rayColor);
    }
}
