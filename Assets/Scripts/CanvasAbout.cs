using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class CanvasAbout : MonoBehaviour
{
  private string WEBSITE = "https://AppsPlantingTrees.org";
  private string TWITTER = "https://twitter.com/AppsTrees";
  private string FACEBOOK = "https://www.facebook.com/AppsTrees";


  public void HideAboutMenu()
  {
    gameObject.SetActive(false);
  }

  public void GoToOurTwitter()
  {
    Application.OpenURL(TWITTER);
  }

  public void GoToOurFB()
  {
    Application.OpenURL(FACEBOOK);
  }

  public void GoToOurWebsite()
  {
    Application.OpenURL(WEBSITE);
  }

}
