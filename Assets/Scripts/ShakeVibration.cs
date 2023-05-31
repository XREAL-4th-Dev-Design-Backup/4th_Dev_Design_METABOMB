using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeVibration : MonoBehaviour
{
    public OVRInput.Controller controller; // ������ ������ ��Ʈ�ѷ� (��: LeftController, RightController)
    public float shakeThreshold = 0.5f; // ��鸲 ���� �Ӱ谪
    public float vibrationDuration = 0.2f; // ���� ���� �ð�
    public float vibrationStrength = 0.5f; // ���� ����

    private bool isShaking = false; // ��鸲 ���� ����
    private float shakeStartTime; // ��鸲 ���� ���� �ð�

    private void Update()
    {
        Vector3 acceleration = Input.gyro.userAcceleration;
        float magnitude = acceleration.magnitude;

        if (magnitude > shakeThreshold)
        {
            if (!isShaking)
            {
                isShaking = true;
                shakeStartTime = Time.time;
                StartCoroutine(TriggerHaptic(vibrationDuration, vibrationStrength));
            }
        }
        else
        {
            isShaking = false;
        }
    }

    private System.Collections.IEnumerator TriggerHaptic(float duration, float strength)
    {
        OVRInput.SetControllerVibration(strength, strength, controller); // ���� ����
        yield return new WaitForSeconds(duration); // ������ �ð� ���� ���
        OVRInput.SetControllerVibration(0, 0, controller); // ���� ����
    }
}
