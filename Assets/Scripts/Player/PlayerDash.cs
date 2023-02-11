using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DashState
{
    Charging, Dashing, Ready
}

public class PlayerDash : MonoBehaviour
{
    public float maxDashTime;
    public float maxChargeTime;
    private float currentChargeTime;
    private float currentDashTime;


    private DashState state;
    private Player_Movement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<Player_Movement>();
        state = DashState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.paused) return;   
        if (state == DashState.Ready && Input.GetKeyDown(KeyCode.LeftShift)) {
            playerMovement.setDashing(true);
            state = DashState.Dashing;
        }
        else if (state == DashState.Dashing) {
            currentDashTime += Time.deltaTime;
            if (currentDashTime >= maxDashTime) {
                currentDashTime = 0;
                playerMovement.setDashing(false);
                state = DashState.Charging;
            }
        }
        else {
            currentChargeTime += Time.deltaTime;
            if (currentChargeTime >= maxChargeTime) {
                currentChargeTime = 0;
                state = DashState.Ready;
            }
        }

    }
}

