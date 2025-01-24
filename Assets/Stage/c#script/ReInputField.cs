using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ReInputField : MonoBehaviour
{
    public TMP_InputField nametext;
    public static string Username;
    public FirebaseInitializer Firebaseapi;
   
    public void clickInput() 
    {
        if(string.IsNullOrEmpty(nametext.text) == false){
            Debug.Log(nametext.text);
            Username = nametext.text;

            string currentNickname = PlayerPrefs.GetString("UserNickname", "DefaultUser");
            Firebaseapi.UpdateNickname(currentNickname, Username); //  파이어베이스 유저 이름 바꾸기 

            SaveNickname(Username); // 기기에도 업데이트
            SceneManager.LoadScene("mainroom"); // 애니가 아니라 메인룸으로 
        }
    }

    void SaveNickname(string nickname)
    {
        PlayerPrefs.SetString("UserNickname", nickname);
        PlayerPrefs.Save(); // 닉네임 저장 기기 영구화 ( 다시 저장됨 )
    }
}