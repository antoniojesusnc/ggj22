using UnityEngine;

public class StartAudioBank : MonoBehaviour
{
    [SerializeField]
    //TextAsset bankName;
    // Start is called before the first frame update
    private void Awake()
    {




        if (FMODUnity.RuntimeManager.HaveMasterBanksLoaded)
        {
            Debug.Log("Carregados");
        }
        else
        {
            Debug.Log("nope");
        }
    }

}
