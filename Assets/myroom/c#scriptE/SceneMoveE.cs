using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMoveE : MonoBehaviour
{
   public void mainroomE(){ // 마이룸
        SceneManager.LoadScene("mainroom");
    }
   
    public void RankingHome(){ // 랭킹홈
        SceneManager.LoadScene("RankingHome");
    }
   
    public void announcement(){ // 공지사항
        SceneManager.LoadScene("announcement");
    }
    public void RankingSeeE(){ // 랭킹확인
        SceneManager.LoadScene("RankingSeeE");
    }
    public void RankingFightE(){ // 랭킹게임
        SceneManager.LoadScene("RankingFightE");
    }
    public void characterShowE(){ // 캐릭터소개
        SceneManager.LoadScene("4.1 character");
    }

      public void Stagehome(){ // 스테이지홈
        SceneManager.LoadScene("Stagehome");
    }
    public void Settings(){ // 설정
        SceneManager.LoadScene("4.2 Settings");
    }

    public void Rankingani(){ // 성
        SceneManager.LoadScene("Rankingani");
    }
    
}
