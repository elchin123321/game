using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject inputNamePanel;
    public GameObject settingsPanel;
    public InputField heroNameInputField;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& (!inputNamePanel.activeSelf || !settingsPanel.activeSelf))
        {
            settingsPanel.SetActive(false);
            inputNamePanel.SetActive(false);
        }
    }
    
    public void SettingsClick()
    {
        settingsPanel.SetActive(true);
    }

    public void PlayClick()
    {
        inputNamePanel.SetActive(true);
    }

    public void EndInput(string name)
    {
        DataHolder.HeroName=heroNameInputField.text;
        SceneManager.LoadScene("Level_1");
    }
}
