using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public class Requests : MonoBehaviour
{
    private readonly string token;

    public Requests()
    {
        token =
            "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIyIiwibmJmIjoxNTc2Njg3NjY3LCJleHAiOjE1NzcyOTI0NjcsImlhdCI6MTU3NjY4NzY2N30.5k8tGmCohSvp_A9jbBbChzSnHDdynwWWVVHIvtmZpAc";
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetEvent());
        StartCoroutine(GetLeaderboard());
        StartCoroutine(SetUserScores());
        StartCoroutine(SignIn());
        StartCoroutine(Authenticate());
    }

    IEnumerator GetEvent()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(String.Format("http://194.32.79.63:8000/api/event")))
        {
            req.SetRequestHeader("Authorization", token);
            yield return req.Send();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<GetEventResponse> info = JsonUtility.FromJson<ApiResponse<GetEventResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");
            if (info.errors.Length == 0)
            {
                GetEventResponse evn = info.data;
                Debug.Log($"ID: {evn.id} Title: {evn.title} Start: {evn.start} Finish: {evn.finish}");
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
        }
    }

    IEnumerator GetLeaderboard()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(String.Format("http://194.32.79.63:8000/api/leaderboard")))
        {
            req.SetRequestHeader("Authorization", token);
            yield return req.Send();

            while (!req.isDone)
                yield return null;

            byte[] result = req.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<GetLeaderboardResponse> info =
                JsonUtility.FromJson<ApiResponse<GetLeaderboardResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");

            if (info.errors.Length == 0)
            {
                GetLeaderboardResponse evn = info.data;
                Debug.Log($"Place: {evn.place} PlayersCount: {evn.players.Length}");
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
        }
    }

    IEnumerator SetUserScores()
    {
        var body = new SetUserScoresRequest
        {
            scores = 20
        };

        using (UnityWebRequest req =
            UnityWebRequest.Put("http://194.32.79.63:8000/api/Player/scores", JsonUtility.ToJson(body)))
        {
            req.SetRequestHeader("Authorization", token);
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.Send();

            while (!req.isDone)
                yield return null;

            byte[] result = req.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<SetUserScoresResponse> info =
                JsonUtility.FromJson<ApiResponse<SetUserScoresResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");

            if (info.errors.Length == 0)
            {
                SetUserScoresResponse evn = info.data;
                Debug.Log($"Scores: {evn.scores}");
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
        }
    }

    IEnumerator SignIn()
    {
        var body = new SignInUserRequest
        {
            name = "wonderfulUser"
        };

        var json = JsonUtility.ToJson(body);

        using (var request = new UnityWebRequest("http://194.32.79.63:8000/api/Player/sign-in", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.Send();

            while (!request.isDone)
                yield return null;

            byte[] result = request.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<SignInUserResponse> info = JsonUtility.FromJson<ApiResponse<SignInUserResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");

            if (info.errors.Length == 0)
            {
                SignInUserResponse evn = info.data;
                Debug.Log($"Scores: {evn.scores} Reward: {evn.reward} Token: {evn.token}");
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
        }
    }
    
    IEnumerator Authenticate()
    {
        var body = new AuthenticateUserRequest()
        {
            name = "wonderfulUser"
        };

        var json = JsonUtility.ToJson(body);

        using (var request = new UnityWebRequest("http://194.32.79.63:8000/api/Player/authentication", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.Send();

            while (!request.isDone)
                yield return null;

            byte[] result = request.downloadHandler.data;
            string eventJSON = System.Text.Encoding.Default.GetString(result);
            Debug.Log(eventJSON);

            ApiResponse<AuthenticateUserResponse> info = JsonUtility.FromJson<ApiResponse<AuthenticateUserResponse>>(eventJSON);
            Debug.Log($"Data: {info.data} Result: {info.result} Errors: {info.errors}");

            if (info.errors.Length == 0)
            {
                AuthenticateUserResponse evn = info.data;
                Debug.Log($"Scores: {evn.scores} Reward: {evn.reward} Token: {evn.token}");
            }
            else
            {
                Debug.Log($"Errors: {info.errors[0]}");
            }
        }
    }
}