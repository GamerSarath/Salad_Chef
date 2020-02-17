using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance; // SingleTon  private instance
    CharacterController characterController;
    [SerializeField]
    Characters currentActiveCharacter;
    [SerializeField]
    int currentCharacterIndex; // Index of the character in the characters array so that we can retrive the current character
    [SerializeField]
    private Characters[] unlockedCharacters;
    private string currentcharacterName;// variable that hold character name
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
    [SerializeField]
    float speed = 6.0f;
    [SerializeField]
    Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    float gravity = 20.0f ;
    #region Singleton
    public static PlayerController Instance
    {
        get
        {
            //create logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("PlayerController");
                go.tag = "PlayerController";
                go.AddComponent<PlayerController>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion

    public string GetCharacterName // Get ans Set character name Property
    {
        get
        {
            return currentcharacterName;
        }
        set
        {
            currentcharacterName = value;
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

    public int GetCurrentCharcterIndex // Get and Set Current Character Index Property
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

    public int GetPlayerSpecialAbility // Get and Set Player special ability Property
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

    public int GetPlayerCash // Get and Set Player cash Property
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

    public Characters GetDefaultCharacter // Get   default character Property
    {
        get
        {
            return currentActiveCharacter;
        }
        
    }
    public Characters GetCurrentActiveCharacter // Get and Set current active character Property
    {
        get
        {
            return currentActiveCharacter;
        }
        set
        {
            currentActiveCharacter = value;
        }

    }
    void InitializeAttributes()
    { 
        Debug.Log("Initializing Player Attributes");
        playerCash = MemoryManager.Instance.GetPlayerCash;
        playerHealth = MemoryManager.Instance.GetPlayerHealth;
        playerXp = MemoryManager.Instance.GetPlayerXP;
        playerLevel = MemoryManager.Instance.GetPlayerLevel;
        playerSpecialAbility = MemoryManager.Instance.GetPlayerSpecialAbility;
        currentActiveCharacter = MemoryManager.Instance.GetCurrentActiveCharater;
    }

    void Awake()
    {
        _instance = this;
        
    }

    void Start()
    {
       // AddComponent<CharacterController>();
        //characterController = GetComponent<CharacterController>();
        InitializeAttributes();
    }

    void Update()
    {
        //PlayerMovement(); // check for input in every frame 
    }
    #region Player Movement Code
    /*public void PlayerMovement()
    {
        if(characterController.isGrounded) // check if character is grounded , if grunded then
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")); // calculate the moveDirection according to the inputs
            moveDirection *= speed * currentActiveCharacter.GetCurrentSpeed(); // 
            moveDirection.y -= gravity * Time.deltaTime; // Apply the gravity
            characterController.Move(moveDirection * Time.deltaTime); // Move the controller
        }
    }*/
    #endregion
}
