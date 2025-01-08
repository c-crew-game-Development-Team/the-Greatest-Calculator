using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class localDataNicknameScene : MonoBehaviour
{
    void SceneChangePlayername(){ 
        SceneManager.LoadScene("2 playername");
    }

    void SceneChangeMainroom(){ 
        SceneManager.LoadScene("mainroom");
    }


    public void LoadNickname()
    {
        if (PlayerPrefs.HasKey("UserNickname"))
        {
            SceneChangeMainroom();// 닉네임 없으면 닉네임 홈 이동
        }
        else
        {
            SceneChangePlayername();// 닉네임 없으면 닉네임 설정 창 이동
        }
    }
}
