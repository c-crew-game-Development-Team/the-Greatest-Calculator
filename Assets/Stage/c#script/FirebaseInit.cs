using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseInitializer : MonoBehaviour
{

    DatabaseReference reference;
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase Initialized");
            }
            else
            {
                Debug.LogError($"Could not resolve Firebase dependencies: {task.Result}");
            }
        });

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        
        reference.Child("User").Child("정람지").Child("Rank").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted) {
                Debug.LogError("Failed");
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists) {
                    string rank = snapshot.Value.ToString();
                    Debug.Log("Rank:" + rank);
                }
            }
        });

    }
}
