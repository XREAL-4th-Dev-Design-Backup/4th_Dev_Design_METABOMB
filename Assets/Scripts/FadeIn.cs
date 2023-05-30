using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class FadeIn : MonoBehaviour

{
    public GameObject CenterEyeObj;
    OVRScreenFade OFade;
    void Start()
    {
        OFade = CenterEyeObj.transform.GetComponent<OVRScreenFade>();
        //OVRScreenFade ��ũ��Ʈ�� ������Ʈ�� �����´�
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OFade.FadeOut();
        }
        if (Input.GetMouseButtonDown(1))
        {
            OFade.FadeIn();
        }
    }

}
