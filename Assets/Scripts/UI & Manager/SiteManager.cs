using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 *  원하는 웹사이트를 띄우는 스크립트입니다.
 */
public class SiteManager : MonoBehaviour
{
    public static void OpenGithub()
    {
        Application.OpenURL("https://store.steampowered.com/app/4336820/The_Developer/?beta=0");
    }

    public static void OpenAnySite(string url)
    {
        Application.OpenURL(url);
    }
}
