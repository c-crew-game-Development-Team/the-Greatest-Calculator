using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class rankingDB_get : MonoBehaviour
{
    public TextMeshProUGUI levelText; // 직위 텍스트
    public TextMeshProUGUI nickNameText; // 닉네임 텍스트
    public TextMeshProUGUI swordText; // 검술숙련도 텍스트
    public FirebaseInitializer Firebaseapi;

    void Start()
    {
        StartCoroutine(WaitForFirebaseInitialization());
    }

    private IEnumerator WaitForFirebaseInitialization()
    {
        // Firebase 초기화가 완료될 때까지 대기
        while (!Firebaseapi.IsInitialized)
        {
            Debug.Log("Firebase 초기화를 기다리는 중...");
            yield return null; // 다음 프레임까지 대기
        }

        Debug.Log("Firebase 초기화 완료. 데이터를 가져옵니다.");

        // 유저 닉네임 정보 가져오기
        string userId = PlayerPrefs.GetString("UserNickname", "DefaultUser");
        nickNameText.text = userId;

        // 검술 숙련도 랭킹 정보 가져오기
        Firebaseapi.GetAllUsersSortedBySwordProficiency((userList) =>
        {
            int userRank = -1; // 유저의 순위를 찾기 위한 변수
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].userId == userId)
                {
                    userRank = i + 1; // 1등부터 시작하므로 +1
                    break;
                }
            }

            // 랭킹이 없는 경우 기본값 처리
            if (userRank == -1)
            {
                levelText.text = "순위 없음";
            }
            else
            {
                // 랭크 그룹을 찾아서 표시
                levelText.text = GetRankGroup(userRank);
            }
        });

        // 검술숙련도 정보 가져오기
        Firebaseapi.GetMainRoomUserInfo(
            userId,
            onSuccess: (snapshot) =>
            {
                // 데이터를 가져온 후 처리
                object sword = snapshot.Child("SwordProficiency").Value ?? "오류";
                swordText.text = sword.ToString();
            },
            onError: (errorMessage) =>
            {
                Debug.LogError("에러 발생: " + errorMessage);
            }
        );
    }

    private string GetRankGroup(int rank)
    {
        if (rank == 1) return "근위기사단장";
        if (rank >= 2 && rank <= 11) return "근위기사단원";
        if (rank == 12) return "기사단장";
        if (rank >= 13 && rank <= 32) return "정규기사";
        return "기사후보생";
    }
}

