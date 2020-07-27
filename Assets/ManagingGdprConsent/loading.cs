using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement; 

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class loading : MonoBehaviour
{
    private void Start()
    {
        bool wasConsentReceived = false;
        int cons = PlayerPrefs.GetInt("result_gdpr", 0);
        if (cons == 1) wasConsentReceived = true;
        SceneManager.LoadScene(wasConsentReceived ? "main" : "GDPR");
    }
}