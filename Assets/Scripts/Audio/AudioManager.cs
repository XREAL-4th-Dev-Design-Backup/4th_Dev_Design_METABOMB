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
        soundSource = GetComponent<AudioSource>();
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

                if ((intro_music[i].name=="글리치 효과음 2")|| (intro_music[i].name=="글리치 효과음 4"))
                {
                    print("하이라이트구간");
                    gameObject.GetComponent<BPM>().enabled = true;
                    cylinders.SetActive(true);
                }

                
                

                if(i== intro_music.Length)
                {
                    break;
                }
                i += 1;

            }

        }
        
    }


}
