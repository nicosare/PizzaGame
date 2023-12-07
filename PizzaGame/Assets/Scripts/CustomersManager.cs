using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    public static CustomersManager Instance;
    [SerializeField] private Moving customer;
    [SerializeField] Transform orderPlace;
    [SerializeField] List<Transform> sittingPlaces;
    [SerializeField] List<Transform> quitPlaces;
    public TextAsset CustomerNames;
    public List<Moving> Customers;
    public List<Moving> WaitingCustomers;
    private List<Transform> freeSittingPlaces;
    public float WaitInQueueScale;

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
        for (int i = 0; i < 7; i++)
        {
            var newCustomer = Instantiate(customer, transform);
            newCustomer.GetComponent<Customer>().WaitingQueueTime *= WaitInQueueScale;
            Customers.Add(newCustomer);
            yield return new WaitForSeconds(1);
        }
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
