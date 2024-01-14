using UnityEngine;

public class ScrollViewContentAdd : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject scrollRect;

    public void CreatePlace()
    {
        var newPrefab = Instantiate(prefab, transform);
    }
}
