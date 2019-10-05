using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyAnalyzer : MonoBehaviour
{
    public GameObject cursor;

    public const int qSamples = 8192;
    public float threshold = 0.1f;  // minimum amplitude to extract pitch

    public float pitchValue;    // sound pitch - Hz

    private float[] spectrum;
    private float fSample;
    AudioSource audioSource;


    float getFrequency()
    {
      
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        // Creates harmonic product spectrum
        float[] hps = spectrum; 
        for (int i = 0; i < qSamples/2; ++i)
        {
            hps[i] = Mathf.Abs( hps[i] * spectrum[i * 2]);

            if (i < qSamples / 3)
            {
                hps[i] = Mathf.Abs(hps[i] * spectrum[i * 3]);
            }

            if (i < qSamples / 4)
            {
                hps[i] = Mathf.Abs(hps[i] * spectrum[i * 4]);
            }

            if (i < qSamples / 5)
            {
                hps[i] = Mathf.Abs(hps[i] * spectrum[i * 5]);
            }
        }

        // Finds the index of strongest base pitch
        float maxV = 0.0f;
        int maxN = 0;
        for (int i = 0; i<qSamples; i++)
        { 
            if (hps[i] > maxV && hps[i] > threshold)
            {
                maxV = hps[i];
                maxN = i;
            }
        }

        // Interpolate index using neighbours
        float freqN = maxN;
        if (maxN > 0 && maxN < qSamples - 1)
        { 
            float dL = hps[maxN - 1] / hps[maxN];
            float dR = hps[maxN + 1] / hps[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }

        // Returns frequency
        return freqN * (fSample / 2) / qSamples;
    }

    void Start()
    {
        spectrum = new float[qSamples];
        fSample = AudioSettings.outputSampleRate;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        pitchValue = getFrequency();
        Debug.Log(pitchValue);
    }
}
