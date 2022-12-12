using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1f;

    public void LoadLevelWrapper(string name) {
        StartCoroutine(LoadLevel(name));
    }

    IEnumerator LoadLevel(string name) {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(name);
    }

}
