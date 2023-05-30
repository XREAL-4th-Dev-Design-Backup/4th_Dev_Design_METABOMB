using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] intro_music;
    AudioSource soundSource;

    public GameObject cylinders;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = this.gameObject.GetComponent<AudioSource>();
        StartCoroutine("Playlist");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Playlist(){

        soundSource.clip = intro_music[0];
        soundSource.Play();

        int i = 1;

        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!soundSource.isPlaying)
            {
                soundSource.clip = intro_music[i];
                soundSource.Play();
                cylinders.SetActive(false);
                gameObject.GetComponent<BPM>().enabled = false;

                //print(intro_music[i].name);

                //(intro_music[i].name=="part2")|| (intro_music[i].name=="part4")
                //(intro_music[i].name=="글리치 효과음 2")|| (intro_music[i].name=="글리치 효과음 4")
                //(soundSource.clip==intro_music[1]|| soundSource.clip==intro_music[3])

                if (soundSource.clip==intro_music[1]|| soundSource.clip==intro_music[3])
                {
                    //응원봉 -> 총 전환
                    GameObject.Find("04_Stickv7_left").GetComponent<SwitchAnimation>().onHighlight();
                    GameObject.Find("04_Stickv7_right").GetComponent<SwitchAnimation>().onHighlight();
                    print("하이라이트구간");
                    gameObject.GetComponent<BPM>().enabled = true;
                    cylinders.SetActive(true);
                }
                //총 -> 응원봉 전환
                GameObject.Find("04_Stickv7_left").GetComponent<SwitchAnimation>().offHighlight();
                GameObject.Find("04_Stickv7_right").GetComponent<SwitchAnimation>().offHighlight();







                if (i== intro_music.Length)
                {
                    break;
                }
                i += 1;

            }

        }
        
    }


}
