using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemType type;
    public string pickUPText;

    private float y_start;

    public void UpdateY_Start() {
        y_start = transform.position.y;
    }

    public void Start()
    {
        UpdateY_Start();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float y = y_start + 0.1f * Mathf.Sin(Time.time * 2);
        transform.position = new Vector3(pos.x, y, pos.z);
    }

    public abstract void PickUpItem();

}

public enum ItemType {Weapon, PowerUp};