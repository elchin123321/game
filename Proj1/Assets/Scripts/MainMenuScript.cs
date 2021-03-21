using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject registerPanel;
    [SerializeField] GameObject signInPanel;
    [SerializeField] GameObject regSginInChoisePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& (!regSginInChoisePanel.activeSelf || !settingsPanel.activeSelf))
        {
            settingsPanel.SetActive(false);
            regSginInChoisePanel.SetActive(false);
            signInPanel.SetActive(false);
            registerPanel.SetActive(false);
        }
    }
    
    public void SettingsClick()
    {
        settingsPanel.SetActive(true);
    }

    public void PlayClick()
    {
        regSginInChoisePanel.SetActive(true);
    }

    public void StartGame(string name)
    {
        SceneManager.LoadScene("Level_1");
        DataHolder.HeroName = name;
        
    }
    public void RegisterActivateButton()
    {
        registerPanel.SetActive(true);
        regSginInChoisePanel.SetActive(false);
    }
    public void SignInActivateButton()
    {
        signInPanel.SetActive(true);
        regSginInChoisePanel.SetActive(false);
    }
}
