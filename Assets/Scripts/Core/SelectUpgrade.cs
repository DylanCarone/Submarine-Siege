using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SelectUpgrade : MonoBehaviour
{
    [Header("Upgrade Options")] [SerializeField]
    private List<UpgradeButtonSO> upgradePool;

    [SerializeField] private int numberOfOptions = 3;
    
    [Header("UI References")]
    [SerializeField] private GameObject upgradeSelectionPanel;
    [SerializeField] private Transform upgradeButtonsContainer;
    [SerializeField] private GameObject upgradeButtonPrefab;
    
    [Header("References")]
    [SerializeField] PlayerUpgradeInventory playerUpgradeInventory;

    private List<UpgradeButtonSO> currentOptions = new List<UpgradeButtonSO>();
    private Action onUpgradeComplete;



    public void PresentUpgradeOptions(Action onComplete = null)
    {
        onUpgradeComplete = onComplete;

        foreach (Transform child in upgradeButtonsContainer)
        {   
            Destroy(child.gameObject);
        }
        
        currentOptions = GetRanomUpgradeOptions(numberOfOptions);
        
        foreach (UpgradeButtonSO upgrade in currentOptions)
        {
            GameObject newButton = Instantiate(upgradeButtonPrefab, upgradeButtonsContainer);
            ConfigureUpgradeButton(newButton, upgrade);
        }
        
        upgradeSelectionPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    

    private void ConfigureUpgradeButton(GameObject buttonObj, UpgradeButtonSO upgrade)
    {
        Button button = buttonObj.GetComponent<Button>();

        Transform nameText = buttonObj.transform.Find("NameText");
        Transform descriptionText = buttonObj.transform.Find("DescriptionText");
        Transform iconImage = buttonObj.transform.Find("IconImage");
        
        if (nameText != null)
            nameText.GetComponent<TMPro.TextMeshProUGUI>().text = upgrade.upgradeName;
            
        if (descriptionText != null)
            descriptionText.GetComponent<TMPro.TextMeshProUGUI>().text = upgrade.description;
            
        if (iconImage != null && upgrade.upgradeIcon != null)
            iconImage.GetComponent<Image>().sprite = upgrade.upgradeIcon;
        
        button.onClick.AddListener(()=>OnUpgradeSelected(upgrade) );

        
    }
    
    private void OnUpgradeSelected(UpgradeButtonSO selectedUpgrade)
    {
        selectedUpgrade.ApplyUpgrade(playerUpgradeInventory);
        upgradeSelectionPanel.SetActive(false);
        Time.timeScale = 1f;
        onUpgradeComplete?.Invoke();
    }



    private List<UpgradeButtonSO> GetRanomUpgradeOptions(int count)
    {
        List<UpgradeButtonSO> availableUpgrades = new List<UpgradeButtonSO>(upgradePool);
        List<UpgradeButtonSO> selectedUpgrades = new List<UpgradeButtonSO>();
        
        count = Mathf.Min(count, availableUpgrades.Count);
        
        for(int i = 0; i < count; i++)
        {
            if (availableUpgrades.Count == 0) break;
            
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            selectedUpgrades.Add(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex);
            
        }
        
        return selectedUpgrades;
    }
    

}
