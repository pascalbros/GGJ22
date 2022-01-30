using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartSelected() {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void OnCreditsSelected() {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    public void OnQuitSelected() {
        Application.Quit();
    }
}
