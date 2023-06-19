using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : MonoBehaviour
{
    private bool leftIsGrabbed, rightIsGrabbed;
    GameObject gameManager;
    public UIControl uIControl;

    void Start()
    {
        leftIsGrabbed = false;
        rightIsGrabbed = false;
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "metabomb")
        {
            gameManager.GetComponent<GameManager>().Crashed();
            OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(0.1f, 0.1f, OVRInput.Controller.LTouch);
        }
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
