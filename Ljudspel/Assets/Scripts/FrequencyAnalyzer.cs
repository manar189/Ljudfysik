using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyAnalyzer : MonoBehaviour
{

    public const int qSamples = 1024;
    public float refValue = 0.1f; // RMS value for 0 dB
    public float threshold = 0.02f;  // minimum amplitude to extract pitch
    public float rmsValue;   // sound level - RMS
    public float dbValue;    // sound level - dB
    public double pitchValue;    // sound pitch - Hz

    private float[] samples;
    private float[] spectrum;
    private float fSample;
    AudioSource audioSource;


    void AnalyzeSound()
    {
        //audioSource.GetSpectrumData(samples, 0, FFTWindow.Hamming);
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float maxV = 0.0f;
        int maxN = 0;

        
        for (int i = 0; i<qSamples; ++i)
        { // find max 
            if (spectrum[i] > maxV && spectrum[i] > threshold)
            {
                maxV = spectrum[i];
                maxN = i; // maxN is the index of max
                //Debug.Log("maxV: "+maxV);
                //Debug.Log("maxN: "+maxN);
            }
        }

        double freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < qSamples - 1)
        { // interpolate index using neighbours
            double dL = spectrum[maxN - 1] / spectrum[maxN];
            double dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5 * (dR * dR - dL * dL);
        }

        pitchValue = freqN * (fSample / 2) / qSamples; // convert index to frequency

        Debug.Log("PitchValue: " + pitchValue);

    }



    void Start()
    {
        samples = new float[qSamples];
        spectrum = new float[qSamples];
        fSample = AudioSettings.outputSampleRate;
        audioSource = GetComponent<AudioSource>();
    }





    void Update()
    {
        AnalyzeSound();
        
    }
    

}

/*
 var qSamples: int = 1024;  // array size
 var refValue: float = 0.1; // RMS value for 0 dB
 var threshold = 0.02;      // minimum amplitude to extract pitch
 var rmsValue: float;   // sound level - RMS
 var dbValue: float;    // sound level - dB
 var pitchValue: float; // sound pitch - Hz
 
 private var samples: float[]; // audio samples
 private var spectrum: float[]; // audio spectrum
 private var fSample: float;
 
 function Start () {
     samples = new float[qSamples];
     spectrum = new float[qSamples];
     fSample = AudioSettings.outputSampleRate;
 }
 
 function AnalyzeSound(){
     audio.GetOutputData(samples, 0); // fill array with samples
     var i: int;
     var sum: float = 0;
     for (i=0; i < qSamples; i++){
         sum += samples[i]*samples[i]; // sum squared samples
     }
     rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
     dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
     if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
     // get sound spectrum
     audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
     var maxV: float = 0;
     var maxN: int = 0;
     for (i=0; i < qSamples; i++){ // find max 
         if (spectrum[i] > maxV && spectrum[i] > threshold){
             maxV = spectrum[i];
             maxN = i; // maxN is the index of max
         }
     }
     var freqN: float = maxN; // pass the index to a float variable
     if (maxN > 0 && maxN < qSamples-1){ // interpolate index using neighbours
         var dL = spectrum[maxN-1]/spectrum[maxN];
         var dR = spectrum[maxN+1]/spectrum[maxN];
         freqN += 0.5*(dR*dR - dL*dL);
     }
     pitchValue = freqN*(fSample/2)/qSamples; // convert index to frequency
 }
 
 var display: GUIText; // drag a GUIText here to show results
 
 function Update () {
     if (Input.GetKeyDown("p")){
         audio.Play();
     }
     AnalyzeSound();
     if (display){ 
         display.text = "RMS: "+rmsValue.ToString("F2")+
         " ("+dbValue.ToString("F1")+" dB)\n"+
         "Pitch: "+pitchValue.ToString("F0")+" Hz";
     }
 }
*/
