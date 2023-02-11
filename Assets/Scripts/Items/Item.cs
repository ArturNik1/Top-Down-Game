using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemType type;
    public string pickUPText;

    private float y_start;

    private bool paused = false;

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
        if (paused) return;
        Vector3 pos = transform.position;
        float y = y_start + 0.1f * Mathf.Sin(Time.time * 2);
        transform.position = new Vector3(pos.x, y, pos.z);
    }

    public abstract void PickUpItem();

    private void Pause()
    {
        paused = true;
    }

    private void Unpause()
    {
        paused = false;
    }

    private void OnEnable()
    {
        UIManager.onPause += Pause;
        UIManager.onUnpause += Unpause;
    }

    private void OnDisable()
    {
        UIManager.onPause -= Pause;
        UIManager.onUnpause -= Unpause;
    }

}

public enum ItemType {Weapon, PowerUp};