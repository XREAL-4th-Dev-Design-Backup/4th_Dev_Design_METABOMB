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
        //festival�� ��ȯ�� �ڵ����� Start() ����
        animator = GetComponent<Animator>();
        tf = GetComponent<Transform>();
        //for test (Debuging) - �ڵ� �����
        Invoke("HideUI", delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        tf.position = controller.position;
       
        //for test (Debuging) - ���� ����
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
