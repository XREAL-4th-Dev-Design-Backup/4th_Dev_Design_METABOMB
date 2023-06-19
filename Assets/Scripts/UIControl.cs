using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private bool leftIsGrabbed, rightIsGrabbed;
    public GameObject guide, reload;
    void Start()
    {
        DisReloadUI();
        StartCoroutine(GuideUI());
    }

    // Update is called once per frame
    void Update()
    {
        isGrabbedFalse();
        isGrabbedTrue();
        if ((leftIsGrabbed == false) && (rightIsGrabbed == false))
        {
            PopReloadUI();
        }
        if ((leftIsGrabbed == true) || (rightIsGrabbed == true))
        {
            DisReloadUI();
        }
    }
    protected IEnumerator GuideUI()
    {
        Color guidecolor = guide.GetComponent<RawImage>().color;
        guidecolor.a = 0.0f;
        guide.GetComponent<RawImage>().color = guidecolor;
        yield return new WaitForSeconds(5.0f);
        guidecolor.a = 1.0f;
        guide.GetComponent<RawImage>().color = guidecolor;
        yield return new WaitForSeconds(7.0f);
        guidecolor.a = 0.0f;
        guide.GetComponent<RawImage>().color = guidecolor;
    }
    public void PopReloadUI()
    {
        Color reloadcolor = reload.GetComponent<RawImage>().color;
        reloadcolor.a = 1.0f;
        reload.GetComponent<RawImage>().color = reloadcolor;
    }

    public void DisReloadUI()
    {
        Color reloadcolor = reload.GetComponent<RawImage>().color;
        reloadcolor.a = 0.0f;
        reload.GetComponent<RawImage>().color = reloadcolor;
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
