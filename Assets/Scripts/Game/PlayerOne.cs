using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MultiPlayerMainClass
{
    [SerializeField]
    protected int score;
    [SerializeField]
    protected float timer;
    [SerializeField]
    private Vegetables[] currentOrder;
    public bool isgameOver;
    public int currentCustomerSlot;
    public delegate void OnVariableChangeDelegate(int newVal);
    public event OnVariableChangeDelegate OnVariableChange;
    [SerializeField]
    private bool[] slotStatus;
    int mushroomCount;
    int cucumberCount;
    int carrotCount;
    int capsicumCount;
    int nutsCount;
    int tomotoCount;
    int vegCollected;
    bool isChopping;
    public float maxChoppingWaitingtime = 15;
    public float currentchoppingTime = 15;
    List<Vegetables> vegetablesList;
    public int GetScore
    {
        get
        {
            return score;
        }
        set
        {
            if (score == value) return;
            score = value;
            if (OnVariableChange != null)
                OnVariableChange(score);
        }
    }

    public float GetTimer
    {
        get
        {
            return timer;
        }
        set
        {
            timer = value;
        }
    }

    public override void GameInitialization()
    {
        timer = 600;
        score = 0;
    }

    void OnEnable()
    {
        
        Customer.OnCustomerLeft += CustomerLeft;
        CustomerSlotManager.OnCustomerSpawn += OnSpawnCustomer;
    }

    void OnDisable()
    {
        Customer.OnCustomerLeft -= CustomerLeft;
        CustomerSlotManager.OnCustomerSpawn -= OnSpawnCustomer;
    }

    void Start()
    {
        isChopping = false;
        slotStatus = new bool[5];
    }
    void CustomerLeft(int num)
    {
        if (num == currentCustomerSlot)
        {
            Debug.Log("Angry Customer Went Away");
            score -= 10;
        }
    }

    void OnSpawnCustomer(int num)
    {
        slotStatus[num] = true;
    }
    void Awake()
    {
        isgameOver = false;
        num = 1;
        characterController = GetComponent<CharacterController>();
        GameInitialization();
        vegetablesList = new List<Vegetables>();
    }
    void FixedUpdate()
    {
        //stopRight = Physics.Raycast(transform.position, Vector3.right, 2f);

       // stopLeft = Physics.Raycast(transform.position, Vector3.left, 2f);

       // stopBack = Physics.Raycast(transform.position, Vector3.back, .2f);

        //stopFront = Physics.Raycast(transform.position, Vector3.forward, 2f);

        PlayerMovement(); // check for input in every frame 
        

    }

    void Update()
    {
        if (isChopping)
            currentchoppingTime -= Time.deltaTime;
    }
    #region Player Movement Code
    public override void PlayerMovement()
    {
        if(!isgameOver && !isChopping)
        {
            if (Input.GetKey(KeyCode.W) && !stopFront)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                //   transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * moveSpeed * 2.5f;
                this.GetComponent<Animator>().SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.S) && !stopBack)
            {
                //  transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * moveSpeed * 2.5f;
                transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
                this.GetComponent<Animator>().SetBool("isWalking", true);
            }

            if (Input.GetKey(KeyCode.A) && !stopLeft)
            {
                //  transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * moveSpeed * 2.5f;
                transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                this.GetComponent<Animator>().SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.D) && !stopRight)
            {
               // transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * moveSpeed * 2.5f;
                this.GetComponent<Animator>().SetBool("isWalking", true);
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

            }

            if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
            {
                this.GetComponent<Animator>().SetBool("isWalking", false);
            }
        }
        

    }
    #endregion

    
    void OnTriggerEnter(Collider other) // When collected power up
    {
        #region Triggering PowerUps Call for player One
        if (other.tag == "AddScorePowerUp")
        {
            score += 10;
            Destroy(other.gameObject);

            Debug.Log("Score Power Up Collected");
        }

        if (other.tag == "AddChoppingSpeedPowerUp")
        {
            Debug.Log("Chopping Speed is increased for some time");
            Destroy(other.gameObject);
        }
        if (other.tag == "AddTimePowerUp")
        {
            Debug.Log("Time Power Up collected");
            timer += 20;
            Destroy(other.gameObject);
        }

        if (other.tag == "ReduceChoppingSpeedPowerDown")
        {
            Debug.Log("Reduce CHopping Speed Powerd down is collected");
            Destroy(other.gameObject);
        }

        if (other.tag == "ReduceScorePowerDown")
        {
            Debug.Log("Reduce Score PowerDOwn is Collected");
            score -= 10;
            Destroy(other.gameObject);
        }

        if (other.tag == "ReduceTimePowerDown")
        {
            Debug.Log("reduce time power down Collected");
            timer -= 20;
            Destroy(other.gameObject);
        }

        #endregion
        #region Taking Orders from respective slots
        if (other.tag == "Slot1")
        {
            if(slotStatus[0])
            {
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                UIManager.Instance.playeroneOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playeroneOrderOne.enabled = true;
                UIManager.Instance.playeroneOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playeroneOrderTwo.enabled = true;
            }
            
        }
        if (other.tag == "Slot2")
        {
            if(slotStatus[1])
            {
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                UIManager.Instance.playeroneOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playeroneOrderOne.enabled = true;
                UIManager.Instance.playeroneOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playeroneOrderTwo.enabled = true;
            }
           
        }
        if (other.tag == "Slot3")
        {
            if(slotStatus[2])
            {
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                UIManager.Instance.playeroneOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playeroneOrderOne.enabled = true;
                UIManager.Instance.playeroneOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playeroneOrderTwo.enabled = true;
            }
            
        }
        if (other.tag == "Slot4")
        {
            if(slotStatus[3])
            {
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                UIManager.Instance.playeroneOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playeroneOrderOne.enabled = true;
                UIManager.Instance.playeroneOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playeroneOrderTwo.enabled = true;
            }
        }
           
        if (other.tag == "Slot5")
        {
            if(slotStatus[4])
            {
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                UIManager.Instance.playeroneOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playeroneOrderOne.enabled = true;
                UIManager.Instance.playeroneOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playeroneOrderTwo.enabled = true;
            }
           
        }
        #endregion

        #region Collecting Vegetables from the baskets
        if(other.tag == "Mushroom")
        {
            Debug.Log("Mushroom is collected");
            if(vegCollected <=3)
            {
                mushroomCount++;
                UIManager.Instance.mushroomCount_playerOne.text = mushroomCount.ToString();
                vegetablesList.Add(RetailStore.Instance.Mushroom);
                vegCollected++;
            }
        }

        if (other.tag == "Cucumber")
        {
            Debug.Log("Cucumber is collected");
            if (vegCollected <= 3)
            {
                cucumberCount++;
                UIManager.Instance.mushroomCount_playerOne.text = cucumberCount.ToString();
                vegetablesList.Add(RetailStore.Instance.Cucumber);
                vegCollected++;
            }
            

        }

        if (other.tag == "Tomoto")
        {
            Debug.Log("Tomoto is collected");
            if (vegCollected <= 3)
            {
                tomotoCount++;
                UIManager.Instance.mushroomCount_playerOne.text = tomotoCount.ToString();
                vegetablesList.Add(RetailStore.Instance.Cucumber);
                vegCollected++;
            }
          
        }

        if (other.tag == "Carrot")
        {
            Debug.Log("Carrot is collected");
            if (vegCollected <= 3)
            {
                carrotCount++;
                UIManager.Instance.mushroomCount_playerOne.text = carrotCount.ToString();
                vegetablesList.Add(RetailStore.Instance.Carrot);
                vegCollected++;
            }

        }
        if (other.tag == "Nuts")
        {
            Debug.Log("Nuts is collected");
            if (vegCollected <= 3)
            {
                nutsCount++;
                UIManager.Instance.mushroomCount_playerOne.text = nutsCount.ToString();
                vegetablesList.Add(RetailStore.Instance.Nuts);
                vegCollected++;
            }
        }


        if (other.tag == "Capsicum")
        {
            Debug.Log("Capsicum is collected");
            if (vegCollected <= 3)
            {
                capsicumCount++;
                UIManager.Instance.mushroomCount_playerOne.text = capsicumCount.ToString();
                vegetablesList.Add(RetailStore.Instance.Capsicum);
                vegCollected++;
            }

        }

        #endregion

        #region Dust Bin
        if (other.tag == "DustBin")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                vegCollected = 0;
                vegetablesList.Clear();
                UIManager.Instance.capsicumCount_playerOne.text = "";
                UIManager.Instance.nutsCount_PlayerOne.text = "";
                UIManager.Instance.carrotCount_playerOne.text = "";
                UIManager.Instance.tomotoCount_playerOne.text = "";
                UIManager.Instance.cucumberCount_playerOne.text = "";
                UIManager.Instance.mushroomCount_playerOne.text = "";
            }

        }
#endregion

        if(other.tag == "ChoppingTable")
        {
            

            
            if(vegetablesList.Contains(currentOrder[0]) && vegetablesList.Contains(currentOrder[1]))
            {
                isChopping = true;
                AnimationManager.Instance.GetChoppingAnimator.SetBool("IsChopping", true);
                StartCoroutine(ChoppingEventUnderProcess());
            }
        }
        UIManager.Instance.playerOneScorePanel.text = this.score.ToString(); 
    }
   
    IEnumerator ChoppingEventUnderProcess()
    {
        yield return new WaitForSeconds(15);
        score += 20;
        AnimationManager.Instance.GetChoppingAnimator.SetBool("IsChopping", false);
    }
}
