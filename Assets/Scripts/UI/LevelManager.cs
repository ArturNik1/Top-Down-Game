using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1f;

    private void Start()
    {
        PlayerPrefs.SetInt("first_start", 0);
        PlayerPrefs.Save();
    }

    public void LoadLevelWrapper(string name) {
        if (!HandleFirstTimer()) 
            StartCoroutine(LoadLevel(name));
    }

    IEnumerator LoadLevel(string name) {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(name);
    }

    private bool HandleFirstTimer() { 
        if (PlayerPrefs.GetInt("first_start") == 0) {
            // first start
            PlayerPrefs.SetInt("first_start", 1);
            PlayerPrefs.Save();

            // load scene
            StartCoroutine(LoadLevel("FirstStart"));
            return true;
        }
        return false;
    }

}
