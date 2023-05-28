using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이 스크립트가 실행이 되면 공이 발사된다.

public class CrystalCtrl : MonoBehaviour
{
    public float shotVelocity;
    public float shotAngle;

    private Rigidbody ballRB2D;
    private bool isGround = true;
    private bool isCenter = false;
    private float totalTime = 0f;
    
    Gameobject cylindes = transform.find("Cylinders").gameObject;
    Gameobject cylinder[8] = cylinders.GetAllChilds();





    void Start()
    {
        ballRB2D = GetComponent<Rigidbody>();
        StartCoroutine(ShotBall());
        //ballRB2D.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }
    void Update()
    {

    }
    IEnumerator ShotBall()
    {
        //Debug.Log("=== Simulation ===");

        isGround = false;
        // 공의 각도를 설정

        ballRB2D.AddForce(Vector3.up * 15, ForceMode.Impulse);
        //transform.right = new Vector3(Mathf.Cos(shotAngle * Mathf.Deg2Rad), Mathf.Sin(shotAngle * Mathf.Deg2Rad));
        // 설정된 각도 shotVelocity 속도로 발사
        ballRB2D.velocity = transform.right * shotVelocity;

        totalTime = 0f;
        while (true)
        {
            yield return null;
            // 착지하면 while문 종료
            if (isGround) break;
            // 작치자히 전까지는 계속 시간 측정
            totalTime += Time.deltaTime;

            // y축의 속도의 절대값이 0.1보다 작을때 isCenter == false일때(딱 한번만 발동)
            if (Mathf.Abs(ballRB2D.velocity.y) < 0.1f && !isCenter)
            {

                isCenter = true;
                //Debug.Log("CenterHeight: " + transform.position.y);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D _col)
    {
        // 그라운드에 착지
        if (isGround == false)
        {
            isGround = true;
            // 더이상 움지기지 않게 하기 위해 속도는 0
            ballRB2D.velocity = Vector2.zero;
            //총 걸린 시간
            Debug.Log("Totaltime: " + totalTime);
            // 초기위치가 -8에서 시작 했기 때문에 +8로 보정 (총 이동거리)
            Debug.Log("TotalMeter: " + (transform.position.x + 8));

            Verification();
        }
    }

    private void Verification()
    {
        // 공식을 적용한 계산값 확인.
        Debug.Log("=== Verification ===");

        // 총걸린 시간은 2t
        // 2 * V* sin(theta)/g 
        float totalTime = 2 * shotVelocity * Mathf.Sin(shotAngle * Mathf.Deg2Rad) / 9.81f;
        // 최고 높이 (V*sin(theta))^2 / (2g)
        float centerHeight = Mathf.Pow(shotVelocity * Mathf.Sin(shotAngle * Mathf.Deg2Rad), 2) / (2 * 9.81f); //
        // 총 날아간 거리 2*V^2*sin(theta)*cos(theta)/g = > 2sin(theta)cos(theta) == sing(2theta) => v^2/g*sin(2*theta)
        float totalMeter = Mathf.Pow(shotVelocity, 2) / 9.81f * Mathf.Sin(2 * shotAngle * Mathf.Deg2Rad); // 

        Debug.Log("Totaltime: " + totalTime);
        Debug.Log("CenterHeight: " + centerHeight);
        Debug.Log("TotalMeter: " + totalMeter);

        // Simulation 값과 Verification 결과의 오차가 발생하는 이유
        // Project Settings - Physics 2D의 속성값에 따라 계산 결과가 다르기 때문이다.
    }
}
