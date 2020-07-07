using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCoins : MonoBehaviour
{
    public GameController gc;
    public PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buy400Coins() {
        if (gc.audioOn) gc.Select.Play();
    }  

    public void Buy1000Coins() { 
        if (gc.audioOn) gc.Select.Play();
        
    }

    public void Buy5000Coins() { 
        if (gc.audioOn) gc.Select.Play();
        
    }
    public void Buy10000Coins() { 
        if (gc.audioOn) gc.Select.Play();
        
    }

    public void BuyNoAdsAndDoubleCoins() { 
        if (gc.audioOn) gc.Select.Play();
        
    }
}
