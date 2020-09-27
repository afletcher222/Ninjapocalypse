using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject levelSelectQuitButton;
    public GameObject helpScreen;
    public GameObject creditsScreen;
    public GameObject loadingScreen;
    public Slider loadingSlider;

    public Image firstStarLevelOne;
    public Image secondStarLevelOne;
    public Image thirdStarLevelOne;
    public Image firstStarLevelTwo;
    public Image secondStarLevelTwo;
    public Image thirdStarLevelTwo;
    public Image firstStarLevelThree;
    public Image secondStarLevelThree;
    public Image thridStarLevelThree;



    public void Start()
    {
        helpScreen.SetActive(false);
        creditsScreen.SetActive(false);
        loadingScreen.SetActive(false);

        Color c = firstStarLevelOne.color;
        c.a = 0f;
        firstStarLevelOne.color = c;

        Color d = secondStarLevelOne.color;
        d.a = 0f;
        secondStarLevelOne.color = d;

        Color e = thirdStarLevelOne.color;
        e.a = 0f;
        thirdStarLevelOne.color = e;

        Color f = firstStarLevelTwo.color;
        f.a = 0f;
        firstStarLevelTwo.color = f;

        Color g = secondStarLevelTwo.color;
        g.a = 0f;
        secondStarLevelTwo.color = g;

        Color h = thirdStarLevelTwo.color;
        h.a = 0f;
        thirdStarLevelTwo.color = h;

        Color i = firstStarLevelThree.color;
        i.a = 0f;
        firstStarLevelThree.color = i;

        Color j = secondStarLevelThree.color;
        j.a = 0f;
        secondStarLevelThree.color = j;

        Color k = thridStarLevelThree.color;
        k.a = 0f;
        thridStarLevelThree.color = k;
    
        if (SaveDataSingleton.Instance.playedOnce == false)
        {
            menuScreen.SetActive(true);
            levelSelectQuitButton.SetActive(false);

        }
        else
        {
            menuScreen.SetActive(false);
            if(SaveDataSingleton.Instance.finishedLevelOne)
            {
                if (SaveDataSingleton.Instance.levelOneStarCount == 3)
                {

                    if (SaveDataSingleton.Instance.hadFadedInLevelOne == false)
                    {
                        StartCoroutine(FadeIn(firstStarLevelOne, 0.5f));
                        StartCoroutine(FadeIn(secondStarLevelOne, 1.5f));
                        StartCoroutine(FadeIn(thirdStarLevelOne, 2.5f));
                        SaveDataSingleton.Instance.hadFadedInLevelOne = true;
                    }
                    else
                    {
                        c = firstStarLevelOne.color;
                        c.a = 1f;
                        firstStarLevelOne.color = c;

                        d = secondStarLevelOne.color;
                        d.a = 1f;
                        secondStarLevelOne.color = d;

                        e = thirdStarLevelOne.color;
                        e.a = 1f;
                        thirdStarLevelOne.color = e;
                    }
                }
                else if (SaveDataSingleton.Instance.levelOneStarCount == 2)
                {
                    if (SaveDataSingleton.Instance.hadFadedInLevelOne == false)
                    {
                        StartCoroutine(FadeIn(firstStarLevelOne, 0.5f));
                        StartCoroutine(FadeIn(secondStarLevelOne, 1.5f));
                        SaveDataSingleton.Instance.hadFadedInLevelOne = true;
                    }
                    else
                    {
                        c = firstStarLevelOne.color;
                        c.a = 1f;
                        firstStarLevelOne.color = c;

                        d = secondStarLevelOne.color;
                        d.a = 1f;
                        secondStarLevelOne.color = d;
                    }
                }
                else if (SaveDataSingleton.Instance.levelOneStarCount == 1)
                {
                    if (SaveDataSingleton.Instance.hadFadedInLevelOne == false)
                    {
                        StartCoroutine(FadeIn(firstStarLevelOne, 0.5f));
                        SaveDataSingleton.Instance.hadFadedInLevelOne = true;
                    }
                    else
                    {
                        c = firstStarLevelOne.color;
                        c.a = 1f;
                        firstStarLevelOne.color = c;

                    }
                }
            }
            if (SaveDataSingleton.Instance.finishedLevelTwo)
            {
                if (SaveDataSingleton.Instance.levelTwoStarCount == 3)
                {
                    if (SaveDataSingleton.Instance.hadFadedInLevelTwo == false)
                    {
                        StartCoroutine(FadeIn(firstStarLevelTwo, 0.5f));
                        StartCoroutine(FadeIn(secondStarLevelTwo, 1.5f));
                        StartCoroutine(FadeIn(thirdStarLevelTwo, 2.5f));
                        SaveDataSingleton.Instance.hadFadedInLevelTwo = true;
                    }
                    else
                    {
                        f = firstStarLevelTwo.color;
                        f.a = 1f;
                        firstStarLevelTwo.color = f;

                        g = secondStarLevelTwo.color;
                        g.a = 1f;
                        secondStarLevelTwo.color = g;

                        h = thirdStarLevelTwo.color;
                        h.a = 1f;
                        thirdStarLevelTwo.color = h;
                    }
                }
                else if (SaveDataSingleton.Instance.levelTwoStarCount == 2)
                {
                    if (SaveDataSingleton.Instance.hadFadedInLevelTwo == false)
                    {
                        StartCoroutine(FadeIn(firstStarLevelTwo, 0.5f));
                        StartCoroutine(FadeIn(secondStarLevelTwo, 1.5f));
                        SaveDataSingleton.Instance.hadFadedInLevelTwo = true;
                    }
                    else
                    {
                        f = firstStarLevelTwo.color;
                        f.a = 1f;
                        firstStarLevelTwo.color = f;

                        g = secondStarLevelTwo.color;
                        g.a = 1f;
                        secondStarLevelTwo.color = g;
                    }
                }
                else if (SaveDataSingleton.Instance.levelTwoStarCount == 1)
                {
                    if (SaveDataSingleton.Instance.hadFadedInLevelTwo == false)
                    {
                        StartCoroutine(FadeIn(firstStarLevelTwo, 0.5f));
                        SaveDataSingleton.Instance.hadFadedInLevelTwo = true;
                    }
                    else
                    {
                        f = firstStarLevelTwo.color;
                        f.a = 1f;
                        firstStarLevelTwo.color = f;

                    }
                }
            }
            if (SaveDataSingleton.Instance.finishedLevelThree)
            {
                if (SaveDataSingleton.Instance.levelThreeStarCount == 3)
                {
                    if (SaveDataSingleton.Instance.hadFadedInLevelThree == false)
                    {
                        StartCoroutine(FadeIn(firstStarLevelThree, 0.5f));
                        StartCoroutine(FadeIn(secondStarLevelThree, 1.5f));
                        StartCoroutine(FadeIn(thridStarLevelThree, 2.5f));
                        SaveDataSingleton.Instance.hadFadedInLevelThree = true;
                    }
                    else
                    {
                        i = firstStarLevelThree.color;
                        i.a = 1f;
                        firstStarLevelThree.color = i;

                        j = secondStarLevelThree.color;
                        j.a = 1f;
                        secondStarLevelThree.color = j;

                        k = thridStarLevelThree.color;
                        k.a = 1f;
                        thridStarLevelThree.color = k;
                    }
                }
                else if (SaveDataSingleton.Instance.levelThreeStarCount == 2)
                {
                    if (SaveDataSingleton.Instance.hadFadedInLevelThree == false)
                    {
                        StartCoroutine(FadeIn(firstStarLevelThree, 0.5f));
                        StartCoroutine(FadeIn(secondStarLevelThree, 1.5f));
                        SaveDataSingleton.Instance.hadFadedInLevelThree = true;
                    }
                    else
                    {
                        i = firstStarLevelThree.color;
                        i.a = 1f;
                        firstStarLevelThree.color = i;

                        j = secondStarLevelThree.color;
                        j.a = 1f;
                        secondStarLevelThree.color = j;
                    }
                }
                else if (SaveDataSingleton.Instance.levelThreeStarCount == 1)
                {
                    if (SaveDataSingleton.Instance.hadFadedInLevelThree == false)
                    {
                        StartCoroutine(FadeIn(firstStarLevelThree, 0.5f));
                        SaveDataSingleton.Instance.hadFadedInLevelThree = true;
                    }
                    else
                    {
                        i = firstStarLevelThree.color;
                        i.a = 1f;
                        firstStarLevelThree.color = i;

                    }
                }
            }
        }
    }

    public void OnStartButtonClick()
    {
        SaveDataSingleton.Instance.playedOnce = true;
        menuScreen.SetActive(false);
        levelSelectQuitButton.SetActive(true);
    }

    public void OnHelpButtonClick()
    {
        helpScreen.SetActive(true);
    }

    public void OnHelpScreenCloseButtonClick()
    {
        helpScreen.SetActive(false);
    }

    public void OnCreditsButtonClick()
    {
        creditsScreen.SetActive(true);
    }

    public void OnCreditsScreenCloseButtonClick()
    {
        creditsScreen.SetActive(false);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnLevelOneButtonClick()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadYourAsyncScene("LevelOne"));
    }

    public void OnLevelTwoButtonClick()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadYourAsyncScene("LevelTwo"));
    }

    public void OnLevelThreeButtonClick()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadYourAsyncScene("LevelThree"));
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
}
