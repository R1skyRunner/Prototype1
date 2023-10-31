using UnityEngine;
using Cinemachine;

public class CameraRecoil : MonoBehaviour
{
    public CinemachinePOV pov;
  
    private PlayerScriptFacade scriptFacade;

    [SerializeField] public Weapon weapon;

    private float differenceInValueY, differenceInValueX;
    internal float duration;


    void Start()
    {             
        pov = PlayerCameraController.Instance.AimCamera.GetCinemachineComponent<CinemachinePOV>();
        scriptFacade = GetComponent<PlayerScriptFacade>();
    }
 
    void LateUpdate()
    {
        if(weapon != null) 
        {
            ApplyRecoil();
            ResetRecoil();
        }
    }


    internal void SetVariables()
    {
        duration = weapon.RecoilDuration;
        GetLastMousePos();
    }

    public void ResetRecoil()
    {
        while (duration > 0)
        {

            if ((scriptFacade.input.mouseDelta != Vector2.zero) || (pov.m_VerticalAxis.Value > weapon.lastShotValueY))
            {
                Debug.Log("Not Zero");
                duration = 0;
                return;
            }

            differenceInValueY = pov.m_VerticalAxis.Value - weapon.lastShotValueY;
            differenceInValueX = pov.m_HorizontalAxis.Value - weapon.lastShotValueX;

            // Y value           
            pov.m_VerticalAxis.Value =
            Mathf.Lerp(pov.m_VerticalAxis.Value,
            pov.m_VerticalAxis.Value - differenceInValueY,
            weapon.RecoilSO.SmothnessForRecoilReset * Time.deltaTime);


            // X value
            pov.m_HorizontalAxis.Value =
            Mathf.Lerp(pov.m_HorizontalAxis.Value,
            pov.m_HorizontalAxis.Value - weapon.recoilValueX,
            weapon.RecoilSO.SmothnessForRecoil * Time.deltaTime);

            duration -= Time.deltaTime * 1;

            return;
        }
    }

    public void ApplyRecoil()
    {  
        weapon.recoilValueY = Mathf.Lerp(weapon.recoilValueY, 0.01f, 10 * Time.deltaTime);
        weapon.recoilValueX = Mathf.Lerp(weapon.recoilValueX, 0.01f, 10 * Time.deltaTime);

        while (weapon.recoilValueY > 1 || weapon.recoilValueX > 1)
        {          
            // Y value
            pov.m_VerticalAxis.Value =
            Mathf.Lerp(pov.m_VerticalAxis.Value,
            pov.m_VerticalAxis.Value - weapon.recoilValueY, 
            weapon.RecoilSO.SmothnessForRecoil* Time.deltaTime);

            // X value
            pov.m_HorizontalAxis.Value =
            Mathf.Lerp(pov.m_HorizontalAxis.Value,
            pov.m_HorizontalAxis.Value - weapon.recoilValueX, 
            weapon.RecoilSO.SmothnessForRecoil* Time.deltaTime);

            return;
        }

    }
    
    private void GetLastMousePos()
    {
        weapon.lastShotValueY = pov.m_VerticalAxis.Value;
        weapon.lastShotValueX = pov.m_HorizontalAxis.Value;
    }

}
