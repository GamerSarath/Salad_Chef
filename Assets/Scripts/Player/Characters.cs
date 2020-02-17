using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour
{
    [SerializeField]
    private int serialNumber;
    [SerializeField]
    private string characterName;// variable that hold character name
    [SerializeField]
    private int playerLevel; // variable that hold player level 
    [SerializeField]
    private int playerHealth; // variable that hold player health 
    [SerializeField]
    private int playerCash; // variable that hold player cash 
    [SerializeField]
    private int playerXp; // variable that hold player XP 
    [SerializeField]
    private int playerSpecialAbility; // variable that hold player special ability bar level. Reset once filled to complete 
    [SerializeField]
    private bool lockStatus; // variable that hold character lock status  true for locked and false for unlocked 
    [SerializeField]
    private int speedValue; // Speed attribute of the character 
    [SerializeField]
    private int agilityValue; // Agility attribute of the character
    [SerializeField]
    private int patienceValue; // patience attribute of the character
    [SerializeField]
    private string specialMove; // specialMove attribute of the character
    [SerializeField]
    private int unlockPrice; // unlock price of the character

    [SerializeField]
    GameObject characterBody; // variable that hold character body
    public  int IncreaseSpeed()// the method that improves the player speed
    {
        if (speedValue < 3)
            speedValue++;
        else
            speedValue = 3;
        return speedValue;
    }
    public  int IncreaseAgility()// the method that improves the player agility
    {
        if (agilityValue < 3)
            agilityValue++;
        else
            agilityValue = 3;
        return agilityValue;
    }
    public  int IncreasePatience()// the method that improves the player patience
    {
        if (patienceValue < 3)
            patienceValue++;
        else
            patienceValue = 3;
        return patienceValue;
    }
    public  int GetCurrentSpeed()// the method that improves the player patience
    {
        return speedValue;
    }
    public  int GetCurrentAgility()// the method that improves the player patience
    {
        return agilityValue;
    }
    public  int GetCurrentPatience()// the method that improves the player patience
    {
        return patienceValue;
    }
    public  int GetUnlockAmount()// the method that improves the player patience
    {
        return unlockPrice;

    }
    public  string GetSpecialMove()// the method that returns the special move
    {
        return specialMove;
    }


    #region Getting and Setting Character Properties
    public string GetCharacterName // Get ans Set character name Property
    {
        get
        {
            return characterName;
        }
        set
        {
            characterName = value;
        }
    }


    public bool GetLockStatus // Get and Set Lovk Status Property
    {
        get
        {
            return lockStatus;
        }
        set
        {
            lockStatus = value;
        }
    }

    public int GetPlayerLevel // Get and Set Player level Property
    {
        get
        {
            return playerLevel;
        }
        set
        {
            playerLevel = value;
        }
    }

    public int GetPlayerHealth // Get and Set Player Health Property
    {
        get
        {
            return playerHealth;
        }
        set
        {
            playerHealth = value;
        }
    }

    public int GetPlayerXP // Get and Set Player XP Property
    {
        get
        {
            return playerXp;
        }
        set
        {
            playerXp = value;
        }
    }

    public int GetPlayerSpecialAbility // Get and Set Player level Property
    {
        get
        {
            return playerSpecialAbility;
        }
        set
        {
            playerSpecialAbility = value;
        }
    }

    public int GetPlayerCash // Get and Set Player level Property
    {
        get
        {
            return playerCash;
        }
        set
        {
            playerCash = value;
        }
    }

    public int GetPlayerSerialNumber // Get and Set Player level Property
    {
        get
        {
            return serialNumber;
        }
        set
        {
            serialNumber = value;
        }
    }

    public void InitializeCharacterAttributes()
    {
        playerCash = 3111;
        playerHealth = 99;
        playerXp = 0;
        playerLevel = 1;
        playerSpecialAbility = 100;
    }
    #endregion
}
