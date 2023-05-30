using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerUI : MonoBehaviour
{
    private Animator animator;
    public Transform controller;
    private Transform tf;
    public float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        //festival씬 전환시 자동으로 Start() 실행
        animator = GetComponent<Animator>();
        tf = GetComponent<Transform>();
        //for test (Debuging) - 자동 사라짐
        Invoke("HideUI", delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        tf.position = controller.position;
       
        //for test (Debuging) - 수동 조절
        //if (OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    HideUI();
        //}
    }

    public void HideUI()
    {
        animator.SetBool("onUI", false);
    }
}
