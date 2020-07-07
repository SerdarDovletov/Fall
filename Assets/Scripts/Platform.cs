using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{
    private bool rotateright, rotateleft,moving,moveright;
    private float initialPos;
    void Start()
    {
        int level = GameObject.Find("GameController").GetComponent<GameController>().level;
        if(Random.Range(0,2) == 1)  moveright = true;
        if (level >= 3 && Random.Range(0, (level <= 50 ? 30 - (int)(level * 0.42f) : 9)) == 0) rotateleft = true; 
        else if (level >= 3 && Random.Range(0, (level <= 50 ? 30 - (int)(level * 0.42f) : 9)) == 1) rotateright = true;
        if (level >= 3 && Random.Range(0, (level <= 50 ? 30 - (int)(level * 0.48f) : 6)) == 2) { moving = true; initialPos = transform.position.x; }
    }

    
    void Update()
    {
        if (rotateleft) transform.Rotate(new Vector3(0, 0, Random.Range(1f, 2f)));
        if (rotateright) transform.Rotate(new Vector3(0, 0, Random.Range(-1f, -2f)));

        if (moving && moveright)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * Random.Range(1f, 2.5f), transform.position.y, transform.position.z);
            if (transform.position.x >= initialPos + 1) moveright = !moveright;
        }
        else if (moving && !moveright)
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * Random.Range(1f, 2.5f), transform.position.y, transform.position.z);
            if (transform.position.x <= initialPos - 1) moveright = !moveright;
        }
    }
}
    