using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum WaitScenarios { NotWaiting, AnyInput, MoveInput, DashInput, AttackInput, ToLocation, Kill, PickUpItem, NextRoom };

public class TutorialManager : MonoBehaviour
{
    [HideInInspector]
    public bool animationIsDone = false;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI healthText;
    public GameObject healthBar;
    public GameObject circleGoal;
    public GameObject dummyEnemy;
    public GameObject dummyItem;
    public GameObject wall;

    private Color firstColor;
    private Color regularColor;

    private WaitScenarios currentCase = WaitScenarios.NotWaiting;
    private int alphaTarget = 1;
    private bool stopOnNextIteration = false;
    [HideInInspector]
    public bool reachedGoal = false;
    [HideInInspector]
    public bool killedDummy = false;
    [HideInInspector]
    public bool pickedUpItem = false;
    [HideInInspector]
    public bool canExit = false;

    private void Start()
    {
        firstColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1);
        regularColor = new Color(183f / 255f, 217f / 255f, 192f / 255f, 1);

        StartCoroutine(HandleStartScene());
    }

    // Update is called once per frame
    void Update()
    {

        if (!animationIsDone) return;

        infoText.alpha = Mathf.MoveTowards(infoText.alpha, alphaTarget, 0.5f * Time.deltaTime);

        if (infoText.alpha == 1) {
            alphaTarget = 0;
        }
        else if (infoText.alpha == 0) {
            alphaTarget = 1;
            if (stopOnNextIteration) {

                if (currentCase == WaitScenarios.ToLocation) {
                    reachedGoal = false;
                    circleGoal.SetActive(false);
                    //  Spawn the dummy enemy in front with an effect.
                    if (!killedDummy) {
                        Instantiate(dummyEnemy, Vector3.zero, Quaternion.identity);
                    }
                    //  Spawn an item.
                    else {
                        Instantiate(dummyItem, Vector3.zero, Quaternion.identity);
                        AudioManager.instance.Play("cartoon_bang");
                    }
                }
                else if (currentCase == WaitScenarios.AnyInput) {
                    healthText.enabled = false;
                }
                else if (currentCase == WaitScenarios.PickUpItem) {
                    //  Open the doors to the exit.
                    wall.SetActive(false);
                }

                currentCase = WaitScenarios.NotWaiting;
                infoText.text = "";
                stopOnNextIteration = false;
            }
        }


        if (currentCase == WaitScenarios.MoveInput && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))) {
            stopOnNextIteration = true;
        }

        else if (currentCase == WaitScenarios.DashInput && Input.GetKeyDown(KeyCode.LeftShift)) {
            stopOnNextIteration = true;
        }

        else if (currentCase == WaitScenarios.AttackInput && Input.GetKeyDown(KeyCode.Space)) {
            stopOnNextIteration = true;
        }

        else if (currentCase == WaitScenarios.AnyInput && Input.anyKeyDown) {
            stopOnNextIteration = true;
        }

        else if (currentCase == WaitScenarios.ToLocation && reachedGoal) {
            stopOnNextIteration = true;
        }

        else if (currentCase == WaitScenarios.Kill && killedDummy) {
            stopOnNextIteration = true;
        }

        else if (currentCase == WaitScenarios.PickUpItem && pickedUpItem) {
            stopOnNextIteration = true;
        }

        else if (currentCase == WaitScenarios.NextRoom && canExit) {
            GameObject.Find("Level Manager").GetComponent<LevelManager>().LoadLevelWrapper("SampleScene");
        }

    }

    IEnumerator HandleStartScene() {
        // If AnimationClip is done:
        //  Enable player movement
        while (!animationIsDone) {

            infoText.color = firstColor;

            string targetText = "I CAN'T BELIEVE I HAVE SURVIVED AND\nREACHED THE THIRD YEAR...";
            infoText.text = "";
            for (int i = 0; i < targetText.Length; i++) {
                infoText.text = infoText.text + targetText[i];
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);

            targetText = "EVEN THE CATS ARE NOT LOOKING AT ME\nARROGANTLY ANYMORE";
            infoText.text = "";
            for (int i = 0; i < targetText.Length; i++) {
                infoText.text = infoText.text + targetText[i];
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);

            targetText = "WELL... THIRD YEAR... HERE I COME\nI AM SURE IT WON'T BE EASY...";
            infoText.text = "";
            for (int i = 0; i < targetText.Length; i++) {
                infoText.text = infoText.text + targetText[i];
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);


            yield return new WaitForEndOfFrame();
        }

        infoText.color = regularColor;

        //  Have an on-screen message for movement buttons and wait for input, once you get the input fade out the message.
        HandleOnScreenText("Press W A S D To Move...");
        currentCase = WaitScenarios.MoveInput;
        while (currentCase == WaitScenarios.MoveInput)
            yield return new WaitForEndOfFrame();

        //  Have an on-screen message for dash button and wait for input, once you get the input fade out the message.
        HandleOnScreenText("Press Left Shift While Moving To Dash...");
        currentCase = WaitScenarios.DashInput;
        while (currentCase == WaitScenarios.DashInput)
            yield return new WaitForEndOfFrame();

        //  Have an on-screen message for attack button and wait for input, once you get the input fade out the message.
        HandleOnScreenText("Press Space To Attack...");
        currentCase = WaitScenarios.AttackInput;
        while (currentCase == WaitScenarios.AttackInput)
            yield return new WaitForEndOfFrame();

        //  Have an on-screen message for health explanation and wait for input, once you get the input fade out the message.
        HandleOnScreenText("Press Any Key To Continue...");
        currentCase = WaitScenarios.AnyInput;
        healthText.enabled = true;
        healthBar.SetActive(true);
        while (currentCase == WaitScenarios.AnyInput)
            yield return new WaitForEndOfFrame();

        //  Have an on-screen message that asks the player to move to location X, once you get there fade out the message.
        HandleOnScreenText("Move To The Highlighted Location...");
        currentCase = WaitScenarios.ToLocation;
        circleGoal.SetActive(true);
        while (currentCase == WaitScenarios.ToLocation)
            yield return new WaitForEndOfFrame();

        //  Have an on-screen message that asks the player to move to kill the dummy enemy, once you do it fade out the message.
        HandleOnScreenText("Attack The Enemy...");
        currentCase = WaitScenarios.Kill;
        while (currentCase == WaitScenarios.Kill)
            yield return new WaitForEndOfFrame();

        //  Have an on-screen message that asks the player to move to location X, once you get there fade out the message.
        HandleOnScreenText("Move To The Highlighted Location...");
        currentCase = WaitScenarios.ToLocation;
        circleGoal.SetActive(true);
        while (currentCase == WaitScenarios.ToLocation)
            yield return new WaitForEndOfFrame();

        //  Have an on-screen message that explains about the items and weapons, wait for input and fade out the message.
        HandleOnScreenText("You Can Pickup Different Items And Weapons To Get Stronger...");
        currentCase = WaitScenarios.PickUpItem;
        while (currentCase == WaitScenarios.PickUpItem)
            yield return new WaitForEndOfFrame();

        //  Have an on-screen message that asks the player exit through the door, once you get there load next scene.
        HandleOnScreenText("Exit The Room To Continue...");
        currentCase = WaitScenarios.NextRoom;
        while (currentCase == WaitScenarios.NextRoom)
            yield return new WaitForEndOfFrame();
    }

    void HandleOnScreenText(string text) {
        infoText.text = text;
        infoText.alpha = 0;
        alphaTarget = 1;
    }

}
