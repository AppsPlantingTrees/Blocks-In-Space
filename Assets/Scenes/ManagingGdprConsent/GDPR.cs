using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GDPR : MonoBehaviour
{
    public Image mainPanel;
    public Image yesPanel;
    public Image noPanel;
    public Text mainText;

    public void onYesClick()
    {
        PlayerPrefs.SetInt("result_gdpr", 1);
        PlayerPrefs.SetInt("result_gdpr_sdk", 1);
        mainPanel.gameObject.SetActive(false);
        yesPanel.gameObject.SetActive(true);
    }

    public void onNoClick()
    {
        PlayerPrefs.SetInt("result_gdpr", 1);
        PlayerPrefs.SetInt("result_gdpr_sdk", 0);
        mainPanel.gameObject.SetActive(false);
        noPanel.gameObject.SetActive(true);
    }

    public void onPLClick()
    {
        Application.OpenURL("https://www.appodeal.com/privacy-policy");
    }

    public void onCloseClick()
    {
        SceneManager.LoadScene("main");
    }
}