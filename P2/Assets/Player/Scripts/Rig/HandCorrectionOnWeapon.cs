using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HandCorrectionOnWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform LeftHandTarget;
   
    [SerializeField]
    private WeaponHolder weaponHolder;
  
    private void LateUpdate()
    {
        SetLeftHand(weaponHolder.leftHandGrip);       
    }

    private void SetLeftHand(Transform target)
    {
        if(target == null)
        {
            LeftHandTarget.position = Vector3.zero;
            LeftHandTarget.rotation = Quaternion.identity;
            return; 
        }

        LeftHandTarget.position = target.position;
        LeftHandTarget.rotation = target.rotation;
    }

}
