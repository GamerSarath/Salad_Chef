using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInstance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestroy", 20);
    }

    // Update is called once per frame
    void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
