using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
   private const float DELETE_SIZE_RATE = 1.2f;
   [SerializeField] 
   private BackgroundConfig _backgroundConfig;
   
   [SerializeField] 
   private List<SpriteRenderer> _spriteRenderers;

   private List<BackgroundConfig.BackgroundConfigInfo> _backgroundInfos =
      new List<BackgroundConfig.BackgroundConfigInfo>();

   void Start()
   {
      ClockService.Instance.OnUpdateEvent += CustomUpdate;

      _backgroundInfos.Add(_backgroundConfig.backgrounds[0]);
   }

   private void CustomUpdate(float deltaTime)
   {
      MoveBackground(deltaTime);
      TryGenerateNextBackground();
   }

   private void TryGenerateNextBackground()
   {
      if (_spriteRenderers[0].transform.position.x < -_backgroundInfos[0].size * DELETE_SIZE_RATE)
      {
         Destroy(_spriteRenderers[0].gameObject);
         _spriteRenderers.RemoveAt(0);
         _backgroundInfos.RemoveAt(0);
      }

      if (_spriteRenderers.Count > 1)
      {
         return;
      }

      if (_spriteRenderers[0].transform.position.x < -_backgroundInfos[0].size * 0.5f)
      {
         var newSpriteRenderer = Instantiate(
            _spriteRenderers[0],
            _spriteRenderers[0].transform.position + Vector3.right * _backgroundInfos[0].size,
            Quaternion.identity,
            _spriteRenderers[0].transform.parent);

         var newBackgroundInfo = GetBackgroundInfo();
         newSpriteRenderer.sprite = newBackgroundInfo.sprite;
         _spriteRenderers.Add(newSpriteRenderer);
         _backgroundInfos.Add(newBackgroundInfo);
      }
   }

   private void MoveBackground(float deltaTime)
   {
      for (int i = 0; i < _spriteRenderers.Count; i++)
      {
         float speed = _backgroundInfos[i].speedPercentageOfSpeed * GameService.Instance.Speed;
         _spriteRenderers[i].transform.Translate(Vector3.left * speed * deltaTime);
      }
   }

   private BackgroundConfig.BackgroundConfigInfo GetBackgroundInfo()
   {
      var backgroundConfig = _backgroundConfig.backgrounds[0];  
      float distance = GameService.Instance.Distance;
      
      for (int i = 0; i < _backgroundConfig.backgrounds.Count; i++)
      {
         var tempBackgroundConfig = _backgroundConfig.backgrounds[i];
         if (tempBackgroundConfig.distanceInit < distance)
         {
            backgroundConfig = tempBackgroundConfig;
         }
         else
         {
            break;
         }
      }

      return backgroundConfig;
   }
}
