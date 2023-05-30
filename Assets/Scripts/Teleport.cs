using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //public Vector3 abc;

    public List<Transform> points;
    public int randomIdx;

    //public Transform startPos;

    public Vector3 startPos;



    // Start is called before the first frame update
    void Start()
    {
        //defaultTransform.transform.position = gameObject.transform.position;
        //abc = this.gameObject.transform.position;
        //abc.transform.Translate(this.transform.position);

        //startPos = gameObject.GetComponent<Transform>();
        startPos = this.gameObject.transform.position;


        GameObject group = GameObject.Find("Points");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(points);
            points.RemoveAt(0);
            //randomIdx = Random.Range(0, points.Count);

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetKeyDown(KeyCode.Space)
        //Input.GetKey(KeyCode.W)
        //(OVRInput.GetDown(OVRInput.Button.Three))|| (OVRInput.GetDown(OVRInput.Button.Four))
        if ((OVRInput.GetDown(OVRInput.Button.Three))|| (OVRInput.GetDown(OVRInput.Button.Four)))
        {
            
            //Debug.Log("텔레포트");

            randomIdx = Random.Range(0, points.Count);

            gameObject.transform.position = points[randomIdx].transform.position;

        }

        //(OVRInput.GetDown(OVRInput.Button.One))|| (OVRInput.GetDown(OVRInput.Button.Two))

        if((OVRInput.GetDown(OVRInput.Button.One))|| (OVRInput.GetDown(OVRInput.Button.Two))){
            //gameObject.transform.position=startPos.transform.position;
            gameObject.transform.position = startPos;
        }

    }
}