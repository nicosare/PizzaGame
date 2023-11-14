using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    [SerializeField] private Moving customer;
    [SerializeField] Transform orderPlace;

    void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        for (int i = 0; i < 5; i++)
        {
            var newCustomer = Instantiate(customer, transform);
            newCustomer.MoveTo(GetOrderPlacePosition());
            yield return new WaitForSeconds(1);
        }
    }

    private Vector3 GetOrderPlacePosition()
    {
        return orderPlace.position - Vector3.right * 2 * transform.childCount;
    }
}
