using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Game field
    float directionMax = 9;
    public float offsetX = 1f;
    public float offsetY = 1f;
    public float speed = 0.1f;

    Vector2 oldPosition = new Vector2(1f, 1f);

    GameMaster1 master1;
    GameMaster2 master2;
    public int scene = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (scene == 1)
        {
            //Finds GameMaster in scene.
            GameObject masterObj = GameObject.Find("Scripts in the scene");
            master1 = masterObj.GetComponent<GameMaster1>();
            transform.position = new Vector2(1, 1);
        }
        else
        {
            //Finds GameMaster in scene.
            GameObject masterObj = GameObject.Find("Scripts in the scene");
            master2 = masterObj.GetComponent<GameMaster2>();
            transform.position = new Vector2(1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal;
        float moveVertical;
        if (scene == 1)
        {
            // Make percentage to into range of choice.
            moveHorizontal = master1.pitchPercentage * directionMax;
            moveVertical = master1.intensityPercentage * directionMax;
        }
        else
        {
            // Make percentage to into range of choice.
            moveHorizontal = master2.pitchPercentage * directionMax;
            moveVertical = master2.intensityPercentage * directionMax;
        }


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


    public void resetPosition()
    {

        transform.position = new Vector2(1f, 1f);
    }
}
