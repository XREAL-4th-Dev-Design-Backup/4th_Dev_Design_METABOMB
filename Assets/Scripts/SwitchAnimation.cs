using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimation : MonoBehaviour
{
    private Animator animator;
    public GameObject ShootingLine;
    public bool isGunSetting;
    void Awake()
    {
        isGunSetting = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //when main interaction start
    public void onHighlight()
    {
        animator.SetBool("isGun", true);
        animator.SetTrigger("initGun");
        ShowGun();
    }

    //when main interaction end
    public void offHighlight()
    {
        animator.SetBool("isGun", false);
        HideGun();
    }

    private void ShowGun()
    {
        ShootingLine.SetActive(true);
        isGunSetting = true;
    }

    private void HideGun()
    {
        ShootingLine.SetActive(false);
        isGunSetting = false;
    }


}
