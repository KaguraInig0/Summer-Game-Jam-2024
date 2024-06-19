using Assets.Model;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    /// <summary>
    /// Intro Setup Variables
    /// </summary>
    public string username { get; private set; }
    [SerializeField] public List<PartyModel> party { get; private set; }
    public int budget { get; private set; }
    [SerializeField] public int currentMoney { get; private set; }

    /// <summary>
    /// All In Game Item Variables
    /// </summary>
    [SerializeField] public townLocations currentDestination { get; private set; }
    [SerializeField] public int tiresNum { get; private set; }
    [SerializeField] public int snacksNum { get; private set; }
    [SerializeField] public int fishbaitNum { get; private set; }
    [SerializeField] public int gameNum { get; private set; }
    [SerializeField] public int medicineNum { get; private set; }

    /// <summary>
    /// Gameplay Variables
    /// </summary>
    public int daysLeft { get; private set; }
    [SerializeField] public float gasNum { get; private set; }
    [SerializeField] public float userHealth { get; private set; }
    public float currentProgress { get; private set; }

    /// <summary>
    /// End Game Scene Variables
    /// </summary>
    public float totalTime { get; private set; }
    public float homeToSolvangTime { get; private set; }
    public float solvangToPismoTime { get; private set; }
    public float pismoToMontereyTime { get; private set; }
    public float montereyToSFTime { get; private set; }
    public int relicsNum { get; private set; }

    // Settings
    public float audioVolume { get; private set; }

    public enum townLocations
    {
        Home,
        Solvang,
        Pismo,
        Monterey,
        SanFrancisco
    }

    public static Manager GetInstance() { return me; }

    public static Manager me;
    void Awake()
    {
        if (me != null) 
        {
            Destroy(gameObject);
            return;
        }

        me = this;  
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        party = new List<PartyModel>();

        budget = 500;
        currentMoney = budget;

        tiresNum = 0;
        snacksNum = 0;
        fishbaitNum = 0;
        gameNum = 0;
        medicineNum = 0;

        currentDestination = townLocations.Solvang;

        daysLeft = 5;
        gasNum = 100;
        userHealth = 100;

        totalTime = 0;
        homeToSolvangTime = 0;
        solvangToPismoTime = 0;
        pismoToMontereyTime = 0;
        montereyToSFTime = 0;

        relicsNum = 0;

        // Settings
        audioVolume = 0.5f;
    }

    int lessCheck(int initial, int new_value) { int val = initial - new_value; if (val >= 0) return val; else return 0; }

    #region PARTY FUNCTIONS
    public void AddMember(PartyModel newMember)
    {
        if (party.Exists(m => m.Name == newMember.Name))
        {
            Debug.Log("Member with this name already exists.");
        }
        else
        {
            party.Add(newMember);
            Debug.Log($"{newMember.Name} has been added to the party.");
        }
    }

    void RemoveMember(string name)
    {
        PartyModel member = party.Find(m => m.Name == name);
        if (member != null)
        {
            party.Remove(member);
            Debug.Log($"{name} has been removed from the party.");
        }
        else
        {
            Debug.Log("Member not found.");
        }
    }

    public void changeHealthToMember(string name, float healthToAdd)
    {
        PartyModel member = party.Find(m => m.Name == name);
        if (member != null)
        {
            if ((member.Health + healthToAdd) > 100 || (member.Health + healthToAdd) < 0) return;

            member.Health += healthToAdd;
        }
        else
        {
            Console.WriteLine("Member not found.");
        }
    }
    #endregion

    #region INTRO SETUP
    public void setUsername(string name) => username = name;
    public void addToParty(string name) { AddMember(new PartyModel { Name = name, Health = 100 }); }
    public void removeToParty(string name) => RemoveMember(name);

    public void setBudget(int value) => budget = value;
    public void increaseMoneyCount(int value) { currentMoney += value; }
    public void decreaseMoneyCount(int value) => currentMoney = lessCheck(currentMoney, value);
    public void increaseHealthToMember(string name, float value) => changeHealthToMember(name, value);
    public void decreaseHealthToMember(string name, float value) => changeHealthToMember(name, -value);
    #endregion

    #region IN GAME ITEMS
    /// <summary>
    /// All the Gameplay Items
    /// </summary>
    /// <param name="value"></param>
    public void increaseTireCount(int value) => tiresNum += value;
    public void decreaseTireCount(int value) => tiresNum = lessCheck(tiresNum, value);
    public void setTireCount(int value) => tiresNum = value;

    public void increaseSnackCount(int value) => snacksNum += value;
    public void decreaseSnackCount(int value) => snacksNum = lessCheck(snacksNum, value);
    public void setSnackCount(int value) => snacksNum = value;

    public void increaseFishbaitCount(int value) => fishbaitNum += value;
    public void decreaseFishbaitCount(int value) => fishbaitNum = lessCheck(fishbaitNum, value);
    public void setFishbaitCount(int value) => fishbaitNum = value;

    public void increaseMedicineCount(int value) => medicineNum += value;
    public void decreaseMedicineCount(int value) => medicineNum = lessCheck(medicineNum, value);
    public void setMedicineCount(int value) => medicineNum = value;

    public void increaseGameCount(int value) => gameNum += value;
    public void decreaseGameCount(int value) => gameNum = lessCheck(gameNum, value);

    public void setDestination(townLocations town) => currentDestination = town;
    #endregion

    #region GAMEPLAY
    public void decreaseDaysLeftCount() => daysLeft -= 1;

    public void setGasCount(float value) => gasNum = value;
    public void increaseGasCount(float value) => gasNum += value;
    public void decreaseGasCount(float value)
    {
        float val = gasNum - value;
        if (val >= 0) gasNum = val; else gasNum = 0; 
    }

    public void increaseUserHealth(float value) => userHealth += value;
    public void decreaseUserHealth(float value)
    {
        float val = userHealth - value;
        Debug.Log(val);
        if (val >= 0) userHealth = val; else userHealth = 0;
    }

    public void setCurrentProgress(float value) => currentProgress = value;
    #endregion

    #region Settings
    public void SetAudioVolume(float value) => audioVolume = value;
    #endregion
}
