using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenTransition : MonoBehaviour
{
    public FadeScreen fadeScreen;

    void Start()
    {
        StartCoroutine(TransitionAfterDelay());
    }

    IEnumerator TransitionAfterDelay()
    {
        float delay = 2 * 60 + 31; // ��� �ð� (2�� 31��; 9089)
        yield return new WaitForSeconds(delay);

        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        // �� ��ȯ
        SceneManager.LoadScene("Cityscene");
    }
}

