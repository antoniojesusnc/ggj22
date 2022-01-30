public class GeneralConfigsService : MonoBehaviorSingleton<GeneralConfigsService>
{
    public bool IsTutorialShown { get; private set; }
    
    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        base.Awake();
    }
    
    public void Tutorial(bool shown)
    {
        IsTutorialShown = shown;
    }
    
}
