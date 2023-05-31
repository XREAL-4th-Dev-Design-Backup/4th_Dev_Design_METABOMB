using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeVibration : MonoBehaviour
{
    public OVRInput.Controller controller; // 진동을 제어할 컨트롤러 (예: LeftController, RightController)
    public float shakeThreshold = 0.5f; // 흔들림 감지 임계값
    public float vibrationDuration = 0.2f; // 진동 지속 시간
    public float vibrationStrength = 0.5f; // 진동 강도

    private bool isShaking = false; // 흔들림 감지 여부
    private float shakeStartTime; // 흔들림 감지 시작 시간

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
        OVRInput.SetControllerVibration(strength, strength, controller); // 진동 시작
        yield return new WaitForSeconds(duration); // 지정된 시간 동안 대기
        OVRInput.SetControllerVibration(0, 0, controller); // 진동 종료
    }
}
