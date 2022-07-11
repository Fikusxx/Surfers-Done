using UnityEngine;
using System.Collections.Generic;


public class HatLogic : MonoBehaviour
{

    [SerializeField] private Transform hatContainer;
    private List<GameObject> hatModels = new List<GameObject>();
    private Hat[] hats;


    public GameObject equippedHat;

    private void Start()
    {
        hats = Resources.LoadAll<Hat>("Hat");
        //SpawnHats();
        SelectHat(SaveManager.Instance.save.CurrentHatIndex);
    }

    //private void SpawnHats()
    //{
    //    for (int i = 0; i < hats.Length; i++)
    //    {
    //        hatModels.Add(Instantiate(hats[i].model, hatContainer) as GameObject);
    //    }
    //}

    //public void DisableAllHats()
    //{
    //    for (int i = 0; i < hatModels.Count; i++)
    //    {
    //        hatModels[i].SetActive(false);
    //    }
    //}

    //public void SelectHat(int index)
    //{
    //    DisableAllHats();
    //    hatModels[index].SetActive(true);
    //}

    public void DisablePreviousHat()
    {
        Destroy(equippedHat);
    }

    public void SelectHat(int index)
    {
        DisablePreviousHat();
        equippedHat = Instantiate(hats[index].model, hatContainer);
    }
}
