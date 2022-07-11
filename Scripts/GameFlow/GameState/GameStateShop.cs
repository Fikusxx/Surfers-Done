using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class GameStateShop : GameState
{

    public GameObject shopUI;
    public HatLogic hatLogic;
    [SerializeField] private TextMeshProUGUI totalFish;
    [SerializeField] private TextMeshProUGUI currentHatName;

    // Shop Item
    public GameObject hatPrefab;
    public Transform hatContainer;
    private Hat[] hats;


    protected override void Awake()
    {
        base.Awake();
        hats = Resources.LoadAll<Hat>("Hat");
        PopulateShop();

        currentHatName.text = hats[SaveManager.Instance.save.CurrentHatIndex].ItemName;

    }

    private void PopulateShop()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            var go = Instantiate(hatPrefab, hatContainer);
            int index = i;

            // Button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));

            // Thumbnail
            go.transform.GetChild(0).GetComponent<Image>().sprite = hats[index].Thumbnail;

            // ItemName
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;

            // Price
            if (SaveManager.Instance.save.UnlockedHatFlag[i] == 0)
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].ItemPrice.ToString();
            else
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void OnHatClick(int index)
    {
        if (SaveManager.Instance.save.UnlockedHatFlag[index] == 1)
        {
            SaveManager.Instance.save.CurrentHatIndex = index;

            currentHatName.text = hats[index].ItemName;
            hatLogic.SelectHat(index);

            SaveManager.Instance.Save();
        }
        else if (hats[index].ItemPrice <= SaveManager.Instance.save.Fish)
        {
            SaveManager.Instance.save.Fish -= hats[index].ItemPrice;
            SaveManager.Instance.save.UnlockedHatFlag[index] = 1;
            SaveManager.Instance.save.CurrentHatIndex = index;

            currentHatName.text = hats[index].ItemName;
            hatLogic.SelectHat(index);

            totalFish.text = SaveManager.Instance.save.Fish.ToString("000");

            hatContainer.GetChild(index).GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

            SaveManager.Instance.Save();
        }
        else
        {
            Debug.Log("Not enough fish");
        }

    }

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCameras.Shop);

        totalFish.text = SaveManager.Instance.save.Fish.ToString("000");

        shopUI.SetActive(true);
    }

    public override void Destruct()
    {
        shopUI.SetActive(false);
    }

    public void OnHomeClick()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }
}
