using UnityEngine;
using UnityEngine.UI;

public class UILives : MonoBehaviour
{
    [SerializeField] private Image _image; 
    
    public void Init()
    {
        
    }
    
    public void SetFill(float fillAmount)
    {
        _image.fillAmount = fillAmount;
    }
}
