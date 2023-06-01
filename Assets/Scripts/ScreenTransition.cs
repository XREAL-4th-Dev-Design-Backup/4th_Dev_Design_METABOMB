using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    public FadeScreen fadeScreen;

    void Start()
    {

        // FadeScreen 오브젝트가 활성화되어 있는지 확인하고 비활성화되어 있다면 활성화한다.
        if (!fadeScreen.gameObject.activeSelf)
        {
            fadeScreen.gameObject.SetActive(true);
        }

        StartCoroutine(TransitionAfterDelay());
       
    }

    IEnumerator TransitionAfterDelay()
    {
        float delay = 2 * 60 + 31; // 대기 시간 (2분 31초; 9089)
        yield return new WaitForSeconds(delay);

        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // 씬 전환
        SceneManager.LoadScene("CityScence");
    }
}