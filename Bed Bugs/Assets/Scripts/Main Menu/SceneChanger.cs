using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator transition;

    void Start()
    {
        transition.Play("FadeOut");
    }

    public void StartGame()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        transition.Play("FadeIn");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Narrator 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
