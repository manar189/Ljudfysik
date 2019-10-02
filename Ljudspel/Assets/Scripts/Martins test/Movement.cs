using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Game field
    float directionMax = 9;
    public float offsetX = 1f;
    public float offsetY = 1f;

    GameMaster master;
    // Start is called before the first frame update
    void Start()
    {
        //Finds GameMaster in scene.
        GameObject masterObj = GameObject.Find("Scripts in the scene");
        master = masterObj.GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make percentage to into range of choice.
        float moveHorizontal = master.pitchPercentage * directionMax;
        float moveVertical = master.intensityPercentage * directionMax;

        transform.position = new Vector2(offsetX + moveHorizontal, offsetY + moveVertical);
    }
}
