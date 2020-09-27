using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class GameController : MonoBehaviour
{
    public NinjaController maleNinja;
    public NinjaController femaleNinja;
    public NinjaController maleNinjaTwo;

    public ZombieController zombieOne;
    public ZombieController zombieTwo;
    public ZombieController zombieThree;

    public List<ISpeed> turnOrder = new List<ISpeed>();
    public List<int> turnOrderNumber = new List<int>();

    public int maxSpeed;

    public bool nextTurn;

    public Transform defaultTarget;

    public GameObject firstAttackButton;
    public GameObject secondAttackButton;

    public int numberOfEnemiesDefeated;
    public int numberOfHeroesDefeated;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject loadingScreen;
    public Slider loadingSlider;

    public Image firstStar;
    public Image secondStar;
    public Image thirdStar;

    public TMP_Text cooldownText;


    // Start is called before the first frame update
    void Start()
    {

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        loadingScreen.SetActive(false);
        cooldownText.text = "";

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "LevelOne" || scene.name == "LevelTwo" || scene.name == "LevelThree")
        {
            numberOfEnemiesDefeated = 0;

            maleNinja.turnSpeed = 101;
            maleNinja.turnRate = 36;
            maleNinja.skill2Cooldown = 0;

            femaleNinja.turnSpeed = 85;
            femaleNinja.turnRate = 26;
            femaleNinja.skill2Cooldown = 0;

            maleNinjaTwo.turnSpeed = 95;
            maleNinjaTwo.turnRate = 21;
            maleNinjaTwo.skill2Cooldown = 0;

            zombieOne.turnSpeed = 60;
            zombieOne.turnRate = 18;

            zombieTwo.turnSpeed = 70;
            zombieTwo.turnRate = 16;

            zombieThree.turnSpeed = 55;
            zombieThree.turnRate = 11;

            maleNinja.arrow.SetActive(true);
        }
        if (scene.name == "LevelTwo2" || scene.name == "LevelThree2" || scene.name == "LevelThree3")
        {
            numberOfEnemiesDefeated = 0;
            maleNinja.turnSpeed = SaveDataSingleton.Instance.ninjaOneSpeed;
            maleNinja.turnRate = 36;
            maleNinja.skill2Cooldown = SaveDataSingleton.Instance.ninjaOneSkill2Cooldown;

            femaleNinja.turnSpeed = SaveDataSingleton.Instance.ninjaTwoSpeed;
            femaleNinja.turnRate = 26;
            femaleNinja.skill2Cooldown = SaveDataSingleton.Instance.ninjaTwoSkill2Cooldown;

            maleNinjaTwo.turnSpeed = SaveDataSingleton.Instance.ninjaThreeSpeed;
            maleNinjaTwo.turnRate = 21;
            maleNinjaTwo.skill2Cooldown = SaveDataSingleton.Instance.ninjaThreeSkill2Cooldown;

            zombieOne.turnSpeed = 60;
            zombieOne.turnRate = 18;
            zombieTwo.turnSpeed = 70;
            zombieTwo.turnRate = 16;
            zombieThree.turnSpeed = 55;
            zombieThree.turnRate = 11;
            maleNinja.health = SaveDataSingleton.Instance.ninjaOneHealth;
            femaleNinja.health = SaveDataSingleton.Instance.ninjaTwoHealth;
            maleNinjaTwo.health = SaveDataSingleton.Instance.ninjaThreeHealth;


            maleNinja.healthSlider.value = maleNinja.health;
            femaleNinja.healthSlider.value = femaleNinja.health;
            maleNinjaTwo.healthSlider.value = maleNinjaTwo.health;
        }

        nextTurn = true;
        maxSpeed = 100;
        Invoke("sortList", 0.2f);
        defaultTarget = zombieOne.transform;
        //MaleNinja.Arrow.SetActive(true);
        zombieOne.arrow.SetActive(true);
        if (scene.name == "LevelOne" || scene.name == "LevelTwo" || scene.name == "LevelThree")
        {
            firstAttackButton.SetActive(true);
            secondAttackButton.SetActive(true);
        }
        else
        {
            firstAttackButton.SetActive(false);
            secondAttackButton.SetActive(false);

            nextTurn = false;
            Invoke("CheckNextTurn", 0.3f);
        }

        maleNinja.speedSlider.value = maleNinja.turnSpeed;
        femaleNinja.speedSlider.value = femaleNinja.turnSpeed;
        maleNinjaTwo.speedSlider.value = maleNinjaTwo.turnSpeed;
        zombieOne.speedSlider.value = zombieOne.turnSpeed;
        zombieTwo.speedSlider.value = zombieTwo.turnSpeed;
        zombieThree.speedSlider.value = zombieThree.turnSpeed;


        Color c = firstStar.color;
        c.a = 0f;
        firstStar.color = c;

        Color d = secondStar.color;
        d.a = 0f;
        secondStar.color = d;

        Color e = thirdStar.color;
        e.a = 0f;
        thirdStar.color = e;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if(hit.collider == null)
                {
                    return;
                }
                if (hit.collider.tag == "Zombie")
                {
                    zombieOne.arrow.SetActive(true);
                    zombieTwo.arrow.SetActive(false);
                    zombieThree.arrow.SetActive(false);

                    if (turnOrder[0].ToString() == maleNinja.ToString())
                    {
                        defaultTarget = hit.transform;
                    }
                    if (turnOrder[0].ToString() == femaleNinja.ToString())
                    {
                        defaultTarget = hit.transform;
                    }
                    if (turnOrder[0].ToString() == maleNinjaTwo.ToString())
                    {
                        defaultTarget = hit.transform;
                    }
                }
                if (hit.collider.tag == "ZombieTwo")
                {
                    zombieOne.arrow.SetActive(false);
                    zombieTwo.arrow.SetActive(true);
                    zombieThree.arrow.SetActive(false);

                    if (turnOrder[0].ToString() == maleNinja.ToString())
                    {
                        defaultTarget = hit.transform;
                    }
                    if (turnOrder[0].ToString() == femaleNinja.ToString())
                    {
                        defaultTarget = hit.transform;
                    }
                    if (turnOrder[0].ToString() == maleNinjaTwo.ToString())
                    {
                        defaultTarget = hit.transform;
                    }
                }
                if (hit.collider.tag == "ZombieThree")
                {
                    zombieOne.arrow.SetActive(false);
                    zombieTwo.arrow.SetActive(false);
                    zombieThree.arrow.SetActive(true);

                    if (turnOrder[0].ToString() == maleNinja.ToString())
                    {
                        defaultTarget = hit.transform;
                    }
                    if (turnOrder[0].ToString() == femaleNinja.ToString())
                    {
                        defaultTarget = hit.transform;
                    }
                    if (turnOrder[0].ToString() == maleNinjaTwo.ToString())
                    {
                        defaultTarget = hit.transform;
                    }
                }
            }
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            if(Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                if (hit)
                {
                    if (hit.collider == null)
                    {
                        return;
                    }
                    if (hit.collider.tag == "Zombie")
                    {
                        zombieOne.arrow.SetActive(true);
                        zombieTwo.arrow.SetActive(false);
                        zombieThree.arrow.SetActive(false);

                        if (turnOrder[0].ToString() == maleNinja.ToString())
                        {
                            defaultTarget = hit.transform;
                        }
                        if (turnOrder[0].ToString() == femaleNinja.ToString())
                        {
                            defaultTarget = hit.transform;
                        }
                        if (turnOrder[0].ToString() == maleNinjaTwo.ToString())
                        {
                            defaultTarget = hit.transform;
                        }
                    }
                    if (hit.collider.tag == "ZombieTwo")
                    {
                        zombieOne.arrow.SetActive(false);
                        zombieTwo.arrow.SetActive(true);
                        zombieThree.arrow.SetActive(false);

                        if (turnOrder[0].ToString() == maleNinja.ToString())
                        {
                            defaultTarget = hit.transform;
                        }
                        if (turnOrder[0].ToString() == femaleNinja.ToString())
                        {
                            defaultTarget = hit.transform;
                        }
                        if (turnOrder[0].ToString() == maleNinjaTwo.ToString())
                        {
                            defaultTarget = hit.transform;
                        }
                    }
                    if (hit.collider.tag == "ZombieThree")
                    {
                        zombieOne.arrow.SetActive(false);
                        zombieTwo.arrow.SetActive(false);
                        zombieThree.arrow.SetActive(true);

                        if (turnOrder[0].ToString() == maleNinja.ToString())
                        {
                            defaultTarget = hit.transform;
                        }
                        if (turnOrder[0].ToString() == femaleNinja.ToString())
                        {
                            defaultTarget = hit.transform;
                        }
                        if (turnOrder[0].ToString() == maleNinjaTwo.ToString())
                        {
                            defaultTarget = hit.transform;
                        }
                    }
                }
            }
        }
    }

    public void sortList()
    {
        turnOrder.Clear();
        if(maleNinja.isActiveAndEnabled)
        {
            turnOrder.Add(maleNinja);
        }
        if (femaleNinja.isActiveAndEnabled)
        {
            turnOrder.Add(femaleNinja);
        }
        if (maleNinjaTwo.isActiveAndEnabled)
        {
            turnOrder.Add(maleNinjaTwo);
        }
        if (zombieOne.isActiveAndEnabled)
        {
            turnOrder.Add(zombieOne);
        }
        if (zombieTwo.isActiveAndEnabled)
        {
            turnOrder.Add(zombieTwo);
        }
        if (zombieThree.isActiveAndEnabled)
        {
            turnOrder.Add(zombieThree);
        }
        turnOrder.Sort();
        turnOrder.Reverse();
    }



    public void OnFirstAttackButtonClick()
    {
        if (turnOrder[0].ToString() == maleNinja.ToString())
        {
            TurnOffAttackButtons();
            maleNinja.FirstAttackButtonPress(defaultTarget);
            maleNinja.turnSpeed = 0;
            nextTurn = false;

        }
        else if (turnOrder[0].ToString() == femaleNinja.ToString())
        {
            TurnOffAttackButtons();
            femaleNinja.FirstAttackButtonPress(defaultTarget);
            femaleNinja.turnSpeed = 0;
            nextTurn = false;

        }
        else if (turnOrder[0].ToString() == maleNinjaTwo.ToString())
        {
            TurnOffAttackButtons();
            maleNinjaTwo.FirstAttackButtonPress(defaultTarget);
            maleNinjaTwo.turnSpeed = 0;
            nextTurn = false;

        }
    }

    public void OnSecondAttackButtonClick()
    {
        if (turnOrder[0].ToString() == maleNinja.ToString())
        {
            TurnOffAttackButtons();
            maleNinja.RangedAttack(defaultTarget.transform.position);
            maleNinja.turnSpeed = 0;
            nextTurn = false;

        }
        else if (turnOrder[0].ToString() == femaleNinja.ToString())
        {
            TurnOffAttackButtons();
            femaleNinja.RangedAttack(defaultTarget.transform.position);
            femaleNinja.turnSpeed = 0;
            nextTurn = false;

        }
        else if (turnOrder[0].ToString() == maleNinjaTwo.ToString())
        {
            TurnOffAttackButtons();
            maleNinjaTwo.RangedAttack(defaultTarget.transform.position);
            maleNinjaTwo.turnSpeed = 0;
            nextTurn = false;

        }
    }

    public void CheckNextTurn()
    {
        while (!nextTurn)
        {
            sortList();

            if (turnOrder[0].speed > maxSpeed)
            {
                if (turnOrder[0].ToString() == maleNinja.ToString())
                {
                    if(maleNinja.skill2Cooldown != 0)
                    {
                        maleNinja.skill2Cooldown -= 1;
                    }
                    StartCoroutine(TurnOnAttackButtons(maleNinja));
                    nextTurn = true;
                    StartCoroutine(FirstNinjaArrows());
                    femaleNinja.turnSpeed += femaleNinja.turnRate;
                    maleNinjaTwo.turnSpeed += maleNinjaTwo.turnRate;
                    zombieOne.turnSpeed += zombieOne.turnRate;
                    zombieTwo.turnSpeed += zombieTwo.turnRate;
                    zombieThree.turnSpeed += zombieThree.turnRate;
                    UpdateSpeedSliders();
                }
                else if (turnOrder[0].ToString() == femaleNinja.ToString())
                {
                    if (femaleNinja.skill2Cooldown != 0)
                    {
                        femaleNinja.skill2Cooldown -= 1;
                    }
                    StartCoroutine(TurnOnAttackButtons(femaleNinja));
                    nextTurn = true;
                    StartCoroutine(SecondNinjaArrows());
                    maleNinja.turnSpeed += maleNinja.turnRate;
                    maleNinjaTwo.turnSpeed += maleNinjaTwo.turnRate;
                    zombieOne.turnSpeed += zombieOne.turnRate;
                    zombieTwo.turnSpeed += zombieTwo.turnRate;
                    zombieThree.turnSpeed += zombieThree.turnRate;
                    UpdateSpeedSliders();
                }
                else if (turnOrder[0].ToString() == maleNinjaTwo.ToString())
                {
                    if (maleNinjaTwo.skill2Cooldown != 0)
                    {
                        maleNinjaTwo.skill2Cooldown -= 1;
                    }
                    StartCoroutine(TurnOnAttackButtons(maleNinjaTwo));
                    nextTurn = true;
                    StartCoroutine(ThirdNinjaArrows());
                    maleNinja.turnSpeed += maleNinja.turnRate;
                    femaleNinja.turnSpeed += femaleNinja.turnRate;
                    zombieOne.turnSpeed += zombieOne.turnRate;
                    zombieTwo.turnSpeed += zombieTwo.turnRate;
                    zombieThree.turnSpeed += zombieThree.turnRate;
                    UpdateSpeedSliders();
                }
                else if (turnOrder[0].ToString() == zombieOne.ToString())
                {
                    StartCoroutine(FirstZombieAttatck());
                    nextTurn = true;
                    maleNinja.arrow.SetActive(false);
                    femaleNinja.arrow.SetActive(false);
                    maleNinjaTwo.arrow.SetActive(false);
                    maleNinja.turnSpeed += maleNinja.turnRate;
                    femaleNinja.turnSpeed += femaleNinja.turnRate;
                    maleNinjaTwo.turnSpeed += maleNinjaTwo.turnRate;
                    zombieTwo.turnSpeed += zombieTwo.turnRate;
                    zombieThree.turnSpeed += zombieThree.turnRate;
                    UpdateSpeedSliders();
                }
                else if (turnOrder[0].ToString() == zombieTwo.ToString())
                {
                    StartCoroutine(SecondZombieAttatck());
                    nextTurn = true;
                    maleNinja.arrow.SetActive(false);
                    femaleNinja.arrow.SetActive(false);
                    maleNinjaTwo.arrow.SetActive(false);
                    maleNinja.turnSpeed += maleNinja.turnRate;
                    femaleNinja.turnSpeed += femaleNinja.turnRate;
                    maleNinjaTwo.turnSpeed += maleNinjaTwo.turnRate;
                    zombieOne.turnSpeed += zombieOne.turnRate;
                    zombieThree.turnSpeed += zombieThree.turnRate;
                    UpdateSpeedSliders();
                }
                else if (turnOrder[0].ToString() == zombieThree.ToString())
                {
                    StartCoroutine(ThirdZombieAttatck());
                    nextTurn = true;
                    maleNinja.arrow.SetActive(false);
                    femaleNinja.arrow.SetActive(false);
                    maleNinjaTwo.arrow.SetActive(false);
                    maleNinja.turnSpeed += maleNinja.turnRate;
                    femaleNinja.turnSpeed += femaleNinja.turnRate;
                    maleNinjaTwo.turnSpeed += maleNinjaTwo.turnRate;
                    zombieOne.turnSpeed += zombieOne.turnRate;
                    zombieTwo.turnSpeed += zombieTwo.turnRate;
                    UpdateSpeedSliders();
                }
            }
            else if (turnOrder[0].speed < maxSpeed)
            {
                maleNinja.turnSpeed += maleNinja.turnRate;
                femaleNinja.turnSpeed += femaleNinja.turnRate;
                maleNinjaTwo.turnSpeed += maleNinjaTwo.turnRate;
                zombieOne.turnSpeed += zombieOne.turnRate;
                zombieTwo.turnSpeed += zombieTwo.turnRate;
                zombieThree.turnSpeed += zombieThree.turnRate;
                UpdateSpeedSliders();
            }
        }
    }

    IEnumerator TurnOnAttackButtons(NinjaController ninja)
    {
        yield return new WaitForSeconds(0.5f);
        firstAttackButton.SetActive(true);
        if (ninja.skill2Cooldown == 0)
        {
            secondAttackButton.SetActive(true);
            secondAttackButton.GetComponent<Button>().interactable = true;
            cooldownText.text = "";
        }
        else if (ninja.skill2Cooldown != 0)
        {
            secondAttackButton.SetActive(true);
            secondAttackButton.GetComponent<Button>().interactable = false;
            cooldownText.text = ninja.skill2Cooldown.ToString();
        }
    }

    public void TurnOffAttackButtons()
    {
        firstAttackButton.SetActive(false);
        secondAttackButton.SetActive(false);
    }

    IEnumerator FirstZombieAttatck()
    {
        yield return new WaitForSeconds(2.5f);
        zombieOne.MeleeAttackZombie();
        zombieOne.turnSpeed = 0;
        nextTurn = false;
    }

    IEnumerator SecondZombieAttatck()
    {
        yield return new WaitForSeconds(2.5f);
        zombieTwo.MeleeAttackZombie();
        zombieTwo.turnSpeed = 0;
        nextTurn = false;
    }

    IEnumerator ThirdZombieAttatck()
    {
        yield return new WaitForSeconds(2.5f);
        zombieThree.MeleeAttackZombie();
        zombieThree.turnSpeed = 0;
        nextTurn = false;
    }

    IEnumerator FirstNinjaArrows()
    {
        yield return new WaitForSeconds(0.1f);
        maleNinja.arrow.SetActive(true);
        femaleNinja.arrow.SetActive(false);
        maleNinjaTwo.arrow.SetActive(false);
    }

    IEnumerator SecondNinjaArrows()
    {
        yield return new WaitForSeconds(0.1f);
        maleNinja.arrow.SetActive(false);
        femaleNinja.arrow.SetActive(true);
        maleNinjaTwo.arrow.SetActive(false);
    }

    IEnumerator ThirdNinjaArrows()
    {
        yield return new WaitForSeconds(0.1f);
        maleNinja.arrow.SetActive(false);
        femaleNinja.arrow.SetActive(false);
        maleNinjaTwo.arrow.SetActive(true);
    }

    public void UpdateSpeedSliders()
    {
        maleNinja.speedSlider.value = maleNinja.turnSpeed;
        femaleNinja.speedSlider.value = femaleNinja.turnSpeed;
        maleNinjaTwo.speedSlider.value = maleNinjaTwo.turnSpeed;
        zombieOne.speedSlider.value = zombieOne.turnSpeed;
        zombieTwo.speedSlider.value = zombieTwo.turnSpeed;
        zombieThree.speedSlider.value = zombieThree.turnSpeed;
    }

    IEnumerator CheckScore()
    {
        yield return new WaitForSeconds(0.5f);

        NextStage();
        
        if (!zombieOne.isActiveAndEnabled || !zombieTwo.isActiveAndEnabled || !zombieThree.isActiveAndEnabled)
        {
                if (zombieOne.isActiveAndEnabled)
                {
                    zombieOne.arrow.SetActive(true);
                    defaultTarget = zombieOne.transform;

                    if (zombieTwo.isActiveAndEnabled)
                    {
                        zombieTwo.arrow.SetActive(false);
                    }
                    if (zombieThree.isActiveAndEnabled)
                    {
                        zombieThree.arrow.SetActive(false);
                    }
                }
                else if (zombieTwo.isActiveAndEnabled)
                {
                    zombieTwo.arrow.SetActive(true);
                    defaultTarget = zombieTwo.transform;

                    if (zombieOne.isActiveAndEnabled)
                    {
                        zombieOne.arrow.SetActive(false);
                    }
                    if (zombieThree.isActiveAndEnabled)
                    {
                        zombieThree.arrow.SetActive(false);
                    }
                }
                else if (zombieThree.isActiveAndEnabled)
                {
                    zombieThree.arrow.SetActive(true);
                    defaultTarget = zombieThree.transform;

                    if (zombieOne.isActiveAndEnabled)
                    {
                        zombieOne.arrow.SetActive(false);
                    }
                    if (zombieTwo.isActiveAndEnabled)
                    {
                        zombieTwo.arrow.SetActive(false);
                    }
                }
        }
            if (!maleNinja.isActiveAndEnabled || !femaleNinja.isActiveAndEnabled || !maleNinjaTwo.isActiveAndEnabled)
            {
                if (zombieOne.isActiveAndEnabled)
                {
                    zombieOne.target.Clear();
                    if (maleNinja.isActiveAndEnabled)
                    {
                        zombieOne.target.Add(maleNinja.transform);
                    }
                    if (femaleNinja.isActiveAndEnabled)
                    {
                        zombieOne.target.Add(femaleNinja.transform);
                    }
                    if (maleNinjaTwo.isActiveAndEnabled)
                    {
                        zombieOne.target.Add(maleNinjaTwo.transform);
                    }
                }
                if (zombieTwo.isActiveAndEnabled)
                {
                    zombieTwo.target.Clear();
                    if (maleNinja.isActiveAndEnabled)
                    {
                        zombieTwo.target.Add(maleNinja.transform);
                    }
                    if (femaleNinja.isActiveAndEnabled)
                    {
                        zombieTwo.target.Add(femaleNinja.transform);
                    }
                    if (maleNinjaTwo.isActiveAndEnabled)
                    {
                        zombieTwo.target.Add(maleNinjaTwo.transform);
                    }
                }
                if (zombieThree.isActiveAndEnabled)
                {
                    zombieThree.target.Clear();
                    if (maleNinja.isActiveAndEnabled)
                    {
                        zombieThree.target.Add(maleNinja.transform);
                    }
                    if (femaleNinja.isActiveAndEnabled)
                    {
                        zombieThree.target.Add(femaleNinja.transform);
                    }
                    if (maleNinjaTwo.isActiveAndEnabled)
                    {
                        zombieThree.target.Add(maleNinjaTwo.transform);
                    }
                }
            }
        
    }

    public void ScoreUpdate()
    {
        StartCoroutine(CheckScore());
    }

    public void OnMainMenuButtonClick()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadYourAsyncScene("MainMenu"));
    }

    public void OnRetryButtonClick()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadYourAsyncScene("LevelOne"));
    }

    IEnumerator LoadYourAsyncScene(string levelName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }

    IEnumerator FadeIn(Image image, float time)
    {
        yield return new WaitForSeconds(time);

        for (float i = 0.1f; i < 1; i += 0.1f)
        {
            Color c = image.color;
            c.a = (i * 2);
            image.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void NextStage()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "LevelOne")
        {
            if (numberOfEnemiesDefeated >= 3)
            {
                int starCount = 0;
                if (numberOfHeroesDefeated == 0)
                {
                    starCount = 3;
                    StartCoroutine(FadeIn(firstStar, 0.5f));
                    StartCoroutine(FadeIn(secondStar, 1.5f));
                    StartCoroutine(FadeIn(thirdStar, 2.5f));
                }
                else if (numberOfHeroesDefeated == 1)
                {
                    starCount = 2;
                    StartCoroutine(FadeIn(firstStar, 0.5f));
                    StartCoroutine(FadeIn(secondStar, 1.5f));
                }
                else if (numberOfHeroesDefeated == 2)
                {
                    starCount = 1;
                    StartCoroutine(FadeIn(firstStar, 0.5f));
                }
                SaveDataSingleton.Instance.finishedLevelOne = true;
                SaveDataSingleton.Instance.levelOneStarCount = starCount;
                winPanel.SetActive(true);
            }
            else if (numberOfHeroesDefeated >= 3)
            {
                losePanel.SetActive(true);
            }
        }
        else if (scene.name == "LevelTwo")
        {
            if (numberOfEnemiesDefeated >= 3)
            {
                SaveDataSingleton.Instance.ninjaOneHealth = maleNinja.health;
                SaveDataSingleton.Instance.ninjaTwoHealth = femaleNinja.health;
                SaveDataSingleton.Instance.ninjaThreeHealth = maleNinjaTwo.health;

                SaveDataSingleton.Instance.ninjaOneSpeed = maleNinja.turnSpeed;
                SaveDataSingleton.Instance.ninjaTwoSpeed = femaleNinja.turnSpeed;
                SaveDataSingleton.Instance.ninjaThreeSpeed = maleNinjaTwo.turnSpeed;

                SaveDataSingleton.Instance.ninjaOneSkill2Cooldown = maleNinja.skill2Cooldown;
                SaveDataSingleton.Instance.ninjaTwoSkill2Cooldown = femaleNinja.skill2Cooldown;
                SaveDataSingleton.Instance.ninjaThreeSkill2Cooldown = maleNinjaTwo.skill2Cooldown;

                StartCoroutine(LoadYourAsyncScene("LevelTwo2"));
            }
            else if (numberOfHeroesDefeated >= 3)
            {
                losePanel.SetActive(true);
            }
        }
        else if (scene.name == "LevelTwo2")
        {
            if (numberOfEnemiesDefeated >= 2)
            {
                int starCount = 0;
                if (numberOfHeroesDefeated == 0)
                {
                    starCount = 3;
                    StartCoroutine(FadeIn(firstStar, 0.5f));
                    StartCoroutine(FadeIn(secondStar, 1.5f));
                    StartCoroutine(FadeIn(thirdStar, 2.5f));
                }
                else if (numberOfHeroesDefeated == 1)
                {
                    starCount = 2;
                    StartCoroutine(FadeIn(firstStar, 0.5f));
                    StartCoroutine(FadeIn(secondStar, 1.5f));
                }
                else if (numberOfHeroesDefeated == 2)
                {
                    starCount = 1;
                    StartCoroutine(FadeIn(firstStar, 0.5f));
                }
                SaveDataSingleton.Instance.finishedLevelTwo = true;
                SaveDataSingleton.Instance.levelTwoStarCount = starCount;
                winPanel.SetActive(true);
            }
            else if (numberOfHeroesDefeated >= 3)
            {
                losePanel.SetActive(true);
            }
        }
        else if (scene.name == "LevelThree")
        {
            if (numberOfEnemiesDefeated >= 2)
            {
                SaveDataSingleton.Instance.ninjaOneHealth = maleNinja.health;
                SaveDataSingleton.Instance.ninjaTwoHealth = femaleNinja.health;
                SaveDataSingleton.Instance.ninjaThreeHealth = maleNinjaTwo.health;

                SaveDataSingleton.Instance.ninjaOneSpeed = maleNinja.turnSpeed;
                SaveDataSingleton.Instance.ninjaTwoSpeed = femaleNinja.turnSpeed;
                SaveDataSingleton.Instance.ninjaThreeSpeed = maleNinjaTwo.turnSpeed;

                StartCoroutine(LoadYourAsyncScene("LevelThree2"));
            }
            else if (numberOfHeroesDefeated >= 3)
            {
                losePanel.SetActive(true);
            }
        }
        else if (scene.name == "LevelThree2")
        {
            if (numberOfEnemiesDefeated >= 3)
            {
                SaveDataSingleton.Instance.ninjaOneHealth = maleNinja.health;
                SaveDataSingleton.Instance.ninjaTwoHealth = femaleNinja.health;
                SaveDataSingleton.Instance.ninjaThreeHealth = maleNinjaTwo.health;

                SaveDataSingleton.Instance.ninjaOneSpeed = maleNinja.turnSpeed;
                SaveDataSingleton.Instance.ninjaTwoSpeed = femaleNinja.turnSpeed;
                SaveDataSingleton.Instance.ninjaThreeSpeed = maleNinjaTwo.turnSpeed;

                StartCoroutine(LoadYourAsyncScene("LevelThree3"));
            }
            else if (numberOfHeroesDefeated >= 3)
            {
                losePanel.SetActive(true);
            }
        }
        else if (scene.name == "LevelThree3")
        {
            if (numberOfEnemiesDefeated >= 1)
            {
                int starCount = 0;
                if (numberOfHeroesDefeated == 0)
                {
                    starCount = 3;
                    StartCoroutine(FadeIn(firstStar, 0.5f));
                    StartCoroutine(FadeIn(secondStar, 1.5f));
                    StartCoroutine(FadeIn(thirdStar, 2.5f));
                }
                else if (numberOfHeroesDefeated == 1)
                {
                    starCount = 2;
                    StartCoroutine(FadeIn(firstStar, 0.5f));
                    StartCoroutine(FadeIn(secondStar, 1.5f));
                }
                else if (numberOfHeroesDefeated == 2)
                {
                    starCount = 1;
                    StartCoroutine(FadeIn(firstStar, 0.5f));
                }
                SaveDataSingleton.Instance.finishedLevelThree = true;
                SaveDataSingleton.Instance.levelThreeStarCount = starCount;
                winPanel.SetActive(true);
            }
            else if (numberOfHeroesDefeated >= 3)
            {
                losePanel.SetActive(true);
            }
        }
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
