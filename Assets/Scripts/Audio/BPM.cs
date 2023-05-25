using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bpm에 맞게 크리스탈을 생성해주는 스크립트를 불러오는 함수이다.


/*
    여기서 Cylinders아래에 있는 Cylinder들을 불러오고 그 아래에 있느 Spawn 위치에 생성을 하는데 
    InstantiateCrystal이 크리스탈을 생성하는 그 스크립트니까.

    InstantiateCrystal이 Spawn 위치에 생성하고 CrystalCtrl을 활성화시키고 (사실상 Instatiate랑 CrystalCtrl이랑 합치면 될 것 같긴 함) InstantiateCrystal 스크립트를 수정하고
    이 스크립트에서는 Cylinders아래에 있는 Cylinder들을 불러오면 되지 않을까?

*/

public class BPM : MonoBehaviour
{
     //ScaleCube moveNote;
    InstantiateCrystal insCrystal;
    public GameObject cylinder;
    //public List<Transform> cylinder;

    float musicBPM = 129f;
    float stdBPM = 60f;

    int muscicTempo = 4;
    int stdTempo = 4;

    float tikTime = 0;
    float nextTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //moveNote = GameObject.Find("Cube").GetComponent<ScaleCube>();
        insCrystal = cylinder.GetComponent<InstantiateCrystal>();
    }

    // Update is called once per frame
    void Update()
    {
        tikTime = (stdBPM / musicBPM) * (muscicTempo / stdTempo); //초당 재생수

        nextTime += Time.deltaTime;

        if (nextTime > tikTime)
        {
            StartCoroutine(PlayTikTime(tikTime));
            nextTime = 0;
        }

        float temp = (musicBPM / stdBPM) * (musicBPM / stdTempo);
        Debug.Log(temp);
    }

    IEnumerator PlayTikTime(float tikTime)
    {
        Debug.Log(nextTime);
        insCrystal.InsCrystal();
        yield return new WaitForFixedUpdate();
    }
}
