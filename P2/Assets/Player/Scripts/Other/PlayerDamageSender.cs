using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class PlayerDamageSender : MonoBehaviour, IDamageSender
{
   public Image HitMarker;

   private float alpha;

    void Start()
    {
        HitMarker.color = new Color(HitMarker.color.r, HitMarker.color.g, HitMarker.color.b, 0);
    }

   public void OnDamageGiven()
   {
       DisplayHitMaker();
   }

   private async void DisplayHitMaker()
    {
        alpha = 1f;

        while(alpha > 0)
        {
          alpha = Mathf.Lerp(alpha, -0.1f, Time.deltaTime * 1);

          HitMarker.color = new Color(HitMarker.color.r, HitMarker.color.g, HitMarker.color.b, alpha);

          await Task.Yield();
        }
    }

}
