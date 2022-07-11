using UnityEngine;
using UnityEngine.Advertisements;


public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public static AdManager Instance { get; private set; }

    [SerializeField] private string gameID;
    [SerializeField] private string rewardedVideoPlacementId;
    [SerializeField] private bool testMode;

    private void Awake()
    {
        Instance = this;
        Advertisement.Initialize(gameID, testMode, FindObjectOfType<GameStateDeath>() as IUnityAdsInitializationListener);
    }



    public void ShowRewardedAd()
    {
        ShowOptions so = new ShowOptions();
        Advertisement.Show(rewardedVideoPlacementId, so);
    }

    public void OnInitializationComplete()
    {
        
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }
}
