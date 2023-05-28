using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{

    AudioSource _audioSource;
    public float[] _samples = new float[512];
    public static float[] _freqBand = new float[8];
    public static float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];

    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];



    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GerSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
    }

    void GerSpectrumAudioSource(){

       _audioSource.GetSpectrumData(_samples,0,FFTWindow.Blackman);
    }

    void CreateAudioBands(){
        for(int i=0; i<8 ; i++){
            if(_freqBand[i] > _freqBandHighest[i]){
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }



    void BandBuffer()
    {
        for(int g=0; g<8; g++){
            if(_freqBand[g] > _bandBuffer[g]){
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }
            if(_freqBand[g] < _bandBuffer[g]){
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }



    void MakeFrequencyBands(){

        int count = 0;


        for (int i=0; i<8; i++){

            float average=0;
            int sampleCount = (int)Mathf.Pow(2, i) *2;

            if(i ==7){
                sampleCount +=2;

            }

            for (int j=0; j<sampleCount; j++){
                average+=_samples[count]*(count+1);
                    count++;

            }

            average/=count;
            _freqBand[i]= average* 10;
        }

    }
}
