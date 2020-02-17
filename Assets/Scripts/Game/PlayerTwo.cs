using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : MultiPlayerMainClass
{
    // Start is called before the first frame update
    [SerializeField]
    protected int score;
    [SerializeField]
    protected float timer;
    public delegate void OnVariableChangeDelegate(int newVal);
    public event OnVariableChangeDelegate OnVariableChange;
    public bool isgameOver;
    public int currentCustomerSlot;
    [SerializeField]
    private bool[] slotStatus;

    [SerializeField]
    private Vegetables[] currentOrder;
    bool isChopping;
    int maxChoppingTime = 15;
    float currentchoppingTime;
    int mushroomCount;
    int cucumberCount;
    int carrotCount;
    int capsicumCount;
    int nutsCount;
    int tomotoCount;
    int vegCollected;
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
        if(num == currentCustomerSlot)
        {
            Debug.Log("Angry Customer Went Away");
            score -= 10;
        }
    }

    void OnSpawnCustomer(int num)
    {
        slotStatus[num] = true;
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



    void Awake()
    {
        num = 2;
        characterController = GetComponent<CharacterController>();
        GameInitialization();
        isgameOver = false;
    }
    void FixedUpdate()
    {


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
       // AnimationManager.Instance.GetCharacterAnimator = GameObject.FindGameObjectWithTag("PlayerOne").GetComponent<Animator>();
       if(!isgameOver && isChopping)
       {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                // transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * moveSpeed * 2.5f;
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    this.GetComponent<Animator>().SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                //  transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * moveSpeed * 2.5f;
                transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
                this.GetComponent<Animator>().SetBool("isWalking", true);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //  transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * moveSpeed * 2.5f;
                transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
                this.GetComponent<Animator>().SetBool("isWalking", true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
              //  transform.position += transform.TransformDirection(Vector3.right) * Time.deltaTime * moveSpeed * 2.5f;
                this.GetComponent<Animator>().SetBool("isWalking", true);
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.UpArrow) == false && Input.GetKey(KeyCode.DownArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) == false)
            {
                this.GetComponent<Animator>().SetBool("isWalking", false);
            }
       }
       

    }
    #endregion

    public override void GameInitialization()
    {
        timer = 600;
        score = 0;
    }

    #region Triggering PowerUps Call for player two
    void OnTriggerEnter(Collider other) // When collected power up
    {
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
        #region Taking Orders from respective slots
        if (other.tag == "Slot1")
        {
            if (slotStatus[0])
            {
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                UIManager.Instance.playertwoOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playertwoOrderOne.enabled = true;
                UIManager.Instance.playertwoOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playertwoOrderTwo.enabled = true;
            }

        }
        if (other.tag == "Slot2")
        {
            if (slotStatus[1])
            {
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                UIManager.Instance.playertwoOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playertwoOrderOne.enabled = true;
                UIManager.Instance.playertwoOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playertwoOrderTwo.enabled = true;
            
            }

        }
        if (other.tag == "Slot3")
        {
            if (slotStatus[2])
            {
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                UIManager.Instance.playertwoOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playertwoOrderOne.enabled = true;
                UIManager.Instance.playertwoOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playertwoOrderTwo.enabled = true;
            }

        }
        if (other.tag == "Slot4")
        {
            if (slotStatus[3])
            {
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                UIManager.Instance.playertwoOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playertwoOrderOne.enabled = true;
                UIManager.Instance.playertwoOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playertwoOrderTwo.enabled = true;
            }
        }

        if (other.tag == "Slot5")
        {
            if (slotStatus[4])
            {
                currentCustomerSlot = other.GetComponent<CustomerSlotScript>().currentCustomer.slotNumber;
                currentOrder = other.GetComponent<CustomerSlotScript>().currentCustomer.currentOrder;
                UIManager.Instance.playertwoOrderOne.sprite = GameManager.Instance.vegetablesSprites[currentOrder[0].id];
                UIManager.Instance.playertwoOrderOne.enabled = true;
                UIManager.Instance.playertwoOrderTwo.sprite = GameManager.Instance.vegetablesSprites[currentOrder[1].id];
                UIManager.Instance.playertwoOrderTwo.enabled = true;
            }

        }
        #endregion

        #region Collecting Vegetables from the baskets
        if (other.tag == "Mushroom")
        {
            Debug.Log("Mushroom is collected");
            if (vegCollected <= 3)
            {
                mushroomCount++;
                UIManager.Instance.mushroomCount_playerTwo.text = mushroomCount.ToString();
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
                UIManager.Instance.cucumberCount_playerTwo.text = cucumberCount.ToString();
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
                UIManager.Instance.tomotoCount_playerTwo.text = tomotoCount.ToString();
                vegetablesList.Add(RetailStore.Instance.Tomoto);

                vegCollected++;
            }

        }

        if (other.tag == "Carrot")
        {
            Debug.Log("Carrot is collected");
            if (vegCollected <= 3)
            {
                carrotCount++;
                UIManager.Instance.carrotCount_playerTwo.text = carrotCount.ToString();
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
                UIManager.Instance.nutsCount_PlayerTwo.text = nutsCount.ToString();
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
                UIManager.Instance.capsicumCount_playerTwo.text = capsicumCount.ToString();
                vegetablesList.Add(RetailStore.Instance.Capsicum);

                vegCollected++;
            }

        }

        #endregion

        if(other.tag == "DustBin")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                vegCollected = 0;
                UIManager.Instance.capsicumCount_playerTwo.text = "";
                UIManager.Instance.nutsCount_PlayerTwo.text = "";
                UIManager.Instance.carrotCount_playerTwo.text = "";
                UIManager.Instance.tomotoCount_playerTwo.text = "";
                UIManager.Instance.cucumberCount_playerTwo.text = "";
                UIManager.Instance.mushroomCount_playerTwo.text = "";


            }

            if (other.tag == "ChoppingTable")
            {



                if (vegetablesList.Contains(currentOrder[0]) && vegetablesList.Contains(currentOrder[1]))
                {
                    isChopping = true;
                    AnimationManager.Instance.GetChoppingAnimator.SetBool("IsChopping", true);
                    StartCoroutine(ChoppingEventUnderProcess());
                }
                else
                {
                    Debug.Log("No Vegetbles in Stock");
                }
            }
        }
        UIManager.Instance.playerTwoScorePanel.text = this.score.ToString();
    }
    #endregion

    IEnumerator ChoppingEventUnderProcess()
    {
        yield return new WaitForSeconds(15);
        score += 20;
        AnimationManager.Instance.GetChoppingAnimator.SetBool("IsChopping", false);
    }
}
