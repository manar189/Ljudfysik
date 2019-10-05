using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster2 : MonoBehaviour
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

    public GameObject scorePoints;
    public GameObject goodObject;
    public GameObject badObject;
    public GameObject player;

    int goodScore;
    int badScore;
    private Movement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        goodScore = 0;
        badScore = 0;
        createNewScorePoint();
        createNewBadScorePoint();
        movementScript = player.GetComponent<Movement>();
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

    public void changeScore(bool goodFlag)
    {

        movementScript.resetPosition();


        //scorePoints.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
        if (!goodFlag) //We hit a badBoy
        {
            badScore++;
            if (badScore == 3)
            {
                SceneManager.LoadScene(4); //End scene
            }

        }
        else // Hit a good boy
        {
            if (goodScore == 3)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }

    public void createNewScorePoint()
    {


        if (goodScore == 0)
        {
            Instantiate(goodObject, new Vector3(8, 2, 0), Quaternion.identity);
            


            Instantiate(badObject, new Vector3(6.5f, 7, 0), Quaternion.identity);
            Instantiate(badObject, new Vector3(10, 4, 0), Quaternion.identity);
            Instantiate(badObject, new Vector3(1.5f, 8, 0), Quaternion.identity);
            goodScore++;
            return;
        }

        if (goodScore == 1)
        {
            Instantiate(badObject, new Vector3(5, 1.5f, 0), Quaternion.identity);

            Instantiate(goodObject, new Vector3(1, 5, 0), Quaternion.identity);
            goodScore++;
            return;
        }

        if (goodScore == 2)
        {
            Instantiate(goodObject, new Vector3(9.5f, 9.5f, 0), Quaternion.identity);
            Instantiate(badObject, new Vector3(3, 4, 0), Quaternion.identity);

            goodScore++;
            return;
        }


    }

    public void createNewBadScorePoint()
    {
        return;
    }

    
}
