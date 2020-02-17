using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MultiPlayerMainClass : MonoBehaviour
{
    protected CharacterController characterController;
    [SerializeField]
    protected bool stopRight;
    [SerializeField]
    protected bool stopLeft;
    [SerializeField]
    protected bool stopFront;
    [SerializeField]
    protected bool stopBack;
    [SerializeField]
    protected float moveSpeed = 20f;
    [SerializeField]
    protected float rotationSpeed = 50;
    [SerializeField]
    protected float gravity = 20.0f;
    [SerializeField]
    protected Vector3 moveDirection = Vector3.zero;
    
 
    
    
    protected int num;
    public abstract void PlayerMovement();
    
    public abstract void  GameInitialization();






}
