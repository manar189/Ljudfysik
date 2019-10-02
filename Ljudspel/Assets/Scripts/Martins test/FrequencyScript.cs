using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyScript : MonoBehaviour
{
    //My microphone
    MicrophoneHandler microphone;

    public const int qSamples = 8192;
    public float threshold = 0.1f;  // minimum amplitude to extract pitch

    public float pitchValue;    // sound pitch - Hz

    private float[] spectrum;
    private float fSample;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        microphone = this.GetComponent<MicrophoneHandler>();
        audioSource = GetComponent<AudioSource>();

        spectrum = new float[qSamples];
        fSample = AudioSettings.outputSampleRate;
    }

    // Update is called once per game-tick
    void Update()
    {
        pitchValue = getFrequency();
        ///Debug.Log( "pitch value: " + pitchValue);
    }

    float getFrequency()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        // Creates harmonic product spectrum
        float[] hps = spectrum;
        for (int i = 0; i < qSamples / 2; ++i)
        {
            hps[i] += spectrum[i * 2];

            if (i < qSamples / 3)
            {
                hps[i] += +spectrum[i * 3];
            }

            if (i < qSamples / 4)
            {
                hps[i] += +spectrum[i * 4];
            }
        }

        // Finds the index of strongest base pitch
        float maxV = 0.0f;
        int maxN = 0;
        for (int i = 0; i < qSamples; i++)
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
}
