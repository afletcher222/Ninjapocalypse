using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataSingleton : MonoBehaviour
{
    public static SaveDataSingleton Instance { get; set; }

    public int levelOneStarCount;
    public int levelTwoStarCount;
    public int levelThreeStarCount;

    public bool playedOnce;

    public bool finishedLevelOne;
    public bool hadFadedInLevelOne;

    public bool finishedLevelTwo;
    public bool hadFadedInLevelTwo;

    public bool finishedLevelThree;
    public bool hadFadedInLevelThree;

    public int ninjaOneHealth;
    public int ninjaTwoHealth;
    public int ninjaThreeHealth;

    public int ninjaOneSpeed;
    public int ninjaTwoSpeed;
    public int ninjaThreeSpeed;

    public int ninjaOneSkill2Cooldown;
    public int ninjaTwoSkill2Cooldown;
    public int ninjaThreeSkill2Cooldown;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
