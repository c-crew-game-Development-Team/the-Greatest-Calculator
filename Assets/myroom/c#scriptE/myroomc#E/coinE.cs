using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinE : MonoBehaviour
{
    public TextMeshProUGUI coinText; // 코인 텍스트
    public TextMeshProUGUI numberBundleText; // 숫뭉 텍스트
    public FirebaseInitializer Firebaseapi;
    int coinscore = 0;
    int testscore = 0;

    void Start()
    {
        StartCoroutine(WaitForFirebaseInitialization());
    }

    private System.Collections.IEnumerator WaitForFirebaseInitialization()
    {
        // Firebase 초기화가 완료될 때까지 대기
        while (!Firebaseapi.IsInitialized)
        {
            Debug.Log("Firebase 초기화를 기다리는 중...");
            yield return null; // 다음 프레임까지 대기
        }

        Debug.Log("Firebase 초기화 완료. 데이터를 가져옵니다.");

        // PlayerPrefs에서 유저 ID 가져오기
        string userId = PlayerPrefs.GetString("UserNickname", "DefaultUser");

        // 메인룸 정보 가져오기
        Firebaseapi.GetMainRoomUserInfo(
            userId,
            onSuccess: (snapshot) =>
            {
                // 데이터를 가져온 후 처리
                object coin = snapshot.Child("Coin").Value ?? "오류";
                object numberBundle = snapshot.Child("NumberBundle").Value ?? "오류";

                // TextMeshPro에 값 업데이트
                coinText.text = coin.ToString();
                numberBundleText.text = numberBundle.ToString();
            },
            onError: (errorMessage) =>
            {
                // 에러 처리
                Debug.LogError("에러 발생: " + errorMessage);
            }
        );
    }

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
