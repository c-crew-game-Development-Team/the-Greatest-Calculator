using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic; // Dictionary

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

    // 메인룸 유저 정보 가져오기 (콜백 방식)
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
        databaseReference.Child("Users").Child(userId).Child("WorldRecovered").SetValueAsync(worldRecovered).ContinueWithOnMainThread(task =>
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
        databaseReference.Child("Users").Child(userId).Child("Coin").SetValueAsync(coin).ContinueWithOnMainThread(task =>
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
        databaseReference.Child("Users").Child(userId).Child("NumberBundle").SetValueAsync(numberBundle).ContinueWithOnMainThread(task =>
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
    public void UpdateSwordProficiency(string userId, int swordProficiency)
    {
        databaseReference.Child("Users").Child(userId).Child("SwordProficiency").SetValueAsync(swordProficiency).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("검술 숙련도가 성공적으로 업데이트되었습니다.");
            }
            else
            {
                Debug.LogError("검술 숙련도 업데이트 실패: " + task.Exception);
            }
        });
    }

    // 전체 유저 정보 가져오기
    public void GetAllUsers()
    {
        databaseReference.Child("Users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    Debug.Log($"유저: {user.Key}, 데이터: {user.GetRawJsonValue()}");
                }
            }
            else
            {
                Debug.LogError("모든 유저 정보를 가져오는 데 실패했습니다: " + task.Exception);
            }
        });
    }
}