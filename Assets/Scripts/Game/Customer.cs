using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private float currentWaitTime;
    [SerializeField]
    private int maxWaitTime;
    public Vegetables[] currentOrder;
    [SerializeField]
    private int happinessQuotient;// ((maximum wait time - current wait time ) / max wait time) * 100
    public int slotNumber;
    public delegate void CustomerWentAwayEventHandler(int num);
    public static event CustomerWentAwayEventHandler OnCustomerLeft;


    public float GetCurrentWaitingTime
    {
        get
            {
            return currentWaitTime;
        }
       
    }
    // Start is called before the first frame update
    void Awake()
    {
        maxWaitTime = Random.Range(100, 150); // max wait time for the customer
        currentWaitTime = maxWaitTime;
        currentOrder = new Vegetables[2]; //currentOrder order of the cusotmer
        currentOrder[0] = new Vegetables(" ",Random.Range(0,6));// max vegetables count is 6 
        currentOrder[1] = new Vegetables(" ", Random.Range(0, 6));

    }

    // Update is called once per frame
    void Update()
    {
        currentWaitTime -= Time.deltaTime;

        if (currentWaitTime <= 0)
            CustomerLeaving();
    }

    void CustomerLeaving()
    {
        this.transform.parent.GetComponent<CustomerSlotScript>().isOccupied = false;
        if (OnCustomerLeft != null)
            OnCustomerLeft(slotNumber);

        Destroy(this.gameObject);
    }

   
}
