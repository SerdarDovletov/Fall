using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnetMove : MonoBehaviour
{
    public bool canMove;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove) transform.position = Vector3.MoveTowards(transform.position,PlayerController.instance.gameObject.transform.position,0.45f);
    }

    private void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "MagnetTrigger") canMove = true;
    }
}
