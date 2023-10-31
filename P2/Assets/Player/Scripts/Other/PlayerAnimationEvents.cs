using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerScriptFacade scriptFacade;
    private GameObject Magazine;
   
    private void Start()
    {
        scriptFacade = GetComponent<PlayerScriptFacade>();
    }
   
    public void CallReload()
    {
        scriptFacade.weaponHolder.Reload();
    }

    public void SpawnMag()
    {            
        Magazine = Instantiate(scriptFacade.weaponHolder.WeaponMagazine, scriptFacade.rigController.LeftHand.position, scriptFacade.rigController.LeftHand.rotation);
        Magazine.transform.localScale = Magazine.transform.lossyScale * 5;
        Magazine.AddComponent<BoxCollider>();
        Magazine.AddComponent<Rigidbody>();
        Magazine.GetComponent<Rigidbody>().isKinematic = false;

        Destroy(Magazine, 10f);

        // maybe add Script or object pool mags.
    }

    public void HideWeaponMag()
    {
        scriptFacade.weaponHolder.weaponClass.Magazine.SetActive(false);
    }

    public void ShowWeaponMag()
    {
        scriptFacade.weaponHolder.weaponClass.Magazine.SetActive(true);
    }

    public void HideMagInHand()
    {
        scriptFacade.weaponHolder.WeaponMagazine.SetActive(false);
    }

    public void ShowMagInHand()
    {
        scriptFacade.weaponHolder.WeaponMagazine.SetActive(true);
    }

}
