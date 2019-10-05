using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    // Values for the game.
    public float intensityPercentage;
    public float pitchPercentage;

    public float intensity;
    public float intensityMax = 300f;
    public float intensityMin= 0;

    public float pitch;
    public float pitchMax = 300;
    public float pitchMin = 0;

    int score = 0;
    public GameObject scorePoints;
    public GameObject goodObject;
    public GameObject badOject;

    int fuling = 1;

    // Start is called before the first frame update
    void Start()
    {
        createNewScorePoint();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the values for pitch and instenity.
        intensity = this.GetComponent<IntensityScript>().intensityValue;
        pitch = this.GetComponent<FrequencyScript>().pitchValue;

        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        intensity = Mathf.Clamp(intensity, intensityMin, intensityMax);

        pitchPercentage = getPercentage(pitch, pitchMin, pitchMax); //Happy math :)
        intensityPercentage = 1 - getPercentage(-intensity, -intensityMax, -intensityMin); //Sad math :(
    }

    float getPercentage(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }

    public void changeScore(int a)
    {
        score += a;
        scorePoints.GetComponent<UnityEngine.UI.Text>().text = score.ToString();

        if(score == 10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

        }
    }

    public void createNewScorePoint()
    {
        if(fuling == 1)
        {
            Instantiate(goodObject, new Vector3(10, 10, 0), Quaternion.identity);
            fuling++;
            return;
        }

        if (fuling == 2)
        {
            Instantiate(goodObject, new Vector3(4, 7, 0), Quaternion.identity);
            fuling++;
            return;
        }

        if (fuling == 3)
        {
            Instantiate(goodObject, new Vector3(7, 10, 0), Quaternion.identity);
            fuling = 1;
            return;
        }
    }
}
