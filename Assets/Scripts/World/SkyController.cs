using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SkyController : MonoBehaviour
{
   private SkyConfig _skyConfig;
   
   [SerializeField] 
   private SpriteRenderer _spriteRenderer;
   
   void Start()
   {
      _skyConfig = GameService.Instance.CurrentDifficulty.skyConfig;
      
      ClockService.Instance.OnUpdateEvent += CustomUpdate;
   }

   private void CustomUpdate(float deltaTime)
   {
      SetSkyColor(deltaTime);
   }

   private void SetSkyColor(float deltaTime)
   {
      var skyInfo = GetSkyInfo();
      var nextSkyInfo = GetNextSkyInfo();

      float factor = 1;
      if (skyInfo.distanceInit != nextSkyInfo.distanceInit)
      {
         var distance= GameService.Instance.Distance;
         factor = (distance - skyInfo.distanceInit) / (nextSkyInfo.distanceInit - skyInfo.distanceInit);
      }
      _spriteRenderer.color = Color.Lerp(skyInfo.color, nextSkyInfo.color, factor);
   }
   
   private SkyConfig.SkyConfigInfo GetSkyInfo()
   {
      var backgroundConfig = _skyConfig.skyes[0];  
      float distance = GameService.Instance.Distance;
      
      for (int i = 0; i < _skyConfig.skyes.Count; i++)
      {
         var tempSkyInfo = _skyConfig.skyes[i];
         if (tempSkyInfo.distanceInit < distance)
         {
            backgroundConfig = tempSkyInfo;
         }
         else
         {
            break;
         }
      }

      return backgroundConfig;
   }

   private SkyConfig.SkyConfigInfo GetNextSkyInfo()
   {
      var backgroundConfig = _skyConfig.skyes[0];  
      float distance = GameService.Instance.Distance;
      
      for (int i = 0; i < _skyConfig.skyes.Count-1; i++)
      {
         var tempSkyInfo = _skyConfig.skyes[i];
         if (tempSkyInfo.distanceInit < distance)
         {
            backgroundConfig = _skyConfig.skyes[i+1];
         }
         else
         {
            break;
         }
      }

      return backgroundConfig;
   }
}
