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
            
    }
    public void FadeOut()
    {
        OFade.FadeOut();
    }

}
