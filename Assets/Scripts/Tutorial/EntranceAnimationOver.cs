using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAnimationOver : MonoBehaviour
{
    public TutorialManager tutorialManager;

    public void EntranceAnimationIsDone() {
        tutorialManager.animationIsDone = true;
        GetComponent<Animator>().enabled = false;
    }

}
