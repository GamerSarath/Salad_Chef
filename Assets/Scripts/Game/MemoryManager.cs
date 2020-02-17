using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MemoryManager : MonoBehaviour
{

    private static MemoryManager _instance;

    [SerializeField]
    Characters currentActiveCharacter;
    [SerializeField]
    int currentCharacterIndex; // playerPref version of current character index
    [SerializeField]
    private int assistanceLevel { get; set; }
    [SerializeField]
    private bool tutorialStatus { get; set; } // false if tutorial is not finished and true if tutorial is finished 
    private static bool[] characterlockStatusArray { get; set; } // false if tutorial is not finished and true if tutorial is finished 

    [SerializeField]
    private int playerLevel; // variable that hold player level 
    [SerializeField]
    private int playerHealth; // variable that hold player health 
    [SerializeField]
    private int playerCash; // variable that hold player cash 
    [SerializeField]
    private int playerXp; // variable that hold player XP 
    [SerializeField]
    private int playerSpecialAbility;

    #region Singleton
    public static MemoryManager Instance
    {
        get
        {
            //create logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("MemoryManager");
                go.tag = "MemoryManager";
                go.AddComponent<MemoryManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion

   
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

    public int GetCurrentCharacterIndex // Get and Set Current CharacterIndex Property
    {
        get
        {
            return currentCharacterIndex;
        }
        set
        {
            currentCharacterIndex = value;
        }
    }
    public Characters GetCurrentActiveCharater
    {
        get
        {
            return currentActiveCharacter;
        }
    }


    void OnEnable()
    {
        GameManager.OnCharacterUnlockedEvent += OnLockStatusChanges;
    }

    void OnDisable()
    {
        GameManager.OnCharacterUnlockedEvent -= OnLockStatusChanges;
    }
    public static void OnLockStatusChanges()
    {
        Debug.Log("Lock Status Changed Method Called");
        if (!GameManager.Instance.GetCurrentDisplayedPlayer.GetLockStatus)
        {
            Debug.Log("Player cash is " + PlayerController.Instance.GetPlayerCash); 
            Debug.Log("Display cash is " + GameManager.Instance.GetCurrentDisplayedPlayer.GetPlayerCash);
            if (PlayerController.Instance.GetPlayerCash  >= GameManager.Instance.GetCurrentDisplayedPlayer.GetUnlockAmount())
            {
                
                Debug.Log("Character " + GameManager.Instance.GetCurrentDisplayedPlayer.GetCharacterName + " is unlocked ");
                characterlockStatusArray[GameManager.Instance.GetCurrentDisplayedPlayerIndex] = true;
                PlayerPrefsExtension.SetBoolArray("CharacterLockStatsArray", characterlockStatusArray);
            }
            
        }
         
            
    }
    public bool GetTutorialStatus //Get and Set Tutorial Status Property
    {
        get
        {
            return tutorialStatus;
        }
        set
        {
            tutorialStatus = value;
        }
    }

    public int GetAssistanceLevel
    {
        get
        {
            return assistanceLevel;
        }
        set
        {
            assistanceLevel = value;
        }
    }

    public bool[] GetCharcterLockStatusArray
    {
        get
        {
            return characterlockStatusArray;    
        }
        set
        {
            characterlockStatusArray = value;
        }
    }


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this; // assinging the this object to the _instance ;
        
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        InitialMemoryLoading();
        yield return new WaitForSeconds(9.0f);
        SceneLoadingManager.Instance.LoadTitleScene(); // Loading the title scene after the splash scene
    }

    void InitialMemoryLoading()
    {
        Debug.Log("Called Initial Memory Loading");
        #region Loading AssistanceLevel value from memory
        if (PlayerPrefs.HasKey("AssistanceLevel")) // if the AssistanceLevel Integer PlayerPref is already present , retrieve it
        {
            assistanceLevel = PlayerPrefs.GetInt("AssistanceLevel");
        }
        else
        {
            assistanceLevel = 0;    // else initialise assistanceLevel with 0 and save it in the memory so that next time it can be loaded from memory.
            PlayerPrefs.SetInt("AssistanceLevel", assistanceLevel);
        }
        #endregion

        #region Loading boolean value of wheather tutorial is finished or not
        if (PlayerPrefs.HasKey("TutorialStatus")) // if the TutorialStatus boolean PlayerPref is already present , retrieve it
        {
            tutorialStatus = PlayerPrefsExtension.GetBool("TutorialStatus");
        }
        else
        {
            tutorialStatus = false;    // else initialise assistanceLevel with 0 and save it in the memory so that next time it can be loaded from memory.
            PlayerPrefsExtension.SetBool("TutorialStatus", tutorialStatus);
        }
        #endregion

        #region Loading boolean Value Array of Character Lock Status

        if (PlayerPrefs.HasKey("CharacterLockStatsArray")) // if the TutorialStatus boolean PlayerPref is already present , retrieve it
        {
            characterlockStatusArray = PlayerPrefsExtension.GetBoolArray("CharacterLockStatsArray");
            for(int i = 0; i <GameManager.Instance.GetCharactersList.Length; i++)
            {
                Debug.Log("Inside loading boolean value arry- " + i +"is"+ GameManager.Instance.GetCharactersList[i].GetLockStatus);
                GameManager.Instance.GetCharactersList[i].GetLockStatus = characterlockStatusArray[i];
            }
        }
        else
        {
            characterlockStatusArray = new bool[3];    // else initialise lockstatusarray with false and save it in the memory so that next time it can be loaded from memory.
            for(int i = 0;i < characterlockStatusArray.Length; i++)
            {
                characterlockStatusArray[i] = false;
            }
            PlayerPrefsExtension.SetBoolArray("CharacterLockStatsArray", characterlockStatusArray);
        }

        #endregion

        #region Loading Player level from memory
        if (PlayerPrefs.HasKey("PlayerLevel")) // if the playerLevel int PlayerPref is already present , retrieve it
        {
            playerLevel = PlayerPrefs.GetInt("PlayerLevel");
            
        }
        else
        {
            playerLevel = 1;
            PlayerPrefs.SetInt("PlayerLevel", playerLevel); // else initialise playerLevel with 1 and save it in the memory so that next time it can be loaded from memory.
        }

        #endregion

        #region Loading Player health from memory
        if (PlayerPrefs.HasKey("PlayerHealth")) // if the playerHealth int PlayerPref is already present , retrieve it
        {
            playerLevel = PlayerPrefs.GetInt("PlayerHealth");

        }
        else
        {
            playerLevel = 100;
            PlayerPrefs.SetInt("PlayerHealth", playerHealth); // else initialise playerHealth with 1 and save it in the memory so that next time it can be loaded from memory.
        }

        #endregion

        #region Loading Player Cash from memory
        if (PlayerPrefs.HasKey("PlayerCash")) // if the playerCash int PlayerPref is already present , retrieve it
        {
            playerLevel = PlayerPrefs.GetInt("PlasyerCash");

        }
        else
        {
            playerLevel = 3999;
            PlayerPrefs.SetInt("PlayerCash", playerCash); // else initialise playerCash with 3999 and save it in the memory so that next time it can be loaded from memory.
        }

        #endregion

        #region Loading Player XP from memory
        if (PlayerPrefs.HasKey("PlayerXP")) // if the playerXp int PlayerPref is already present , retrieve it
        {
            playerXp = PlayerPrefs.GetInt("PlayerXP");

        }
        else
        {
            playerXp = 0;
            PlayerPrefs.SetInt("PlayerXP", playerXp); // else initialise playerXp with 0 and save it in the memory so that next time it can be loaded from memory.
        }

        #endregion

        #region Loading Player Special ability bar from memory
        if (PlayerPrefs.HasKey("PlayerSpecialAbility")) // if the playerSpecialAbility int PlayerPref is already present , retrieve it
        {
            playerSpecialAbility = PlayerPrefs.GetInt("PlayerSpecialAbility");

        }
        else
        {
            playerSpecialAbility = 100;
            PlayerPrefs.SetInt("PlayerSpecialAbility", playerSpecialAbility); // else initialise playerSpecialAbility with 3999 and save it in the memory so that next time it can be loaded from memory.
        }

        #endregion


        #region Loading current character index from memory
        if (PlayerPrefs.HasKey("CurrentCharacterIndex")) // if the playerSpecialAbility int PlayerPref is already present , retrieve it
        {
            currentCharacterIndex = PlayerPrefs.GetInt("CurrentCharacterIndex");

        }
        else
        {
            currentCharacterIndex = 0;
            PlayerPrefs.SetInt("CurrentCharacterIndex", currentCharacterIndex); // else initialise CurrentCharacterIndex with 0 and save it in the memory so that next time it can be loaded from memory.
        }

        #endregion
    }


}
