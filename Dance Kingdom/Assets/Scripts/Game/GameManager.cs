using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Class GameManager, that controls the whole game.
public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject controlsEdit;
    public GameObject imageOptsMenu;
    public GameObject soundOptsMenu;

    public GameObject countDown;
    public Button btn;
    private Button resumeButton;
    private bool fromGame;

    public GameObject blackFilterCD;
    public GameObject enterInitials;
    public GameObject inputField;

    public bool startPlaying;

    public static GameManager instance;
    public string songName;
    public int lvl;
    private bool enterRanking;

    //Musical part.
    public int goldPerNote;
    public int[] multiplierThresholds;

    public int currentGold;
    public int currentMultiplier;
    public int multiplierTracker;

    public int enemyGold;
    public int enemyMultiplier;
    public int enemyMultiplierTracker;
    public float counterProb;
    public float noteHitProb;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public TextMeshProUGUI downTimerText;
    public float downTimer;
    private float songTime;

    public bool hasBPMUpdate;
    public float timeWhenBPMUpdate;
    public float newBPM;

    //Strategic part.
    public GameObject allyTroops;
    public GameObject enemyTroops;
    public GameObject archer;
    public GameObject knight;
    public GameObject squire;

    public Transform shopSelectionArrow;
    public Transform streetSelectionArrow;
    public GameObject backStreetLight;
    public GameObject middleStreetLight;
    public GameObject frontStreetLight;
    public Transform shopLight;
    private int shopSelector;
    private int streetSelector;

    public SpriteRenderer shield;
    public SpriteRenderer bow;
    public SpriteRenderer sword;

    public int allyHP;
    public int enemyHP;
    public Transform allyBar;
    public Transform enemyBar;
    public int damageOnHit;

    public bool timeOver;

    public bool gameOver;
    public TextMeshProUGUI gameOverText;
    public Button buttonBack;

    public int generatedGold;

    //On Start instance itself, set scoreText to 0 and multiplier to 1.
    //Then starts the music and send notes down.
    //Also ignore collision between layers and set some variables.
    void Start()
    {
        instance = this;
        scoreText.text = "Gold: " + 0;
        currentMultiplier = 1;
        enemyMultiplier = 1;
        AudioManager.instance.ManageAudio("MainTheme", "music", "stop");

        fromGame = true;
        resumeButton = btn.GetComponent<Button>();
        StartCoroutine("StartDelay");

        //Ignorar colisiones entre capas.
        Physics2D.IgnoreLayerCollision(10, 10);
        Physics2D.IgnoreLayerCollision(11, 11);

        shopSelector = 0;
        streetSelector = 0;

        allyHP = 100;
        enemyHP = 100;

        gameOver = false;
        songTime = downTimer;
        enterRanking = false;
    }

    //On Update, we manage all game events.
    void Update()
    {
        //Timer ingame.
        if (downTimer > 0)
            downTimer -= Time.deltaTime;

        int seconds = (int)(downTimer % 60);
        int minutes = (int)((downTimer / 60) % 60);

        string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);

        downTimerText.text = timerString;

        //If gold is more than 20:
        if (currentGold >= 20)
        {
            shield.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            bow.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            sword.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

            //Instantiate troop.
            if (Input.GetKeyDown(GlobalVars.globalVars.keyBinds["TroopKeyC"]) && !timeOver && !gameOver && Time.timeScale != 0)
            {

                switch (shopSelector)
                {
                    case 0:
                        switch (streetSelector)
                        {
                            case 0:
                                GameObject squireTroop0 = Instantiate(squire, new Vector3(-5.2f, 1.8f, 5.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), allyTroops.transform);
                                squireTroop0.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                                squireTroop0.GetComponent<TroopManager>().allyOrEnemy = true;
                                squireTroop0.GetComponent<TroopManager>().type = 0;
                                squireTroop0.GetComponent<TroopManager>().street = 0;
                                squireTroop0.layer = 10;
                                squireTroop0.transform.tag = "AllySquire";
                                break;
                            case 1:
                                GameObject squireTroop1 = Instantiate(squire, new Vector3(-5.9f, 1.0f, 4.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), allyTroops.transform);
                                squireTroop1.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                                squireTroop1.GetComponent<TroopManager>().allyOrEnemy = true;
                                squireTroop1.GetComponent<TroopManager>().type = 0;
                                squireTroop1.GetComponent<TroopManager>().street = 1;
                                squireTroop1.layer = 10;
                                squireTroop1.transform.tag = "AllySquire";
                                break;
                            case 2:
                                GameObject squireTroop2 = Instantiate(squire, new Vector3(-6.6f, 0.1f, 3.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), allyTroops.transform);
                                squireTroop2.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                                squireTroop2.GetComponent<TroopManager>().allyOrEnemy = true;
                                squireTroop2.GetComponent<TroopManager>().type = 0;
                                squireTroop2.GetComponent<TroopManager>().street = 2;
                                squireTroop2.layer = 10;
                                squireTroop2.transform.tag = "AllySquire";
                                break;
                        }
                        break;
                    case 1:
                        switch (streetSelector)
                        {
                            case 0:
                                GameObject archerTroop0 = Instantiate(archer, new Vector3(-5.1f, 1.8f, 5.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), allyTroops.transform);
                                archerTroop0.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                                archerTroop0.GetComponent<TroopManager>().allyOrEnemy = true;
                                archerTroop0.GetComponent<TroopManager>().type = 1;
                                archerTroop0.GetComponent<TroopManager>().street = 0;
                                archerTroop0.layer = 10;
                                archerTroop0.transform.tag = "AllyArcher";
                                break;
                            case 1:
                                GameObject archerTroop1 = Instantiate(archer, new Vector3(-5.75f, 1.0f, 4.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), allyTroops.transform);
                                archerTroop1.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                                archerTroop1.GetComponent<TroopManager>().allyOrEnemy = true;
                                archerTroop1.GetComponent<TroopManager>().type = 1;
                                archerTroop1.GetComponent<TroopManager>().street = 1;
                                archerTroop1.layer = 10;
                                archerTroop1.transform.tag = "AllyArcher";
                                break;
                            case 2:
                                GameObject archerTroop2 = Instantiate(archer, new Vector3(-6.4f, 0.1f, 3.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), allyTroops.transform);
                                archerTroop2.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                                archerTroop2.GetComponent<TroopManager>().allyOrEnemy = true;
                                archerTroop2.GetComponent<TroopManager>().type = 1;
                                archerTroop2.GetComponent<TroopManager>().street = 2;
                                archerTroop2.layer = 10;
                                archerTroop2.transform.tag = "AllyArcher";
                                break;
                        }
                        break;
                    case 2:
                        switch (streetSelector)
                        {
                            case 0:
                                GameObject knightTroop0 = Instantiate(knight, new Vector3(-4.8f, 1.8f, 5.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), allyTroops.transform);
                                knightTroop0.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                                knightTroop0.GetComponent<TroopManager>().allyOrEnemy = true;
                                knightTroop0.GetComponent<TroopManager>().type = 2;
                                knightTroop0.GetComponent<TroopManager>().street = 0;
                                knightTroop0.layer = 10;
                                knightTroop0.transform.tag = "AllyKnight";
                                break;
                            case 1:
                                GameObject knightTroop1 = Instantiate(knight, new Vector3(-5.4f, 1.0f, 4.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), allyTroops.transform);
                                knightTroop1.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                                knightTroop1.GetComponent<TroopManager>().allyOrEnemy = true;
                                knightTroop1.GetComponent<TroopManager>().type = 2;
                                knightTroop1.GetComponent<TroopManager>().street = 1;
                                knightTroop1.layer = 10;
                                knightTroop1.transform.tag = "AllyKnight";
                                break;
                            case 2:
                                GameObject knightTroop2 = Instantiate(knight, new Vector3(-6.0f, 0.1f, 3.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), allyTroops.transform);
                                knightTroop2.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                                knightTroop2.GetComponent<TroopManager>().allyOrEnemy = true;
                                knightTroop2.GetComponent<TroopManager>().type = 2;
                                knightTroop2.GetComponent<TroopManager>().street = 2;
                                knightTroop2.layer = 10;
                                knightTroop2.transform.tag = "AllyKnight";
                                break;
                        }
                        break;
                } //End of shopSelector switch.
                AudioManager.instance.ManageAudio("Gold", "sound", "play");
                currentGold -= 20;
                scoreText.text = "Gold: " + currentGold;
            } //Input If end.
        } // Gold If end.
        else
        {
            shield.color = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
            bow.color = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
            sword.color = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
        }


        //Change shop selection.
        if (Input.GetKeyDown(GlobalVars.globalVars.keyBinds["ShopKeyC"]) && !timeOver && !gameOver && Time.timeScale != 0)
        {
            switch (shopSelector)
            {
                case 0:
                    shopSelector = 1;
                    shopSelectionArrow.position = new Vector3(-3.69f, -4.67f, -9.0f);
                    shopLight.position = new Vector3(-3.69f, -2.91f, -9.0f);
                    break;
                case 1:
                    shopSelector = 2;
                    shopSelectionArrow.position = new Vector3(-1.01f, -4.67f, -9.0f);
                    shopLight.position = new Vector3(-1.01f, -2.91f, -9.0f);
                    break;
                case 2:
                    shopSelector = 0;
                    shopSelectionArrow.position = new Vector3(-6.37f, -4.67f, -9.0f);
                    shopLight.position = new Vector3(-6.37f, -2.91f, -9.0f);
                    break;
            }
        }

        //Change street selection.
        if (Input.GetKeyDown(GlobalVars.globalVars.keyBinds["StreetKeyC"]) && !timeOver && !gameOver && Time.timeScale != 0)
        {
            switch (streetSelector)
            {
                case 0:
                    streetSelector = 1;
                    streetSelectionArrow.position = new Vector3(-8.4f, 0.75f, -9.0f);
                    backStreetLight.gameObject.SetActive(false);
                    middleStreetLight.gameObject.SetActive(true);
                    break;
                case 1:
                    streetSelector = 2;
                    streetSelectionArrow.position = new Vector3(-8.4f, -0.3f, -9.0f);
                    middleStreetLight.gameObject.SetActive(false);
                    frontStreetLight.gameObject.SetActive(true);
                    break;
                case 2:
                    streetSelector = 0;
                    streetSelectionArrow.position = new Vector3(-8.4f, 1.75f, -9.0f);
                    frontStreetLight.gameObject.SetActive(false);
                    backStreetLight.gameObject.SetActive(true);
                    break;
            }
        }

        //Control the IA.
        manageIA();

        //We check if time is over.
        if (downTimer <= 0)
            timeOver = true;

        //We check if game has ended.
        if (!timeOver && !gameOver)
        {
            if (allyHP <= 0)
            {
                timeOver = true;
                gameOver = true;
                Time.timeScale = 0;
                AudioManager.instance.ManageAudio(songName, "music", "stop");
                blackFilterCD.gameObject.SetActive(true);
                buttonBack.gameObject.SetActive(true);
                gameOverText.text = "You Lose.";
            }
            else if (enemyHP <= 0)
            {
                timeOver = true;
                gameOver = true;
                Time.timeScale = 0;
                AudioManager.instance.ManageAudio(songName, "music", "stop");
                blackFilterCD.gameObject.SetActive(true);
                enterRanking = RankingManager.instance.checkRanking(generatedGold, downTimer, songTime, lvl);
                if (enterRanking)
                    enterInitials.gameObject.SetActive(true);
                else
                {
                    gameOverText.text = "You Win!";
                    buttonBack.gameObject.SetActive(true);
                }
            }
        }
        else if (!gameOver)
        {
            AudioManager.instance.ManageAudio(songName, "music", "stop");
            TroopManager[] allyChilds = allyTroops.GetComponentsInChildren<TroopManager>();
            TroopManager[] enemyChilds = enemyTroops.GetComponentsInChildren<TroopManager>();

            if (allyChilds.Length == 0 && enemyChilds.Length == 0)
            {
                gameOver = true;
                Time.timeScale = 0;
                blackFilterCD.gameObject.SetActive(true);
                if (allyHP > enemyHP)
                {
                    enterRanking = RankingManager.instance.checkRanking(generatedGold, downTimer, songTime, lvl);
                    if (enterRanking)
                        enterInitials.gameObject.SetActive(true);
                    else
                    {
                        gameOverText.text = "You Win!";
                        buttonBack.gameObject.SetActive(true);
                    }
                }
                else if (allyHP < enemyHP)
                {
                    buttonBack.gameObject.SetActive(true);
                    gameOverText.text = "You Lose.";
                }
                else if (allyHP == enemyHP)
                {
                    buttonBack.gameObject.SetActive(true);
                    gameOverText.text = "Draw.";
                }
            } else
            {
                if (allyHP <= 0)
                {
                    timeOver = true;
                    gameOver = true;
                    Time.timeScale = 0;
                    AudioManager.instance.ManageAudio(songName, "music", "stop");
                    blackFilterCD.gameObject.SetActive(true);
                    buttonBack.gameObject.SetActive(true);
                    gameOverText.text = "You Lose.";
                }
                else if (enemyHP <= 0)
                {
                    timeOver = true;
                    gameOver = true;
                    Time.timeScale = 0;
                    AudioManager.instance.ManageAudio(songName, "music", "stop");
                    blackFilterCD.gameObject.SetActive(true);
                    enterRanking = RankingManager.instance.checkRanking(generatedGold, downTimer, songTime, lvl);
                    if (enterRanking)
                        enterInitials.gameObject.SetActive(true);
                    else
                    {
                        gameOverText.text = "You Win!";
                        buttonBack.gameObject.SetActive(true);
                    }
                }
            }
        }

        //Pause menu navigation.
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!pauseMenu.activeSelf)
                {
                    if (optionsMenu.activeSelf)
                    {
                        fromGame = false;
                        pauseMenu.SetActive(true);
                        optionsMenu.SetActive(false);
                        AudioManager.instance.ManageAudio("Button", "sound", "play");
                    }
                    else if (controlsEdit.activeSelf)
                    {
                        optionsMenu.SetActive(true);
                        controlsEdit.SetActive(false);
                        AudioManager.instance.ManageAudio("Button", "sound", "play");
                    }
                    else if (imageOptsMenu.activeSelf)
                    {
                        optionsMenu.SetActive(true);
                        imageOptsMenu.SetActive(false);
                        AudioManager.instance.ManageAudio("Button", "sound", "play");
                    }
                    else if (soundOptsMenu.activeSelf)
                    {
                        optionsMenu.SetActive(true);
                        soundOptsMenu.SetActive(false);
                        AudioManager.instance.ManageAudio("Button", "sound", "play");
                    }
                    else
                    {
                        fromGame = true;
                        pauseMenu.SetActive(true);
                        pauseGame();
                        AudioManager.instance.ManageAudio("Button", "sound", "play");
                    }
                }
                else
                {
                    fromGame = false;
                    pauseMenu.SetActive(false);
                    pauseGame();
                }
            } //Input If end.
        } //Gameover If end.

        if (gameOver)
        {
            string iText = inputField.GetComponent<TextMeshProUGUI>().text;
            if (!iText.Equals(iText.ToUpper()))
            {
                iText = iText.ToUpper();
            }
        }

        if (hasBPMUpdate)
        {
            if ((songTime - downTimer) >= timeWhenBPMUpdate)
            {
                BeatScroller.instance.beatTempo = newBPM / 60.0f;
            }
        }


    } //Update end.

    //When player hits a note, give him gold and count the multiplier.
    public void NoteHit()
    {
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[(currentMultiplier - 1)] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        currentGold += goldPerNote * currentMultiplier;
        generatedGold += goldPerNote * currentMultiplier;
        scoreText.text = "Gold: " + currentGold;
        NotesIA();
    }

    //When player misses a note, set multiplier to 1.
    public void NoteMissed()
    {
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
        NotesIA();
    }

    public void NotesIA()
    {
        float rn = Random.Range(0f, 1f);
        if (rn < noteHitProb)
        {
            if (enemyMultiplier - 1 < multiplierThresholds.Length)
            {
                enemyMultiplierTracker++;

                if (multiplierThresholds[(enemyMultiplier - 1)] <= enemyMultiplierTracker)
                {
                    enemyMultiplierTracker = 0;
                    enemyMultiplier++;
                }
            }
            enemyGold += goldPerNote * enemyMultiplier;
        }
        else
        {
            enemyMultiplier = 1;
            enemyMultiplierTracker = 0;
        }
    }

    //Manage the IA which player has to fight.
    //Counter on counterProb %.
    //Get gold in noteHitProb% at notes (this is in NotesIA() at NoteHit() and NoteMissed() methods).
    public void manageIA()
    {
        if (enemyGold >= 20)
        {
            int rnShop = Random.Range(0, 3);
            int rnStreet = Random.Range(0, 3);
            float rnRandom = Random.Range(0f, 1f);
            TroopManager[] allyChilds = allyTroops.GetComponentsInChildren<TroopManager>();
            TroopManager[] enemyChilds = enemyTroops.GetComponentsInChildren<TroopManager>();

            if (enemyHP <= 25)
                rnRandom /= 2;

            if (rnRandom < counterProb && allyChilds.Length != 0)
            {
                float maxX = -7.0f;
                int selectedATroop = 0;
                for (int i = 0; i < allyChilds.Length; i++)
                {
                    if (allyChilds[i].transform.position.x > maxX)
                    {
                        maxX = allyChilds[i].transform.position.x;
                        selectedATroop = i;
                    }

                }

                TroopManager randomObject = allyChilds[selectedATroop];
                bool countered = false;

                if (enemyChilds.Length != 0)
                {
                    for (int i = 0; i < enemyChilds.Length; i++)
                    {
                        if (enemyChilds[i].street == randomObject.street)
                        {
                            if (randomObject.type == 0)
                            {
                                if (enemyChilds[i].type == 2)
                                {
                                    countered = true;
                                }
                            }
                            else
                            {
                                if (enemyChilds[i].type < randomObject.type)
                                {
                                    countered = true;
                                }
                            }

                        } //If end.
                    } //For end.
                } //If end.

                if (!countered)
                {
                    rnShop = (randomObject.type == 0) ? 2 : randomObject.type - 1;
                    rnStreet = randomObject.street;
                }
            }

            switch (rnShop)
            {
                case 0:
                    switch (rnStreet)
                    {
                        case 0:
                            GameObject squireTroop0 = Instantiate(squire, new Vector3(0.7f, 1.8f, 5.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), enemyTroops.transform);
                            squireTroop0.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                            squireTroop0.GetComponent<TroopManager>().allyOrEnemy = false;
                            squireTroop0.GetComponent<TroopManager>().type = 0;
                            squireTroop0.GetComponent<TroopManager>().street = 0;
                            squireTroop0.layer = 11;
                            squireTroop0.transform.tag = "EnemySquire";
                            break;
                        case 1:
                            GameObject squireTroop1 = Instantiate(squire, new Vector3(1.4f, 1.0f, 4.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), enemyTroops.transform);
                            squireTroop1.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                            squireTroop1.GetComponent<TroopManager>().allyOrEnemy = false;
                            squireTroop1.GetComponent<TroopManager>().type = 0;
                            squireTroop1.GetComponent<TroopManager>().street = 1;
                            squireTroop1.layer = 11;
                            squireTroop1.transform.tag = "EnemySquire";
                            break;
                        case 2:
                            GameObject squireTroop2 = Instantiate(squire, new Vector3(2.0f, 0.1f, 3.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), enemyTroops.transform);
                            squireTroop2.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                            squireTroop2.GetComponent<TroopManager>().allyOrEnemy = false;
                            squireTroop2.GetComponent<TroopManager>().type = 0;
                            squireTroop2.GetComponent<TroopManager>().street = 2;
                            squireTroop2.layer = 11;
                            squireTroop2.transform.tag = "EnemySquire";
                            break;
                    }
                    break;
                case 1:
                    switch (rnStreet)
                    {
                        case 0:
                            GameObject archerTroop0 = Instantiate(archer, new Vector3(0.7f, 1.8f, 5.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), enemyTroops.transform);
                            archerTroop0.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                            archerTroop0.GetComponent<TroopManager>().allyOrEnemy = false;
                            archerTroop0.GetComponent<TroopManager>().type = 1;
                            archerTroop0.GetComponent<TroopManager>().street = 0;
                            archerTroop0.layer = 11;
                            archerTroop0.transform.tag = "EnemyArcher";
                            break;
                        case 1:
                            GameObject archerTroop1 = Instantiate(archer, new Vector3(1.3f, 1.0f, 4.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), enemyTroops.transform);
                            archerTroop1.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                            archerTroop1.GetComponent<TroopManager>().allyOrEnemy = false;
                            archerTroop1.GetComponent<TroopManager>().type = 1;
                            archerTroop1.GetComponent<TroopManager>().street = 1;
                            archerTroop1.layer = 11;
                            archerTroop1.transform.tag = "EnemyArcher";
                            break;
                        case 2:
                            GameObject archerTroop2 = Instantiate(archer, new Vector3(2.0f, 0.1f, 3.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), enemyTroops.transform);
                            archerTroop2.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                            archerTroop2.GetComponent<TroopManager>().allyOrEnemy = false;
                            archerTroop2.GetComponent<TroopManager>().type = 1;
                            archerTroop2.GetComponent<TroopManager>().street = 2;
                            archerTroop2.layer = 11;
                            archerTroop2.transform.tag = "EnemyArcher";
                            break;
                    }
                    break;
                case 2:
                    switch (rnStreet)
                    {
                        case 0:
                            GameObject knightTroop0 = Instantiate(knight, new Vector3(0.4f, 1.8f, 5.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), enemyTroops.transform);
                            knightTroop0.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
                            knightTroop0.GetComponent<TroopManager>().allyOrEnemy = false;
                            knightTroop0.GetComponent<TroopManager>().type = 2;
                            knightTroop0.GetComponent<TroopManager>().street = 0;
                            knightTroop0.layer = 11;
                            knightTroop0.transform.tag = "EnemyKnight";

                            break;
                        case 1:
                            GameObject knightTroop1 = Instantiate(knight, new Vector3(1.0f, 1.0f, 4.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), enemyTroops.transform);
                            knightTroop1.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
                            knightTroop1.GetComponent<TroopManager>().allyOrEnemy = false;
                            knightTroop1.GetComponent<TroopManager>().type = 2;
                            knightTroop1.GetComponent<TroopManager>().street = 1;
                            knightTroop1.layer = 11;
                            knightTroop1.transform.tag = "EnemyKnight";
                            break;
                        case 2:
                            GameObject knightTroop2 = Instantiate(knight, new Vector3(1.7f, 0.1f, 3.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), enemyTroops.transform);
                            knightTroop2.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                            knightTroop2.GetComponent<TroopManager>().allyOrEnemy = false;
                            knightTroop2.GetComponent<TroopManager>().type = 2;
                            knightTroop2.GetComponent<TroopManager>().street = 2;
                            knightTroop2.layer = 11;
                            knightTroop2.transform.tag = "EnemyKnight";
                            break;
                    }
                    break;
            } //End of rnShop switch.
            enemyGold -= 20;
        } //If end.
    }

    //When called, pause the game.
    public void pauseGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            AudioManager.instance.ManageAudio(songName, "music", "unpause");
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            AudioManager.instance.ManageAudio(songName, "music", "pause");
        }
    }

    //Delay to start the game always at the same time.
    IEnumerator StartDelay()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3f;
        float quitAnimTime = Time.realtimeSinceStartup + 3.5f;
        float auxPT = pauseTime;
        float auxQAT = quitAnimTime;
        bool resumeButtonPress = false;

        void pressedResume()
        {
            resumeButtonPress = true;
        }

        resumeButton.onClick.AddListener(pressedResume);

        while (Time.realtimeSinceStartup < pauseTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || resumeButtonPress)
            {
                if (pauseMenu.activeSelf)
                {
                    if (!optionsMenu.activeSelf && !imageOptsMenu.activeSelf && !soundOptsMenu.activeSelf && fromGame)
                    {
                        auxPT = pauseTime - Time.realtimeSinceStartup;
                        auxQAT = quitAnimTime - Time.realtimeSinceStartup;
                        countDown.GetComponent<Animator>().enabled = false;

                        pauseTime = Time.realtimeSinceStartup + 1000000f;
                        quitAnimTime = Time.realtimeSinceStartup + 1000000f;

                    }
                }
                else if (!optionsMenu.activeSelf && !imageOptsMenu.activeSelf && !soundOptsMenu.activeSelf)
                {
                    pauseTime = Time.realtimeSinceStartup + auxPT;
                    quitAnimTime = Time.realtimeSinceStartup + auxQAT;
                    countDown.GetComponent<Animator>().enabled = true;
                    resumeButtonPress = false;
                }
            }
            yield return 0;
        }

        Time.timeScale = 1;
        blackFilterCD.gameObject.SetActive(false);
        startPlaying = true;
        BeatScroller.instance.hasStarted = true;
        AudioManager.instance.ManageAudio(songName, "music", "play");

        while (Time.realtimeSinceStartup < quitAnimTime)
        {
            yield return 0;
        }
        countDown.gameObject.SetActive(false);
    }

    //Set a bar size from the hp.
    public void setBarSize(float sizeNormalized, string bar)
    {
        if (bar == "allyBar")
            allyBar.localScale = new Vector3(sizeNormalized, 1f, 1f);
        if (bar == "enemyBar")
            enemyBar.localScale = new Vector3(sizeNormalized, 1f, 1f);
    }

    //Update the ranking with the initial
    public void saveInitials()
    {
        string initials = inputField.GetComponent<TextMeshProUGUI>().text;
        if (!initials.Equals(""))
        {
            initials = initials.ToUpper();
            gameOverText.text = "You Win!";
            RankingManager.instance.updateRanking(generatedGold, downTimer, songTime, initials, lvl);
        }
    }
}
