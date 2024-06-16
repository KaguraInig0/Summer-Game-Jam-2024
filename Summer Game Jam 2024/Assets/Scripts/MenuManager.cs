using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] public GameObject controlsPanel;
    [SerializeField] public GameObject settingsPanel;

    void Start()
    {
        // controlsPanel = GetComponent<GameObject>();
        // settingsPanel = GetComponent<GameObject>();
    }

    public void StartGame()
    {
        //Debug.Log("StartGame method called");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void RestartGame()
    {
        //Debug.Log("RestartGame method called");
        SceneManager.LoadScene(0);
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
