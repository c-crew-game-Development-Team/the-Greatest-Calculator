using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mountainstagego : MonoBehaviour
{
    public MySetting alertPanel; // 경고 창 (비활성화된 상태

    public void sum3(){

        // StageProcess PlayerPrefs 값 확인
        int stageProcess = PlayerPrefs.GetInt("StageProcess", 0); // 기본값은 0

        if (stageProcess == 2)
        {
            Debug.Log($"StageProcess : {stageProcess}, 씬 이동");
            SceneManager.LoadScene("Stage 3");
        }
        else
        {
            Debug.Log($"StageProcess : {stageProcess}, StageProcess가 2이 아님");
            ShowAlertPanel(); // 경고 창 표시
        }
    }
    
    void ShowAlertPanel()
    {
        alertPanel.Loadingblack();
    }
}