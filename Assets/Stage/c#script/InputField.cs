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
            SaveNickname(Username);
            //scRankingapi.SendScore(Username, 0);  => 정은체 | 0 | 견습기사 | 0 | 0 이런 식의 초기화
            SceneManager.LoadScene("3 Startani");
        }
    }

    void SaveNickname(string nickname)
    {
        PlayerPrefs.SetString("UserNickname", nickname);
        PlayerPrefs.Save(); // 닉네임 저장 기기 영구화
    }
}
