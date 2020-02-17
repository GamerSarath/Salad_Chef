using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopManager : MonoBehaviour
{
    private static CoopManager _instance; // SingleTon  private instance    
    public bool isGamefinished;
    [SerializeField]
    private PlayerOne playerOne;
    private PlayerTwo playerTwo;
    [SerializeField]
    Vector3 playerOnePos;
    [SerializeField]
    Vector3 playerTwoPos;

    
    float playeroneMinutes;
    float playeroneSeconds;

    float playertwoMinutes;
    float playertwoSeconds;
    #region Singleton
    public static CoopManager Instance
    {
        get
        {
            //logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("CoopManager");
                go.AddComponent<CoopManager>();
                go.tag = "CoopManager";
            }
            return _instance;
        }
    }
    #endregion
    Transform hotelEnvironmnet;
    public delegate void OnCoopMatchEvetFinished();
    public static event OnCoopMatchEvetFinished OnCoopMatchFinished;
    public delegate void OnPowerUpInstatntiationEventHandler();
    public static event OnCoopMatchEvetFinished OnPowerUpInstantiationEvent;

    
    void Awake()
    {
        _instance = this;
        isGamefinished = false;
        hotelEnvironmnet = GameObject.FindGameObjectWithTag("Hotel").GetComponent<Transform>();
        playerOnePos = new Vector3(-6, 1382f, 1453f); //playerOne one initial position
        playerTwoPos = new Vector3(17, 1382f, 1453f);// player wo initial position
        playerOne = Instantiate(Camera.main.GetComponent<CameraScript>().playerOnePrefab, playerOnePos, transform.rotation, hotelEnvironmnet);// cloning player one
        playerOne.transform.localScale = new Vector3(10, 10, 10); // scaling player one 
        playerTwo = Instantiate(Camera.main.GetComponent<CameraScript>().playerTwoPrefab, playerTwoPos, transform.rotation, hotelEnvironmnet); // cloning player two
        playerTwo.transform.localScale = new Vector3(10, 10, 10);// scaling player two

    }
    public void Start()
    {
       
            Debug.Log("Setting  Power Up Instantiation start on the go");
        InvokeRepeating("PowerUpInstantiation", 10, 10);
      

    }
    #region Calculating Distance between the players
    public float PlayerDistance() // calclating the distance between the  players
    {
        float distance = Vector3.Distance(playerOne.transform.position, playerTwo.transform.position);
        return distance;
    }
    #endregion

   

    void GameTimerCalculation()
    {
        #region Player One timer calculation
        playeroneMinutes = Mathf.Floor(playerOne.GetTimer / 60);
        playeroneSeconds = Mathf.RoundToInt(playerOne.GetTimer % 60);
        playerOne.GetTimer -= Time.deltaTime;
        #endregion
        #region Timer Calculation of player Two
        playertwoMinutes = Mathf.Floor(playerTwo.GetTimer / 60);
        playertwoSeconds = Mathf.RoundToInt(playerTwo.GetTimer % 60);
        playerTwo.GetTimer -= Time.deltaTime;
        if(playerOne.GetTimer <=0 && playerTwo.GetTimer <= 0)
        {
            Debug.Log("is Calling. " +playerTwo.GetTimer + " 1 is " + playerOne.GetTimer);
            CooPMatchFinished();
            AnimationManager.Instance.GetCoopFinalResultAnimator.SetBool("FinalResultUp", true);
        }
        #endregion
       
    }

    void Update()
    {
        GameTimerCalculation();
        if (playerOne.GetTimer > 0)
            UIManager.Instance.playerOneTimePanel.text = playeroneMinutes.ToString("00") + " : " + playeroneSeconds.ToString("00");
        else
            playerOne.isgameOver = true;
        if(playerTwo.GetTimer >0)
            UIManager.Instance.playerTwoTimePanel.text = playertwoMinutes.ToString("00") + " : " + playertwoSeconds.ToString("00");
        else
            playerTwo.isgameOver = true;
    }

    void CooPMatchFinished()
    {
        isGamefinished = true;
        if (OnCoopMatchFinished != null)
            OnCoopMatchFinished();
    }

    void PowerUpInstantiation()
    {
        Debug.Log("PowerUp Spawning Set ");
        if (OnPowerUpInstantiationEvent != null)
            OnPowerUpInstantiationEvent();
    }

    public PlayerOne GetPlayerOne
    {get
        {
            return playerOne;

        }
    }

    public PlayerTwo GetPlayerTwo
    {get
        {
            return playerTwo;

        }
    }
}