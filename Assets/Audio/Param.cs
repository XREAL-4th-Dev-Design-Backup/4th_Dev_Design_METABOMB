using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Param : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultplier;
    private bool _useBuffer = true;
    Material _material;




    //public float threshold=0;
    //public Transform targetsky;
    //public GameObject crystal;
    //public float speed=1;





    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {   

        if(_useBuffer){
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._bandBuffer[_band]*_scaleMultplier) * _startScale, transform.localScale.z);
            Color _color = new Color (AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band]);
            _material.SetColor("Emission Color", _color);
        }
        else{
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._freqBand[_band]*_scaleMultplier)+_startScale, transform.localScale.z);
            Color _color = new Color (AudioPeer._audioBand[_band], AudioPeer._audioBand[_band], AudioPeer._audioBand[_band]);
            _material.SetColor("Emission Color", _color);
        
        }
        /*
        Debug.Log((AudioPeer._freqBand[_band]*_scaleMultplier)+_startScale);
        float val = (AudioPeer._freqBand[_band]*_scaleMultplier)+_startScale;
        if(val > threshold){
            crystal.transform.position = Vector3.Lerp(crystal.transform.position, targetsky.position, speed*Time.deltaTime);
        }
        */
    
        
    }
}
