using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBlow : MonoBehaviour
{
    [SerializeField] private GameObject blowVersion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCollisionEnter()
    {
        Destroy(gameObject);
        Instantiate(blowVersion, transform.position, transform.rotation);
    }
}
