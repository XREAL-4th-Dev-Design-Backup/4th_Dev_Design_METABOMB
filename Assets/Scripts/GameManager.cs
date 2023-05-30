using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public FadeIn fadeIn;
    public GameObject guitarPrefab;
    public GameObject ElectroHitPrefab;
    public GameObject portalPrefab;
    public GameObject MetaSphere, Supernova, MetabombTP, MetaBomb, Thunder, MetabombSurface, MagicShield, MagicSphere, ShockWave, GlowFlare;
    GameObject prev, electrohit, metasphere, portal, supernova, metabombtp, metabomb, thunder, metabombsurface, magicshield, magicsphere, shockwave, glowflare;
    Guitar guitar;

    void Start()
    {
        Player.GetComponent<PortalEnter>().enabled = false;
        StartCoroutine(GenerateMetaBomb());
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            GenerateGuitar();
            StartCoroutine(VibrateControll(0.2f, 0.5f, 0.5f, OVRInput.Controller.LTouch));
            StartCoroutine(VibrateControll(0.2f, 0.5f, 0.5f, OVRInput.Controller.RTouch));
        }
    }
    protected IEnumerator VibrateControll(float waitTime, float frequency, float amplitude, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(waitTime);
        OVRInput.SetControllerVibration(0, 0, controller);
    }

    protected IEnumerator GenerateMetaBomb()
    {
        yield return new WaitForSeconds(3.0f);
        metasphere = Instantiate(MetaSphere);
        supernova = Instantiate(Supernova);
        metabombtp = Instantiate(MetabombTP);
        metabomb = Instantiate(MetaBomb);
        thunder = Instantiate(Thunder);
        metabombsurface = Instantiate(MetabombSurface);
        magicshield = Instantiate(MagicShield);
        magicsphere = Instantiate(MagicSphere);
        prev = Instantiate(guitarPrefab);
    }
    protected IEnumerator GeneratePortal()
    {
        
        Destroy(prev);
        electrohit = Instantiate(ElectroHitPrefab);
        Destroy(electrohit, 1.0f);
        shockwave = Instantiate(ShockWave);

        yield return new WaitForSeconds(1.0f);

        
        supernova.GetComponent<Animator>().SetTrigger("Reduction");
        metabombtp.GetComponent<Animator>().SetTrigger("Reduction");
        metabomb.GetComponent<Animator>().SetTrigger("Reduction");
        Destroy(thunder);
        Destroy(metabombsurface);
        Destroy(magicshield);
        Destroy(magicsphere);
        Destroy(metasphere);

        yield return new WaitForSeconds(1.0f);

        supernova.GetComponent<Animator>().SetTrigger("Expension");
        metabombtp.GetComponent<Animator>().SetTrigger("Expension");
        portal = Instantiate(portalPrefab);
        glowflare = Instantiate(GlowFlare);

        yield return new WaitForSeconds(1.0f);
        Player.GetComponent<CityMove>().enabled = false;
        Player.GetComponent<PortalEnter>().enabled = true;

        yield return new WaitForSeconds(1.0f);
        fadeIn.FadeOut();
    }
    void GenerateGuitar()
    {
        Destroy(prev);
        prev = Instantiate(guitarPrefab);
    }

    public void Crashed()
    {
        StartCoroutine(GeneratePortal());
    }
}
