using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private int nextScorePosition, nextSpawnPosition, platformNumber;
    private float multiplyScoreTimerAmount,magnetTimerAmount,turboTimerAmount, fallforcetolerp, tapTime, StartPos, MoveVelocity, initialVelocity, zoomsize;
    private Touch touch,touch2;
    private bool deepFallCanJump, deepfallsoundcanreduce,multiplyingScore, magneting,firstInst, startFirst, firstend,canDestroyPlatform;

    [SerializeField]private Animator DeepFallEndingAnim;
    public GameObject rocket,rateButtonSettings, instagramButton,addedScore, rateUsButton,NoAdsButtonMenu,buyCoinsPanel,challengesButton, freegiftObject, topborder, deepfallobject,deepfallparticle,magnetIcon,multiplyScoreIcon,salut,inappPurchaseBtn, plus, coin, explosion, finish, borders, scoreObject, PauseButton, platform;
    public Text scoreText, GameCoins;
    private  GameObject[] UIS,currentRockets;
    public CircleCollider2D Collider;
    public Rigidbody2D mybody;
    [SerializeField] private Slider levelSlider;
    public int platformBreakAddScore,ScorePosition, jumpsInOneGame, coinsInOneGame;
    public float initfallforce,sensitivity, diePositionY,diePositionX, fallForce;
    public bool buyingCoins,dead, deepfall ;
    public AudioSource PlayerAudio,BGMAudioSource;
    public AudioClip magnetSound,win,die, coinClip, jump;
    public GameController gc;
    public GameObject magnetTrigger,PlatformBreakParticle,CoinAddedPos, CoinAdded;
    public Image multiplyScoreTimer,TurboTimer,magnetTimer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        deepfallsoundcanreduce = false;
        buyingCoins = false;
        deepfall = false;
        startFirst = true;
        deepFallCanJump = true;
        dead = true;
        magneting = false;
        firstInst = false;
        multiplyingScore = false;
        canDestroyPlatform = false;
        UIS = GameObject.FindGameObjectsWithTag("UI");
        zoomsize = 10;
        GameCoins.text = gc.coins.ToString();
        MoveVelocity = 0;
        nextScorePosition = -8;
        ScorePosition = 0;
        scoreText.text = (gc.scoreCount + ScorePosition).ToString();
        nextSpawnPosition = -40;
        if (Random.Range(0, 4) == 1 && !gc.noAdsAnddoubleCoins) NoAdsButtonMenu.SetActive(true);
        if (Random.Range(0, 4) == 1 && !gc.rated) rateUsButton.SetActive(true);
    }

    void Update()
    {
        if (deepfallsoundcanreduce) deepfallparticle.GetComponent<AudioSource>().volume -= Time.deltaTime * 0.40f;
        magnetTrigger.transform.position = transform.position;
        if (deepfall) {
            if (turboTimerAmount > 0){
                turboTimerAmount -= Time.deltaTime * (100f/(gc.turboTime + 1));
                TurboTimer.fillAmount = turboTimerAmount / 100;
            }
            else TurboTimer.transform.parent.gameObject.SetActive(false);
        }
        if (magneting) {
            if (magnetTimerAmount > 0){
                magnetTimerAmount -= Time.deltaTime * 12.5f;
                magnetTimer.fillAmount = (float)(magnetTimerAmount / ((float)gc.magnetTime/100f))/100;
            }
            else DeactivateMagnet();
        }
        if (multiplyingScore) {
            if (multiplyScoreTimerAmount > 0){
                multiplyScoreTimerAmount -= Time.deltaTime * 12.5f;
                multiplyScoreTimer.fillAmount = (float)(multiplyScoreTimerAmount / ((float)gc.xTime/100f))/100;
            }
            else DeactivateMultiplyScore();
        }

        //First Platform instance
        if (dead == false && firstInst == false)
        {
            for (int i = -8; i >= -36; i -= 4)
            {
                for (float j = Random.Range(-22f, -18f); j <= 22; j += (gc.level <= 50 ? Random.Range(5f, 8f - (gc.level / 50)) : Random.Range(5f, 7f))) 
                {
                    GameObject newplat = Instantiate(platform, new Vector3(j, i, 8), Quaternion.identity) as GameObject;
                    if (i != -8 && ((gc.level - 1) % 4 != 0 || gc.level != 2))
                    {
                        if (Random.Range(0, 5) == 0 && j >= -19 && j <= 19) Instantiate(coin, new Vector3(Random.Range((float)j - 3f, (float)j + 3f), i + 2, 0), Quaternion.identity);
                        else if (Random.Range(0, 80) == 0 && j >= -19 && j <= 19) Instantiate(deepfallobject, new Vector3(Random.Range((float)j - 3f, (float)j + 3f), i + 2f, 0), Quaternion.identity);
                        else if (Random.Range(0, 100) == 0 && j >= -19 && j <= 19) Instantiate(magnetIcon, new Vector3(Random.Range((float)j - 3f, (float)j + 3f), i + 2f, 0), Quaternion.identity);
                        else if (Random.Range(0, 100) == 0 && j >= -19 && j <= 19) Instantiate(multiplyScoreIcon, new Vector3(Random.Range((float)j - 3f, (float)j + 3f), i + 2f, 0), Quaternion.identity);
                    }
                Vector3 temp = newplat.transform.localScale;
                    temp.x = Random.Range(1f, 3f);
                    newplat.transform.localScale = temp;
                }
                platformNumber++;
                if (gc.level >= 4 && Random.Range(0, (gc.level <= 50 ? 20 - (int)(gc.level * 0.2f) : 10)) == 1) Instantiate(rocket, new Vector3(Random.Range(-19f, 19f), nextSpawnPosition, 0), Quaternion.identity);
            }
            firstInst = true;
        }

        //Instantiate patforms
        if (transform.position.y < nextSpawnPosition + 40 && platformNumber < 19 + (gc.level) && dead == false)
        {
            for (float j = Random.Range(-22f, -18f); j <= 22; j += (gc.level <= 50 ? Random.Range(5f, 8f - (gc.level / 50)) : Random.Range(5f, 7f)))
            {
                GameObject newplat = Instantiate(platform, new Vector3(j, nextSpawnPosition, 8), Quaternion.identity);
                if (Random.Range(0, 5) == 0 && j >= -19 && j <= 19) Instantiate(coin, new Vector3(Random.Range((float)j - 3f, (float)j + 3f), nextSpawnPosition + 2, 0), Quaternion.identity);
                else if (Random.Range(0, 80) == 0 && j >= -19 && j <= 19) Instantiate(deepfallobject, new Vector3(Random.Range((float)j - 3f, (float)j + 3f), nextSpawnPosition + 2f, 0), Quaternion.identity);
                else if (Random.Range(0, 100) == 0 && j >= -19 && j <= 19) Instantiate(magnetIcon, new Vector3(Random.Range((float)j - 3f, (float)j + 3f), nextSpawnPosition + 2f, 0), Quaternion.identity);
                else if (Random.Range(0, 100) == 0 && j >= -19 && j <= 19) Instantiate(multiplyScoreIcon, new Vector3(Random.Range((float)j - 3f, (float)j + 3f), nextSpawnPosition + 2f, 0), Quaternion.identity);
                Vector3 temp = newplat.transform.localScale;
                temp.x = Random.Range(1f, 3f);
                newplat.transform.localScale = temp;
            }
            platformNumber++;
            nextSpawnPosition -= 4;
            if(gc.level >= 4 && Random.Range(0,(gc.level <= 50 ? 20 - (int)(gc.level * 0.2f) : 10)) == 1) Instantiate(rocket,new Vector3(Random.Range(-19f,19f),nextSpawnPosition,0),Quaternion.identity);
        }

        //InstansiateFinish
        if (platformNumber == 19 + (gc.level) && gc.finished == false)
        {
            Instantiate(finish, new Vector3(2.26f, nextSpawnPosition, 0), Quaternion.identity);
            gc.finished = true;
        }

        if (mybody.velocity.y < fallForce) mybody.velocity = new Vector2(mybody.velocity.x, fallForce);

        borders.transform.position = new Vector3(0, Camera.main.transform.position.y, 0);

        //Display score
        if (transform.position.y < nextScorePosition && dead == false)
        {
            if(!multiplyingScore) ScorePosition += gc.level;
            else ScorePosition += (gc.level * 2);
            nextScorePosition -= 4;
            scoreText.text = (gc.scoreCount + ScorePosition).ToString();
            levelSlider.value += 1;
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && startFirst) { StartGame(); startFirst = false;}

        if ((gc.win || gc.gameover) && Input.GetKeyDown(KeyCode.Space))
        {
            if (gc.musicOn) gc.musictime = gc.bgmusic.time;
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
            gc.save();
        }

    }
    private void FixedUpdate()
    {

        //Camera zoom
        if (mybody.velocity.y < initfallforce + 1 && dead == false) Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, -mybody.velocity.y + zoomsize, 0.01f);
        else if (mybody.velocity.y > initfallforce + 1 && dead == false) Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 15, 0.01f);
        else if (dead && !gc.isFirstJumping) Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 15, 0.05f);

        //Camera follow
        if (dead == false)
        {
            //Camera follow Y
            Vector3 camPos = Camera.main.transform.position;
            if (transform.position.y - 5 < camPos.y)
            {
                float playerPositionY = transform.position.y - 5;
                float positionY = Mathf.LerpUnclamped(camPos.y, playerPositionY, 0.1f);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, positionY, Camera.main.transform.position.z);
            }
            //Camera follow X
            float playerPosition = transform.position.x;
            float position = Mathf.LerpUnclamped(camPos.x, playerPosition, 0.1f);
            Camera.main.transform.position = new Vector3(position, Camera.main.transform.position.y - Time.deltaTime * 4.5f, Camera.main.transform.position.z);
            float initialborderpos = -16.9f + (15.22f * ((float)Screen.width / (float)Screen.height - 0.4611111f));
            float borderpos = initialborderpos + ((Camera.main.orthographicSize - 15) * ((float)Screen.width / (float)Screen.height));
            //borders limits
            if (Camera.main.transform.position.x < borderpos) Camera.main.transform.position = new Vector3(borderpos, Camera.main.transform.position.y, -10);
            if (Camera.main.transform.position.x > -borderpos + 0.2f) Camera.main.transform.position = new Vector3(-borderpos + 0.2f    , Camera.main.transform.position.y, -10);
        }
        fallForce = Mathf.Lerp(fallForce, fallforcetolerp, 0.05f);
    }
    private void LateUpdate()
    {
        //Move by tapping
        if (SplashScreen.isFinished && Input.touchCount > 0 && !gc.shopping && !buyingCoins && EventSystem.current.currentSelectedGameObject == null && !gc.challenge && !gc.settings && !gc.paused)
        {
            if (Input.touchCount >= 1) touch = Input.GetTouch(0);
            if (Input.touchCount == 2) touch2 = Input.GetTouch(1);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (startFirst)
                    {
                        StartGame(); startFirst = false; 
                    }
                    if (touch.fingerId == 0)
                    {
                        initialVelocity = mybody.velocity.x;
                        StartPos = touch.position.x;
                    }
                    tapTime = Time.time;
                    break;

                case TouchPhase.Ended:
                    tapTime = Time.time - tapTime;
                    if (tapTime <= 0.2f && firstend)
                    {
                        if (gc.win || gc.gameover)
                        {
                            if (gc.musicOn) gc.musictime = gc.bgmusic.time;
                            SceneManager.LoadScene(0);
                            Time.timeScale = 1;
                            gc.save();
                        }
                        if (gc.isFirstJumping)
                        {
                            gc.firstJumpFinger.SetActive(false);
                            gc.firstJumpText.SetActive(false);
                            gc.isFirstJumping = false;
                            dead = false;
                            gc.PauseButton.SetActive(true);
                            mybody.isKinematic = false;
                        }
                        if (!dead && !gc.win && deepFallCanJump && !gc.paused)
                        {
                            if (gc.vibrateOn) Vibration.VibratePop();
                            jumpsInOneGame++;
                            if (gc.audioOn && !gc.paused) PlayerAudio.PlayOneShot(jump);
                            mybody.velocity = new Vector2(mybody.velocity.x, 0);
                            mybody.angularVelocity = 0f;
                            mybody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                        }
                    }
                    firstend = true;
                    break;
            }
            if (Input.touchCount == 2)
                switch (touch2.phase)
                {
                    case TouchPhase.Began:
                        tapTime = Time.time;
                        break;

                    case TouchPhase.Ended:
                        tapTime = Time.time - tapTime;
                        if (tapTime <= 0.2f)
                        {
                            if (gc.win || gc.gameover)
                            {
                                if (gc.musicOn) gc.musictime = gc.bgmusic.time;
                                SceneManager.LoadScene(0);
                                Time.timeScale = 1;
                                gc.save();
                            }
                            if (gc.isFirstJumping)
                            {
                                gc.firstJumpFinger.SetActive(false);
                                gc.firstJumpText.SetActive(false);
                                gc.isFirstJumping = false;
                                dead = false;
                                gc.PauseButton.SetActive(true);
                                mybody.isKinematic = false;
                            }
                            if (!dead && !gc.win && deepFallCanJump && !gc.paused)
                            {
                                if (gc.vibrateOn) Vibration.VibratePop();
                                jumpsInOneGame++;
                                if (gc.audioOn && !gc.paused) PlayerAudio.PlayOneShot(jump);
                                mybody.velocity = new Vector2(mybody.velocity.x, 0);
                                mybody.angularVelocity = 0f;
                                mybody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                            }
                        }
                        break;
                }
            if (dead == false && touch.fingerId == 0)
            {
                float x = ((float)1) / (((float)Screen.width / (float)2) / (float)sensitivity) * (touch.position.x - StartPos) / (float)100;
                MoveVelocity = x * 100;
                mybody.velocity = new Vector2(MoveVelocity + initialVelocity, mybody.velocity.y);
            }
        }
        transform.Rotate(new Vector3(0, 0, -(mybody.velocity.x)));

    }

    public void StartGame()
    {
        if (gc.firstJump) StartCoroutine(gc.FirstJump());
        levelSlider.gameObject.SetActive(true);
        freegiftObject.SetActive(false);
        if (gc.level == 1) fallForce = -6.5f;
        else if (gc.level == 2) fallForce = -6.55f;
        else if (gc.level == 3) fallForce = -6.6f;
        else if (gc.level == 4) fallForce = -6.65f;
        else if (gc.level == 5) fallForce = -6.7f;
        else if (gc.level == 6) fallForce = -6.75f;
        else if (gc.level == 7) fallForce = -6.8f;
        else if (gc.level == 8) fallForce = -6.85f;
        else if (gc.level == 9) fallForce = -6.9f;
        else if (gc.level == 10) fallForce = -6.95f;
        else if (gc.level <= 150) fallForce = -7f - (gc.level / 100f);
        else fallForce = -8.5f;
        fallforcetolerp = fallForce;
        initfallforce = fallForce;
        levelSlider.maxValue = 18 + (gc.level);

        if (mybody.isKinematic == true) mybody.isKinematic = false;
        mybody.velocity = new Vector2(0, fallForce);

        GameCoins.gameObject.transform.parent.gameObject.SetActive(true);
        dead = false;
        if (rateUsButton.activeInHierarchy) rateUsButton.SetActive(false);
        if (NoAdsButtonMenu.activeInHierarchy) NoAdsButtonMenu.SetActive(false);
        scoreObject.SetActive(true);
        PauseButton.SetActive(true);
        jumpsInOneGame = 0;
        challengesButton.SetActive(false);

        for (int i = 0; i < UIS.Length; i++)
            UIS[i].SetActive(false);
        plus.SetActive(false);
        inappPurchaseBtn.SetActive(false);
        if (gc.level == 2 || ((gc.level - 1) % 4 == 0 && gc.level != 1)) for (float j = -22f; j <= 23; j += 3) Instantiate(deepfallobject, new Vector3(j, -6, 0), Quaternion.identity);
    }
    private IEnumerator DeactivateDeepFall()
    {
        yield return new WaitForSeconds(gc.turboTime);
        DeepFallEndingAnim.Play("DeepFallEnding");
        if (gc.level == 1) fallforcetolerp = -6.5f;
        else if (gc.level == 2) fallforcetolerp = -6.55f;
        else if (gc.level == 3) fallforcetolerp = -6.6f;
        else if (gc.level == 4) fallforcetolerp = -6.65f;
        else if (gc.level == 5) fallforcetolerp = -6.7f;
        else if (gc.level == 6) fallforcetolerp = -6.75f;
        else if (gc.level == 7) fallforcetolerp = -6.8f;
        else if (gc.level == 8) fallforcetolerp = -6.85f;
        else if (gc.level == 9) fallforcetolerp = -6.9f;
        else if (gc.level == 10) fallforcetolerp = -6.95f;
        else if (gc.level <= 150) fallforcetolerp = -7f - (gc.level / 100f);
        else fallforcetolerp = -8.5f;
        deepfallsoundcanreduce = true;
        deepFallCanJump = true;
        yield return new WaitForSeconds(1.2f);
        deepfall = false;
        canDestroyPlatform = false;
        deepfallparticle.SetActive(false);
        Collider.isTrigger = false;
        gameObject.GetComponent<TrailRenderer>().enabled = true;
        platformBreakAddScore = gc.level;
    }
    private void DeactivateMultiplyScore() {
        multiplyingScore = false;
        multiplyScoreTimer.transform.parent.gameObject.SetActive(false);
    }
    private void DeactivateMagnet() { 
        magnetTrigger.SetActive(false);
        magneting = false;
        magnetTimer.transform.parent.gameObject.SetActive(false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Platform") && !dead)
        {
            currentRockets = GameObject.FindGameObjectsWithTag("rocket");
            for (int i = 0; i < currentRockets.Length; i++) currentRockets[i].GetComponent<rocketfollow>().DestroyRocket();
            currentRockets = null;

            DeactivateTimers();
            if (gc.audioOn) PlayerAudio.PlayOneShot(die);
            StartCoroutine(CameraShake.instance.Shake(.15f, 0.2f));
            Instantiate(explosion, transform.position, Quaternion.identity);
            if (!gc.diedOnce && gc.level >= 3)
            {
                gc.diedOnce = true;
                gc.save();
                diePositionY = transform.position.y;
                diePositionX = transform.position.x;
                StartCoroutine(gc.ActivateWatchVideoPanel());
            }
            else GameController.instance.Gameover();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dead)
        {
            if ((collision.gameObject.tag == "Platform"))
            {
                currentRockets = GameObject.FindGameObjectsWithTag("rocket");
                for (int i = 0; i < currentRockets.Length; i++) currentRockets[i].GetComponent<rocketfollow>().DestroyRocket(); 
                currentRockets = null;
                
                DeactivateTimers();
                if (gc.audioOn) PlayerAudio.PlayOneShot(die);
                StartCoroutine(CameraShake.instance.Shake(.15f, 0.2f));
                Instantiate(explosion, transform.position, Quaternion.identity);
                if (!gc.diedOnce && gc.level >= 3)
                {
                    gc.diedOnce = true;
                    gc.save();
                    diePositionY = transform.position.y;
                    diePositionX = transform.position.x;
                    StartCoroutine(gc.ActivateWatchVideoPanel());
                }
                else GameController.instance.Gameover();
            }
            
            else if (collision.gameObject.tag == "Border")
            {
                currentRockets = GameObject.FindGameObjectsWithTag("rocket");
                for (int i = 0; i < currentRockets.Length; i++) currentRockets[i].GetComponent<rocketfollow>().DestroyRocket();
                currentRockets = null;
                DeactivateTimers();
                if (gc.audioOn) PlayerAudio.PlayOneShot(die);
                StartCoroutine(CameraShake.instance.Shake(.15f, 0.2f));
                Instantiate(explosion, transform.position, Quaternion.identity);
                gc.Gameover();
            }
        }
    }
    public void BuyCoins() {
        if (gc.audioOn) gc.Select.Play();
        if (!buyingCoins)
        {
            buyCoinsPanel.SetActive(true);
            buyingCoins = true;
        }
        else {
            buyCoinsPanel.SetActive(false);
            buyingCoins = false;
        }
    }
    public void DeactivateTimers() {
        DeactivateMagnet();
        DeactivateMultiplyScore();
        deepfall = false;
        canDestroyPlatform = false;
        deepfallparticle.SetActive(false);
        Collider.isTrigger = false;
        gameObject.GetComponent<TrailRenderer>().enabled = true;
        TurboTimer.transform.parent.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (!dead)
        {
            if (target.tag == "rocket" && canDestroyPlatform && gc.vibrateOn) Vibration.VibratePeek();

            if (target.tag == "rocket" && !canDestroyPlatform)
            {
                currentRockets = GameObject.FindGameObjectsWithTag("rocket");
                for (int i = 0; i < currentRockets.Length; i++) currentRockets[i].GetComponent<rocketfollow>().DestroyRocket();
                currentRockets = null;
                DeactivateTimers();
                if (gc.audioOn) PlayerAudio.PlayOneShot(die);
                StartCoroutine(CameraShake.instance.Shake(.15f, 0.2f));
                Instantiate(explosion, transform.position, Quaternion.identity);
                if (!gc.diedOnce && gc.level >= 3)
                {
                    gc.diedOnce = true;
                    gc.save();
                    diePositionY = transform.position.y;
                    diePositionX = transform.position.x;
                    StartCoroutine(gc.ActivateWatchVideoPanel());
                }
                else GameController.instance.Gameover();
            }
            if (target.tag == "2x") {
                if(gc.audioOn) PlayerAudio.PlayOneShot(magnetSound);
                Destroy(target.gameObject);
                multiplyingScore = true;
                multiplyScoreTimer.transform.parent.gameObject.SetActive(true);
                multiplyScoreTimerAmount = gc.xTime;
            }
            if (target.tag == "Magnet") {
                if(gc.audioOn) PlayerAudio.PlayOneShot(magnetSound);
                magnetTimer.transform.parent.gameObject.SetActive(true);
                Destroy(target.gameObject);
                magnetTrigger.SetActive(true);
                magnetTimerAmount = gc.magnetTime;
                magneting = true;
                gc.magnetUsed++;
            }
            if (target.tag == "Platform" && canDestroyPlatform) {
                if (gc.vibrateOn) Vibration.VibratePeek();
                GameObject instantiatedPlatformBreakParticle = Instantiate(PlatformBreakParticle,target.transform.position,PlatformBreakParticle.transform.rotation) as GameObject;
                if (!gc.audioOn) instantiatedPlatformBreakParticle.GetComponent<AudioSource>().Stop();
                Destroy(instantiatedPlatformBreakParticle,2);
                Destroy(target.gameObject);
                gc.brokenPlatforms++;
                if(multiplyingScore) platformBreakAddScore += gc.level * 2;
                else platformBreakAddScore += gc.level;
                ScorePosition += platformBreakAddScore;
                GameObject instScore = Instantiate(addedScore,target.gameObject.transform.position,Quaternion.identity) as GameObject;
                instScore.gameObject.transform.GetChild(0).GetComponent<Text>().text = "+" + platformBreakAddScore;
                scoreText.text = (gc.scoreCount + ScorePosition).ToString();
            }
            if (target.tag == "Border")
            {
                currentRockets = GameObject.FindGameObjectsWithTag("rocket");
                for (int i = 0; i < currentRockets.Length; i++) currentRockets[i].GetComponent<rocketfollow>().DestroyRocket();
                currentRockets = null;
                DeactivateTimers();
                Collider.isTrigger = false;
                if (gc.audioOn) PlayerAudio.PlayOneShot(die);
                StartCoroutine(CameraShake.instance.Shake(.15f, 0.2f));
                Instantiate(explosion, transform.position, Quaternion.identity);
                gc.Gameover();
            }
            if (target.tag == "DeepFall")
            {
                deepfallsoundcanreduce = false;
                TurboTimer.transform.parent.gameObject.SetActive(true);
                turboTimerAmount = 100;
                gc.deepDiveUsed++;
                deepFallCanJump = false;
                canDestroyPlatform = true;
                gameObject.GetComponent<TrailRenderer>().enabled = false;
                deepfall = true;
                fallforcetolerp = initfallforce - (2f + (((float)Screen.height/(float)Screen.width) - 1.33f) / 0.2075f);
                Collider.isTrigger = true;
                Destroy(target.gameObject.transform.parent.gameObject);
                deepfallparticle.SetActive(true);
                if (gc.audioOn) deepfallparticle.GetComponent<AudioSource>().Play();
                deepfallparticle.GetComponent<AudioSource>().time = 0;
                deepfallparticle.GetComponent<AudioSource>().volume = 0.6f;
                mybody.velocity = new Vector2(mybody.velocity.x,-15);
                DeepFallEndingAnim.Play("DeepFallIdle");
                StopCoroutine("DeactivateDeepFall");
                StartCoroutine("DeactivateDeepFall");
            }

            if (target.tag == "Finish")
            {
                currentRockets = GameObject.FindGameObjectsWithTag("rocket");
                for (int i = 0; i < currentRockets.Length; i++) currentRockets[i].GetComponent<rocketfollow>().DestroyRocket();
                TurboTimer.transform.parent.gameObject.SetActive(false);
                magnetTimer.transform.parent.gameObject.SetActive(false);
                multiplyScoreTimer.transform.parent.gameObject.SetActive(false);
                float leftsalutx = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 6, 0, 0)).x;
                float rightsalutx = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - Screen.width / 6, 0, 0)).x;
                float middlesalutx = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).x;
                Instantiate(salut, new Vector3(leftsalutx, nextScorePosition, 0), Quaternion.Euler(270, -90, -90));
                Instantiate(salut, new Vector3(middlesalutx, nextScorePosition, 0), Quaternion.Euler(270, -90, -90));
                Instantiate(salut, new Vector3(rightsalutx, nextScorePosition, 0), Quaternion.Euler(270, -90, -90));
                gc.Win();
                PlayerAudio.volume = 0.4f;
                if (gc.audioOn) PlayerAudio.PlayOneShot(win);
            }

            else if (target.tag == "Coin")
            {
                Destroy(target.gameObject.transform.parent.gameObject);
                if (gc.audioOn) PlayerAudio.PlayOneShot(coinClip);
                coinsInOneGame++;
                if (gc.noAdsAnddoubleCoins) gc.coins += 2;
                else gc.coins++;
                GameCoins.text = "x" + gc.coins;
                GameObject instAddedCoins = Instantiate(CoinAdded,CoinAddedPos.transform.position, Quaternion.identity,GameCoins.transform.parent) as GameObject;
                if (gc.noAdsAnddoubleCoins) instAddedCoins.GetComponent<Text>().text = "+" + 2.ToString();
            }
        }
    }
}