using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InputField : MonoBehaviour
{
    public TMP_InputField nametext;
    public static string Username;
    public Rankingapi scRankingapi;
   
    public void clickInput() 
    {
        if(string.IsNullOrEmpty(nametext.text) == false){
            Debug.Log(nametext.text);
            Username = nametext.text;
            scRankingapi.SendScore(Username, 0);
            SceneManager.LoadScene("3 Startani");
        }
    }
}
