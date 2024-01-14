using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    public static CustomersManager Instance;
    [SerializeField] private Moving customer;
    [SerializeField] Transform orderPlace;
    [SerializeField] List<Transform> sittingPlaces;
    [SerializeField] List<Transform> quitPlaces;
    [SerializeField] private int startDayHour;
    [SerializeField] private int endDayHour;
    [SerializeField] private int minTimeBetweenCustomersLeftBorder;
    [SerializeField] private int minTimeBetweenCustomersRightBorder;
    [SerializeField] private int maxTimeBetweenCustomersLeftBorder;
    [SerializeField] private int maxTimeBetweenCustomersRightBorder;

    public TextAsset CustomerNames;
    public List<Moving> Customers;
    public List<Moving> WaitingCustomers;
    private List<Transform> freeSittingPlaces;
    public float WaitInQueueScale;
    public bool IsDayStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        freeSittingPlaces = new List<Transform>();
        foreach (var place in sittingPlaces)
            freeSittingPlaces.Add(place);
        Customers = new List<Moving>();
        WaitingCustomers = new List<Moving>();
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        yield return new WaitUntil(() => WeatherControl.Instance.Hour == startDayHour || IsDayStarted);
        IsDayStarted = true;
        yield return new WaitForSeconds(5);
        while (WeatherControl.Instance.Hour < endDayHour)
        {
            var countCustomers = GetCountCustomers();

            for (int i = 0; i < countCustomers; i++)
            {
                var newCustomer = Instantiate(customer, transform);
                newCustomer.GetComponent<Customer>().WaitingQueueTime *= WaitInQueueScale;
                Customers.Add(newCustomer);
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(Random.Range(
                GetTimeBetweenCustomersByRating(minTimeBetweenCustomersLeftBorder, maxTimeBetweenCustomersRightBorder),
                GetTimeBetweenCustomersByRating(maxTimeBetweenCustomersLeftBorder, maxTimeBetweenCustomersRightBorder)));
        }
    }

    private int GetCountCustomers()
    {
        var rnd = Random.Range(1, 101);
        var countCustomers = 0;
        if (rnd <= 80)
            countCustomers = 1;
        else if (rnd <= 95)
            countCustomers = 2;
        else
            countCustomers = 3;
        return countCustomers;
    }

    private float GetTimeBetweenCustomersByRating(float minTime, float maxTime)
    {
        var maxRating = 1000;
        var rating = RatingManager.Instance.GetRatingValue();
        var normalizedRating = rating > maxRating ? 1 : rating / maxRating;
        return Mathf.Lerp(maxTime, minTime, maxRating);
    }

    public Vector3 GetSittingPlacePosition()
    {
        MoveQueue();
        var place = freeSittingPlaces.Shuffle().First();
        freeSittingPlaces.Remove(place);
        return place.position;
    }

    public Vector3 GetOrderPlacePosition(Moving customer)
    {
        return orderPlace.position - Vector3.right * 2 * (WaitingCustomers.IndexOf(customer) + 1);
    }


    public Vector3 GetQuitPosition()
    {
        MoveQueue();
        return quitPlaces.Shuffle().First().position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
    }

    private void MoveQueue()
    {
        foreach (var customer in WaitingCustomers)
            customer.MoveTo(GetOrderPlacePosition(customer));
    }
}
