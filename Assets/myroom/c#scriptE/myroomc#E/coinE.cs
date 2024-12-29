using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinE : MonoBehaviour
{
    public TextMeshProUGUI coinText;// 코인텍스트
    int coinscore = 0;
    int testscore = 0;

    public void coinUp(){ //점수오르기
        testscore = coinscore; //점수
        StartCoroutine("scoreanimation"); 
        Invoke("realswordscore", 1f);
    }

    IEnumerator scoreanimation(){ // 점수애니매이션
        int a = 10;
        while(a > 0){
            testscore += 20;
            string coinString = testscore.ToString();
            coinText.text = coinString; 
            yield return new WaitForSeconds(0.1f);
            a-- ;
        }
    }
    public void realswordscore(){ //찐점수
        StopCoroutine("scoreanimation");
        coinscore += 200; 
        string coinString = coinscore.ToString();
        coinText.text = coinString; 
    }
}
