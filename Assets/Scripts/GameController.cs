using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public PlayerController pc;
    private GameData data;

    public Image[] buyCoinscoins, buyballcoins;
    public GameObject[] themeTicks, ballTicks, selectBallButtons, Locks, colourinfobuttons;
    public Button[] BallBuyButtons, ColourButtons;
    public Sprite[] Balls;
    [SerializeField] private GameObject EarnCoinsPanel,NoAdsButton, freegiftRewardvalue, challengesPanel, Player, watchVideoCoins, leftBorder, rightBorder, menuCoins, shop, winTap, levelPassed, PausePanel;
    [SerializeField] private GameObject watchVideoPanel, musicOff, vibrateOff, audioOff, pausemusicoff, pauseaudiooff, pausevibrateoff;
    [SerializeField] private Animator newRecord, FreeGiftAnim, PlayerAnim;
    [SerializeField] private Image freegiftRewardcoin, watchVideoTimer;
    public AudioSource rewardAudio, newBallAudio, Select, bgmusic;
    [SerializeField] private Slider Sens, pauseSens;
    [SerializeField] private Text magnetUpdatePriceText, turboUpdatePriceText, xUpdatePriceText,turboTimeText, magnetTimeText, xTimeText,canCollectChallengesNumber, shopThemesCounts, shopBallCounts, freegifttext, coloursinfoValue, sensitivityValue, menusensitivityvalue, currentLevel, MenuHighScore, currentScore, colourInfoText, levelSliderLevelValue;
    public Material trail;
    private bool newthemeUnlocked, resumed, isStartedFirstTime, watchVideo;
    private int magnetUpdatePrice, turboUpdatePrice, xUpdatePrice, currentbgmindex, unlockedThemes, unlockedBalls, selectedBall, gamesPlayed, gameswithoutjumps, gameswithoutcoins, onehighscore, currentColor;
    private float currentTimeAmount;
    public float magnetTime, xTime, freegiftlefttime, initialfreegifttime, musictime;
    public GameObject firstJumpFinger, firstJumpText, MenuSettingsPanel, newThemeAdded, PauseButton;
    public AudioClip[] BGM;

    public bool rated, noAdsAnddoubleCoins, isFirstJumping,firstJump,diedOnce, challenge, paused, musicOn, vibrateOn, audioOn, settings, shopping, finished, win, gameover;
    public int bestScore,canShowInterstitial,turboTime,  magnetUsed, brokenPlatforms, deepDiveUsed, jumps, scoreCount, level, coins;

    private bool instantiatedColour,instagram,newrecord, canCollectFreeGift, greenColour, blueColour, redColour, yellowColour, pinkColour, purpleColour, goluboyColour, limeColour, orangeColour, adamColour, yapykColour;
    private bool[] BoughtBalls;

    [SerializeField] private GameObject reachLevelRewardCollectButton, jumpRewardCollectButton, winwithoutcoinsRewardCollectButton, winwithoutjumpsRewardCollectButton, useturboRewardCollectButton, gamesplayedRewardCollectButton, unlockballsRewardCollectButton, magnetUsedRewardCollectButton, BreakPlatformsRewardCollectButton;
    [SerializeField] private Text reachlevelchallengeTask, jumpchallengeTask, winwithoutcoinschallengetask, winwithoutjumpschallengetask, useturbochallengetask, gamesplayedchallengetask, unlockballschallengetask, magnetUsedchallengetask, BreakPlatformschallengetask;
    [SerializeField] private Text reachLevelChallengeCurrentStat, jumpChallengeCurrentStat, winwithoutcoinsChallengeCurrentStat, winwithoutjumpsChallengeCurrentStat, useturboChallengeCurrentStat, gamesplayedChallengeCurrentStat, unlockballsChallengeCurrentStat, magnetUsedChallengeCurrentStat, BreakPlatformsChallengeCurrentStat;
    [SerializeField] private Text reachLevelChallengeCollectCoinsValue, jumpChallengeCollectCoinsValue, winwithoutcoinsChallengeCollectCoinsValue, winwithoutjumpsChallengeCollectCoinsValue, useturboChallengeCollectCoinsValue, gamesplayedChallengeCollectCoinsValue, unlockballsChallengeCollectCoinsValue, magnetUsedChallengeCollectCoinsValue, BreakPlatformsChallengeCollectCoinsValue;
    [SerializeField] private GameObject ReachLevelChalengeDone, JumpChalengeDone, winwithoutcoinsChalengeDone, winwithoutjumpsChalengeDone, useturboChalengeDone, gamesplayedChalengeDone, unlockballsChalengeDone, magnetUsedChalengeDone, BreakPlatformsChalengeDone;

    private int[] currentReachLevelChallenge;
    private int currentReachLevelChallengeIndex, canCollectReachLevelChallenge, currentReachLevelReward;

    private int[] currentJumpChallenge;
    private int currentJumpChallengeIndex, canCollectJumpChallenge, currentJumpReward;

    private int[] currentwinwithoutcoinsChallenge;
    private int currentwinwithoutcoinsChallengeIndex, canCollectwinwithoutcoinsChallenge, currentwinwithoutcoinsReward;

    private int[] currentwinwithoutjumpsChallenge;
    private int currentwinwithoutjumpsChallengeIndex, canCollectwinwithoutjumpsChallenge, currentwinwithoutjumpsReward;

    private int[] currentuseturboChallenge;
    private int currentuseturboChallengeIndex, canCollectuseturboChallenge, currentuseturboReward;

    private int[] currentgamesplayedChallenge;
    private int currentgamesplayedChallengeIndex, canCollectgamesplayedChallenge, currentgamesplayedReward;

    private int[] currentunlockballsChallenge;
    private int currentunlockballsChallengeIndex, canCollectunlockballsChallenge, currentunlockballsReward;

    private int[] currentMagnetUsedChallenge;
    private int currentMagnetUsedChallengeIndex, canCollectMagnetUsedChallenge, currentMagnetUsedReward;

    private int[] currentBreakPlatformsChallenge;
    private int currentBreakPlatformsChallengeIndex, canCollectBreakPlatformsChallenge, currentBreakPlatformsReward;

    void Awake()
    {
        Application.targetFrameRate = 120;
        newthemeUnlocked = false;
        InstansiateVariables();
        if (level >= 30 && !goluboyColour) { goluboyColour = true; newthemeUnlocked = true; }
        if (jumps >= 100 && !adamColour) { adamColour = true; newthemeUnlocked = true; }
        if (magnetUsed >= 25 && !redColour) { redColour = true; newthemeUnlocked = true; }
        if (gameswithoutjumps >= 10 && !purpleColour) { purpleColour = true; newthemeUnlocked = true; }
        if (gameswithoutcoins >= 10 && !pinkColour) { pinkColour = true; newthemeUnlocked = true; }
        if (deepDiveUsed >= 50 && !yapykColour) { yapykColour = true; newthemeUnlocked = true; }
        if (brokenPlatforms >= 250 && !limeColour) { limeColour = true; newthemeUnlocked = true; }
        if (gamesPlayed >= 100 && !orangeColour) { orangeColour = true; newthemeUnlocked = true; }
        if (canShowInterstitial == 0) { canShowInterstitial = 6; }
        canShowInterstitial--;
        save();

        finished = false;
        if (instance == null) instance = this;
        unlockedThemes = 1;
    }
    void Start()
    {
        bestScore = data.GetHighScore();
        bgmusic.clip = BGM[currentbgmindex];
        if (newrecord) { newRecord.Play("CanCollectFreeGiftAnim"); newrecord = false; save(); };
        InstantiateChallenges();
        InstantiateUnlockBallsChallenge();
        InstantiateColors();
        instantiatedColour = true;
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);

        if (freegiftlefttime <= 0)
        {
            canCollectFreeGift = true;
            FreeGiftAnim.Play("CanCollectFreeGiftAnim");
            freegifttext.gameObject.SetActive(false);
            freegiftRewardvalue.SetActive(true);
        }


        if (newthemeUnlocked) { newThemeAdded.SetActive(true); StartCoroutine(newThemeAddedEnd()); }
        shopBallCounts.text = unlockedBalls + "/49";
        for (int i = 1; i <= 48; i++) if (BoughtBalls[i]) BallBuyButtons[i].gameObject.SetActive(false);
        for (int i = 0; i <= 48; i++) if (BoughtBalls[i] && i != selectedBall) selectBallButtons[i].SetActive(true);
        Player.GetComponent<SpriteRenderer>().sprite = Balls[selectedBall];
        ballTicks[selectedBall].SetActive(true);
        currentTimeAmount = 60;
        onehighscore = 0;
        if (musicOn) { bgmusic.time = musictime; bgmusic.Play(); }
        Sens.value = pc.sensitivity;
        pauseSens.value = pc.sensitivity;
        sensitivityValue.text = pc.sensitivity.ToString();
        menusensitivityvalue.text = pc.sensitivity.ToString();
        if (!audioOn) { audioOff.SetActive(true); pauseaudiooff.SetActive(true); }
        if (!musicOn) { musicOff.SetActive(true); pausemusicoff.SetActive(true); }
        if (!vibrateOn) { vibrateOff.SetActive(true); pausevibrateoff.SetActive(true); }
        MenuHighScore.text = "B e s t  : " + data.GetHighScore().ToString();
        currentScore.text = "S c o r e : " + data.GetCurrentScore().ToString();
        levelSliderLevelValue.text = "L e v e l : " + data.GetLevel().ToString(); currentLevel.text = "L e v e l : " + data.GetLevel().ToString();
        shopThemesCounts.text = unlockedThemes + "/12";
        magnetTimeText.text = (magnetTime / 12.5f).ToString() + "s";
        xTimeText.text = (xTime / 12.5f).ToString() + "s";
        turboTimeText.text = (turboTime + 1) + "s";
        magnetUpdatePriceText.text = magnetUpdatePrice.ToString();
        turboUpdatePriceText.text = turboUpdatePrice.ToString();
        xUpdatePriceText.text = xUpdatePrice.ToString();
        if (noAdsAnddoubleCoins) NoAdsButton.SetActive(false);
        if (paused == true)
        {
            paused = false;
            resumed = false;
            PausePanel.SetActive(false);
            PauseButton.SetActive(false);
            if (musicOn) bgmusic.Play();
            Time.timeScale = 1;
        }
        if (instagram) pc.instagramButton.SetActive(false);
        if (rated) pc.rateButtonSettings.SetActive(false);
    }

    public IEnumerator FirstJump() {
        yield return new WaitForSeconds(1f);
        PauseButton.SetActive(false);
        firstJumpFinger.SetActive(true);
        firstJumpText.SetActive(true);
        isFirstJumping = true;
        pc.dead = true;
        pc.mybody.velocity = new Vector2(0,0);
        pc.mybody.isKinematic = true;
    }

    void Update()
    {
        if (bgmusic.time >= BGM[currentbgmindex].length) {
            currentbgmindex++;
            if (currentbgmindex == 7) currentbgmindex = 0;
            bgmusic.clip = BGM[currentbgmindex];
            musictime = 0;
            bgmusic.time = 0;
            bgmusic.Play();
            save();
        }
        if (!canCollectFreeGift) freegiftlefttime -= Time.deltaTime;
        if (freegiftlefttime <= 0 && canCollectFreeGift == false)
        {
            FreeGiftAnim.Play("CanCollectFreeGiftAnim");
            canCollectFreeGift = true;
            freegifttext.gameObject.SetActive(false);
            freegiftRewardvalue.SetActive(true);
        }

        if (!canCollectFreeGift) freegifttext.text = ((int)(freegiftlefttime / 60 + 1)).ToString() + " min";

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (paused) { Resume(); }
            else if (!pc.dead) { Pause(); }
            if (shopping) BackToMenu();
            if (settings) Settings();
            if (challenge) Challenges();
            if (watchVideo) TimerButton();
            if (pc.buyingCoins) pc.BuyCoins();
        }
        if (Time.timeScale < 1f && resumed)
        {
            Time.timeScale += 0.01f;
        }
        if (watchVideo)
        {
            if (currentTimeAmount > 0f)
            {
                currentTimeAmount -= Time.deltaTime * 20;
                watchVideoTimer.fillAmount = currentTimeAmount / 60;
            }
            else { watchVideoPanel.SetActive(false); Gameover(); watchVideo = false; }
        }
        if (pc.deepfallparticle.GetComponent<AudioSource>().isPlaying && !audioOn) pc.deepfallparticle.GetComponent<AudioSource>().Pause();

    }

    public void Earn50Coins() {
        coins += 50;
        if (audioOn) freegiftRewardvalue.gameObject.transform.parent.GetComponent<AudioSource>().Play();
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+50";
        pc.GameCoins.text = coins.ToString();
        AdsManager.instance.earnCoins = false;
        save();
        EarnCoinsPanel.SetActive(false);
    }

    public void Rate()
    {
        if (audioOn) Select.Play();
        Application.OpenURL("https://apps.apple.com/app/falling-ball-2d/id1500412528");
        rated = true;
        pc.rateUsButton.SetActive(false);
        pc.rateButtonSettings.SetActive(false);
        if (audioOn) freegiftRewardvalue.gameObject.transform.parent.GetComponent<AudioSource>().Play();
        coins += 50;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+50";
        pc.GameCoins.text = coins.ToString();
        save();
    }
    public void Instagram()
    {
        if (audioOn) Select.Play();
        Application.OpenURL("https://www.instagram.com/fall.game/");
        instagram = true;
        pc.instagramButton.SetActive(false);
        if (audioOn) freegiftRewardvalue.gameObject.transform.parent.GetComponent<AudioSource>().Play();
        coins += 50;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+50";
        pc.GameCoins.text = coins.ToString();
        save();
    }
    void InstansiateVariables() {
        load();
        if (data != null)
        {
            isStartedFirstTime = data.GetIsStartedFirstTime();
        }
        else {
            isStartedFirstTime = true;
        }
        if (isStartedFirstTime)
        {
            canShowInterstitial = 6;
            firstJump = true;
            diedOnce = false;
            isStartedFirstTime = false;
            musicOn = true;
            audioOn = true;
            vibrateOn = true;
            pc.sensitivity = 22;
            data = new GameData();
            level = 1;
            scoreCount = 0;
            musictime = 0;
            coins = 200;
            currentColor = 1;
            yellowColour = true;
            greenColour = true;
            blueColour = false;
            redColour = false;
            pinkColour = false;
            purpleColour = false;
            goluboyColour = false;
            limeColour = false;
            orangeColour = false;
            adamColour = false;
            yapykColour = false;
            jumps = 0;
            gameswithoutcoins = 0;
            gameswithoutjumps = 0;
            deepDiveUsed = 0;
            gamesPlayed = 0;
            selectedBall = 0;
            BoughtBalls = new bool[49];
            BoughtBalls[0] = true;
            freegiftlefttime = 900;
            unlockedBalls = 1;
            newrecord = false;
            magnetUsed = 0;
            brokenPlatforms = 0;
            currentbgmindex = 0;
            xTime = 100;
            magnetTime = 100;
            turboTime = 4;
            magnetUpdatePrice = 200;
            turboUpdatePrice = 200;
            xUpdatePrice = 200;
            noAdsAnddoubleCoins = false;
            rated = false;
            instagram = false;

            currentReachLevelChallenge = new int[] { 10, 25, 50, 100, 300, 300 };
            currentReachLevelReward = 50;
            currentReachLevelChallengeIndex = 0;
            canCollectReachLevelChallenge = 0;
            data.setCanCollectReachLevelChallenge(canCollectReachLevelChallenge);
            data.SetCurrentReachLevelChallengeIndex(currentReachLevelChallengeIndex);
            data.setCurrentReachLevelReward(currentReachLevelReward);
            data.setCurrentReachLevelChallenge(currentReachLevelChallenge);

            currentJumpChallenge = new int[] { 50, 100, 500, 2000, 5000, 5000 };
            currentJumpReward = 50;
            currentJumpChallengeIndex = 0;
            canCollectJumpChallenge = 0;
            data.setCanCollectJumpChallenge(canCollectJumpChallenge);
            data.SetCurrentJumpChallengeIndex(currentJumpChallengeIndex);
            data.setCurrentJumpReward(currentJumpReward);
            data.setCurrentJumpChallenge(currentJumpChallenge);

            currentuseturboChallenge = new int[] { 10, 50, 100, 500, 5000, 5000 };
            currentuseturboReward = 50;
            currentuseturboChallengeIndex = 0;
            canCollectuseturboChallenge = 0;
            data.setCanCollectuseturboChallenge(canCollectuseturboChallenge);
            data.SetCurrentuseturboChallengeIndex(currentuseturboChallengeIndex);
            data.setCurrentuseturboReward(currentuseturboReward);
            data.setCurrentuseturboChallenge(currentuseturboChallenge);

            currentwinwithoutcoinsChallenge = new int[] { 5, 10, 30, 60, 100, 100 };
            currentwinwithoutcoinsReward = 50;
            currentwinwithoutcoinsChallengeIndex = 0;
            canCollectwinwithoutcoinsChallenge = 0;
            data.setCanCollectwinwithoutcoinsChallenge(canCollectwinwithoutcoinsChallenge);
            data.SetCurrentwinwithoutcoinsChallengeIndex(currentwinwithoutcoinsChallengeIndex);
            data.setCurrentwinwithoutcoinsReward(currentwinwithoutcoinsReward);
            data.setCurrentwinwithoutcoinsChallenge(currentwinwithoutcoinsChallenge);

            currentwinwithoutjumpsChallenge = new int[] { 5, 10, 30, 60, 100, 100 };
            currentwinwithoutjumpsReward = 50;
            currentwinwithoutjumpsChallengeIndex = 0;
            canCollectwinwithoutjumpsChallenge = 0;
            data.setCanCollectwinwithoutjumpsChallenge(canCollectwinwithoutjumpsChallenge);
            data.SetCurrentwinwithoutjumpsChallengeIndex(currentwinwithoutjumpsChallengeIndex);
            data.setCurrentwinwithoutjumpsReward(currentwinwithoutjumpsReward);
            data.setCurrentwinwithoutjumpsChallenge(currentwinwithoutjumpsChallenge);

            currentgamesplayedChallenge = new int[] { 25, 100, 300, 750, 2000, 2000 };
            currentgamesplayedReward = 50;
            currentgamesplayedChallengeIndex = 0;
            canCollectgamesplayedChallenge = 0;
            data.setCanCollectgamesplayedChallenge(canCollectgamesplayedChallenge);
            data.SetCurrentgamesplayedChallengeIndex(currentgamesplayedChallengeIndex);
            data.setCurrentgamesplayedReward(currentgamesplayedReward);
            data.setCurrentgamesplayedChallenge(currentgamesplayedChallenge);

            currentunlockballsChallenge = new int[] { 5, 10, 20, 30, 49, 49 };
            currentunlockballsReward = 50;
            currentunlockballsChallengeIndex = 0;
            canCollectunlockballsChallenge = 0;
            data.setCanCollectunlockballsChallenge(canCollectunlockballsChallenge);
            data.SetCurrentunlockballsChallengeIndex(currentunlockballsChallengeIndex);
            data.setCurrentunlockballsReward(currentunlockballsReward);
            data.setCurrentunlockballsChallenge(currentunlockballsChallenge);

            currentMagnetUsedChallenge = new int[] { 10, 50, 100, 500, 5000, 5000 };
            currentMagnetUsedReward = 50;
            currentMagnetUsedChallengeIndex = 0;
            canCollectMagnetUsedChallenge = 0;
            data.setCanCollectMagnetUsedChallenge(canCollectMagnetUsedChallenge);
            data.SetCurrentMagnetUsedChallengeIndex(currentMagnetUsedChallengeIndex);
            data.setCurrentMagnetUsedReward(currentMagnetUsedReward);
            data.setCurrentMagnetUsedChallenge(currentMagnetUsedChallenge);

            currentBreakPlatformsChallenge = new int[] { 100, 500, 2000, 8000, 20000, 20000 };
            currentBreakPlatformsReward = 50;
            currentBreakPlatformsChallengeIndex = 0;
            canCollectBreakPlatformsChallenge = 0;
            data.setCanCollectBreakPlatformsChallenge(canCollectBreakPlatformsChallenge);
            data.SetCurrentBreakPlatformsChallengeIndex(currentBreakPlatformsChallengeIndex);
            data.setCurrentBreakPlatformsReward(currentBreakPlatformsReward);
            data.setCurrentBreakPlatformsChallenge(currentBreakPlatformsChallenge);

            data.SetInstagram(instagram);
            data.SetRated(rated);
            data.SetCanShowInterstitial(canShowInterstitial);
            data.SetNoAdsAndDoubleCoins(noAdsAnddoubleCoins);
            data.SetTurboUpdatePrice(turboUpdatePrice);
            data.SetMagnetUpdatePrice(magnetUpdatePrice);
            data.SetxUpdatePrice(xUpdatePrice);
            data.SetMagnetTime(magnetTime);
            data.SetxTime(xTime);
            data.SetCurrentBGMIndex(currentbgmindex);
            data.SetBrokenPlatforms(magnetUsed);
            data.SetMagnetUsed(magnetUsed);
            data.SetDiedOnce(diedOnce);
            data.SetNewRecord(newrecord);
            data.setUnlockedBalls(unlockedBalls);
            data.setBoughtBalls(BoughtBalls);
            data.setSelectedBall(selectedBall);
            data.setGamesPlayed(gamesPlayed);
            data.setDeepDiveUsed(deepDiveUsed);
            data.setGamesWithoutCoins(gameswithoutcoins);
            data.setGamesWithoutJumps(gameswithoutjumps);
            data.setJumps(jumps);
            data.setYellowColour(yellowColour);
            data.setGreenColour(greenColour);
            data.setBlueColour(blueColour);
            data.setRedColour(redColour);
            data.setPinkColour(pinkColour);
            data.setPurpleColour(purpleColour);
            data.setGoluboyColour(goluboyColour);
            data.setLimeColour(limeColour);
            data.setOrangeColour(orangeColour);
            data.setAdamColour(adamColour);
            data.setYapykColour(yapykColour);
            data.setCurrentColor(currentColor);
            data.SetCoins(coins);
            data.SetMusicTime(musictime);
            data.SetAudio(audioOn);
            data.Setmusic(musicOn);
            data.Setvibrate(vibrateOn);
            data.SetSensitivity(pc.sensitivity);
            data.SetIsStartedFirstTime(isStartedFirstTime);
            data.SetCurrentScore(scoreCount);
            data.SetLevel(level);
            data.setFreeGiftLeftTime(freegiftlefttime);
            save();
            load();
        }
        else {
            canCollectReachLevelChallenge = data.getCanCollectReachLevelChallenge();
            currentReachLevelChallenge = data.getCurrentReachLevelChallenge();
            currentReachLevelChallengeIndex = data.GetCurrentReachLevelChallengeIndex();
            currentReachLevelReward = data.getCurrentReachLevelReward();

            canCollectJumpChallenge = data.getCanCollectJumpChallenge();
            currentJumpChallenge = data.getCurrentJumpChallenge();
            currentJumpChallengeIndex = data.GetCurrentJumpChallengeIndex();
            currentJumpReward = data.getCurrentJumpReward();

            canCollectuseturboChallenge = data.getCanCollectuseturboChallenge();
            currentuseturboChallenge = data.getCurrentuseturboChallenge();
            currentuseturboChallengeIndex = data.GetCurrentuseturboChallengeIndex();
            currentuseturboReward = data.getCurrentuseturboReward();

            canCollectwinwithoutcoinsChallenge = data.getCanCollectwinwithoutcoinsChallenge();
            currentwinwithoutcoinsChallenge = data.getCurrentwinwithoutcoinsChallenge();
            currentwinwithoutcoinsChallengeIndex = data.GetCurrentwinwithoutcoinsChallengeIndex();
            currentwinwithoutcoinsReward = data.getCurrentwinwithoutcoinsReward();

            canCollectwinwithoutjumpsChallenge = data.getCanCollectwinwithoutjumpsChallenge();
            currentwinwithoutjumpsChallenge = data.getCurrentwinwithoutjumpsChallenge();
            currentwinwithoutjumpsChallengeIndex = data.GetCurrentwinwithoutjumpsChallengeIndex();
            currentwinwithoutjumpsReward = data.getCurrentwinwithoutjumpsReward();

            canCollectgamesplayedChallenge = data.getCanCollectgamesplayedChallenge();
            currentgamesplayedChallenge = data.getCurrentgamesplayedChallenge();
            currentgamesplayedChallengeIndex = data.GetCurrentgamesplayedChallengeIndex();
            currentgamesplayedReward = data.getCurrentgamesplayedReward();

            canCollectunlockballsChallenge = data.getCanCollectunlockballsChallenge();
            currentunlockballsChallenge = data.getCurrentunlockballsChallenge();
            currentunlockballsChallengeIndex = data.GetCurrentunlockballsChallengeIndex();
            currentunlockballsReward = data.getCurrentunlockballsReward();

            canCollectMagnetUsedChallenge = data.getCanCollectMagnetUsedChallenge();
            currentMagnetUsedChallenge = data.getCurrentMagnetUsedChallenge();
            currentMagnetUsedChallengeIndex = data.GetCurrentMagnetUsedChallengeIndex();
            currentMagnetUsedReward = data.getCurrentMagnetUsedReward();

            canCollectBreakPlatformsChallenge = data.getCanCollectBreakPlatformsChallenge();
            currentBreakPlatformsChallenge = data.getCurrentBreakPlatformsChallenge();
            currentBreakPlatformsChallengeIndex = data.GetCurrentBreakPlatformsChallengeIndex();
            currentBreakPlatformsReward = data.getCurrentBreakPlatformsReward();

            instagram = data.GetInstagram();
            rated = data.getRated();
            canShowInterstitial = data.GetCanShowInterstitial();
            noAdsAnddoubleCoins = data.GetNoAdsAndDoubleCoins();
            turboUpdatePrice = data.GetTurboUpdatePrice();
            magnetUpdatePrice = data.GetMagnetUpdatePrice();
            xUpdatePrice = data.GetxUpdatePrice();
            turboTime = data.GetTurboTime();
            magnetTime = data.GetMagnetTime();
            xTime = data.GetxTime();
            currentbgmindex = data.GetCurrentBGMIndex();
            magnetUsed = data.GetMagnetused();
            brokenPlatforms = data.GetBrokenPlatforms();
            diedOnce = data.GetDiedOnce();
            newrecord = data.GetNewRecord();
            unlockedBalls = data.getUnlockedBalls();
            freegiftlefttime = data.getFreeGiftLeftTime();
            BoughtBalls = data.getBoughtBalls();
            selectedBall = data.GetSelectedBall();
            gamesPlayed = data.getGamesPlayed();
            deepDiveUsed = data.getDeepDiveUsed();
            gameswithoutcoins = data.getGamesWithoutCoins();
            gameswithoutjumps = data.getGamesWithoutJumps();
            jumps = data.getJumps();
            yellowColour = data.GetYellowColour();
            greenColour = data.GetGreenColour();
            blueColour = data.GetBlueColour();
            redColour = data.GetRedColour();
            pinkColour = data.GetPinkColour();
            purpleColour = data.GetPurpleColour();
            goluboyColour = data.GetGoluboyColour();
            limeColour = data.GetLimeColour();
            orangeColour = data.GetOrangeColour();
            adamColour = data.GetAdamColour();
            yapykColour = data.GetYapykColour();
            currentColor = data.getcurrentcolor();
            coins = data.getCoins();
            musicOn = data.Getmusic();
            audioOn = data.Getaudio();
            vibrateOn = data.Getvibrate();
            isStartedFirstTime = data.GetIsStartedFirstTime();
            pc.sensitivity = data.GetSensitivity();
            level = data.GetLevel();
            scoreCount = data.GetCurrentScore();
            musictime = data.GetMusicTime();
        }
    }
    private void InstantiateChallenges() {
        reachlevelchallengeTask.text = "Reach Level " + currentReachLevelChallenge[currentReachLevelChallengeIndex - canCollectReachLevelChallenge];
        if (currentReachLevelChallengeIndex <= 4) reachLevelChallengeCurrentStat.text = level + "/" + currentReachLevelChallenge[currentReachLevelChallengeIndex];
        reachLevelChallengeCollectCoinsValue.text = currentReachLevelReward.ToString();
        if (currentReachLevelChallengeIndex <= 4 && level == currentReachLevelChallenge[currentReachLevelChallengeIndex])
        {
            canCollectReachLevelChallenge++;
            currentReachLevelChallengeIndex++;
            save();
        }
        if (currentReachLevelChallengeIndex == 5) { ReachLevelChalengeDone.SetActive(true); reachLevelChallengeCurrentStat.gameObject.SetActive(false); }
        if (canCollectReachLevelChallenge > 0) reachLevelRewardCollectButton.SetActive(true);





        jumpchallengeTask.text = "Jump " + currentJumpChallenge[currentJumpChallengeIndex - canCollectJumpChallenge] + " times";

        if (currentJumpChallengeIndex <= 4) jumpChallengeCurrentStat.text = jumps + "/" + currentJumpChallenge[currentJumpChallengeIndex];
        if (currentJumpChallengeIndex == 5) { JumpChalengeDone.SetActive(true); jumpChallengeCurrentStat.gameObject.SetActive(false); }
        jumpChallengeCollectCoinsValue.text = currentJumpReward.ToString();

        if (currentJumpChallengeIndex <= 4 && jumps >= currentJumpChallenge[currentJumpChallengeIndex])
        {
            canCollectJumpChallenge++;
            currentJumpChallengeIndex++;
            save();
        }
        if (currentJumpChallengeIndex == 5) { JumpChalengeDone.SetActive(true); jumpChallengeCurrentStat.gameObject.SetActive(false); }
        if (canCollectJumpChallenge > 0) jumpRewardCollectButton.SetActive(true);




        useturbochallengetask.text = "Use TURBO powerup " + currentuseturboChallenge[currentuseturboChallengeIndex - canCollectuseturboChallenge] + " times";

        if (currentuseturboChallengeIndex <= 4) useturboChallengeCurrentStat.text = deepDiveUsed + "/" + currentuseturboChallenge[currentuseturboChallengeIndex];
        if (currentuseturboChallengeIndex == 5) { useturboChalengeDone.SetActive(true); useturboChallengeCurrentStat.gameObject.SetActive(false); }
        useturboChallengeCollectCoinsValue.text = currentuseturboReward.ToString();

        if (currentuseturboChallengeIndex <= 4 && deepDiveUsed >= currentuseturboChallenge[currentuseturboChallengeIndex])
        {
            canCollectuseturboChallenge++;
            currentuseturboChallengeIndex++;
            save();
        }
        if (currentuseturboChallengeIndex == 5) { useturboChalengeDone.SetActive(true); useturboChallengeCurrentStat.gameObject.SetActive(false); }
        if (canCollectuseturboChallenge > 0) useturboRewardCollectButton.SetActive(true);





        winwithoutcoinschallengetask.text = "Win level without collecting coins " + currentwinwithoutcoinsChallenge[currentwinwithoutcoinsChallengeIndex - canCollectwinwithoutcoinsChallenge] + " times";
        if (currentwinwithoutcoinsChallengeIndex <= 4) winwithoutcoinsChallengeCurrentStat.text = gameswithoutcoins + "/" + currentwinwithoutcoinsChallenge[currentwinwithoutcoinsChallengeIndex];
        winwithoutcoinsChallengeCollectCoinsValue.text = currentwinwithoutcoinsReward.ToString();
        if (currentwinwithoutcoinsChallengeIndex <= 4 && gameswithoutcoins == currentwinwithoutcoinsChallenge[currentwinwithoutcoinsChallengeIndex])
        {
            canCollectwinwithoutcoinsChallenge++;
            currentwinwithoutcoinsChallengeIndex++;
            save();
        }
        if (currentwinwithoutcoinsChallengeIndex == 5) { winwithoutcoinsChalengeDone.SetActive(true); winwithoutcoinsChallengeCurrentStat.gameObject.SetActive(false); }
        if (canCollectwinwithoutcoinsChallenge > 0) winwithoutcoinsRewardCollectButton.SetActive(true);





        winwithoutjumpschallengetask.text = "Win level without jumping " + currentwinwithoutjumpsChallenge[currentwinwithoutjumpsChallengeIndex - canCollectwinwithoutjumpsChallenge] + " times";
        if (currentwinwithoutjumpsChallengeIndex <= 4) winwithoutjumpsChallengeCurrentStat.text = gameswithoutjumps + "/" + currentwinwithoutjumpsChallenge[currentwinwithoutjumpsChallengeIndex];
        winwithoutjumpsChallengeCollectCoinsValue.text = currentwinwithoutjumpsReward.ToString();
        if (currentwinwithoutjumpsChallengeIndex <= 4 && gameswithoutjumps == currentwinwithoutjumpsChallenge[currentwinwithoutjumpsChallengeIndex])
        {
            canCollectwinwithoutjumpsChallenge++;
            currentwinwithoutjumpsChallengeIndex++;
            save();
        }
        if (currentwinwithoutjumpsChallengeIndex == 5) { winwithoutjumpsChalengeDone.SetActive(true); winwithoutjumpsChallengeCurrentStat.gameObject.SetActive(false); }
        if (canCollectwinwithoutjumpsChallenge > 0) winwithoutjumpsRewardCollectButton.SetActive(true);



        gamesplayedchallengetask.text = "Play " + currentgamesplayedChallenge[currentgamesplayedChallengeIndex - canCollectgamesplayedChallenge] + " times";
        if (currentgamesplayedChallengeIndex <= 4) gamesplayedChallengeCurrentStat.text = gamesPlayed + "/" + currentgamesplayedChallenge[currentgamesplayedChallengeIndex];
        gamesplayedChallengeCollectCoinsValue.text = currentgamesplayedReward.ToString();
        if (currentgamesplayedChallengeIndex <= 4 && gamesPlayed == currentgamesplayedChallenge[currentgamesplayedChallengeIndex])
        {
            canCollectgamesplayedChallenge++;
            currentgamesplayedChallengeIndex++;
            save();
        }
        if (currentgamesplayedChallengeIndex == 5) { gamesplayedChalengeDone.SetActive(true); gamesplayedChallengeCurrentStat.gameObject.SetActive(false); }
        if (canCollectgamesplayedChallenge > 0) gamesplayedRewardCollectButton.SetActive(true);


        magnetUsedchallengetask.text = "Use MAGNET powerup " + currentMagnetUsedChallenge[currentMagnetUsedChallengeIndex - canCollectMagnetUsedChallenge] + " times";
        if (currentMagnetUsedChallengeIndex <= 4) magnetUsedChallengeCurrentStat.text = magnetUsed + "/" + currentMagnetUsedChallenge[currentMagnetUsedChallengeIndex];
        if (currentMagnetUsedChallengeIndex == 5) { magnetUsedChalengeDone.SetActive(true); magnetUsedChallengeCurrentStat.gameObject.SetActive(false); }
        magnetUsedChallengeCollectCoinsValue.text = currentMagnetUsedReward.ToString();

        if (currentMagnetUsedChallengeIndex <= 4 && magnetUsed >= currentMagnetUsedChallenge[currentMagnetUsedChallengeIndex])
        {
            canCollectMagnetUsedChallenge++;
            currentMagnetUsedChallengeIndex++;
            save();
        }
        if (currentMagnetUsedChallengeIndex == 5) { magnetUsedChalengeDone.SetActive(true); magnetUsedChallengeCurrentStat.gameObject.SetActive(false); }
        if (canCollectMagnetUsedChallenge > 0) magnetUsedRewardCollectButton.SetActive(true);


        BreakPlatformschallengetask.text = "Break " + currentBreakPlatformsChallenge[currentBreakPlatformsChallengeIndex - canCollectBreakPlatformsChallenge] + " Platforms";
        if (currentBreakPlatformsChallengeIndex <= 4) BreakPlatformsChallengeCurrentStat.text = brokenPlatforms + "/" + currentBreakPlatformsChallenge[currentBreakPlatformsChallengeIndex];
        if (currentBreakPlatformsChallengeIndex == 5) { BreakPlatformsChalengeDone.SetActive(true); BreakPlatformsChallengeCurrentStat.gameObject.SetActive(false); }
        BreakPlatformsChallengeCollectCoinsValue.text = currentBreakPlatformsReward.ToString();

        if (currentBreakPlatformsChallengeIndex <= 4 && brokenPlatforms >= currentBreakPlatformsChallenge[currentBreakPlatformsChallengeIndex])
        {
            canCollectBreakPlatformsChallenge++;
            currentBreakPlatformsChallengeIndex++;
            save();
        }
        if (currentBreakPlatformsChallengeIndex == 5) { BreakPlatformsChalengeDone.SetActive(true); BreakPlatformsChallengeCurrentStat.gameObject.SetActive(false); }
        if (canCollectBreakPlatformsChallenge > 0) BreakPlatformsRewardCollectButton.SetActive(true);

    }
    private void InstantiateUnlockBallsChallenge() {
        unlockballschallengetask.text = "Unlock " + currentunlockballsChallenge[currentunlockballsChallengeIndex - canCollectunlockballsChallenge] + " balls";

        if (currentunlockballsChallengeIndex <= 4) unlockballsChallengeCurrentStat.text = unlockedBalls + "/" + currentunlockballsChallenge[currentunlockballsChallengeIndex];
        if (currentunlockballsChallengeIndex == 5) { unlockballsChalengeDone.SetActive(true); unlockballsChallengeCurrentStat.gameObject.SetActive(false); }
        unlockballsChallengeCollectCoinsValue.text = currentunlockballsReward.ToString();

        if (currentunlockballsChallengeIndex <= 4 && unlockedBalls >= currentunlockballsChallenge[currentunlockballsChallengeIndex])
        {
            canCollectunlockballsChallenge++;
            currentunlockballsChallengeIndex++;
            save();
        }
        if (currentunlockballsChallengeIndex == 5) { unlockballsChalengeDone.SetActive(true); unlockballsChallengeCurrentStat.gameObject.SetActive(false); }
        if (canCollectunlockballsChallenge > 0) unlockballsRewardCollectButton.SetActive(true);
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge > 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(true);

    }
    private void InstantiateColors() {
        switch (currentColor)
        {
            case 0:
                WhiteColour();
                break;
            case 2:
                BlueColour();
                break;
            case 3:
                GreenColour();
                break;
            case 4:
                RedColour();
                break;
            case 1:
                YellowColour();
                break;
            case 5:
                PinkColour();
                break;
            case 6:
                PurpleColour();
                break;
            case 7:
                GoluboyColour();
                break;
            case 8:
                LimeColour();
                break;
            case 9:
                OrangeColour();
                break;
            case 10:
                AdamColour();
                break;
            case 11:
                YapykColour();
                break;
        }

        if (yellowColour)
        {
            unlockedThemes++;
            Locks[0].SetActive(false);
            ColourButtons[0].interactable = true;
            colourinfobuttons[0].SetActive(false);
        }
        if (blueColour)
        {
            unlockedThemes++;
            Locks[1].SetActive(false);
            ColourButtons[1].interactable = true;
            colourinfobuttons[1].SetActive(false);
        }
        if (greenColour)
        {
            unlockedThemes++;
            Locks[2].SetActive(false);
            ColourButtons[2].interactable = true;
            colourinfobuttons[2].SetActive(false);
        }
        if (redColour)
        {
            unlockedThemes++;
            Locks[3].SetActive(false);
            ColourButtons[3].interactable = true;
            colourinfobuttons[3].SetActive(false);
        }
        if (pinkColour)
        {
            unlockedThemes++;
            Locks[4].SetActive(false);
            ColourButtons[4].interactable = true;
            colourinfobuttons[4].SetActive(false);
        }
        if (purpleColour)
        {
            unlockedThemes++;
            Locks[5].SetActive(false);
            ColourButtons[5].interactable = true;
            colourinfobuttons[5].SetActive(false);
        }
        if (goluboyColour)
        {
            unlockedThemes++;
            Locks[6].SetActive(false);
            ColourButtons[6].interactable = true;
            colourinfobuttons[6].SetActive(false);
        }
        if (limeColour)
        {
            unlockedThemes++;
            Locks[7].SetActive(false);
            ColourButtons[7].interactable = true;
            colourinfobuttons[7].SetActive(false);
        }
        if (orangeColour)
        {
            unlockedThemes++;
            Locks[8].SetActive(false);
            ColourButtons[8].interactable = true;
            colourinfobuttons[8].SetActive(false);
        }
        if (adamColour)
        {
            unlockedThemes++;
            Locks[9].SetActive(false);
            ColourButtons[9].interactable = true;
            colourinfobuttons[9].SetActive(false);
        }
        if (yapykColour)
        {
            unlockedThemes++;
            Locks[10].SetActive(false);
            ColourButtons[10].interactable = true;
            colourinfobuttons[10].SetActive(false);
        }

    }



    public void save()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameData.dat");
            if (data != null)
            {
                data.setCanCollectReachLevelChallenge(canCollectReachLevelChallenge);
                data.SetCurrentReachLevelChallengeIndex(currentReachLevelChallengeIndex);
                data.setCurrentReachLevelReward(currentReachLevelReward);

                data.setCanCollectJumpChallenge(canCollectJumpChallenge);
                data.SetCurrentJumpChallengeIndex(currentJumpChallengeIndex);
                data.setCurrentJumpReward(currentJumpReward);

                data.setCanCollectuseturboChallenge(canCollectuseturboChallenge);
                data.SetCurrentuseturboChallengeIndex(currentuseturboChallengeIndex);
                data.setCurrentuseturboReward(currentuseturboReward);

                data.setCanCollectwinwithoutcoinsChallenge(canCollectwinwithoutcoinsChallenge);
                data.SetCurrentwinwithoutcoinsChallengeIndex(currentwinwithoutcoinsChallengeIndex);
                data.setCurrentwinwithoutcoinsReward(currentwinwithoutcoinsReward);

                data.setCanCollectwinwithoutjumpsChallenge(canCollectwinwithoutjumpsChallenge);
                data.SetCurrentwinwithoutjumpsChallengeIndex(currentwinwithoutjumpsChallengeIndex);
                data.setCurrentwinwithoutjumpsReward(currentwinwithoutjumpsReward);

                data.setCanCollectgamesplayedChallenge(canCollectgamesplayedChallenge);
                data.SetCurrentgamesplayedChallengeIndex(currentgamesplayedChallengeIndex);
                data.setCurrentgamesplayedReward(currentgamesplayedReward);

                data.setCanCollectunlockballsChallenge(canCollectunlockballsChallenge);
                data.SetCurrentunlockballsChallengeIndex(currentunlockballsChallengeIndex);
                data.setCurrentunlockballsReward(currentunlockballsReward);

                data.setCanCollectMagnetUsedChallenge(canCollectMagnetUsedChallenge);
                data.SetCurrentMagnetUsedChallengeIndex(currentMagnetUsedChallengeIndex);
                data.setCurrentMagnetUsedReward(currentMagnetUsedReward);

                data.setCanCollectBreakPlatformsChallenge(canCollectBreakPlatformsChallenge);
                data.SetCurrentBreakPlatformsChallengeIndex(currentBreakPlatformsChallengeIndex);
                data.setCurrentBreakPlatformsReward(currentBreakPlatformsReward);

                if (data.GetHighScore() < onehighscore)
                {
                    data.SetHighScore(onehighscore);
                    newrecord = true;
                }

                data.SetInstagram(instagram);
                data.SetRated(rated);
                data.SetCanShowInterstitial(canShowInterstitial);
                data.SetNoAdsAndDoubleCoins(noAdsAnddoubleCoins);
                data.SetTurboUpdatePrice(turboUpdatePrice);
                data.SetMagnetUpdatePrice(magnetUpdatePrice);
                data.SetxUpdatePrice(xUpdatePrice);
                data.SetMagnetTime(magnetTime);
                data.SetTurboTime(turboTime);
                data.SetxTime(xTime);
                data.SetCurrentBGMIndex(currentbgmindex);
                data.SetMagnetUsed(magnetUsed);
                data.SetBrokenPlatforms(brokenPlatforms);
                data.SetDiedOnce(diedOnce);
                data.SetNewRecord(newrecord);
                data.setUnlockedBalls(unlockedBalls);
                data.setFreeGiftLeftTime(freegiftlefttime);
                data.setBoughtBalls(BoughtBalls);
                data.setSelectedBall(selectedBall);
                data.setGamesPlayed(gamesPlayed);
                data.setDeepDiveUsed(deepDiveUsed);
                data.Setmusic(musicOn);
                data.setGamesWithoutCoins(gameswithoutcoins);
                data.setGamesWithoutJumps(gameswithoutjumps);
                data.Setvibrate(vibrateOn);
                data.SetAudio(audioOn);
                data.SetSensitivity(pc.sensitivity);
                data.SetLevel(level);
                data.SetCurrentScore(scoreCount);
                data.SetMusicTime(musictime);
                data.SetCoins(coins);
                data.setCurrentColor(currentColor);
                data.setYellowColour(yellowColour);
                data.setGreenColour(greenColour);
                data.setBlueColour(blueColour);
                data.setRedColour(redColour);
                data.setPinkColour(pinkColour);
                data.setPurpleColour(purpleColour);
                data.setGoluboyColour(goluboyColour);
                data.setLimeColour(limeColour);
                data.setOrangeColour(orangeColour);
                data.setAdamColour(adamColour);
                data.setYapykColour(yapykColour);
                data.setJumps(jumps);
                bf.Serialize(file, data);
            }

        }
        catch (Exception e) {

        } finally {
            if (file != null) file.Close();
        }
    }
    public void load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);

        }
        catch (Exception e)
        {

        }
        finally
        {
            if (file != null)
                file.Close();
        }

    }


    public void Win() {
        if (pc.coinsInOneGame == 0) gameswithoutcoins++;
        if (pc.jumpsInOneGame == 0) gameswithoutjumps++;
        gamesPlayed++;
        winTap.SetActive(true);
        pc.dead = true;
        StartCoroutine(WINENUM());
        scoreCount += pc.ScorePosition;
        onehighscore = scoreCount;
        jumps += pc.jumpsInOneGame;
        levelPassed.SetActive(true);
        PauseButton.SetActive(false);
        level++;
        if (pc.deepfallparticle.GetComponent<AudioSource>().isPlaying) pc.deepfallparticle.GetComponent<AudioSource>().Stop();
        if (vibrateOn) Vibration.VibratePop();
        coins += 25;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+25";
        pc.GameCoins.text = coins.ToString();
        save();
        if (canShowInterstitial == 0 && !noAdsAnddoubleCoins) InterstitialAds.instance.ShowInterstitial();

    }
    IEnumerator WINENUM() {
        yield return new WaitForSeconds(0.5f);
        win = true;
    }
    public void Gameover() {
        gamesPlayed++;
        onehighscore = scoreCount + pc.ScorePosition;
        scoreCount = 0;
        jumps += pc.jumpsInOneGame;
        diedOnce = false;
        save();
        pc.PauseButton.SetActive(false);
        pc.dead = true;
        Player.GetComponent<SpriteRenderer>().enabled = false;
        Player.GetComponent<TrailRenderer>().enabled = false;
        if (vibrateOn) Vibration.VibratePop();
        if (canShowInterstitial == 0 && !noAdsAnddoubleCoins) InterstitialAds.instance.ShowInterstitial();
        StartCoroutine(CanRestart());
    }

    IEnumerator CanRestart() {
        yield return new WaitForSeconds(0.2f);
        gameover = true;
        winTap.SetActive(true);
    }

    public void Challenges() {
        if (audioOn) Select.Play();
        if (!challenge) {
            challenge = true;
            challengesPanel.SetActive(true);
        }
        else {
            challenge = false;
            challengesPanel.SetActive(false);
        }
    }
    public void ChangeSensitivity(float value)
    {
        Sens.value = pc.sensitivity;
        pauseSens.value = pc.sensitivity;
        pc.sensitivity = value;
        sensitivityValue.text = value.ToString();
        menusensitivityvalue.text = value.ToString();
        save();
    }

    public void Settings()
    {
        if (SplashScreen.isFinished)
        {
            if (audioOn) Select.Play();
            if (!settings)
            {
                settings = true;
                MenuSettingsPanel.SetActive(true);
            }
            else
            {
                settings = false;
                MenuSettingsPanel.SetActive(false);
            }
        }
    }

    public void MusicSwitch()
    {
        if (audioOn) Select.Play();
        if (musicOn)
        {
            bgmusic.Stop();
            musicOn = false;
            pausemusicoff.SetActive(true);
            musicOff.SetActive(true);
        }
        else
        {
            bgmusic.Play();
            musicOff.SetActive(false);
            pausemusicoff.SetActive(false);
            musicOn = true;
        }
        save();
    }
    public void VibrateSwitch()
    {
        if (audioOn) Select.Play();
        if (vibrateOn) { vibrateOff.SetActive(true); pausevibrateoff.SetActive(true); vibrateOn = false; }
        else {vibrateOff.SetActive(false); pausevibrateoff.SetActive(false); vibrateOn = true;Handheld.Vibrate();}
        save();
    }
    public void AudioSwitch()
    {
        if (!audioOn) Select.Play();
        if (audioOn) { audioOff.SetActive(true); pauseaudiooff.SetActive(true); audioOn = false; }
        else { audioOff.SetActive(false); pauseaudiooff.SetActive(false); audioOn = true; }
        save();
    }
    public void PlayAgain()
    {
        if (audioOn) Select.Play();
        if (musicOn) musictime = bgmusic.time;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        scoreCount = 0;
        save();
    }
    public void Shop()
    {
        if (SplashScreen.isFinished)
        {
            shopping = true;
            if (audioOn) Select.Play();
            shop.SetActive(true);
        }
    }
    public void BackToMenu()
    {
        shopping = false;
        if (audioOn) Select.Play();
        shop.SetActive(false);
    }
    public void Pause()
    {
        if (!pc.dead && SplashScreen.isFinished && Time.realtimeSinceStartup >= 2 && !paused) {
            //pc.dead = true;
            resumed = false;
            if (bgmusic.isPlaying) bgmusic.Pause();
            if (pc.deepfallparticle.GetComponent<AudioSource>().isPlaying) pc.deepfallparticle.GetComponent<AudioSource>().Pause();
            GameObject[] currentRockets = GameObject.FindGameObjectsWithTag("rocket");
            for (int i = 0; i < currentRockets.Length; i++) currentRockets[i].transform.GetChild(0).GetComponent<AudioSource>().Pause();
            if (audioOn) Select.Play();
            PausePanel.SetActive(true);
            PauseButton.SetActive(false);
            paused = true;
            Time.timeScale = 0;
        }
    }
    public void Resume()
    {
        if (audioOn) Select.Play();
        //pc.dead = false;
        if (musicOn) bgmusic.Play();
        if (pc.deepfall) pc.deepfallparticle.GetComponent<AudioSource>().Play();
        if (audioOn)
        {
            GameObject[] currentRockets = GameObject.FindGameObjectsWithTag("rocket");
            for (int i = 0; i < currentRockets.Length; i++) currentRockets[i].transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
        PausePanel.SetActive(false);
        resumed = true;
        paused = false;
        PauseButton.SetActive(true);

    }
    public void WatchVideo() {
        Player.GetComponent<SpriteRenderer>().enabled = true;
        Player.GetComponent<TrailRenderer>().enabled = true;
        watchVideo = false;
        PauseButton.SetActive(true);
        pc.Collider.isTrigger = true;
        watchVideoPanel.SetActive(false);
        pc.dead = false;
        pc.gameObject.transform.position = new Vector3(pc.diePositionX, pc.diePositionY, pc.gameObject.transform.position.z);
        pc.mybody.velocity = new Vector2(0, pc.fallForce);
        pc.mybody.angularVelocity = 0;
        StartCoroutine(DeactivateCollider());
        PlayerAnim.Play("PlayerRevive");
        AdsManager.instance.reviveVideo = false;
    }
    public void CoinsToRevive() {
        if (audioOn) Select.Play();
        if (coins >= 100)
        {
            if (vibrateOn) Vibration.VibratePeek();

            Player.GetComponent<SpriteRenderer>().enabled = true;
            Player.GetComponent<TrailRenderer>().enabled = true;
            watchVideo = false;
            PauseButton.SetActive(true);
            pc.Collider.isTrigger = true;
            watchVideoPanel.SetActive(false);
            pc.dead = false;
            pc.gameObject.transform.position = new Vector3(pc.diePositionX, pc.diePositionY, pc.gameObject.transform.position.z);
            pc.mybody.velocity = new Vector2(0, pc.fallForce);
            pc.mybody.angularVelocity = 0;
            StartCoroutine(DeactivateCollider());
            PlayerAnim.Play("PlayerRevive");
            coins -= 100;
            save();
            pc.GameCoins.text = "x" + coins;
        }
    }
    public void TimerButton() {
        if (audioOn) Select.Play();
        watchVideoPanel.SetActive(false);
        Gameover();
        watchVideo = false;
    }
    public void CollectFreeGift()
    {
        if (canCollectFreeGift)
        {
            if (vibrateOn) Vibration.VibratePeek();

            if (audioOn) freegiftRewardvalue.gameObject.transform.parent.GetComponent<AudioSource>().Play();
            GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
            instAddedCoins.GetComponent<Text>().text = "+50";
            pc.GameCoins.text = coins.ToString();
            coins += 100;
            freegiftlefttime = 900;
            FreeGiftAnim.Play("FreeGiftIdle");
            freegiftRewardvalue.SetActive(false);
            freegifttext.gameObject.SetActive(true);
            save();
            canCollectFreeGift = false;
        }
    }

    public void CollectReachLevelChallengeReward() {
        if (vibrateOn) Vibration.VibratePeek();

        if (audioOn) rewardAudio.Play();
        coins += currentReachLevelReward;
        canCollectReachLevelChallenge--;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + currentReachLevelReward.ToString();
        if (currentReachLevelReward == 50) currentReachLevelReward = 100;
        else if (currentReachLevelReward == 100) currentReachLevelReward = 200;
        else if (currentReachLevelReward == 200) currentReachLevelReward = 500;
        else if (currentReachLevelReward == 500) currentReachLevelReward = 1000;
        pc.GameCoins.text = coins.ToString();

        if (canCollectReachLevelChallenge == 0) reachLevelRewardCollectButton.SetActive(false);

        reachlevelchallengeTask.text = "Reach Level " + currentReachLevelChallenge[currentReachLevelChallengeIndex - canCollectReachLevelChallenge];
        if (currentReachLevelChallengeIndex <= 4) reachLevelChallengeCurrentStat.text = level + "/" + currentReachLevelChallenge[currentReachLevelChallengeIndex];
        reachLevelChallengeCollectCoinsValue.text = currentReachLevelReward.ToString();
        save();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);

    }
    public void CollectJumpChallengeReward()
    {
        if (vibrateOn) Vibration.VibratePeek();

        if (audioOn) rewardAudio.Play();
        canCollectJumpChallenge--;
        coins += currentJumpReward;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + currentJumpReward.ToString();
        if (currentJumpReward == 50) currentJumpReward = 100;
        else if (currentJumpReward == 100) currentJumpReward = 200;
        else if (currentJumpReward == 200) currentJumpReward = 500;
        else if (currentJumpReward == 500) currentJumpReward = 1000;
        pc.GameCoins.text = coins.ToString();

        jumpchallengeTask.text = "Jump " + currentJumpChallenge[currentJumpChallengeIndex - canCollectJumpChallenge] + " times";

        if (currentJumpChallengeIndex <= 4) jumpChallengeCurrentStat.text = jumps + "/" + currentJumpChallenge[currentJumpChallengeIndex];
        if (currentJumpChallengeIndex == 5) { JumpChalengeDone.SetActive(true); jumpChallengeCurrentStat.gameObject.SetActive(false); }
        jumpChallengeCollectCoinsValue.text = currentJumpReward.ToString();
        if (currentJumpChallengeIndex <= 4 && jumps >= currentJumpChallenge[currentJumpChallengeIndex])
        {
            canCollectJumpChallenge++;
            currentJumpChallengeIndex++;
            save();
        }
        jumpRewardCollectButton.SetActive(false);
        if (canCollectJumpChallenge > 0) jumpRewardCollectButton.SetActive(true);
        save();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);

    }
    public void CollectwinwithoutcoinsChallengeReward()
    {
        if (vibrateOn) Vibration.VibratePeek();

        if (audioOn) rewardAudio.Play();
        coins += currentwinwithoutcoinsReward;
        canCollectwinwithoutcoinsChallenge--;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + currentwinwithoutcoinsReward.ToString();
        if (currentwinwithoutcoinsReward == 50) currentwinwithoutcoinsReward = 100;
        else if (currentwinwithoutcoinsReward == 100) currentwinwithoutcoinsReward = 200;
        else if (currentwinwithoutcoinsReward == 200) currentwinwithoutcoinsReward = 500;
        else if (currentwinwithoutcoinsReward == 500) currentwinwithoutcoinsReward = 1000;
        pc.GameCoins.text = coins.ToString();

        if (canCollectwinwithoutcoinsChallenge == 0) winwithoutcoinsRewardCollectButton.SetActive(false);

        winwithoutcoinschallengetask.text = "Win level without collecting coins " + currentwinwithoutcoinsChallenge[currentwinwithoutcoinsChallengeIndex - canCollectwinwithoutcoinsChallenge] + " times";
        if (currentwinwithoutcoinsChallengeIndex <= 4) winwithoutcoinsChallengeCurrentStat.text = gameswithoutcoins + "/" + currentwinwithoutcoinsChallenge[currentwinwithoutcoinsChallengeIndex];
        winwithoutcoinsChallengeCollectCoinsValue.text = currentwinwithoutcoinsReward.ToString();
        save();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);

    }
    public void CollectwinwithoutjumpsChallengeReward()
    {
        if (vibrateOn) Vibration.VibratePeek();

        if (audioOn) rewardAudio.Play();
        coins += currentwinwithoutjumpsReward;
        canCollectwinwithoutjumpsChallenge--;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + currentwinwithoutjumpsReward.ToString();
        if (currentwinwithoutjumpsReward == 50) currentwinwithoutjumpsReward = 100;
        else if (currentwinwithoutjumpsReward == 100) currentwinwithoutjumpsReward = 200;
        else if (currentwinwithoutjumpsReward == 200) currentwinwithoutjumpsReward = 500;
        else if (currentwinwithoutjumpsReward == 500) currentwinwithoutjumpsReward = 1000;
        pc.GameCoins.text = coins.ToString();

        if (canCollectwinwithoutjumpsChallenge == 0) winwithoutjumpsRewardCollectButton.SetActive(false);

        winwithoutjumpschallengetask.text = "Win level without jumping " + currentwinwithoutjumpsChallenge[currentwinwithoutjumpsChallengeIndex - canCollectwinwithoutjumpsChallenge] + " times";
        if (currentwinwithoutjumpsChallengeIndex <= 4) winwithoutjumpsChallengeCurrentStat.text = gameswithoutjumps + "/" + currentwinwithoutjumpsChallenge[currentwinwithoutjumpsChallengeIndex];
        winwithoutjumpsChallengeCollectCoinsValue.text = currentwinwithoutjumpsReward.ToString();
        save();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);

    }
    public void CollectuseturboChallengeReward()
    {
        if (vibrateOn) Vibration.VibratePeek();

        if (audioOn) rewardAudio.Play();
        canCollectuseturboChallenge--;
        coins += currentuseturboReward;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + currentuseturboReward.ToString();
        if (currentuseturboReward == 50) currentuseturboReward = 100;
        else if (currentuseturboReward == 100) currentuseturboReward = 200;
        else if (currentuseturboReward == 200) currentuseturboReward = 500;
        else if (currentuseturboReward == 500) currentuseturboReward = 1000;
        pc.GameCoins.text = coins.ToString();

        useturbochallengetask.text = "Use TURBO powerup " + currentuseturboChallenge[currentuseturboChallengeIndex - canCollectuseturboChallenge] + " times";

        if (currentuseturboChallengeIndex <= 4) useturboChallengeCurrentStat.text = deepDiveUsed + "/" + currentuseturboChallenge[currentuseturboChallengeIndex];
        if (currentuseturboChallengeIndex == 5) { useturboChalengeDone.SetActive(true); useturboChallengeCurrentStat.gameObject.SetActive(false); }
        useturboChallengeCollectCoinsValue.text = currentuseturboReward.ToString();
        if (currentuseturboChallengeIndex <= 4 && deepDiveUsed >= currentuseturboChallenge[currentuseturboChallengeIndex])
        {
            canCollectuseturboChallenge++;
            currentuseturboChallengeIndex++;
            save();
        }
        useturboRewardCollectButton.SetActive(false);
        if (canCollectuseturboChallenge > 0) useturboRewardCollectButton.SetActive(true);
        save();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);
    }
    public void CollectgamesplayedChallengeReward()
    {
        if (vibrateOn) Vibration.VibratePeek();

        if (audioOn) rewardAudio.Play();
        coins += currentgamesplayedReward;
        canCollectgamesplayedChallenge--;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + currentgamesplayedReward.ToString();
        if (currentgamesplayedReward == 50) currentgamesplayedReward = 100;
        else if (currentgamesplayedReward == 100) currentgamesplayedReward = 200;
        else if (currentgamesplayedReward == 200) currentgamesplayedReward = 500;
        else if (currentgamesplayedReward == 500) currentgamesplayedReward = 1000;
        pc.GameCoins.text = coins.ToString();

        if (canCollectgamesplayedChallenge == 0) gamesplayedRewardCollectButton.SetActive(false);

        gamesplayedchallengetask.text = "Play " + currentgamesplayedChallenge[currentgamesplayedChallengeIndex - canCollectgamesplayedChallenge] + " times";
        if (currentgamesplayedChallengeIndex <= 4) gamesplayedChallengeCurrentStat.text = gamesPlayed + "/" + currentgamesplayedChallenge[currentgamesplayedChallengeIndex];
        gamesplayedChallengeCollectCoinsValue.text = currentgamesplayedReward.ToString();
        save();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);

    }
    public void CollectunlockballsChallengeReward()
    {
        if (vibrateOn) Vibration.VibratePeek();

        if (audioOn) rewardAudio.Play();
        canCollectunlockballsChallenge--;
        coins += currentunlockballsReward;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + currentunlockballsReward.ToString();
        if (currentunlockballsReward == 50) currentunlockballsReward = 100;
        else if (currentunlockballsReward == 100) currentunlockballsReward = 200;
        else if (currentunlockballsReward == 200) currentunlockballsReward = 500;
        else if (currentunlockballsReward == 500) currentunlockballsReward = 1000;
        pc.GameCoins.text = coins.ToString();

        unlockballschallengetask.text = "Unlock " + currentunlockballsChallenge[currentunlockballsChallengeIndex - canCollectunlockballsChallenge] + " balls";

        if (currentunlockballsChallengeIndex <= 4) unlockballsChallengeCurrentStat.text = unlockedBalls + "/" + currentunlockballsChallenge[currentunlockballsChallengeIndex];
        if (currentunlockballsChallengeIndex == 5) { unlockballsChalengeDone.SetActive(true); unlockballsChallengeCurrentStat.gameObject.SetActive(false); }
        unlockballsChallengeCollectCoinsValue.text = currentunlockballsReward.ToString();
        if (currentunlockballsChallengeIndex <= 4 && unlockedBalls >= currentunlockballsChallenge[currentunlockballsChallengeIndex])
        {
            canCollectunlockballsChallenge++;
            currentunlockballsChallengeIndex++;
            save();
        }
        unlockballsRewardCollectButton.SetActive(false);
        if (canCollectunlockballsChallenge > 0) unlockballsRewardCollectButton.SetActive(true);
        save();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);

    }
    public void CollectMagnetUsedChallengeReward()
    {
        if (vibrateOn) Vibration.VibratePeek();

        if (audioOn) rewardAudio.Play();
        canCollectMagnetUsedChallenge--;
        coins += currentMagnetUsedReward;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + currentMagnetUsedReward.ToString();
        if (currentMagnetUsedReward == 50) currentMagnetUsedReward = 100;
        else if (currentMagnetUsedReward == 100) currentMagnetUsedReward = 200;
        else if (currentMagnetUsedReward == 200) currentMagnetUsedReward = 500;
        else if (currentMagnetUsedReward == 500) currentMagnetUsedReward = 1000;
        pc.GameCoins.text = coins.ToString();

        magnetUsedchallengetask.text = "Use MAGNET powerup " + currentMagnetUsedChallenge[currentMagnetUsedChallengeIndex - canCollectMagnetUsedChallenge] + " times";
        if (currentMagnetUsedChallengeIndex <= 4) magnetUsedChallengeCurrentStat.text = magnetUsed + "/" + currentMagnetUsedChallenge[currentMagnetUsedChallengeIndex];
        if (currentMagnetUsedChallengeIndex == 5) { magnetUsedChalengeDone.SetActive(true); magnetUsedChallengeCurrentStat.gameObject.SetActive(false); }
        magnetUsedChallengeCollectCoinsValue.text = currentMagnetUsedReward.ToString();
        if (currentMagnetUsedChallengeIndex <= 4 && magnetUsed >= currentMagnetUsedChallenge[currentMagnetUsedChallengeIndex])
        {
            canCollectMagnetUsedChallenge++;
            currentMagnetUsedChallengeIndex++;
            save();
        }
        if (canCollectMagnetUsedChallenge == 0) magnetUsedRewardCollectButton.SetActive(false);
        save();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);
    }
    public void CollectBrokenPlatformsChallengeReward()
    {
        if (vibrateOn) Vibration.VibratePeek();

        if (audioOn) rewardAudio.Play();
        canCollectBreakPlatformsChallenge--;
        coins += currentBreakPlatformsReward;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + currentBreakPlatformsReward.ToString();
        if (currentBreakPlatformsReward == 50) currentBreakPlatformsReward = 100;
        else if (currentBreakPlatformsReward == 100) currentBreakPlatformsReward = 200;
        else if (currentBreakPlatformsReward == 200) currentBreakPlatformsReward = 500;
        else if (currentBreakPlatformsReward == 500) currentBreakPlatformsReward = 1000;
        pc.GameCoins.text = coins.ToString();

        BreakPlatformschallengetask.text = "Break " + currentBreakPlatformsChallenge[currentBreakPlatformsChallengeIndex - canCollectBreakPlatformsChallenge] + " Platforms";
        if (currentBreakPlatformsChallengeIndex <= 4) BreakPlatformsChallengeCurrentStat.text = brokenPlatforms + "/" + currentBreakPlatformsChallenge[currentBreakPlatformsChallengeIndex];
        if (currentBreakPlatformsChallengeIndex == 5) { BreakPlatformsChalengeDone.SetActive(true); BreakPlatformsChallengeCurrentStat.gameObject.SetActive(false); }
        BreakPlatformsChallengeCollectCoinsValue.text = currentBreakPlatformsReward.ToString();
        if (currentBreakPlatformsChallengeIndex <= 4 && brokenPlatforms >= currentBreakPlatformsChallenge[currentBreakPlatformsChallengeIndex])
        {
            canCollectBreakPlatformsChallenge++;
            currentBreakPlatformsChallengeIndex++;
            save();
        }
        if (canCollectBreakPlatformsChallenge == 0) BreakPlatformsRewardCollectButton.SetActive(false);
        save();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);
    }

    IEnumerator newThemeAddedEnd()
    {
        yield return new WaitForSeconds(2.3f);
        newThemeAdded.GetComponent<Animator>().Play("NewThemeAddedUp");
        yield return new WaitForSeconds(1f);
        newThemeAdded.SetActive(false);
    }
    IEnumerator DeactivateColourInfoText() {
        yield return new WaitForSeconds(3);
        colourInfoText.text = "";
        coloursinfoValue.text = "";
    }
    IEnumerator DeactivateCollider() {
        yield return new WaitForSeconds(2.3f);
        if (!pc.deepfall) pc.Collider.isTrigger = false;
        PlayerAnim.Play("PlayerIdle");
    }
    public IEnumerator ActivateWatchVideoPanel() {
        Player.GetComponent<SpriteRenderer>().enabled = false;
        Player.GetComponent<TrailRenderer>().enabled = false;
        pc.dead = true;
        if (vibrateOn) Vibration.VibratePop();
        PauseButton.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        watchVideoPanel.SetActive(true);
        watchVideo = true;
    }


    private void OnApplicationPause(bool pause)
    {
        save();
        if (SplashScreen.isFinished && Time.realtimeSinceStartup >= 2 && pause == true && !pc.dead) Pause();
    }

    public void UpdateTurbo() {
        if (coins >= turboUpdatePrice)
        {
            if (vibrateOn) Vibration.VibratePeek();

            if (audioOn) newBallAudio.Play();
            coins -= turboUpdatePrice;
            turboTime++;
            turboUpdatePrice += 400;
            save();
            turboTimeText.text = (turboTime + 1) + "s";
            turboUpdatePriceText.text = turboUpdatePrice.ToString();
            pc.GameCoins.text = coins.ToString();
        }
        else if (audioOn) Select.Play();
    }
    public void UpdateMagnet()
    {
        if (coins >= magnetUpdatePrice)
        {
            if (vibrateOn) Vibration.VibratePeek();

            if (audioOn) newBallAudio.Play();
            coins -= magnetUpdatePrice;
            magnetTime += 12.5f;
            magnetUpdatePrice += 400;
            save();
            magnetTimeText.text = (magnetTime / 12.5f).ToString() + "s";
            magnetUpdatePriceText.text = magnetUpdatePrice.ToString();
            pc.GameCoins.text = coins.ToString();
        }
        else if (audioOn) Select.Play();
    }
    public void UpdateScoreMultiplier() {
        if (coins >= xUpdatePrice)
        {
            if (vibrateOn) Vibration.VibratePeek();

            if (audioOn) newBallAudio.Play();
            coins -= xUpdatePrice;
            xTime += 12.5f;
            xUpdatePrice += 400;
            save();
            xTimeText.text = (xTime / 12.5f).ToString() + "s";
            xUpdatePriceText.text = xUpdatePrice.ToString();
            pc.GameCoins.text = coins.ToString();
        }
        else if (audioOn) Select.Play();
    }

    public void ColourInfo() {
        if (audioOn) Select.Play();
        string colourinfoname = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (colourinfoname) {
            case "blueinfo":
                coloursinfoValue.text = unlockedBalls.ToString() + " / 20";
                colourInfoText.text = "Unlock 20 balls";
                StopCoroutine("DeactivateColourInfoText");
                StartCoroutine("DeactivateColourInfoText");
                break;
            case "redinfo":
                coloursinfoValue.text = magnetUsed.ToString() + " / 25";
                colourInfoText.text = "Use MAGNET powerup 25 times";
                StopCoroutine("DeactivateColourInfoText");
                StartCoroutine("DeactivateColourInfoText");
                break;
            case "pinkinfo":
                coloursinfoValue.text = gameswithoutcoins.ToString() + " / 10";
                colourInfoText.text = "Win 10 levels not collecting coins";
                StopCoroutine("DeactivateColourInfoText");
                StartCoroutine("DeactivateColourInfoText");
                break;
            case "purpleinfo":
                coloursinfoValue.text = gameswithoutjumps.ToString() + " / 10";
                colourInfoText.text = "Win 10 Levels not jumping";
                StopCoroutine("DeactivateColourInfoText");
                StartCoroutine("DeactivateColourInfoText");
                break;
            case "goluboyinfo":
                coloursinfoValue.text = level.ToString() + " / 30";
                colourInfoText.text = "Reach Level 30";
                StopCoroutine("DeactivateColourInfoText");
                StartCoroutine("DeactivateColourInfoText"); 
                break;
            case "limeinfo":
                coloursinfoValue.text = brokenPlatforms.ToString() + " / 250";
                colourInfoText.text = "Break 250 platforms";
                StopCoroutine("DeactivateColourInfoText");
                StartCoroutine("DeactivateColourInfoText");
                break;
            case "orangeinfo":
                coloursinfoValue.text = gamesPlayed.ToString() + " / 100";
                colourInfoText.text = "Play 100 times";
                StopCoroutine("DeactivateColourInfoText");
                StartCoroutine("DeactivateColourInfoText");
                break;
            case "adaminfo":
                coloursinfoValue.text = jumps.ToString() + " / 100";
                colourInfoText.text = "jump 100 times";
                StopCoroutine("DeactivateColourInfoText");
                StartCoroutine("DeactivateColourInfoText");
                break;
            case "yapykinfo":
                coloursinfoValue.text = deepDiveUsed.ToString() + " / 50";
                colourInfoText.text = "Use TURBO powerup 50 times";
                StopCoroutine("DeactivateColourInfoText");
                StartCoroutine("DeactivateColourInfoText");
                break;
        }
    }
    public void BuyBall()
    {
        string buyNumber = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (buyNumber) {
            case "Buy1":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[1].gameObject.SetActive(false);
                    selectBallButtons[1].SetActive(true);
                    BoughtBalls[1] = true;
                    unlockedBalls++;
                    save();
                }
            break;
            case "Buy2":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();
                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[2].gameObject.SetActive(false);
                    BoughtBalls[2] = true;
                    selectBallButtons[2].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy3":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();
                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[3].gameObject.SetActive(false);
                    BoughtBalls[3] = true;
                    selectBallButtons[3].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy4":
                if (coins >= 200)
                {
            if (vibrateOn) Vibration.VibratePeek();
                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[4].gameObject.SetActive(false);
                    BoughtBalls[4] = true;
                    selectBallButtons[4].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy5":
                if (coins >= 200)
                {
                    
            if (vibrateOn) Vibration.VibratePeek();
                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[5].gameObject.SetActive(false);
                    BoughtBalls[5] = true;
                    selectBallButtons[5].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy6":
                if (coins >= 200)
                {
            if (vibrateOn) Vibration.VibratePeek();
                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[6].gameObject.SetActive(false);
                    BoughtBalls[6] = true;
                    selectBallButtons[6].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy7":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[7].gameObject.SetActive(false);
                    BoughtBalls[7] = true;
                    selectBallButtons[7].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy8":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[8].gameObject.SetActive(false);
                    BoughtBalls[8] = true;
                    selectBallButtons[8].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy9":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[9].gameObject.SetActive(false);
                    BoughtBalls[9] = true;
                    selectBallButtons[9].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy10":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[10].gameObject.SetActive(false);
                    BoughtBalls[10] = true;
                    selectBallButtons[10].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy11":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[11].gameObject.SetActive(false);
                    BoughtBalls[11] = true;
                    selectBallButtons[11].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy12":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[12].gameObject.SetActive(false);
                    BoughtBalls[12] = true;
                    selectBallButtons[12].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy13":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[13].gameObject.SetActive(false);
                    BoughtBalls[13] = true;
                    selectBallButtons[13].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy14":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[14].gameObject.SetActive(false);
                    BoughtBalls[14] = true;
                    selectBallButtons[14].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy15":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[15].gameObject.SetActive(false);
                    BoughtBalls[15] = true;
                    selectBallButtons[15].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy16":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[16].gameObject.SetActive(false);
                    BoughtBalls[16] = true;
                    selectBallButtons[16].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy17":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[17].gameObject.SetActive(false);
                    BoughtBalls[17] = true;
                    selectBallButtons[17].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy18":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[18].gameObject.SetActive(false);
                    BoughtBalls[18] = true;
                    selectBallButtons[18].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy19":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[19].gameObject.SetActive(false);
                    BoughtBalls[19] = true;
                    selectBallButtons[19].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy20":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[20].gameObject.SetActive(false);
                    BoughtBalls[20] = true;
                    selectBallButtons[20].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy21":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[21].gameObject.SetActive(false);
                    BoughtBalls[21] = true;
                    selectBallButtons[21].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy22":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[22].gameObject.SetActive(false);
                    BoughtBalls[22] = true;
                    selectBallButtons[22].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy23":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[23].gameObject.SetActive(false);
                    BoughtBalls[23] = true;
                    selectBallButtons[23].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy24":
                if (coins >= 200)
                {
            if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[24].gameObject.SetActive(false);
                    BoughtBalls[24] = true;
                    selectBallButtons[24].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy25":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[25].gameObject.SetActive(false);
                    BoughtBalls[25] = true;
                    selectBallButtons[25].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy26":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[26].gameObject.SetActive(false);
                    BoughtBalls[26] = true;
                    selectBallButtons[26].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy27":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[27].gameObject.SetActive(false);
                    BoughtBalls[27] = true;
                    selectBallButtons[27].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy28":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[28].gameObject.SetActive(false);
                    BoughtBalls[28] = true;
                    selectBallButtons[28].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy29":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[29].gameObject.SetActive(false);
                    BoughtBalls[29] = true;
                    selectBallButtons[29].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy30":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[30].gameObject.SetActive(false);
                    BoughtBalls[30] = true;
                    selectBallButtons[30].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy31":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[31].gameObject.SetActive(false);
                    BoughtBalls[31] = true;
                    selectBallButtons[31].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy32":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[32].gameObject.SetActive(false);
                    BoughtBalls[32] = true;
                    selectBallButtons[32].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy33":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[33].gameObject.SetActive(false);
                    BoughtBalls[33] = true;
                    selectBallButtons[33].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy34":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[34].gameObject.SetActive(false);
                    BoughtBalls[34] = true;
                    selectBallButtons[34].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy35":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[35].gameObject.SetActive(false);
                    BoughtBalls[35] = true;
                    selectBallButtons[35].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy36":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[36].gameObject.SetActive(false);
                    BoughtBalls[36] = true;
                    selectBallButtons[36].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy37":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[37].gameObject.SetActive(false);
                    BoughtBalls[37] = true;
                    selectBallButtons[37].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy38":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[38].gameObject.SetActive(false);
                    BoughtBalls[38] = true;
                    selectBallButtons[38].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy39":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[39].gameObject.SetActive(false);
                    BoughtBalls[39] = true;
                    selectBallButtons[39].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy40":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[40].gameObject.SetActive(false);
                    BoughtBalls[40] = true;
                    selectBallButtons[40].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy41":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[41].gameObject.SetActive(false);
                    BoughtBalls[41] = true;
                    selectBallButtons[41].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy42":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[42].gameObject.SetActive(false);
                    BoughtBalls[42] = true;
                    selectBallButtons[42].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy43":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[43].gameObject.SetActive(false);
                    BoughtBalls[43] = true;
                    selectBallButtons[43].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy44":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[44].gameObject.SetActive(false);
                    BoughtBalls[44] = true;
                    selectBallButtons[44].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy45":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[45].gameObject.SetActive(false);
                    BoughtBalls[45] = true;
                    selectBallButtons[45].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy46":
                if (coins >= 200)
                {
            if (vibrateOn) Vibration.VibratePeek();
                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[46].gameObject.SetActive(false);
                    BoughtBalls[46] = true;
                    selectBallButtons[46].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy47":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[47].gameObject.SetActive(false);
                    BoughtBalls[47] = true;
                    selectBallButtons[47].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
            case "Buy48":
                if (coins >= 200)
                {
                    if (vibrateOn) Vibration.VibratePeek();

                    if (audioOn) newBallAudio.Play();
                    coins -= 200;
                    pc.GameCoins.text = coins.ToString();
                    BallBuyButtons[48].gameObject.SetActive(false);
                    BoughtBalls[48] = true;
                    selectBallButtons[48].SetActive(true);
                    unlockedBalls++;
                    save();
                }
                break;
        }
        if (coins < 200 && audioOn) Select.Play();
        shopBallCounts.text = unlockedBalls + "/49";
        if (unlockedBalls >= 20 && !blueColour) blueColour = true;
        if (blueColour)
        {
            unlockedThemes++;
            Locks[1].SetActive(false);
            ColourButtons[1].interactable = true;
            colourinfobuttons[1].SetActive(false);
        }
        InstantiateUnlockBallsChallenge();
        canCollectChallengesNumber.text = (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge).ToString();
        //if (canCollectgamesplayedChallenge + canCollectJumpChallenge + canCollectReachLevelChallenge + canCollectunlockballsChallenge + canCollectuseturboChallenge + canCollectwinwithoutcoinsChallenge + canCollectwinwithoutjumpsChallenge + canCollectBreakPlatformsChallenge + canCollectMagnetUsedChallenge == 0) canCollectChallengesNumber.transform.parent.gameObject.SetActive(false);
        save();
    }
    public void SelectBall()
    {
        if (audioOn) Select.Play();
        string selectNumber = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        switch (selectNumber) {
            case "select0":
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[0].SetActive(false);
                ballTicks[selectedBall].SetActive(false);
                selectedBall = 0;
                ballTicks[selectedBall].SetActive(true);
                save();
            break;
            case "select1":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[1].SetActive(false);
                selectedBall = 1;
                ballTicks[selectedBall].SetActive(true);
                save();
            break;
            case "select2":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[2].SetActive(false);
                selectedBall = 2;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select3":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[3].SetActive(false);
                selectedBall = 3;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select4":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[4].SetActive(false);
                selectedBall = 4;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select5":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[5].SetActive(false);
                selectedBall = 5;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select6":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[6].SetActive(false);
                selectedBall = 6;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select7":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[7].SetActive(false);
                selectedBall = 7;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select8":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[8].SetActive(false);
                selectedBall = 8;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select9":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[9].SetActive(false);
                selectedBall = 9;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select10":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[10].SetActive(false);
                selectedBall = 10;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select11":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[11].SetActive(false);
                selectedBall = 11;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select12":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[12].SetActive(false);
                selectedBall = 12;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select13":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[13].SetActive(false);
                selectedBall = 13;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select14":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[14].SetActive(false);
                selectedBall = 14;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select15":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[15].SetActive(false);
                selectedBall = 15;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select16":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[16].SetActive(false);
                selectedBall = 16;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select17":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[17].SetActive(false);
                selectedBall = 17;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select18":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[18].SetActive(false);
                selectedBall = 18;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select19":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[19].SetActive(false);
                selectedBall = 19;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select20":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[20].SetActive(false);
                selectedBall = 20;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select21":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[21].SetActive(false);
                selectedBall = 21;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select22":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[22].SetActive(false);
                selectedBall = 22;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select23":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[23].SetActive(false);
                selectedBall = 23;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select24":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[24].SetActive(false);
                selectedBall = 24;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select25":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[25].SetActive(false);
                selectedBall = 25;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select26":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[26].SetActive(false);
                selectedBall = 26;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select27":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[27].SetActive(false);
                selectedBall = 27;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select28":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[28].SetActive(false);
                selectedBall = 28;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select29":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[29].SetActive(false);
                selectedBall = 29;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select30":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[30].SetActive(false);
                selectedBall = 30;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select31":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[31].SetActive(false);
                selectedBall = 31;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select32":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[32].SetActive(false);
                selectedBall = 32;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select33":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[33].SetActive(false);
                selectedBall = 33;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select34":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[34].SetActive(false);
                selectedBall = 34;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select35":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[35].SetActive(false);
                selectedBall = 35;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select36":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[36].SetActive(false);
                selectedBall = 36;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select37":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[37].SetActive(false);
                selectedBall = 37;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select38":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[38].SetActive(false);
                selectedBall = 38;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select39":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[39].SetActive(false);
                selectedBall = 39;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select40":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[40].SetActive(false);
                selectedBall = 40;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select41":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[41].SetActive(false);
                selectedBall = 41;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select42":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[42].SetActive(false);
                selectedBall = 42;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select43":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[43].SetActive(false);
                selectedBall = 43;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select44":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[44].SetActive(false);
                selectedBall = 44;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select45":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[45].SetActive(false);
                selectedBall = 45;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select46":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[46].SetActive(false);
                selectedBall = 46;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select47":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[47].SetActive(false);
                selectedBall = 47;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
            case "select48":
                ballTicks[selectedBall].SetActive(false);
                selectBallButtons[selectedBall].SetActive(true);
                selectBallButtons[48].SetActive(false);
                selectedBall = 48;
                ballTicks[selectedBall].SetActive(true);
                save();
                break;
        }
        Player.GetComponent<SpriteRenderer>().sprite = Balls[selectedBall];
    }

    [Obsolete]
    public void AdamColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 10;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#f6d663", out PlayerColour);
        ColorUtility.TryParseHtmlString("#ff004f", out PlatformColour);
        ColorUtility.TryParseHtmlString("#170218", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;
    }
    [Obsolete]
    public void YapykColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 11;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#e9d600", out PlayerColour);
        ColorUtility.TryParseHtmlString("#ff007a", out PlatformColour);
        ColorUtility.TryParseHtmlString("#10091d", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void WhiteColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 0;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#B40036", out PlayerColour);
        ColorUtility.TryParseHtmlString("#F5FF00", out PlatformColour);
        ColorUtility.TryParseHtmlString("#1B030D", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void BlueColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 2;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#003cff", out PlayerColour);
        ColorUtility.TryParseHtmlString("#ffff00", out PlatformColour);
        ColorUtility.TryParseHtmlString("#020216", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void GreenColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 3;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#08ff00", out PlayerColour);
        ColorUtility.TryParseHtmlString("#df00ff", out PlatformColour);
        ColorUtility.TryParseHtmlString("#180214", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void YellowColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 1;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#ffff00", out PlayerColour);
        ColorUtility.TryParseHtmlString("#00f4ff", out PlatformColour);
        ColorUtility.TryParseHtmlString("#1d1d1d", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void RedColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 4;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#d61a00", out PlayerColour);
        ColorUtility.TryParseHtmlString("#ffa300", out PlatformColour);
        ColorUtility.TryParseHtmlString("#180203", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void PurpleColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 6;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#5700ff", out PlayerColour);
        ColorUtility.TryParseHtmlString("#7eff00", out PlatformColour);
        ColorUtility.TryParseHtmlString("#160524", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void PinkColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 5;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#ff00da", out PlayerColour);
        ColorUtility.TryParseHtmlString("#0076ff", out PlatformColour);
        ColorUtility.TryParseHtmlString("#051a2e", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void LimeColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 8;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#7eff00", out PlayerColour);
        ColorUtility.TryParseHtmlString("#ff3f00", out PlatformColour);
        ColorUtility.TryParseHtmlString("#170218", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void OrangeColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 9;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#ff7b00", out PlayerColour);
        ColorUtility.TryParseHtmlString("#0054ff", out PlatformColour);
        ColorUtility.TryParseHtmlString("#020818", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
        PausePanel.GetComponent<Image>().color = CameraColour;

    }
    [Obsolete]
    public void GoluboyColour()
    {
        if (audioOn && instantiatedColour) Select.Play();
        themeTicks[currentColor].SetActive(false);
        currentColor = 7;
        themeTicks[currentColor].SetActive(true);
        save();
        Color PlayerColour = new Color();
        Color PlatformColour = new Color();
        Color CameraColour = new Color();
        ColorUtility.TryParseHtmlString("#00edff", out PlayerColour);
        ColorUtility.TryParseHtmlString("#ffdc00", out PlatformColour);
        ColorUtility.TryParseHtmlString("#021818", out CameraColour);
        pc.coin.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PlayerColour;
        menuCoins.GetComponent<Image>().color = PlayerColour;
        pc.platform.GetComponent<SpriteRenderer>().color = PlatformColour;
        leftBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        rightBorder.GetComponent<SpriteRenderer>().color = PlatformColour;
        Player.GetComponent<SpriteRenderer>().color = PlayerColour;
        trail.color = PlayerColour;
        Camera.main.GetComponent<Camera>().backgroundColor = CameraColour;
        watchVideoCoins.GetComponent<Image>().color = PlayerColour;
        for (int i = 0; i < 8; i++) pc.finish.transform.GetChild(i).GetComponent<SpriteRenderer>().color = PlatformColour;
        pc.explosion.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.deepfallparticle.GetComponent<ParticleSystem>().startColor = PlayerColour;
        pc.PlatformBreakParticle.GetComponent<ParticleSystem>().startColor = PlatformColour;
        for (int i = 1; i <= 48; i++) buyballcoins[i].color = PlayerColour;
        freegiftRewardcoin.color = PlayerColour;
        challengesPanel.GetComponent<Image>().color = CameraColour;
        shop.GetComponent<Image>().color = CameraColour;
        pc.buyCoinsPanel.GetComponent<Image>().color = CameraColour;
        for (int i = 0; i < 11; i++) buyCoinscoins[i].color = PlayerColour;
    }

    public void Purchased400Coins() {
        coins += 400;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + 400;
        pc.GameCoins.text = coins.ToString();
        if (audioOn) freegiftRewardvalue.gameObject.transform.parent.GetComponent<AudioSource>().Play();

        save();
    }
    public void Purchased1000Coins()
    {
        coins += 1000;
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + 1000;
        pc.GameCoins.text = coins.ToString();
            if (audioOn) freegiftRewardvalue.gameObject.transform.parent.GetComponent<AudioSource>().Play();
        save();
    }
    public void Purchased5000Coins()
    {
        coins += 5000;
        pc.GameCoins.text = coins.ToString();
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + 5000;
            if (audioOn) freegiftRewardvalue.gameObject.transform.parent.GetComponent<AudioSource>().Play();
        save();
    }
    public void Purchased10000Coins()
    {
        coins += 10000;
        pc.GameCoins.text = coins.ToString();
        GameObject instAddedCoins = Instantiate(pc.CoinAdded, pc.CoinAddedPos.transform.position, Quaternion.identity, pc.GameCoins.transform.parent) as GameObject;
        instAddedCoins.GetComponent<Text>().text = "+" + 10000;
            if (audioOn) freegiftRewardvalue.gameObject.transform.parent.GetComponent<AudioSource>().Play();
        save();
    }
    public void PurchasedNoAdsAndDoubleCoins()
    {
        if(pc.NoAdsButtonMenu.activeInHierarchy)pc.NoAdsButtonMenu.SetActive(false);
        NoAdsButton.SetActive(false);
        noAdsAnddoubleCoins = true;
        save();
    }

    [Serializable]
    class GameData
    {
        private int canShowInterstitial,magnetUpdatePrice, turboUpdatePrice, xUpdatePrice, turboTime, currentbgmindex, magnetUsed, platformsDestroyed, multiplyScore,unlockedBalls, selectedBall, gamesPlayed, deepDiveUsed, gameswithoutjumps, gameswithoutcoins, jumps, highScore, level, currentscore, coins, currentColor;
        private bool rated,noAdsAnddoubleCoins,newrecord, musicOn, vibrateOn, audioOn, isStartedFirstTime;
        private float magnetTime, xTime, freegiftlefttime, sensitivity, musicTime;
        private bool instagram, diedOnce,greenColour, blueColour, redColour, yellowColour, pinkColour, purpleColour, goluboyColour, limeColour, orangeColour, adamColour, yapykColour;
        private bool[] BoughtBalls;

        private int[] currentReachLevelChallenge;
        private int currentReachLevelChallengeIndex, canCollectReachLevelChallenge, currentReachLevelReward;
        private int[] currentJumpChallenge;
        private int currentJumpChallengeIndex, canCollectJumpChallenge, currentJumpReward;
        private int[] currentwinwithoutcoinsChallenge;
        private int currentwinwithoutcoinsChallengeIndex, canCollectwinwithoutcoinsChallenge, currentwinwithoutcoinsReward;
        private int[] currentwinwithoutjumpsChallenge;
        private int currentwinwithoutjumpsChallengeIndex, canCollectwinwithoutjumpsChallenge, currentwinwithoutjumpsReward;
        private int[] currentuseturboChallenge;
        private int currentuseturboChallengeIndex, canCollectuseturboChallenge, currentuseturboReward;
        private int[] currentgamesplayedChallenge;
        private int currentgamesplayedChallengeIndex, canCollectgamesplayedChallenge, currentgamesplayedReward;
        private int[] currentunlockballsChallenge;
        private int currentunlockballsChallengeIndex, canCollectunlockballsChallenge, currentunlockballsReward;
        private int[] currentMagnetUsedChallenge;
        private int currentMagnetUsedChallengeIndex, canCollectMagnetUsedChallenge, currentMagnetUsedReward;
        private int[] currentBreakPlatformsChallenge;
        private int currentBreakPlatformsChallengeIndex, canCollectBreakPlatformsChallenge, currentBreakPlatformsReward;

        public void SetInstagram(bool instagram)
        {
            this.instagram = instagram;
        }
        public bool GetInstagram()
        {
            return this.instagram;
        }

        public void SetRated(bool rated) {
            this.rated = rated;
        }
        public bool getRated() {
            return this.rated;
        }

        public void SetCanShowInterstitial(int canShowInterstitial) {
            this.canShowInterstitial = canShowInterstitial;
        }
        public int GetCanShowInterstitial() {
            return this.canShowInterstitial;
        }
        public void SetNoAdsAndDoubleCoins(bool NoAdsAndDoubleCoins) {
            this.noAdsAnddoubleCoins = NoAdsAndDoubleCoins;
        }
        public bool GetNoAdsAndDoubleCoins() {
            return this.noAdsAnddoubleCoins;
        }
        public void SetMagnetUpdatePrice(int magnetUpdatePrice)
        {
            this.magnetUpdatePrice = magnetUpdatePrice;
        }
        public int GetMagnetUpdatePrice() {
            return this.magnetUpdatePrice;
        }

        public void SetTurboUpdatePrice(int turboUpdatePrice)
        {
            this.turboUpdatePrice = turboUpdatePrice;
        }
        public int GetTurboUpdatePrice()
        {
            return this.turboUpdatePrice;
        }
        public void SetxUpdatePrice(int xUpdateprice) {
            this.xUpdatePrice = xUpdateprice;
        }
        public int GetxUpdatePrice() {
            return this.xUpdatePrice;
        }
        public void SetTurboTime(int turboTime) {
            this.turboTime = turboTime;
        }
        public int GetTurboTime() {
            return this.turboTime;
        }
        public void SetMagnetTime(float magnetTime) {
            this.magnetTime = magnetTime;
        }
        public float GetMagnetTime() {
            return this.magnetTime;
        }
        public void SetxTime(float xtime) {
            this.xTime = xtime;
        }
        public float GetxTime() {
            return this.xTime;
        }

        public void SetCurrentBGMIndex(int currentbgmindex) {
            this.currentbgmindex = currentbgmindex;
        }
        public int GetCurrentBGMIndex() {
            return this.currentbgmindex;
        }
        public void SetMagnetUsed(int magnetUsed) {
            this.magnetUsed = magnetUsed;
        }
        public int GetMagnetused() {
            return this.magnetUsed;
        }
        public void SetBrokenPlatforms(int platformsDestroyed) {
            this.platformsDestroyed = platformsDestroyed;
        }
        public int GetBrokenPlatforms() {
            return this.platformsDestroyed;
        }

        public void SetMultiplyScore(int multiplyScore) {
            this.multiplyScore = multiplyScore;
        }
        public int GetMultiplyScore() {
            return this.multiplyScore;
        }
        public void SetDiedOnce(bool diedOnce) {
            this.diedOnce = diedOnce;
        }
        public bool GetDiedOnce() {
            return this.diedOnce;
        }

        public void SetNewRecord(bool newrecord) {
            this.newrecord = newrecord;
        }
        public bool GetNewRecord() {
            return this.newrecord;
        }



        public void SetCurrentBreakPlatformsChallengeIndex(int currentBreakPlatformsChallengeIndex)
        {
            this.currentBreakPlatformsChallengeIndex = currentBreakPlatformsChallengeIndex;
        }
        public int GetCurrentBreakPlatformsChallengeIndex()
        {
            return this.currentBreakPlatformsChallengeIndex;
        }

        public void setCanCollectBreakPlatformsChallenge(int cancollectBreakPlatformsChallenge)
        {
            this.canCollectBreakPlatformsChallenge = cancollectBreakPlatformsChallenge;
        }

        public int getCanCollectBreakPlatformsChallenge()
        {
            return this.canCollectBreakPlatformsChallenge;
        }

        public void setCurrentBreakPlatformsReward(int currentBreakPlatformsReward)
        {
            this.currentBreakPlatformsReward = currentBreakPlatformsReward;
        }
        public int getCurrentBreakPlatformsReward()
        {
            return this.currentBreakPlatformsReward;
        }
        public void setCurrentBreakPlatformsChallenge(int[] currentBreakPlatformsChallenge)
        {
            this.currentBreakPlatformsChallenge = currentBreakPlatformsChallenge;
        }
        public int[] getCurrentBreakPlatformsChallenge()
        {
            return this.currentBreakPlatformsChallenge;
        }



        public void SetCurrentMagnetUsedChallengeIndex(int currentMagnetUsedChallengeIndex)
        {
            this.currentMagnetUsedChallengeIndex = currentMagnetUsedChallengeIndex;
        }
        public int GetCurrentMagnetUsedChallengeIndex()
        {
            return this.currentMagnetUsedChallengeIndex;
        }

        public void setCanCollectMagnetUsedChallenge(int cancollectMagnetUsedChallenge)
        {
            this.canCollectMagnetUsedChallenge = cancollectMagnetUsedChallenge;
        }

        public int getCanCollectMagnetUsedChallenge()
        {
            return this.canCollectMagnetUsedChallenge;
        }

        public void setCurrentMagnetUsedReward(int currentMagnetUsedReward)
        {
            this.currentMagnetUsedReward = currentMagnetUsedReward;
        }
        public int getCurrentMagnetUsedReward()
        {
            return this.currentMagnetUsedReward;
        }
        public void setCurrentMagnetUsedChallenge(int[] currentMagnetUsedChallenge)
        {
            this.currentMagnetUsedChallenge = currentMagnetUsedChallenge;
        }
        public int[] getCurrentMagnetUsedChallenge()
        {
            return this.currentMagnetUsedChallenge;
        }




        public void SetCurrentReachLevelChallengeIndex(int currentReachLevelChallengeIndex) {
            this.currentReachLevelChallengeIndex = currentReachLevelChallengeIndex;
        }
        public int GetCurrentReachLevelChallengeIndex() {
            return this.currentReachLevelChallengeIndex;
        }

        public void setCanCollectReachLevelChallenge(int cancollectReachLevelChallenge) {
            this.canCollectReachLevelChallenge = cancollectReachLevelChallenge;
        }

        public int getCanCollectReachLevelChallenge() {
            return this.canCollectReachLevelChallenge;
        }

        public void setCurrentReachLevelReward(int currentReachLevelReward) {
            this.currentReachLevelReward = currentReachLevelReward;
        }
        public int getCurrentReachLevelReward() {
            return this.currentReachLevelReward;
        }
        public void setCurrentReachLevelChallenge(int[] currentReachLevelChallenge) {
            this.currentReachLevelChallenge = currentReachLevelChallenge; 
        }
        public int[] getCurrentReachLevelChallenge() {
            return this.currentReachLevelChallenge;
        }


        public void SetCurrentgamesplayedChallengeIndex(int currentgamesplayedChallengeIndex)
        {
            this.currentgamesplayedChallengeIndex = currentgamesplayedChallengeIndex;
        }
        public int GetCurrentgamesplayedChallengeIndex()
        {
            return this.currentgamesplayedChallengeIndex;
        }

        public void setCanCollectgamesplayedChallenge(int cancollectgamesplayedChallenge)
        {
            this.canCollectgamesplayedChallenge = cancollectgamesplayedChallenge;
        }

        public int getCanCollectgamesplayedChallenge()
        {
            return this.canCollectgamesplayedChallenge;
        }

        public void setCurrentgamesplayedReward(int currentgamesplayedReward)
        {
            this.currentgamesplayedReward = currentgamesplayedReward;
        }
        public int getCurrentgamesplayedReward()
        {
            return this.currentgamesplayedReward;
        }
        public void setCurrentgamesplayedChallenge(int[] currentgamesplayedChallenge)
        {
            this.currentgamesplayedChallenge = currentgamesplayedChallenge;
        }
        public int[] getCurrentgamesplayedChallenge()
        {
            return this.currentgamesplayedChallenge;
        }



        public void SetCurrentunlockballsChallengeIndex(int currentunlockballsChallengeIndex)
        {
            this.currentunlockballsChallengeIndex = currentunlockballsChallengeIndex;
        }
        public int GetCurrentunlockballsChallengeIndex()
        {
            return this.currentunlockballsChallengeIndex;
        }

        public void setCanCollectunlockballsChallenge(int cancollectunlockballsChallenge)
        {
            this.canCollectunlockballsChallenge = cancollectunlockballsChallenge;
        }

        public int getCanCollectunlockballsChallenge()
        {
            return this.canCollectunlockballsChallenge;
        }

        public void setCurrentunlockballsReward(int currentunlockballsReward)
        {
            this.currentunlockballsReward = currentunlockballsReward;
        }
        public int getCurrentunlockballsReward()
        {
            return this.currentunlockballsReward;
        }
        public void setCurrentunlockballsChallenge(int[] currentunlockballsChallenge)
        {
            this.currentunlockballsChallenge = currentunlockballsChallenge;
        }
        public int[] getCurrentunlockballsChallenge()
        {
            return this.currentunlockballsChallenge;
        }



        public void SetCurrentJumpChallengeIndex(int currentJumpChallengeIndex)
        {
            this.currentJumpChallengeIndex = currentJumpChallengeIndex;
        }
        public int GetCurrentJumpChallengeIndex()
        {
            return this.currentJumpChallengeIndex;
        }

        public void setCanCollectJumpChallenge(int cancollectJumpChallenge)
        {
            this.canCollectJumpChallenge = cancollectJumpChallenge;
        }

        public int getCanCollectJumpChallenge()
        {
            return this.canCollectJumpChallenge;
        }

        public void setCurrentJumpReward(int currentJumpReward)
        {
            this.currentJumpReward = currentJumpReward;
        }
        public int getCurrentJumpReward()
        {
            return this.currentJumpReward;
        }
        public void setCurrentJumpChallenge(int[] currentJumpChallenge)
        {
            this.currentJumpChallenge = currentJumpChallenge;
        }
        public int[] getCurrentJumpChallenge()
        {
            return this.currentJumpChallenge;
        }


        public void SetCurrentuseturboChallengeIndex(int currentuseturboChallengeIndex)
        {
            this.currentuseturboChallengeIndex = currentuseturboChallengeIndex;
        }
        public int GetCurrentuseturboChallengeIndex()
        {
            return this.currentuseturboChallengeIndex;
        }

        public void setCanCollectuseturboChallenge(int cancollectuseturboChallenge)
        {
            this.canCollectuseturboChallenge = cancollectuseturboChallenge;
        }

        public int getCanCollectuseturboChallenge()
        {
            return this.canCollectuseturboChallenge;
        }

        public void setCurrentuseturboReward(int currentuseturboReward)
        {
            this.currentuseturboReward = currentuseturboReward;
        }
        public int getCurrentuseturboReward()
        {
            return this.currentuseturboReward;
        }
        public void setCurrentuseturboChallenge(int[] currentuseturboChallenge)
        {
            this.currentuseturboChallenge = currentuseturboChallenge;
        }
        public int[] getCurrentuseturboChallenge()
        {
            return this.currentuseturboChallenge;
        }


        public void SetCurrentwinwithoutcoinsChallengeIndex(int currentwinwithoutcoinsChallengeIndex)
        {
            this.currentwinwithoutcoinsChallengeIndex = currentwinwithoutcoinsChallengeIndex;
        }
        public int GetCurrentwinwithoutcoinsChallengeIndex()
        {
            return this.currentwinwithoutcoinsChallengeIndex;
        }

        public void setCanCollectwinwithoutcoinsChallenge(int cancollectwinwithoutcoinsChallenge)
        {
            this.canCollectwinwithoutcoinsChallenge = cancollectwinwithoutcoinsChallenge;
        }

        public int getCanCollectwinwithoutcoinsChallenge()
        {
            return this.canCollectwinwithoutcoinsChallenge;
        }

        public void setCurrentwinwithoutcoinsReward(int currentwinwithoutcoinsReward)
        {
            this.currentwinwithoutcoinsReward = currentwinwithoutcoinsReward;
        }
        public int getCurrentwinwithoutcoinsReward()
        {
            return this.currentwinwithoutcoinsReward;
        }
        public void setCurrentwinwithoutcoinsChallenge(int[] currentwinwithoutcoinsChallenge)
        {
            this.currentwinwithoutcoinsChallenge = currentwinwithoutcoinsChallenge;
        }
        public int[] getCurrentwinwithoutcoinsChallenge()
        {
            return this.currentwinwithoutcoinsChallenge;
        }


        public void SetCurrentwinwithoutjumpsChallengeIndex(int currentwinwithoutjumpsChallengeIndex)
        {
            this.currentwinwithoutjumpsChallengeIndex = currentwinwithoutjumpsChallengeIndex;
        }
        public int GetCurrentwinwithoutjumpsChallengeIndex()
        {
            return this.currentwinwithoutjumpsChallengeIndex;
        }

        public void setCanCollectwinwithoutjumpsChallenge(int cancollectwinwithoutjumpsChallenge)
        {
            this.canCollectwinwithoutjumpsChallenge = cancollectwinwithoutjumpsChallenge;
        }

        public int getCanCollectwinwithoutjumpsChallenge()
        {
            return this.canCollectwinwithoutjumpsChallenge;
        }

        public void setCurrentwinwithoutjumpsReward(int currentwinwithoutjumpsReward)
        {
            this.currentwinwithoutjumpsReward = currentwinwithoutjumpsReward;
        }
        public int getCurrentwinwithoutjumpsReward()
        {
            return this.currentwinwithoutjumpsReward;
        }
        public void setCurrentwinwithoutjumpsChallenge(int[] currentwinwithoutjumpsChallenge)
        {
            this.currentwinwithoutjumpsChallenge = currentwinwithoutjumpsChallenge;
        }
        public int[] getCurrentwinwithoutjumpsChallenge()
        {
            return this.currentwinwithoutjumpsChallenge;
        }



        public void setUnlockedBalls(int unlockedBalls) {
            this.unlockedBalls = unlockedBalls;
        }
        public int getUnlockedBalls() {
            return this.unlockedBalls;
        }
        public void Setmusic(bool music) {
            this.musicOn = music;

        }
        public bool Getmusic() {
            return this.musicOn;
        }
        public void SetAudio(bool audio) {
            this.audioOn = audio;
        }
        public bool Getaudio() {
            return this.audioOn;
        }
        public void SetMusicTime(float time) {
            this.musicTime = time;
        }
        public float GetMusicTime() {
            return musicTime;
        }
        public void Setvibrate(bool vibrate)
        {
            this.vibrateOn = vibrate;
        }
        public bool Getvibrate() {
            return this.vibrateOn;
        }
        public void SetSensitivity(float sensitivity) {
            this.sensitivity = sensitivity;

        }
        public float GetSensitivity() {
            return this.sensitivity;

        }

        public void SetIsStartedFirstTime(bool toSet)
        {
            this.isStartedFirstTime = toSet;
        }
        public bool GetIsStartedFirstTime()
        {
            return this.isStartedFirstTime;
        }
        public void SetHighScore(int score)
        {
            this.highScore = score;
        }
        public int GetHighScore()
        {
            return this.highScore;

        }
        public void SetLevel(int level) {
            this.level = level;
        }
        public int GetLevel() {
            return this.level;
        }
        public void SetCurrentScore(int score) {
            this.currentscore = score;

        }
        public int GetCurrentScore() {
            return this.currentscore;

        }
        public int getCoins() {
            return this.coins;
        }
        public void SetCoins(int coins) {
            this.coins = coins;
        }

        public void setBlueColour(bool blueColou) {
            this.blueColour = blueColou;
        }
        public bool GetBlueColour() {
            return this.blueColour;


        } public void setRedColour(bool redColour) {
            this.redColour = redColour;
        }
        public bool GetRedColour() {
            return this.redColour;


        } public void setGreenColour(bool greenColour) {
            this.greenColour = greenColour;
        }
        public bool GetGreenColour() {
            return this.greenColour;


        } public void setYellowColour(bool yellowColour) {
            this.yellowColour = yellowColour;
        }
        public bool GetYellowColour() {
            return this.yellowColour;


        } public void setPinkColour(bool pinkColour) {
            this.pinkColour = pinkColour;
        }
        public bool GetPinkColour() {
            return this.pinkColour;


        } public void setPurpleColour(bool purpleColour) {
            this.purpleColour = purpleColour;
        }
        public bool GetPurpleColour() {
            return this.purpleColour;


        } public void setGoluboyColour(bool goluboyColour) {
            this.goluboyColour = goluboyColour;
        }
        public bool GetGoluboyColour() {
            return this.goluboyColour;


        } public void setLimeColour(bool limeColour) {
            this.limeColour = limeColour;
        }
        public bool GetLimeColour() {
            return this.limeColour;


        } public void setOrangeColour(bool orangeColour) {
            this.orangeColour = orangeColour;
        }
        public bool GetOrangeColour() {
            return this.orangeColour;


        } public void setAdamColour(bool adamColour) {
            this.adamColour = adamColour;
        }
        public bool GetAdamColour() {
            return this.adamColour;

        }
        public void setYapykColour(bool yapykColour)
        {
            this.yapykColour = yapykColour;
        }
        public bool GetYapykColour()
        {
            return this.yapykColour;
        }
        public void setCurrentColor(int currentColor) {
            this.currentColor = currentColor;

        }
        public int getcurrentcolor() {
            return this.currentColor;
        }
        public void setJumps(int jumps) {
            this.jumps = jumps; 
        }
        public int getJumps() {
            return this.jumps;
        }
        public void setGamesWithoutCoins(int gameswithoutcoins) {
            this.gameswithoutcoins = gameswithoutcoins;
        }
        public int getGamesWithoutCoins() {
            return this.gameswithoutcoins;
        }
        public void setGamesWithoutJumps(int gameswithoutjumps) {
            this.gameswithoutjumps = gameswithoutjumps;
        }
        public int getGamesWithoutJumps() {
            return this.gameswithoutjumps;
        }
        public void setDeepDiveUsed(int deepDiveUsed) {
            this.deepDiveUsed = deepDiveUsed;
        }
        public int getDeepDiveUsed() {
            return this.deepDiveUsed;
        }
        public void setGamesPlayed(int gamesPlayed) {
            this.gamesPlayed = gamesPlayed;
        }
        public int getGamesPlayed() {
            return this.gamesPlayed;
        }
        public void setSelectedBall(int selectedBall) {
            this.selectedBall = selectedBall;   
        }
        public int GetSelectedBall() {
            return this.selectedBall;
        }
        public void setBoughtBalls(bool[] boughtBalls) {
            this.BoughtBalls = boughtBalls;
        }
        public bool[] getBoughtBalls (){
            return this.BoughtBalls;
        }
        public void setFreeGiftLeftTime(float freegiftlefttime) {
            this.freegiftlefttime = freegiftlefttime;
        }
        public float getFreeGiftLeftTime() {
            return this.freegiftlefttime;
        }
    }
}