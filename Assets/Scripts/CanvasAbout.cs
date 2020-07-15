using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAbout : MonoBehaviour
{
  /*string OUR_WEBSITE = "https://AppsPlantingTrees.org";
  string TWITTER_PAGE_ID = "https://twitter.com/AppsTrees";
  string FACEBOOK_URL = "https://www.facebook.com/AppsTrees";
  string FACEBOOK_PAGE_ID = "AppsTrees";*/

  public void HideAboutMenu()
  {
    GameObject aboutMenu = GameObject.FindWithTag("MenuAbout");
    aboutMenu.SetActive(false);
  }

  public void GoToOurTwitter()
  {
      Debug.Log("Going to twitter...");
      //Intent intent = new Intent(Intent.ACTION_VIEW);
      //intent.setData(Uri.parse(TWITTER_PAGE_ID));
      //startActivity(intent);
  }

  public void GoToOurFB()
  {
    Debug.Log("Going to FB...");
    /*Intent fbIntent = new Intent(Intent.ACTION_VIEW);
    String fbUrl;
    PackageManager packageManager = this.getPackageManager();
        try {
              int versionCode = packageManager.getPackageInfo("com.facebook.katana", 0).versionCode;
              if (packageManager.getApplicationInfo("com.facebook.katana", 0).enabled) {
                  if (versionCode >= 3002850) { //newer versions of fb app
                      fbUrl = "fb://facewebmodal/f?href=" + FACEBOOK_URL;
                  } else {
                      fbUrl = "fb://page/" + FACEBOOK_PAGE_ID;
                  }
              } else {
                  fbUrl = FACEBOOK_URL;
              }
            } catch (PackageManager.NameNotFoundException e) {
                fbUrl = FACEBOOK_URL;
            }
            fbIntent.setData(Uri.parse(fbUrl));
        startActivity(fbIntent);*/
  }

  public void GoToOurWebsite()
  {
    Debug.Log("Going to web...");
    //Intent intent = new Intent(Intent.ACTION_VIEW);
    //intent.setData(Uri.parse(OUR_WEBSITE));
    //startActivity(intent);
  }

}
