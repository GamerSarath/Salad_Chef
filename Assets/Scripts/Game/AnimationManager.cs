using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private static AnimationManager _instance; // SingleTon  private instance    [SerializeField]
    private Animator assistanceAnimator { get; set; }
    private Animator characterUnlockAnimator { get; set; }
    private Animator characterAnimator;
    private Animator coopFinalResultAnimator;
    private Animator choppingAnimator;
    // Start is called before the first frame update
    #region Singleton
    public static AnimationManager Instance
    {
        get
        {
            //logic to create the instance 
            if (_instance == null)
            {
                GameObject go = new GameObject("AnimationManager");
                go.AddComponent<AnimationManager>();
                go.tag = "AnimationManager";
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion

    void Awake()
    {
        _instance = this;

    }

    public Animator GetAssistanceAnimator
    {
        get
        {
            return assistanceAnimator;
        }
        set
        {
            assistanceAnimator = value;
        }
    }

    public Animator GetCharacterUnlockAnimator
    {
        get
        {
            return characterUnlockAnimator;
        }
        set
        {
            characterUnlockAnimator = value;
        }
    }

    public Animator GetCharacterAnimator
    {
        get
        {
            return characterAnimator;
        }
        set
        {
            characterAnimator = value;
        }
    }

    public Animator GetCoopFinalResultAnimator
    {
        get
        {
            return coopFinalResultAnimator;
        }
        set
        {
            coopFinalResultAnimator = value;
        }
    }

    public Animator GetChoppingAnimator
    {
        get
        {
            return choppingAnimator;
        }
        set
        {
            choppingAnimator = value;
        }
    }


}
