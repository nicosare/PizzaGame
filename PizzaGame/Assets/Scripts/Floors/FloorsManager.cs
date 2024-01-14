using System.Collections.Generic;
using UnityEngine;

public class FloorsManager : MonoBehaviour
{
    public static FloorsManager Instance;
    public static Dictionary<int, int> FloorLevels;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            FloorLevels = new Dictionary<int, int>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
