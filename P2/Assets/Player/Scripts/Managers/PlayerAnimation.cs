using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    private Animator animator;
    private PlayerScriptFacade playerScriptFacade;

    [SerializeField] private int curentLayer = 0;

    internal int WeaponBaseAnimationLayer;

    #region // Animation ID
    internal int aiming;
    internal int reload;
    internal int shoot;
    internal int fall;
    internal int crouch; 

    internal int MoveX;
    internal int MoveY;

    #endregion

    void Start()
    {
        playerScriptFacade = GetComponent<PlayerScriptFacade>();
        animator = playerScriptFacade.componentManager.PlayerAnimator;

        WeaponBaseAnimationLayer = animator.GetLayerIndex("WeaponBase");

        HashAnimations();
    }

    private void LateUpdate()
    {
        ConstantCheck();        
    }

    internal void WalkAnim(float x, float y) 
    {
        if (playerScriptFacade.playerState.isRunning)
        {
            x = 2;
            y = 2;
        }

        animator.SetFloat(MoveX, x, 0.05f, Time.deltaTime);
        animator.SetFloat(MoveY, y, 0.05f, Time.deltaTime);
    }

    internal void ConstantCheck()
    {       
        SetBools();

        if (animator.GetCurrentAnimatorStateInfo(WeaponBaseAnimationLayer).IsTag("Reload"))
        {
            playerScriptFacade.playerState.isReloading = true;
        }
        else
        {
            playerScriptFacade.playerState.isReloading = false;
        }
    }

    #region // Layers
    // only one weapon layer can be active
    internal void SetWeaponLayer(int index) 
    {
        ResetCurentWeaponLayer();
        curentLayer = index;
        animator.SetLayerWeight(curentLayer, 1);
    }

    internal void ResetCurentWeaponLayer()
    {
        animator.SetLayerWeight(curentLayer, 0);
    }

    internal int GetWeaponLayerIndex(string WeaponName)
    {
        int index = animator.GetLayerIndex("WeaponLayer_" + WeaponName);

        return index;
    }

    #endregion

    private void SetBools()
    {
        animator.SetBool(aiming, playerScriptFacade.playerState.isAiming);
        animator.SetBool(fall, playerScriptFacade.playerState.isFalling);
        animator.SetBool(crouch, playerScriptFacade.playerState.isCrouching);
    }

    private void HashAnimations()
    {
        aiming = Animator.StringToHash("Aiming");
        reload = Animator.StringToHash("Reload");
        fall = Animator.StringToHash("Falling");
        shoot = Animator.StringToHash("Shoot");
        crouch = Animator.StringToHash("Crouching");

        MoveX = Animator.StringToHash("MoveX");
        MoveY = Animator.StringToHash("MoveY");
    }

}
