using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //public Vector3 abc;

    public List<Transform> points;
    public int randomIdx;



    // Start is called before the first frame update
    void Start()
    {
        //defaultTransform.transform.position = gameObject.transform.position;
        //abc = this.gameObject.transform.position;
        //abc.transform.Translate(this.transform.position);

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
        if ((OVRInput.GetDown(OVRInput.Button.Three))|| (OVRInput.GetDown(OVRInput.Button.Four)))
        {
            
            //Debug.Log("텔레포트");

            randomIdx = Random.Range(0, points.Count);

            gameObject.transform.position = points[randomIdx].transform.position;

        }

    }
}
