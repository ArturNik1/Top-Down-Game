using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int powerUpsToSpawn = 2;
    public int weaponsToSpawn = 1;

    public GameObject spawnLocationsHolder;
    private Vector3[] spawnLocations;

    public GameObject[] powerUps;
    public GameObject[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] children = spawnLocationsHolder.GetComponentsInChildren<Transform>();
        spawnLocations = new Vector3[children.Length - 1];
        for (int i = 1; i < children.Length; i++) {
            spawnLocations[i - 1] = children[i].transform.position;
        }
        SpawnItemsOnMap();
    }

    public void SpawnItemsOnMap() {
        Vector3[] randomLocations = (Vector3[])spawnLocations.Clone();
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

    private void ShuffleArray<T>(T[] arr) {
        for (int i = 0; i < arr.Length; i++)
        {
            T tmp = arr[i];
            int r = UnityEngine.Random.Range(i, arr.Length);
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }


}

