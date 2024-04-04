using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }
    public static bool dataRetrieved { get; private set; } = false;
    public static UnityEvent userDataRetrieved = new UnityEvent();
    private readonly string leaderboardID = "vermin_leaderboard";

    // Start is called before the first frame update
    void Start()
    {
        // Set up singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        userDataRetrieved.AddListener(() => { dataRetrieved = true; });
        StartLootLockerSession();
    }

    private void StartLootLockerSession()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");
                return;
            }

            var playerId = response.player_identifier;
            DataManager.Instance.SetId(playerId);
            LootLockerSDKManager.GetPlayerName((response) =>
            {
                if (!response.success)
                {
                    return;
                }

                DataManager.Instance.SetName(response.name);
                userDataRetrieved.Invoke();
                Debug.Log("successfully started LootLocker session");
            });
        });
    }

    public void SubmitLootLockerScore(int score)
    {
        LootLockerSDKManager.SubmitScore(DataManager.Instance.GetId(), score, leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
            }
        });
    }

    public void SetUserName(string username, Action<string> callback)
    {
        LootLockerSDKManager.SetPlayerName(username, (response) =>
        {
            if (!response.success)
            {
                callback(null);
                return;
            }

            DataManager.Instance.SetName(response.name);
            callback(response.name);
            Debug.Log("successfully started LootLocker session");
        });
    }

    public void GetHighScores(int count, Action<LootLockerLeaderboardMember[]> callback)
    {
        LootLockerSDKManager.GetScoreList(leaderboardID, count, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
                callback(response.items);
            }
        });
    }
}
