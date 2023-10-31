using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentManager : MonoBehaviour
{
    [SerializeField] internal Animator PlayerAnimator;
    [SerializeField] internal  Rigidbody _rigidbody;
    [SerializeField] internal Collider mainCollider;
   
    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();               
    }

}
