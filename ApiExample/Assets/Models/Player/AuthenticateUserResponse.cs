using System;
using UnityEngine.Serialization;

[Serializable]
public class AuthenticateUserResponse
{
    public int scores;

    public int reward;

    public string token;
}