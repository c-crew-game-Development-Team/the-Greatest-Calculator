using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingShowList : MonoBehaviour
{
    public Transform rankParent; // 순위를 표시할 공통 UI 부모 객체
    public GameObject rankFirstPrefab; // 근위기사단장 프리팹
    public GameObject rankSecondPrefab; // 근위기사단원 프리팹
    public GameObject rankThirdPrefab; // 기사단장 프리팹
    public GameObject rankFourthPrefab; // 정규기사 프리팹
    public GameObject rankFifthPrefab; // 기사후보생 프리팹

    public GameObject rankPrefab; // 순위 항목 Prefab

    public FirebaseInitializer firebaseAPI;

    private GameObject currentRankGroup; // 현재 추가된 그룹의 프리팹을 추적

    void Start()
    {
        StartCoroutine(WaitForFirebaseInitialization());
    }

    private IEnumerator WaitForFirebaseInitialization()
    {
        // Firebase 초기화가 완료될 때까지 대기
        while (!firebaseAPI.IsInitialized)
        {
            Debug.Log("Firebase 초기화를 기다리는 중...");
            yield return null; // 다음 프레임까지 대기
        }

        Debug.Log("Firebase 초기화 완료. 데이터를 가져옵니다.");

        // Firebase에서 데이터를 가져와 UI 생성
        firebaseAPI.GetAllUsersSortedBySwordProficiency((userList) =>
        {
            string previousRankGroup = null;

            for (int i = 0; i < userList.Count; i++)
            {
                int rank = i + 1; // 순위는 1부터 시작
                string userId = userList[i].userId;
                int proficiency = userList[i].SwordProficiency;

                // 현재 순위 그룹 가져오기
                string currentRankGroup = GetRankGroup(rank);

                // 그룹이 변경될 때 프리팹 추가
                if (currentRankGroup != previousRankGroup)
                {
                    AddRankGroup(currentRankGroup);
                    previousRankGroup = currentRankGroup;
                }

                // 순위 항목 생성
                CreateRankItem(userId, proficiency);
            }
        });
    }

    private string GetRankGroup(int rank)
    {
        if (rank == 1) return "근위기사단장";
        if (rank >= 2 && rank <= 11) return "근위기사단원";
        if (rank == 12) return "기사단장";
        if (rank >= 13 && rank <= 32) return "정규기사";
        return "기사후보생";
    }

    private void AddRankGroup(string rankGroup)
    {
        GameObject rankGroupPrefab = null;

        // 그룹에 맞는 프리팹 선택
        switch (rankGroup)
        {
            case "근위기사단장":
                rankGroupPrefab = rankFirstPrefab;
                break;
            case "근위기사단원":
                rankGroupPrefab = rankSecondPrefab;
                break;
            case "기사단장":
                rankGroupPrefab = rankThirdPrefab;
                break;
            case "정규기사":
                rankGroupPrefab = rankFourthPrefab;
                break;
            case "기사후보생":
                rankGroupPrefab = rankFifthPrefab;
                break;
        }

        // 프리팹을 rankParent의 자식으로 추가
        if (rankGroupPrefab != null)
        {
            currentRankGroup = Instantiate(rankGroupPrefab, rankParent);
        }
    }

    private void CreateRankItem(string userId, int proficiency)
{
    // rankItem을 rankParent 아래에 추가
    GameObject rankItem = Instantiate(rankPrefab, rankParent);
    
    // Debug 로그 출력 (값이 정상적으로 들어오는지 확인)
    Debug.Log($"유저 ID: {userId}, SwordProficiency: {proficiency}");

    // Text 컴포넌트가 존재하는지 확인한 후 설정
    TextMeshProUGUI userIdText = rankItem.transform.Find("userId")?.GetComponent<TextMeshProUGUI>();
    TextMeshProUGUI userScoreText = rankItem.transform.Find("userScore")?.GetComponent<TextMeshProUGUI>();

    if (userIdText != null)
    {
        userIdText.text = userId;
    }
    else
    {
        Debug.LogError("userIdText를 찾을 수 없습니다. Hierarchy에서 경로 확인 필요!");
    }

    if (userScoreText != null)
    {
        userScoreText.text = $"{proficiency}";
    }
    else
    {
        Debug.LogError("userScoreText를 찾을 수 없습니다. Hierarchy에서 경로 확인 필요!");
    }
}
}