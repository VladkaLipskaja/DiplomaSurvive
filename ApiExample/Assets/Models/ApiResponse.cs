using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ApiResponse<T>
{
    public T data;

    public string[] errors;

    public bool result;
}