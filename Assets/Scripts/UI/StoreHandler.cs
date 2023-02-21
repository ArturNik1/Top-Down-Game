using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoreHandler : MonoBehaviour
{
    string filePath;
    Dictionary<string, StoreItem> storeItems = new Dictionary<string, StoreItem>();
    HashSet<StoreItem> purchasedItems = new HashSet<StoreItem>();

    public bool resetFile = false;
    //[Header("if adding/changing, remember to do it in PlayerStats, function UpdateAchievementsStats() too")]
    public List<StoreItem> storeItemSetup;

    public GameObject storeItemButton;
    public GameObject contentGameObject;


    // Start is called before the first frame update
    void Start() {
        filePath = Application.dataPath + "/Resources/storeItems.json";

        if (resetFile && File.Exists(filePath)) File.Delete(filePath);

        ReadDataFromInspector();
        ReadDataFromFile();
        UpdateButtons();
    }

    public void WriteDataToFile() {
        // write to the file only the items we've bought.
        string json = JsonConvert.SerializeObject(purchasedItems, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public void ReadDataFromFile() { 
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            purchasedItems = JsonConvert.DeserializeObject<HashSet<StoreItem>>(json);
            foreach(StoreItem storeItem in purchasedItems) { 
                if (storeItems.ContainsKey(storeItem.itemName)) {
                    storeItems[storeItem.itemName].purchased = true;
                }
            }
        }
    }

    public void AddObjectToPurchasedSet(StoreItem storeItem) {
        purchasedItems.Add(storeItem);
        WriteDataToFile();
    }

    public void AddObject(StoreItem storeItem) {
        if (storeItems.ContainsKey(storeItem.itemName)) {
            storeItems.Remove(storeItem.itemName);
        }
        storeItems.Add(storeItem.itemName, storeItem);
    }

    public void ReadDataFromInspector() {
        foreach (StoreItem storeItem in storeItemSetup) {
            AddObject(storeItem);
        }
    }

    public void UpdateButtons() {
        bool create = true;
        if (contentGameObject.transform.childCount > 0) create = false;
        int i = 0;
        foreach (StoreItem storeItem in storeItems.Values) {
            GameObject buttonObject;
            if (create) buttonObject = Instantiate(storeItemButton, contentGameObject.transform);
            else {
                buttonObject = contentGameObject.transform.GetChild(i).gameObject;
                i++;
            }
            StoreItemButton storeItemUI = buttonObject.GetComponent<StoreItemButton>();
            storeItemUI.storeItem = storeItem;
            storeItemUI.UpdateButton(storeItem);
        }
    }

}

[System.Serializable]
public class StoreItem
{
    public string prefabName;
    public string itemName;
    public string itemDescription;
    public int price;
    public bool purchased;

    public StoreItem(string prefabName, string itemName, string itemDescription, int price, bool purchased)
    {
        this.prefabName = prefabName;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.price = price;
        this.purchased = purchased;
    }
}
