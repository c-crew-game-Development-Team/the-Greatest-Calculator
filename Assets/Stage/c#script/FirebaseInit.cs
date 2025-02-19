using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic; // Dictionary
using System.Linq;
using System;

public class FirebaseInitializer : MonoBehaviour
{
    private DatabaseReference databaseReference;
    public bool IsInitialized { get; private set; } = false; // Firebase 초기화 여부

    void Start()
    {
        // Firebase 초기화
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                InitializeFirebase();
                Debug.Log("Firebase 초기화 성공!");
                IsInitialized = true; // 초기화 완료 플래그 설정
            }
            else
            {
                Debug.LogError($"Firebase의 모든 의존성을 해결하지 못했습니다: {task.Result}");
            }
        });
    }

    void InitializeFirebase()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // 유저 가입 초기 정보 서버 전송 함수
    public void InitializeUserData(string userId)
    {
        // 초기 데이터 설정
        var userData = new Dictionary<string, object>
        {
            { "SwordProficiency", 0 },
            { "WorldRecovered", 0 },
            { "Coin", 0 },
            { "NumberBundle", 0 }
        };

        // Firebase에 데이터 전송
        databaseReference.Child("User").Child(userId).SetValueAsync(userData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("유저 초기 데이터가 성공적으로 저장되었습니다.");
            }
            else
            {
                Debug.LogError("유저 초기 데이터를 저장하는 데 실패했습니다: " + task.Exception);
            }
        });
    }

    // 닉네임(userId) 변경 함수
    public void UpdateNickname(string currentUserId, string newUserId)
    {
        // 기존 데이터를 새로운 userId로 복사
        databaseReference.Child("User").Child(currentUserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                // 새로운 userId에 데이터 복사
                databaseReference.Child("User").Child(newUserId).SetValueAsync(snapshot.Value).ContinueWithOnMainThread(copyTask =>
                {
                    if (copyTask.IsCompleted)
                    {
                        Debug.Log($"유저 데이터: {snapshot.GetRawJsonValue()}");
                        Debug.Log($"데이터가 새로운 userId {newUserId} 로 성공적으로 복사되었습니다.");

                        // 기존 데이터 삭제
                        databaseReference.Child("User").Child(currentUserId).RemoveValueAsync().ContinueWithOnMainThread(deleteTask =>
                        {
                            if (deleteTask.IsCompleted)
                            {
                                Debug.Log($"기존 {currentUserId} userId 데이터가 성공적으로 삭제되었습니다.");
                            }
                            else
                            {
                                Debug.LogError($"기존 {currentUserId} userId 데이터를 삭제하는 데 실패했습니다: " + deleteTask.Exception);
                            }
                        });
                    }
                    else
                    {
                        Debug.LogError("데이터를 새로운 userId로 복사하는 데 실패했습니다: " + copyTask.Exception);
                    }
                });
            }
            else
            {
                Debug.LogError("기존 데이터를 가져오는 데 실패했습니다: " + task.Exception);
            }
        });
    }

    // 유저 정보 가져오기 (콜백 방식)
    public void GetMainRoomUserInfo(string userId, System.Action<DataSnapshot> onSuccess, System.Action<string> onError)
    {
        if (databaseReference == null)
        {
            Debug.LogError("Firebase가 초기화되지 않았습니다. InitializeFirebase()를 확인하세요.");
            return;
        }
        databaseReference.Child("User").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    Debug.Log("데이터를 성공적으로 가져왔습니다.");
                    Debug.Log($"유저 데이터: {snapshot.GetRawJsonValue()}");

                    // 성공 콜백 호출
                    onSuccess?.Invoke(snapshot);
                }
                else
                {
                    Debug.LogWarning("데이터가 존재하지 않습니다.");
                    onError?.Invoke("데이터가 존재하지 않습니다.");
                }
            }
            else
            {
                Debug.LogError("유저 정보를 가져오는 데 실패했습니다: " + task.Exception);
                onError?.Invoke("유저 정보를 가져오는 데 실패했습니다: " + task.Exception.Message);
            }
        });
    }


    // 세계회복도 업데이트
    public void UpdateWorldRecovered(string userId, int worldRecovered)
    {
        databaseReference.Child("User").Child(userId).Child("WorldRecovered").SetValueAsync(worldRecovered).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("세계회복도가 성공적으로 업데이트되었습니다.");
            }
            else
            {
                Debug.LogError("세계회복도 업데이트 실패: " + task.Exception);
            }
        });
    }

    // 코인 값 업데이트
    public void UpdateCoin(string userId, int coin)
    {
        databaseReference.Child("User").Child(userId).Child("Coin").SetValueAsync(coin).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("코인이 성공적으로 업데이트되었습니다.");
            }
            else
            {
                Debug.LogError("코인 업데이트 실패: " + task.Exception);
            }
        });
    }

    // 숫자 뭉치 값 업데이트
    public void UpdateNumberBundle(string userId, int numberBundle)
    {
        databaseReference.Child("User").Child(userId).Child("NumberBundle").SetValueAsync(numberBundle).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("숫자 뭉치가 성공적으로 업데이트되었습니다.");
            }
            else
            {
                Debug.LogError("숫자 뭉치 업데이트 실패: " + task.Exception);
            }
        }); 
    }


    // 검술 숙련도 값 업데이트
    public void UpdateSwordProficiency(string userId, int additionalProficiency)
    {
        DatabaseReference swordProficiencyRef = databaseReference.Child("User").Child(userId).Child("SwordProficiency");

        swordProficiencyRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int currentProficiency = 0;

                if (snapshot.Exists && int.TryParse(snapshot.Value.ToString(), out int existingProficiency))
                {
                    currentProficiency = existingProficiency;
                }

                int newProficiency = currentProficiency + additionalProficiency;

                swordProficiencyRef.SetValueAsync(newProficiency).ContinueWithOnMainThread(updateTask =>
                {
                    if (updateTask.IsCompleted)
                    {
                        Debug.Log($"검술 숙련도가 성공적으로 업데이트되었습니다. (기존: {currentProficiency}, 추가: {additionalProficiency}, 최종: {newProficiency})");
                    }
                    else
                    {
                        Debug.LogError("검술 숙련도 업데이트 실패: " + updateTask.Exception);
                    }
                });
            }
            else
            {
                Debug.LogError("검술 숙련도 값을 가져오는 데 실패함: " + task.Exception);
            }
        });
    }

    // 랭킹용 전체 유저 숙련도 정렬
    public void GetAllUsersSortedBySwordProficiency(System.Action<List<UserData>> callback)
    {
        databaseReference.Child("User").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<UserData> userList = new List<UserData>();

                foreach (DataSnapshot user in snapshot.Children)
                {
                    string userId = user.Key;
                    string json = user.GetRawJsonValue();

                    if (!string.IsNullOrEmpty(json))
                    {
                        UserData userData = JsonUtility.FromJson<UserData>(json);
                        userData.userId = userId;
                        userList.Add(userData);

                        // 콘솔에 가져온 데이터 출력
                        //Debug.Log($"UserID: {userId}, SwordProficiency: {userData.SwordProficiency}");
                    }
                }

                // SwordProficiency 기준 내림차순 정렬
                userList = userList.OrderByDescending(u => u.SwordProficiency).ToList();

                // 정렬된 유저 리스트 출력
                //Debug.Log("==== Sorted User List ====");
                // foreach (var user in userList)
                // {
                //     Debug.Log($"Ranked UserID: {user.userId}, SwordProficiency: {user.SwordProficiency}");
                // }

                // 콜백 호출
                callback?.Invoke(userList);
            }
            else
            {
                Debug.LogError("Firebase 데이터 가져오기에 실패했습니다: " + task.Exception);
                callback?.Invoke(new List<UserData>()); // 빈 리스트 반환
            }
        });
    }

    [System.Serializable]
    public class UserData
    {
        public string userId;
        public int SwordProficiency;
    }
}