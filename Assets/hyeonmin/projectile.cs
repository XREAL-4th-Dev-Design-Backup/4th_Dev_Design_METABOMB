using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float life = 15f;
    public float speed = 1f;
    public Transform targetsky;
    public Transform targetfloor;
    public GameObject rangeObject;
    BoxCollider rangeCollider;



    // Start is called before the first frame update
    void Start()
    {
        //rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetsky.position, speed*Time.deltaTime);
        ///transform.position = Vector3.Lerp(transform.position, Return_RandomPos(), speed*Time.deltaTime);
        //Destroy(gameObject, life);
        //StartCoroutine(LerpTime());
        //transform.position = Vector3.MoveTowards(transform.position, targetfloor.position, speed*Time.deltaTime);
    }

    IEnumerator LerpTime(){
        yield return new WaitForSeconds(2f);
    }

  

    Vector3 Return_RandomPos(){
        Vector3 originPos = rangeObject.transform.position;
        float range_x = rangeCollider.bounds.size.x;
        float range_z = rangeCollider.bounds.size.z;

        range_x = Random.Range( (range_x / 2) * -1, range_x / 2);
        range_z = Random.Range( (range_z / 2) * -1, range_z / 2);
        Vector3 RandomPos = new Vector3(range_x, targetsky.position.y, range_z);

        Vector3 LandingPos = originPos + RandomPos;
        return LandingPos;
    }
}
