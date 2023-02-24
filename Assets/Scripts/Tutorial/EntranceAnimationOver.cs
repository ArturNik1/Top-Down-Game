using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAnimationOver : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public GameObject leftWall;
    public RuntimeAnimatorController playerController;
    public Animator playerAnimator;

    public void ChangeLeftWall()
    {
        leftWall.SetActive(!leftWall.activeSelf);
    }

    public void EntranceAnimationIsDone() {
        ChangeLeftWall();
        tutorialManager.animationIsDone = true;
        GetComponent<Animator>().enabled = false;
        playerAnimator.runtimeAnimatorController = playerController;

    }

}
