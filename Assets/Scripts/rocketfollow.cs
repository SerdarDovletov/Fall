using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketfollow : MonoBehaviour
{
    Transform player;
    public GameObject rocketExplosion;
    private PlayerController pc;
    private GameController gc;
    public float flyForce;
    private bool candecrease, decrease;
    public SpriteRenderer spr;
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        flyForce = -pc.initfallforce + 1;
        player = GameObject.Find("Sphere").gameObject.transform;
        StartCoroutine(DestroyRocketEnum());
        if (!gc.audioOn) transform.GetChild(0).GetComponent<AudioSource>().Stop();
        decrease = true;
    }

    [System.Obsolete]
    void Update()
    {
        if (decrease) spr.color = new Color(1, 0.118f, 0.118f, spr.color.a - (2f / 255f));
        if (spr.color.a <= 0.25 && decrease) decrease = false;
        if (!decrease) spr.color = new Color(1, 0.118f, 0.118f, spr.color.a + (2f / 255f));
        if (spr.color.a >= 0.5 && !decrease) decrease = true;

        if (candecrease)
        {
            flyForce -= Time.deltaTime * 4;
            transform.GetChild(0).GetComponent<ParticleSystem>().emissionRate -= 5;
        }
        transform.Translate(Vector3.up * Time.deltaTime * flyForce);
        Vector3 vectorToTarget = player.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "rocket" || collision.tag == "Player") DestroyRocket();
    }
    IEnumerator DestroyRocketEnum()
    {
        yield return new WaitForSeconds(8);
        candecrease = true;
        yield return new WaitForSeconds(1);
        DestroyRocket();
    }
    public void DestroyRocket()
    {
        Destroy(gameObject);
        GameObject rocketEx = Instantiate(rocketExplosion, transform.position, Quaternion.identity) as GameObject;
        if (gc.audioOn) rocketEx.GetComponent<AudioSource>().Play();
        Destroy(rocketEx, 2);
    }
}
