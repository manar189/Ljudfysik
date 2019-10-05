using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Game field
    float directionMax = 9;
    public float offsetX = 1f;
    public float offsetY = 1f;
    float speed = 0.4f;

    Vector2 oldPosition = new Vector2(1f,1f);

    GameMaster master;
    // Start is called before the first frame update
    void Start()
    {
        //Finds GameMaster in scene.
        GameObject masterObj = GameObject.Find("Scripts in the scene");
        master = masterObj.GetComponent<GameMaster>();
        transform.position = new Vector2(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Make percentage to into range of choice.
        float moveHorizontal = master.pitchPercentage * directionMax;
        float moveVertical = master.intensityPercentage * directionMax;

        Vector2 newPosition = new Vector2(offsetX + moveHorizontal, offsetY + moveVertical);
        transform.position = transform.position + new Vector3(Mathf.Clamp(newPosition.x - oldPosition.x, -speed, speed), Mathf.Clamp(newPosition.y - oldPosition.y, -speed, speed), 0);

        if (transform.position.x < 1)
        {
            transform.position = new Vector2(1f, transform.position.y);
        }
        if (transform.position.y < 1)
        {
            transform.position = new Vector2(transform.position.x, 1f);
        }

        oldPosition = transform.position;
    }
}
