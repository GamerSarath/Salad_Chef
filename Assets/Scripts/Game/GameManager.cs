using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    private static GameManager _instance; // SingleTon  private instance
    GameMode currentGameMode;
    public enum GameMode { GameMode_CareerMode , GameMode_Multiplayer};
    [SerializeField]
    private int sceneloadedIndex; // currently loaded scene index
    private Characters currentDisplayedCharacter { get; set; } // currently displayed character
    private int currentDisplayedCharacterIndex { get; set; } // The Index of the current displayed character in the character list
    private int currentActiveCharacterIndex { get; set; } // The Index of the current active character in the character list
    public Sprite[] vegetablesSprites;
    public delegate void CharacterUnlockEventHandler(); // Delegate that handles the event character unlock
    public static event CharacterUnlockEventHandler OnCharacterUnlockedEvent; // character unlocked event
 
    [SerializeField]
    Characters[] characters;// characters list
    GameObject characterHolder; // Display character holder
    Text characterNameHolder; // Display Character Name Holder 
    #region Singleton
    public static GameManager Instance
    {
        get
        {
            //logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
                go.tag = "GameManager";
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

      void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

 


    public static void OnNewCharacterUnlockClicked()
    {
        if (OnCharacterUnlockedEvent != null)
            OnCharacterUnlockedEvent();
    }
    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        sceneloadedIndex = scene.buildIndex;
        UIManager.Instance.ActionsNeededOnLevelLoad();
        #region Action For Character Selection Scene
        if (scene.name == "CharacterSelectionScene")
        {
            //currentActiveCharacter.InitializeCharacterAttributes();
            GameObject.FindGameObjectWithTag("NextButton").GetComponent<Button>().onClick.AddListener(OnClickNextButton);// Assigning the event OnClickNext to the button NextButton.
            GameObject.FindGameObjectWithTag("PreviousButton").GetComponent<Button>().onClick.AddListener(OnClickPreviousButton);// Assigning the event OnClickPrevious to the button PreiousButton.
            characterHolder = GameObject.FindGameObjectWithTag("CharacterHolder");
            characterNameHolder = GameObject.FindGameObjectWithTag("CharacterNameHolder").GetComponent<Text>();
            
            AnimationManager.Instance.GetCharacterUnlockAnimator = GameObject.FindGameObjectWithTag("PlayerUnlockedPanel").GetComponent<Animator>();
            UIManager.Instance.playerXPText.text = PlayerController.Instance.GetPlayerXP.ToString();
            UIManager.Instance.playerLevelText.text = PlayerController.Instance.GetPlayerLevel.ToString();
            UIManager.Instance.playerHealthText.text = PlayerController.Instance.GetPlayerHealth.ToString();
            UIManager.Instance.playerCashText.text = PlayerController.Instance.GetPlayerCash.ToString();
            UIManager.Instance.specialabilityText.text = PlayerController.Instance.GetPlayerSpecialAbility.ToString();
            // Check will be done to see wheather tutorial is completed or not
            DisplayPlayerDetails(currentDisplayedCharacterIndex);

            if (!MemoryManager.Instance.GetTutorialStatus)
            {
                //Debug.Log(AnimationManager.Instance.GetAssistanceAnimator);
                    
            }
        }
        #endregion

        #region Action for Coop Scene
        
        if(scene.name == "CoopScene")
        {
            if(currentGameMode == GameMode.GameMode_Multiplayer)
            {
                Debug.Log(CoopManager.Instance.name);
            }
            else
            {

            }
        }
        #endregion
    }

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
        Debug.Log(UIManager.Instance.name);
        Debug.Log(AnimationManager.Instance.name);
       // Debug.Log(PlayerController.Instance.name);

    }
    // Start is called before the first frame update
    void Start()
    {
        // InitializeCharacters if required
        currentDisplayedCharacterIndex = 0;

        if (PlayerController.Instance.GetDefaultCharacter != null)
            PlayerController.Instance.GetCurrentActiveCharacter = characters[PlayerController.Instance.GetCurrentCharcterIndex];

    }
  /*Future Use  #region Initializing Characters
    private void InitializeCharacters() // To Initialize characters with the attribute values when required
    {
        Bob bob = new Bob(1, 1, 3, "Jump", 29990);
        Shannon shannon = new Shannon(2, 2, 1, "Run", 3999);
        Jasper jasper = new Jasper(3, 3, 3, "Big Jump", 5999);
        characters = new Characters[] { bob, shannon,  jasper };
    }
    #endregion
    */
    #region Method called when the Next character button is clicked
    void OnClickNextButton() // Method called when the next button is clicked . next character will be displayed and the attributes wil be shown
    {
        Debug.Log("Next Character button is clicked");
        if(currentDisplayedCharacterIndex < characters.Length-1) // Check if currenctCharacterIndex is less than  total characters array length
        {
            DisplayPlayerDetails(++currentDisplayedCharacterIndex);
        }



    }
    #endregion

    #region Method called when the Previous character button is clicked
    void OnClickPreviousButton() // Method called when the previous button is clicked . previous character will be displayed and the attributes wil be shown
    {
        Debug.Log("Previous Character Button is Clicked");
        if (currentDisplayedCharacterIndex > 0) // Check if currenctCharacterIndex is less than  zero
        {
            DisplayPlayerDetails(--currentDisplayedCharacterIndex);
        }
    }
    #endregion

    #region Method to Display CurrentPlayer Details
    void DisplayPlayerDetails(int playerIndex)
    {
      
        #region Changing the Character Model and Name For Previous Character
        currentDisplayedCharacter = characters[playerIndex]; //If the count is less than characters total count, decrement the character index and change the character
        Destroy(characterHolder.transform.GetChild(0).gameObject);// Destroy the previous character model
        Characters go = Instantiate(characters[playerIndex], new Vector3(-455.49f, -238.68f, 632.43f), Quaternion.Euler(0, 149, 0), characterHolder.transform); // Instantiate the new chracter model in the required location and rotation with character holder as parent
        go.transform.localScale = new Vector3(334, 334, 334); // scale the character model prefab to the given vector
        characterNameHolder.text = currentDisplayedCharacter.GetCharacterName; //the name of the character is printed
        Debug.Log("current character name is " + currentDisplayedCharacter.GetCharacterName); // name of character is debbugged 
        #endregion

        #region  Assinging Stars to Speed Attribute 
        //Assigning the number of stars for the Speed attribute for the previous player 
        for (int i = 0; i < 3; i++)
        {
            if (i < currentDisplayedCharacter.GetCurrentSpeed())
            {
                UIManager.Instance.speedStarImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                UIManager.Instance.speedStarImages[i].GetComponent<Image>().enabled = false;
            }

        }
        #endregion

        #region  Assinging Stars to Agility Attribute
        //Assigning the number of stars for the Agility attribute for the previous player 
        for (int i = 0; i < 3; i++)
        {
            if (i < currentDisplayedCharacter.GetCurrentAgility())
            {
                UIManager.Instance.agilityStarImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                UIManager.Instance.agilityStarImages[i].GetComponent<Image>().enabled = false;
            }

        }
        #endregion

        #region  Assinging Stars to Patience Attribute
        //Assigning the number of stars for the Patience attribute for the previous player 
        for (int i = 0; i < 3; i++)
        {
            if (i < currentDisplayedCharacter.GetCurrentPatience())
            {
                UIManager.Instance.patienceStarImages[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                UIManager.Instance.patienceStarImages[i].GetComponent<Image>().enabled = false;
            }

        }
        #endregion

        UIManager.Instance.specialMoveText.text = currentDisplayedCharacter.GetSpecialMove(); // Displays the special move of the previous player
        
        //if locked show the price else show unlocked 
        if (!currentDisplayedCharacter.GetLockStatus)
            UIManager.Instance.unlockAmountText.text = currentDisplayedCharacter.GetUnlockAmount().ToString();
        else
            UIManager.Instance.unlockAmountText.text = "USE";
    
    }
    #endregion
    public int GetSceneLoadedIndex
    {
        get
        {
            return sceneloadedIndex;
        }
        set
        { sceneloadedIndex = value;
        }
    }

    public Characters GetCurrentDisplayedPlayer
    {
        get
        {
            return currentDisplayedCharacter;
        }
        set
        {
            currentDisplayedCharacter = value;
        }
    }

    public int GetCurrentDisplayedPlayerIndex
    {
        get
        {
            return currentDisplayedCharacterIndex;
        }
        set
        {
            currentDisplayedCharacterIndex = value;
        }
    }

    public Characters[] GetCharactersList
    {
        get
        {
            return characters;
        }
        set
        {
            characters = value;
        }
    }

  
    public int GetCurrentActiveCharacterIndex
    {
        get
        {
            return currentActiveCharacterIndex;
        }
        set
        {
            currentActiveCharacterIndex = value;
        }
    }

    public GameMode GetCurrentGameMode
    {
        get
        {
            return currentGameMode;
        }
        set
        {
            currentGameMode = value;
        }
    }

}
