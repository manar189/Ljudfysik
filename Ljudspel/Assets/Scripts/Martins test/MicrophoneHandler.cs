using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] //Lägger till Audio source component automatiskt. (funkar inte??)
public class MicrophoneHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public string selectedDevice;
    public bool useMicrophone = true;

    int fSample;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //Hittar en component av Audio source om den finns.
        fSample = AudioSettings.outputSampleRate;

        if (!useMicrophone)
            return;

        if (Microphone.devices.Length > 0)
        {
            // Prints all microphones found in the device.
            for (int i = 0; i < Microphone.devices.Length; i++)
            {
                Debug.Log(Microphone.devices[i].ToString());
            }

            selectedDevice = Microphone.devices[0].ToString();
            audioSource.clip = Microphone.Start(selectedDevice, true, 1, fSample);
            audioSource.loop = true;

            while (!(Microphone.GetPosition(null) > 0))
            {
            }
            audioSource.Play();
        }
    }

}
