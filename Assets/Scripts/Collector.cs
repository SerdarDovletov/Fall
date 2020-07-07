using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
            Destroy(collision.gameObject);
        if (collision.tag == "Coin")
            Destroy(collision.gameObject.transform.parent.gameObject);
        if (collision.tag == "DeepFall")
            Destroy(collision.gameObject.transform.parent.gameObject);
        if (collision.tag == "Magnet")
            Destroy(collision.gameObject.transform.parent.gameObject);
        if (collision.tag == "2x")
            Destroy(collision.gameObject);
    }
}
