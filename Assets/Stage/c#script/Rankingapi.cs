using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;

public class Rankingapi : MonoBehaviour
{
    private Firebase.FirebaseApp app;

    private Rankingapi() {}

    private static Rankingapi _instance = null;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            var task = Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                Debug.Log(dependencyStatus);
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    app = Firebase.FirebaseApp.DefaultInstance;
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
            _instance = this;
        }
    }

    public static Rankingapi Instance
    {get{
        return _instance;
    }}

    // 랭킹등록
    public void SendScore(string userId, int score)
    {
        var reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("scores").Child(userId).SetRawJsonValueAsync(score.ToString());
    }

    public void UpdateScore(string userId, int delta)
    {
        var reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("scores").Child(userId).GetValueAsync().ContinueWithOnMainThread(task => {
            if(task.IsCompleted) {
                if(task.Result == null)
                {
                    SendScore(userId, delta);
                }
                else
                {
                    int newScore = int.Parse(task.Result.GetRawJsonValue()) + delta;
                    if(newScore < 0)
                        newScore = 0;
                    SendScore(userId, newScore);
                }
            }
        });
    }

    // 랭킹조회 OnRequest_Api_Ranking_Req
    public async Task<DataSnapshot> GetScore(string userId)
    {
        var reference = FirebaseDatabase.DefaultInstance.RootReference;
        var task = await reference.Child("scores").Child(userId).GetValueAsync();
        return task;
    }

    public async Task<DataSnapshot> GetTopScoreList(int n)
    {
        var reference = FirebaseDatabase.DefaultInstance.RootReference;
        var task = await reference.Child("scores").OrderByValue().LimitToLast(n).GetValueAsync();
        return task;
    }

    public object GetAllScoreList()
    {
        var reference = FirebaseDatabase.DefaultInstance.RootReference;
        var task = reference.Child("scores").OrderByValue().GetValueAsync();
        task.Wait();
        return task.Result.GetValue(true);
    }
}