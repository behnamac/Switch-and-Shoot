using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Player;

public class UpgradeController : MonoBehaviour
{
    public static UpgradeController Instance;

    [SerializeField] private UpgradeHolder[] upgradeHolders;
    [SerializeField] private Transform buttonParent;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        GenarateButton();
    }

    private void LoadData() 
    {
        for (int i = 0; i < upgradeHolders.Length; i++)
        {
            var levelPrefs = PlayerPrefs.GetInt("Upgrade" + upgradeHolders[i].Name);
            upgradeHolders[i].Level = levelPrefs;
            PlayerUpgrade.Instance.StartCoroutine(upgradeHolders[i].functionName, upgradeHolders[i].AddPower * levelPrefs);
            upgradeHolders[i].CurrentPrice = upgradeHolders[i].FirstPrice + (levelPrefs * upgradeHolders[i].AddPrice);
        }
    }
    private void GenarateButton() 
    {
        for (int i = 0; i < upgradeHolders.Length; i++)
        {
            var button = upgradeHolders[i].PrefabButton;
            var buttonClone = Instantiate(button, buttonParent);
            GetTexts(buttonClone.transform, out TextMeshProUGUI levelText, out TextMeshProUGUI priceText);
            upgradeHolders[i].levelText = levelText;
            upgradeHolders[i].priceText = priceText;
            UpdateText(upgradeHolders[i]);

            buttonClone.GetComponent<UpgradeButton>().Active(upgradeHolders[i]);
        }
    }

    public void Upgrade(UpgradeHolder holder) 
    {
        if (!CheckCurrency(holder.CurrentPrice)) return;
        UIManager.Instance.AddCurrency(-holder.CurrentPrice);
        PlayerUpgrade.Instance.StartCoroutine(holder.functionName, holder.AddPower);
        holder.Level++;
        holder.CurrentPrice += holder.AddPrice;
        UpdateText(holder);
        PlayerPrefs.SetInt("Upgrade" + holder.Name, holder.Level);
    }

    private void UpdateText(UpgradeHolder holder)
    {
        int level = holder.Level + 1;
        holder.levelText.text = "Level " + level;
        holder.priceText.text = holder.CurrentPrice + "$";
    }

    private void GetTexts(Transform parentObject ,out TextMeshProUGUI levelText, out TextMeshProUGUI priceText) 
    {
        TextMeshProUGUI level;
        TextMeshProUGUI price;
        var child0 = parentObject.GetChild(0);
        var child1= parentObject.GetChild(1);
        child0.TryGetComponent(out level);
        child1.TryGetComponent(out price);

        levelText = level;
        priceText = price;
    }
    private bool CheckCurrency(int price) 
    {
        if (UIManager.Instance.Currency < price)
            return false;

        return true;
    }

    [System.Serializable]
    public class UpgradeHolder
    {
        public string Name;
        public string functionName;
        public float AddPower;
        public int FirstPrice;
        public int AddPrice;
        public Button PrefabButton;

        [HideInInspector] public int Level;
        [HideInInspector] public int CurrentPrice;
        [HideInInspector] public TextMeshProUGUI levelText;
        [HideInInspector] public TextMeshProUGUI priceText;
    }
}
