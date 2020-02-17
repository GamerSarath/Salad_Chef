using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager _instance; // SingleTon  private instance
    public GameObject[] speedStarImages;
    public GameObject[] agilityStarImages;
    public GameObject[] patienceStarImages;
    public Text specialMoveText ;
    public Text unlockAmountText;
    public Text specialabilityText;
    public Text playerHealthText;
    public Text playerCashText;
    public Text playerXPText;
    public Text playerLevelText;
    public Text currentCharacterNameText;
    public Text assistanceText;

    public Text playerOneTimePanel;
    public Text playerTwoTimePanel;
    public Text playerOneScorePanel;
    public Text playerTwoScorePanel;
    public Text matchWinningTagText;

    public Text playeroneFinalScore;
    public Text playertwoFinalScore;

    public Image playeroneOrderOne;
    public Image playeroneOrderTwo;
    public Image playertwoOrderOne;
    public Image playertwoOrderTwo;
    public RectTransform slotOne;
    public RectTransform slotTwo;
    public RectTransform slotThree;
    public RectTransform slotFour;
    public RectTransform slotFive;
    public RectTransform[] slots;

    public Text mushroomCount_playerOne;
    public Text carrotCount_playerOne;
    public Text tomotoCount_playerOne;
    public Text capsicumCount_playerOne;
    public Text nutsCount_PlayerOne;
    public Text cucumberCount_playerOne;


    public Text mushroomCount_playerTwo;
    public Text carrotCount_playerTwo;
    public Text tomotoCount_playerTwo;
    public Text capsicumCount_playerTwo;
    public Text nutsCount_PlayerTwo;
    public Text cucumberCount_playerTwo;

    #region Singleton
    public static UIManager Instance
    {
        get
        {
            //create logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("UIManager");
                go.tag = "UIManager";
                go.AddComponent<UIManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion


    #region Actions Needed On Level Load
    public void ActionsNeededOnLevelLoad()
    {
        Debug.Log("Actions Needed on Level Load Function is Called");

        switch (GameManager.Instance.GetSceneLoadedIndex) // switch case that determine which scene is loadeded depending upon on the buildidex of the currently loaded scene and performing the necessary actions
        {
            case 0:
                Debug.Log("Splash Scene is Loaded");
                break;
            case 1:
                GameObject.FindGameObjectWithTag("CareerButton").GetComponent<Button>().onClick.AddListener(OnClickCareerButton);// Assigning the event OnClickCareerButton to the button CareerButton.
                GameObject.FindGameObjectWithTag("MultiPlayerButton").GetComponent<Button>().onClick.AddListener(OnClickMultiplayerButton);// Assigning the event OnClickMultiplayer to the button MultiplayerButton.
                GameObject.FindGameObjectWithTag("SettingsButton").GetComponent<Button>().onClick.AddListener(OnClickSettingsButton);// Assigning the event OnClickSettingsButton to the button SettingsButton.
                GameObject.FindGameObjectWithTag("ExitButton").GetComponent<Button>().onClick.AddListener(OnClikcExitButton);// Assigning the event OnClickExit to the button ExitButton.        }
                break;
            case 2:
                speedStarImages = GameObject.FindGameObjectsWithTag("SpeedStar");
                agilityStarImages = GameObject.FindGameObjectsWithTag("AgilityStar");
                patienceStarImages = GameObject.FindGameObjectsWithTag("PatienceStar");
                specialMoveText = GameObject.FindGameObjectWithTag("SpecialMoveText").GetComponent<Text>();
                unlockAmountText = GameObject.FindGameObjectWithTag("UnlockAmountText").GetComponent<Text>();
                specialabilityText = GameObject.FindGameObjectWithTag("SpecialAbilityText").GetComponent<Text>();
                playerCashText = GameObject.FindGameObjectWithTag("PlayerCashText").GetComponent<Text>();
                playerHealthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
                playerXPText = GameObject.FindGameObjectWithTag("PlayerXpText").GetComponent<Text>();
                playerLevelText = GameObject.FindGameObjectWithTag("PlayerLevelText").GetComponent<Text>();
                assistanceText = GameObject.FindGameObjectWithTag("AssistanceText").GetComponent<Text>();
                currentCharacterNameText = GameObject.FindGameObjectWithTag("CurrentCharacterNameText").GetComponent<Text>();


               
                GameObject.FindGameObjectWithTag("TutorialDoneButton").GetComponent<Button>().onClick.AddListener(OnClickDoneButton);// Assigning the event OnClickDoneButton to the button TutorialDoneButton.
                GameObject.FindGameObjectWithTag("AssistanceNextButton").GetComponent<Button>().onClick.AddListener(delegate { OnClickAssistanceNextButton(AssistanceManager.Instance.GetAssistanceLevel); }); ; ;// Assigning the event OnClickDoneButton to the button TutorialDoneButton.
                GameObject.FindGameObjectWithTag("UnlockButton").GetComponent<Button>().onClick.AddListener(GameManager.OnNewCharacterUnlockClicked);// Assigning the event OnNewCharacterUnlockClicked to the button UnlockButton.
                GameObject.FindGameObjectWithTag("unlockDoneButton").GetComponent<Button>().onClick.AddListener(OnClickNewCharacterDone);// Assigning the event OnClickNewCharacterDone to the button UnlockButtonDoneButton.
                if(!MemoryManager.Instance.GetTutorialStatus)
                {
                    if(MemoryManager.Instance.GetAssistanceLevel <=0)
                    {
                        AssistanceManager.Instance.AssistanceChat(0);
                        
                    }
                    
                }
               
                break;
            case 3:
                playerOneScorePanel = GameObject.FindGameObjectWithTag("PlayerOneScorePanel").GetComponent<Text>(); 

                playerTwoScorePanel = GameObject.FindGameObjectWithTag("PlayerTwoScorePanel").GetComponent<Text>(); 

                playerOneTimePanel = GameObject.FindGameObjectWithTag("PlayerOneTimePanel").GetComponent<Text>(); 

                playerTwoTimePanel = GameObject.FindGameObjectWithTag("PlayerTwoTimePanel").GetComponent<Text>();

                playeroneFinalScore = GameObject.FindGameObjectWithTag("PlayerOneFinalScore").GetComponent<Text>();

                playertwoFinalScore = GameObject.FindGameObjectWithTag("PlayerTwoFinalScore").GetComponent<Text>();

                mushroomCount_playerOne = GameObject.FindGameObjectWithTag("MushroomOrderCOunt_PlayerOne").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                mushroomCount_playerTwo = GameObject.FindGameObjectWithTag("MushroomOrderCount_playerTwo").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                cucumberCount_playerOne = GameObject.FindGameObjectWithTag("CucumberOrderCount_PlayerOne").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                cucumberCount_playerTwo = GameObject.FindGameObjectWithTag("CucumberOrderCount_PlayerTwo").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                carrotCount_playerOne = GameObject.FindGameObjectWithTag("CarrotOrderCount_PlayerOne").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                carrotCount_playerTwo = GameObject.FindGameObjectWithTag("CarrotOrderCount_PlayerTwo").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                tomotoCount_playerOne = GameObject.FindGameObjectWithTag("TomotoOrderCount_PlayerOne").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                tomotoCount_playerTwo = GameObject.FindGameObjectWithTag("TomotoOrderCount_PlayerTwo").GetComponent<Transform>().GetChild(0).GetComponent<Text>(); matchWinningTagText = GameObject.FindGameObjectWithTag("MatchWinningTag").GetComponent<Text>();

                nutsCount_PlayerOne = GameObject.FindGameObjectWithTag("NutsOrderCount_PlayerOne").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                nutsCount_PlayerTwo = GameObject.FindGameObjectWithTag("NutsOrderCount_PlayerTwo").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                capsicumCount_playerOne = GameObject.FindGameObjectWithTag("CapsicumOrderCount_playerOne").GetComponent<Transform>().GetChild(0).GetComponent<Text>();

                capsicumCount_playerTwo = GameObject.FindGameObjectWithTag("CapsicumOrderCountPlayerTwo").GetComponent<Transform>().GetChild(0).GetComponent<Text>();


                playeroneOrderOne = GameObject.FindGameObjectWithTag("PlayerOneOrderOne").GetComponent<Image>();

                playeroneOrderTwo = GameObject.FindGameObjectWithTag("PlayerOneOrderTwo").GetComponent<Image>();

                playertwoOrderOne = GameObject.FindGameObjectWithTag("PlayerTwoOrderOne").GetComponent<Image>();

                playertwoOrderTwo = GameObject.FindGameObjectWithTag("PlayerTwoOrderTwo").GetComponent<Image>();

                slotOne = GameObject.FindGameObjectWithTag("Slot1").GetComponent<RectTransform>();

                slotTwo = GameObject.FindGameObjectWithTag("Slot2").GetComponent<RectTransform>();

                slotThree = GameObject.FindGameObjectWithTag("Slot3").GetComponent<RectTransform>();

                slotFour = GameObject.FindGameObjectWithTag("Slot4").GetComponent<RectTransform>();

                slotFive = GameObject.FindGameObjectWithTag("Slot5").GetComponent<RectTransform>();

                slots = new RectTransform[5];
                slots[0] = slotOne;

                slots[1] = slotTwo;

                slots[2] = slotThree;

                slots[3] = slotFour;

                slots[4] = slotFive;
                GameObject.FindGameObjectWithTag("CoopMatchFinalResultPlayAgainButton").GetComponent<Button>().onClick.AddListener(OnClickCoopMatchFinalResultPlayAgainButton);// Assigning the event OnClickCoopMatchFinalResultPlayAgainButton to the button CoopMatchFinalResultPlayAgain

                GameObject.FindGameObjectWithTag("CoopMatchFinalResultHomeButton").GetComponent<Button>().onClick.AddListener(OnClickCoopMatchFinalResultHomeButton);// Assigning the event OnClickCoopMatchFinalResultHomeButton to the button CoopMatchFinalResultHome.

                AnimationManager.Instance.GetCoopFinalResultAnimator = GameObject.FindGameObjectWithTag("CoopFinalResultAnimator").GetComponent<Animator>();
                break;

        }

    }

    void OnClickCoopMatchFinalResultPlayAgainButton()
    {
        SceneLoadingManager.Instance.LoadMultiPlayerScene();
    }

    void OnClickCoopMatchFinalResultHomeButton()
    {
        SceneLoadingManager.Instance.LoadTitleScene();
    }

    #endregion
    void Awake()
    {
       //PlayerPrefs.DeleteAll();
        _instance = this; // Assinging this object to the _instance 
    }
    void OnEnable()
    {
        GameManager.OnCharacterUnlockedEvent += OnNewCharacterUnlockClicked; // Event subscription for character unlock 
        CoopManager.OnCoopMatchFinished += CoopMatchResultPrediction;
    }

    #region MajorButtons four major buttons in the title scene of the game. they are career, multiplayer,info, settings and exit // Self explainable
    void OnClickCareerButton()
    {
        Debug.Log("Career Button is Clicked");

        GameManager.Instance.GetCurrentGameMode = GameManager.GameMode.GameMode_CareerMode;
        SceneLoadingManager.Instance.LoadCharacterSelectionScene();
        
    } // Event called when the career button is clicked

    void OnClickMultiplayerButton()
    {
        Debug.Log("Multiplayer Button is Clicked");
        GameManager.Instance.GetCurrentGameMode = GameManager.GameMode.GameMode_Multiplayer;
        SceneLoadingManager.Instance.LoadMultiPlayerScene();
    } // Event called when the multiplayer button is clicked

    void OnClickSettingsButton()
    {
        Debug.Log("Settings Button is Clicked");
    } // Event called when the settings button is clicked

    void OnClikcExitButton()
    {
        Debug.Log("Exit Button is Clicked");
        Application.Quit();
    } // Event called when the Exit button is clicked
    #endregion
    
    #region Done and Unlock Buttons
    void OnClickDoneButton()
    {
        Debug.Log("Clicked Tutorial dialouge is understood ");
        AnimationManager.Instance.GetAssistanceAnimator.SetBool("boardUp", false);
        if(AssistanceManager.Instance.GetAssistanceLevel==1)
            AssistanceManager.Instance.AssistanceChat(2);
    }

    void OnClickUnlock()
    {
        Debug.Log("Unlock button clicked");

    }
    #endregion

    #region method called when New Character is Unlocked 
    void OnClickNewCharacterDone()
    {
        Debug.Log("New Character Unlocked Done Button clicked");
        AnimationManager.Instance.GetCharacterUnlockAnimator.SetBool("CharacterUnlockPanelUp", false);

    }
    #endregion

    void OnNewCharacterUnlockClicked()
    {

        Debug.Log("Lock status is " + GameManager.Instance.GetCurrentDisplayedPlayer.GetLockStatus);
        if (!GameManager.Instance.GetCurrentDisplayedPlayer.GetLockStatus)
        {
            Debug.Log("Lock Status is " + GameManager.Instance.GetCurrentDisplayedPlayer.GetLockStatus);
            if (PlayerController.Instance.GetPlayerCash >= GameManager.Instance.GetCurrentDisplayedPlayer.GetUnlockAmount())
            {
                if(MemoryManager.Instance.GetAssistanceLevel <= 1)
                {
                    AssistanceManager.Instance.AssistanceChat(1);
                }
                
                Debug.Log("New Character Unlocked " + GameManager.Instance.GetCurrentDisplayedPlayer.GetPlayerCash);
                AnimationManager.Instance.GetCharacterUnlockAnimator.SetBool("CharacterUnlockPanelUp", true);
                unlockAmountText.text = "USE";
                currentCharacterNameText.text = GameManager.Instance.GetCurrentDisplayedPlayer.GetCharacterName;
                PlayerController.Instance.GetCurrentActiveCharacter = GameManager.Instance.GetCurrentDisplayedPlayer;
                GameManager.Instance.GetCurrentActiveCharacterIndex = GameManager.Instance.GetCurrentActiveCharacterIndex;
                PlayerController.Instance.GetPlayerCash -= GameManager.Instance.GetCurrentDisplayedPlayer.GetUnlockAmount();
                playerCashText.text = PlayerController.Instance.GetPlayerCash.ToString();
                GameManager.Instance.GetCurrentDisplayedPlayer.GetLockStatus = true;
            }
            else
            {
                Debug.Log("Not Enough Cash to buy the player ");
            }
        }
        else
        {

            Debug.Log("The Player is already purchased");

            currentCharacterNameText.text = GameManager.Instance.GetCurrentDisplayedPlayer.GetCharacterName;
            PlayerController.Instance.GetCurrentActiveCharacter = GameManager.Instance.GetCurrentDisplayedPlayer;
            GameManager.Instance.GetCurrentActiveCharacterIndex = GameManager.Instance.GetCurrentActiveCharacterIndex;
            PlayerController.Instance.GetCurrentCharcterIndex = GameManager.Instance.GetCurrentDisplayedPlayerIndex;
            MemoryManager.Instance.GetCurrentCharacterIndex = PlayerController.Instance.GetCurrentCharcterIndex;
            PlayerPrefs.SetInt("CurrentCharacterIndex", PlayerController.Instance.GetCurrentCharcterIndex);
        }
    }

    void CoopMatchResultPrediction()
    {
        playeroneFinalScore.text = CoopManager.Instance.GetPlayerOne.GetScore.ToString();
        playertwoFinalScore.text = CoopManager.Instance.GetPlayerTwo.GetScore.ToString();
       
        if (CoopManager.Instance.GetPlayerOne.GetScore > CoopManager.Instance.GetPlayerTwo.GetScore)
        {
            matchWinningTagText.text = "Player One Won";
        }
        else if (CoopManager.Instance.GetPlayerOne.GetScore < CoopManager.Instance.GetPlayerTwo.GetScore)
        {
            matchWinningTagText.text = "Player Two Won";
        }
        else
        {
            Instance.matchWinningTagText.text = "Match Tied";
        }
    }
    void OnClickAssistanceNextButton(int dialougeNumber)
    {

        Debug.Log("Clicked Assistance Next Button  . dialougeNumber Number is " + dialougeNumber);
        AssistanceManager.Instance.AssistanceChat(++dialougeNumber);
    }

   
}

