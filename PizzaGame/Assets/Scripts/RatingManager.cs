using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingManager : MonoBehaviour
{
    public static RatingManager Instance;
    private int rating;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddRating(int rating)
    {
        this.rating += rating;
    }

    public int GetRatingValue()
    {
        return rating;
    }
}
