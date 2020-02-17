using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssistanceManager : MonoBehaviour
{
    private static AssistanceManager _instance; // SingleTon  private instance   
    [SerializeField]
    private int assistanceLevel;
    public string[] assistanceDialouges;

    // Start is called before the first frame update
    #region Singleton
    public static AssistanceManager Instance
    {
        get
        {
            //logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("AssistanceManager");
                go.AddComponent<AssistanceManager>();
                go.tag = "AssistanceManager";
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion

    public int GetAssistanceLevel
    {
        get
        {
            return assistanceLevel;
        }
        set
        {
            assistanceLevel = value;
        }
    }
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

   public void AssistanceChat(int assistantLevel)
    {
        AnimationManager.Instance.GetAssistanceAnimator = GameObject.FindGameObjectWithTag("AssistanceAnimationPanel").GetComponent<Animator>();
        Debug.Log(AnimationManager.Instance.name);
        AnimationManager.Instance.GetAssistanceAnimator.SetBool("boardUp", true);

        switch (assistantLevel)
        {
            case 0: UIManager.Instance.assistanceText.text = assistanceDialouges[0];
                assistanceLevel = 0; 
                PlayerPrefs.SetInt("AssistanceLevel", assistanceLevel);
                break;
            case 1: UIManager.Instance.assistanceText.text = assistanceDialouges[1];
                assistanceLevel = 1;
                PlayerPrefs.SetInt("AssistanceLevel", assistanceLevel);

               
                break;

            case 2:assistanceLevel = 2;
                UIManager.Instance.assistanceText.text = assistanceDialouges[2];
                PlayerPrefs.SetInt("AssistanceLevel", assistanceLevel);
                GameObject.FindGameObjectWithTag("TutorialDoneButton").GetComponent<Button>().enabled = false;
                GameObject.FindGameObjectWithTag("TutorialDoneButton").GetComponent<Image>().enabled = false;
                GameObject.FindGameObjectWithTag("AssistanceNextButton").GetComponent< Button > ().enabled = true;
                GameObject.FindGameObjectWithTag("AssistanceNextButton").GetComponent< Image > ().enabled = true; ;
                break;
            case 3: assistanceLevel = 3;
                UIManager.Instance.assistanceText.text = assistanceDialouges[3];
                PlayerPrefs.SetInt("AssistanceLevel", assistanceLevel);
                break;
            case 4:
                assistanceLevel = 4;
                UIManager.Instance.assistanceText.text = assistanceDialouges[4];
                PlayerPrefs.SetInt("AssistanceLevel", assistanceLevel);
                break;
            case 5:   assistanceLevel = 5;
                UIManager.Instance.assistanceText.text = assistanceDialouges[5];
                PlayerPrefs.SetInt("AssistanceLevel", assistanceLevel);
                break;
            case 6:
                assistanceLevel = 6;
                UIManager.Instance.assistanceText.text = assistanceDialouges[6];
                PlayerPrefs.SetInt("AssistanceLevel", assistanceLevel);
                break;
            case 7:
                assistanceLevel = 7;
                UIManager.Instance.assistanceText.text = assistanceDialouges[7];
                PlayerPrefs.SetInt("AssistanceLevel", assistanceLevel);
                GameObject.FindGameObjectWithTag("TutorialDoneButton").GetComponent<Button>().enabled = true;
                GameObject.FindGameObjectWithTag("TutorialDoneButton").GetComponent<Image>().enabled = true;
                GameObject.FindGameObjectWithTag("AssistanceNextButton").GetComponent<Button>().enabled = false;
                GameObject.FindGameObjectWithTag("AssistanceNextButton").GetComponent<Image>().enabled = false; ;
                break;

        }
    }


}
