using System.Collections;
using TMPro;
using UnityEngine;

public class RatingManager : MonoBehaviour
{
    public static RatingManager Instance;
    [SerializeField] private int rating;
    [SerializeField] private Sprite icon;
    private TextMeshProUGUI ratingText;

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

    public void Initialization()
    {
        StopAllCoroutines();
        ratingText = ShowItemManager.Instance.RatingText;
        ratingText.text = rating.ToString();
    }

    public void AddRating(int rating)
    {
        ShowItemManager.Instance.ShowTakeItem(icon, "Рейтинг", rating);
        this.rating += rating;
        StartCoroutine(Counting(rating, true));
    }

    public void TakeRating(int rating)
    {
        ShowItemManager.Instance.ShowGiveItem(icon, "Рейтинг", rating);
        this.rating -= rating;
        StartCoroutine(Counting(rating, false));
    }

    IEnumerator Counting(int rating, bool isIncrease)
    {
        for (; rating > 0; rating--)
        {
            if (isIncrease)
                ratingText.text = (int.Parse(ratingText.text) + 1).ToString();
            else
                ratingText.text = (int.Parse(ratingText.text) - 1).ToString();

            yield return new WaitForSeconds(0.001f);
        }
    }

    public int GetRatingValue()
    {
        return rating;
    }
}
