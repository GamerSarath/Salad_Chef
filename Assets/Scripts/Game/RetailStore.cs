using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetailStore : MonoBehaviour
{
    public static RetailStore Instance;
    public Vegetables Mushroom;
    public Vegetables Cucumber;

    public Vegetables Tomoto;

    public Vegetables Carrot;

    public Vegetables Capsicum;

    public Vegetables Nuts;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Mushroom = new Vegetables("Mushroom", 0);
        Cucumber = new Vegetables("Cucumber", 1);
        Tomoto = new Vegetables("Tomoto", 2);
        Carrot = new Vegetables("Carrot", 3);
        Capsicum = new Vegetables("Capsicum", 4);
        Nuts = new Vegetables("Nuts", 5);
    }


}
