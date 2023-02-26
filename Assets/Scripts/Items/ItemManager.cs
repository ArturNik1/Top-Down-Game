using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int powerUpsToSpawn = 2;
    public int weaponsToSpawn = 1;

    public Vector3[] spawnLocationsArray;

    public GameObject[] powerUps;
    public GameObject[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        AddPowerUpsToStoreList();
    }

    public void AddPowerUpsToStoreList() {
        string filePath = Application.dataPath + "/Resources/storeItems.json";
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            HashSet<StoreItem> purchasedItems = JsonConvert.DeserializeObject<HashSet<StoreItem>>(json);
            StoreItem[] storeItems = new StoreItem[purchasedItems.Count];
            int i = 0;
            foreach (StoreItem storeItem in purchasedItems) {
                storeItems[i++] = storeItem;
            }

            GameObject[] combinedPowerUps = new GameObject[storeItems.Length + powerUps.Length];
            for (i = 0; i < powerUps.Length; i++) {
                combinedPowerUps[i] = powerUps[i];
            }
            for (; i < combinedPowerUps.Length; i++) {
                string path = "Prefabs/Items/Store Items/" + storeItems[i - powerUps.Length].prefabName;
                //combinedPowerUps[i] = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                combinedPowerUps[i] = (GameObject)Resources.Load(path);
            }

            powerUps = combinedPowerUps;
        }
    }

    public void SpawnItemsOnMap() {
        for (int i = 0; i < powerUpsToSpawn && powerUps.Length > 0; i++) {
            int r = UnityEngine.Random.Range(0, powerUps.Length);
            float x = UnityEngine.Random.Range(spawnLocationsArray[0].x, spawnLocationsArray[1].x);
            float y = UnityEngine.Random.Range(spawnLocationsArray[1].y, spawnLocationsArray[0].y);
            GameObject obj = Instantiate(powerUps[r]);
            obj.transform.position = new Vector3(x, y, 0);
            obj.transform.GetComponent<Item>().UpdateY_Start();
        }

        for (int i = 0; i < weaponsToSpawn && weapons.Length > 0; i++) {
            int r = UnityEngine.Random.Range(0, weapons.Length);
            float x = UnityEngine.Random.Range(spawnLocationsArray[0].x, spawnLocationsArray[1].x);
            float y = UnityEngine.Random.Range(spawnLocationsArray[1].y, spawnLocationsArray[0].y);
            GameObject obj = Instantiate(weapons[r]);
            obj.transform.position = new Vector3(x, y, 0);
            obj.transform.GetComponent<Item>().UpdateY_Start(); 
        }
    }

    public Item[] SpawnThreeItemsOnMapForPrize(GameObject[] toSpawn) {
        Vector3 location = new Vector3(-1000, -1000, -1000);
        return new Item[] {Instantiate(toSpawn[0], location, Quaternion.identity).GetComponent<Item>(),
                            Instantiate(toSpawn[1], location, Quaternion.identity).GetComponent<Item>(),
                            Instantiate(toSpawn[2], location, Quaternion.identity).GetComponent<Item>()};
    }

    public void SpawnWeapon(Vector3 position) {
        if (weapons.Length == 0) return;

        int r = UnityEngine.Random.Range(0, weapons.Length);
        GameObject obj = Instantiate(weapons[r]);
        obj.transform.position = position;
        obj.transform.GetComponent<Item>().UpdateY_Start();
    }

    public void SpawnPowerUp(Vector3 position) {
        if (powerUps.Length == 0) return;

        int r = UnityEngine.Random.Range(0, powerUps.Length);
        GameObject obj = Instantiate(powerUps[r]);
        obj.transform.position = position;
        obj.transform.GetComponent<Item>().UpdateY_Start();
    }

    private void ShuffleArray<T>(T[] arr) {
        for (int i = 0; i < arr.Length; i++)
        {
            T tmp = arr[i];
            int r = UnityEngine.Random.Range(i, arr.Length);
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }

    public Item[] GetThreeItemsForPrizePanel() {
        if (powerUps.Length < 3) throw new Exception("not enough powerups...");
        GameObject[] randomPowerUps = (GameObject[])powerUps.Clone();
        ShuffleArray(randomPowerUps);

        return SpawnThreeItemsOnMapForPrize(randomPowerUps);
    }


    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < spawnLocationsArray.Length; ++i)
        {
            Gizmos.DrawWireSphere(spawnLocationsArray[i], 0.5f);
        }

    }

}

