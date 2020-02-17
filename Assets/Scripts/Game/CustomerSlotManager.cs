using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSlotManager : MonoBehaviour
{
    public static CustomerSlotManager Instance ; //    private instance

    public GameObject[] customerArray; // the customer character array
    public Transform[] customerSlots; // customer spawn points array
    public Sprite slotTakenSprite;
    public Sprite slotEmptySprite;
    int randNum ;
    int randCustomerNum;

    public delegate void CustomerSpawnEvenHandler(int num);
    public static event CustomerSpawnEvenHandler OnCustomerSpawn;
    void OnEnable ()
    {
        Customer.OnCustomerLeft += CustomerLeft;
    }

    void OnDisable()
    {
        Customer.OnCustomerLeft -= CustomerLeft;
    }
    void Start()
    {
        InvokeRepeating("SpawnCustomer", 5, 15);
    }

    void CustomerLeft(int num)
    {
        UIManager.Instance.slots[num].GetComponent<Image>().sprite = slotEmptySprite ;
    }
    void SpawnCustomer()
    {
        if(!CoopManager.Instance.isGamefinished)
        {
            randNum = Random.Range(0, customerSlots.Length);
            randCustomerNum = Random.Range(0, customerArray.Length);
            if (!customerSlots[randNum].GetComponent<CustomerSlotScript>().isOccupied)
            {
                customerSlots[randNum].GetComponent<CustomerSlotScript>().isOccupied = true;
                GameObject go = Instantiate(customerArray[randCustomerNum], customerSlots[randNum]);
                go.transform.localPosition = Vector3.zero;
                customerSlots[randNum].GetComponent<CustomerSlotScript>().currentCustomer = go.GetComponent<Customer>();
                customerSlots[randNum].GetComponent<CustomerSlotScript>().currentCustomer.slotNumber = randNum;
                UIManager.Instance.slots[randNum].GetComponent<Image>().sprite = slotTakenSprite;
                if (OnCustomerSpawn != null)
                    OnCustomerSpawn(randNum);
            }
        }
       
    }
}
