using UnityEngine;

[CreateAssetMenu(fileName = "WeaponRecoil", menuName = "Recoil")]
public class WeaponRecoilSO : ScriptableObject
{
    public float maxRecoilY, minY;

    public float maxRecoilX, minX;

    public float RecoilResetDuration;

    public float SmothnessForRecoil, SmothnessForRecoilReset;

    public float RecoilChangeSmoothness;

    public bool UseRecoilCurve;

    public AnimationCurve RecoilY;
    public AnimationCurve RecoilX;
}
