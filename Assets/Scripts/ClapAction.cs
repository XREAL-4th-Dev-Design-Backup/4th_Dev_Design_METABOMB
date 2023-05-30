using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapAction : MonoBehaviour
{
    public bool isGunSetting;
    void Start()
    {

    }
    // Update is called once per frame
    private void Update()
    {
        isGunSetting = GetComponent<SwitchAnimation>().isGunSetting;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bong" && !isGunSetting)       //normal interaction일때만 작동
        {
            StartCoroutine(VibrateControll(0.1f, 0.5f, 0.5f, OVRInput.Controller.LTouch));
            StartCoroutine(VibrateControll(0.1f, 0.3f, 0.2f, OVRInput.Controller.RTouch));
        }
    }
    protected IEnumerator VibrateControll(float waitTime, float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(waitTime);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
