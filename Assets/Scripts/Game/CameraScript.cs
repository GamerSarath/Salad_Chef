using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    float speed = 10.0f;
    [SerializeField]
    float coopPanningSpeed = 5f;
    public PlayerOne playerOnePrefab;
    public PlayerTwo playerTwoPrefab;
    [SerializeField]
    Transform target;
    [SerializeField]
    private Vector3 target_Offset;

    [SerializeField]
    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetCurrentGameMode == GameManager.GameMode.GameMode_CareerMode)
        {
            if (Input   .GetAxis("Mouse X") > 0)
            {
                transform.position += new Vector3((Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed), 0.0f, (Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed));
            }
            else if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position += new Vector3((Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed), 0.0f, (Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed));
            }
            
        }
      
       
    }
   
}
