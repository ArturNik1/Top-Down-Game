using System;
using System.Collections;
using System.Collections.Generic;
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
        SpawnItemsOnMap();
    }

    public void SpawnItemsOnMap() {
        Vector3[] randomLocations = (Vector3[])spawnLocationsArray.Clone();
        ShuffleArray(randomLocations);
        int currentIndex = 0;

        for (int i = 0; i < powerUpsToSpawn && currentIndex < randomLocations.Length && powerUps.Length > 0; i++) {
            int r = UnityEngine.Random.Range(0, powerUps.Length);
            GameObject obj = Instantiate(powerUps[r]);
            obj.transform.position = randomLocations[currentIndex];
            obj.transform.GetComponent<Item>().UpdateY_Start();
            currentIndex++;
        }

        for (int i = 0; i < weaponsToSpawn && currentIndex < randomLocations.Length && weapons.Length > 0; i++) {
            int r = UnityEngine.Random.Range(0, weapons.Length);
            GameObject obj = Instantiate(weapons[r]);
            obj.transform.position = randomLocations[currentIndex];
            obj.transform.GetComponent<Item>().UpdateY_Start(); 
            currentIndex++;
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

