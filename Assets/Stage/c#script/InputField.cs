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
    public FirebaseInitializer Firebaseapi;
   
    public void clickInput() 
    {
        if(string.IsNullOrEmpty(nametext.text) == false){
            Debug.Log(nametext.text);
            Username = nametext.text;
            SaveNickname(Username);
            Firebaseapi.InitializeUserData(Username); //  파이어베이스 내용 초기화
            SceneManager.LoadScene("3 Startani");
        }
    }

    void SaveNickname(string nickname)
    {
        PlayerPrefs.SetString("UserNickname", nickname);
        PlayerPrefs.Save(); // 닉네임 저장 기기 영구화
    }
}
