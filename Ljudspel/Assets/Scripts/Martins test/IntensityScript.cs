using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntensityScript : MonoBehaviour
{
    MicrophoneHandler microphone;

    public const int qSamples = 8192;
    public int sampleOffset = 300;

    public float intensityValue;

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
        //Get the data from audio source.
        audioSource.GetOutputData(spectrum, 0);

        //Calculate decibel.
        intensityValue = ComputeDB(spectrum, sampleOffset, qSamples);

        ///Debug.Log("Instenity value: " + intensityValue);
    }

    public static float ComputeRMS(float[] buffer, int offset, int length)
    {
        // sum of squares
        float sos = 0f;
        float val;

        if (offset + length > buffer.Length)
        {
            length = buffer.Length - offset;
        }

        for (int i = 0; i < length; i++)
        {
            val = buffer[offset];
            sos += val * val;
            offset++;
        }

        // return sqrt of average
        return Mathf.Sqrt(sos / length);
    }

    public static float ComputeDB(float[] buffer, int offset, int length)
    {
        float rms;

        rms = ComputeRMS(buffer, offset, length);

        // could divide rms by reference power, simplified version here with ref power of 1f.
        // will return negative values: 0db is the maximum.
        return 10 * Mathf.Log10(rms);
    }
}
