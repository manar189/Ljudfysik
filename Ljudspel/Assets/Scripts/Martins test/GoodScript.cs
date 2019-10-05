using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodScript : MonoBehaviour
{

    public GameObject player;
    public GameMaster1 master1;
    public GameMaster2 master2;
    public float radius = 1;
    public int scene = 1;
    // Start is called before the first frame update
    void Start()
    {
        GameObject masterObj = GameObject.Find("Scripts in the scene");
        if(scene == 1)
        {
            master1 = masterObj.GetComponent<GameMaster1>();
        }
        else if(scene == 2)
        {
            master2 = masterObj.GetComponent<GameMaster2>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scene == 1)
        {
            if ((this.transform.position - player.transform.position).magnitude < radius)
            {
                Debug.Log("HIT GOOD OBJECT!");
                master1.changeScore(true);
                master1.createNewScorePoint();
                Destroy(gameObject);
            }
        }
        else if (scene == 2)
        {
            if ((this.transform.position - player.transform.position).magnitude < radius)
            {
                Debug.Log("HIT GOOD OBJECT!");
                master2.changeScore(true);
                master2.createNewScorePoint();
                master2.createNewBadScorePoint();
                Destroy(gameObject);
            }
        }

    }
}
