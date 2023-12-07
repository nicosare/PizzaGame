using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public partial class Customer : ActionObject
{
    [SerializeField] private TimerCircle timer;
    [SerializeField] private ActionButtonCanvas takeOrderButton;
    [SerializeField] private Sprite cancelOrderIcon;
    [SerializeField] private Transform customerUI;
    [SerializeField] private int minusQuitRating;
    public float WaitingQueueTime;
    public TextMeshProUGUI CustomerName;
    private CustomersManager customersManager;
    private Moving moving;
    private NavMeshObstacle meshObstacle;
    private NavMeshAgent meshAgent;
    private CustomerStage currentStage;
    private List<StageAction> actions;
    private ActionButtonCanvas actionButton;
    private delegate void StageAction();

    public CustomerStage CurrentStage { get => currentStage; set => currentStage = SetStage(); }

    public override Type typeOfNeededItem => throw new NotImplementedException();

    private void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        meshObstacle = GetComponent<NavMeshObstacle>();
        meshObstacle.enabled = false;
        window = WindowsController.Instance.PizzaWindow;
        actions = new List<StageAction>
        {
            GoToQueue,
            WaitOrderAcceptance,
            Quit,
            Destroy,
            GoToSittingPlace,
            WaitOrder
        };
        timer.gameObject.SetActive(false);
        takeOrderButton.gameObject.SetActive(false);
        moving = GetComponent<Moving>();
        customersManager = transform.parent.GetComponent<CustomersManager>();
        CustomerName.text = customersManager.CustomerNames.GetRandomName();
    }

    private void Start()
    {
        CurrentStage = CustomerStage.GoToQueue;
        GetDesiredPizza();
    }

    private void Update()
    {
        customerUI.eulerAngles = new Vector3(0, 0, 0);
        if (currentStage == CustomerStage.WaitOrderAcceptance)
        {
            if (OrderController.Instance.Orders.Count >= OrderController.Instance.MaxAmountOfOrders)
                takeOrderButton.gameObject.SetActive(false);
            else
                takeOrderButton.gameObject.SetActive(true);
        }

        if (actionButton != null
            && OrderController.Instance.FirstActiveOrder != null
            && OrderController.Instance.FirstActiveOrder.Customer == this
            || OrderController.Instance.SecondActiveOrder != null
            && OrderController.Instance.SecondActiveOrder.Customer == this)
            actionButton.gameObject.SetActive(false);
    }

    private void GetDesiredPizza()
    {
        var availableMenu = Menu.Instance.AvailablePizzas.Shuffle();

        for (var i = 0; i < 3; i++)
        {
            Pizzas.Add(availableMenu[i]);
            if (availableMenu.Count < 3)
                break;
        }
    }

    private void GoToQueue()
    {
        customersManager.WaitingCustomers.Add(GetComponent<Moving>());
        moving.MoveTo(customersManager.GetOrderPlacePosition(GetComponent<Moving>()));
        StartCoroutine(WaitIsCome());
    }

    private void WaitOrderAcceptance()
    {
        timer.MaxTime = WaitingQueueTime;
        timer.gameObject.SetActive(true);
        takeOrderButton.gameObject.SetActive(true);
    }
    private void GoToSittingPlace()
    {
        takeOrderButton.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        customersManager.WaitingCustomers.Remove(GetComponent<Moving>());
        moving.MoveTo(customersManager.GetSittingPlacePosition());
        StartCoroutine(WaitIsCome());
    }

    private void WaitOrder()
    {
        OpenButton(spawnPosition, cancelOrderIcon);
        actionButton = GetComponentInChildren<ActionButtonCanvas>();
        meshAgent.enabled = false;
        meshObstacle.enabled = true;
    }

    private void Quit()
    {
        meshObstacle.enabled = false;
        meshAgent.enabled = true;
        timer.gameObject.SetActive(false);
        takeOrderButton.gameObject.SetActive(false);
        if (customersManager.WaitingCustomers.Contains(GetComponent<Moving>()))
        {
            RatingManager.Instance.TakeRating(minusQuitRating);
            customersManager.WaitingCustomers.Remove(GetComponent<Moving>());
        }
        moving.MoveTo(customersManager.GetQuitPosition());
        StartCoroutine(WaitIsCome());
    }

    private void Destroy()
    {
        customersManager.Customers.Remove(GetComponent<Moving>());
        Destroy(gameObject);
    }

    private IEnumerator WaitIsCome()
    {
        yield return new WaitUntil(() => moving.IsCome);
        NextStage();
    }

    private void NextStage()
    {
        if (currentStage < CustomerStage.WaitOrder)
            CurrentStage = currentStage++;
    }
    public override void DoAfterTimer()
    {
        NextStage();
    }

    private CustomerStage SetStage()
    {
        actions[(int)currentStage].Invoke();
        return CurrentStage;
    }

    public void GetOrder()
    {
        timer.StopTimer();
        WindowsController.Instance.OpenWindow(window, this);
    }

    public override void Interact()
    {
        if (actionButton != null)
            actionButton.gameObject.SetActive(false);
        CurrentStage = currentStage = CustomerStage.Quit;
        var order = OrderController.Instance.Orders.Find(order => order.Customer = this);
        RatingManager.Instance.TakeRating(minusQuitRating);
        if (order != null)
            OrderController.Instance.Orders.Remove(order);
    }


    public override void StartAction()
    {
        if (currentStage == CustomerStage.WaitOrderAcceptance)
            CurrentStage = currentStage = CustomerStage.GoToSittingPlace;
        else if (currentStage == CustomerStage.WaitOrder)
        {
            CurrentStage = currentStage = CustomerStage.Quit;
            if (OrderController.Instance.FirstActiveOrder != null)
            {
                if (OrderController.Instance.SecondActiveOrder == null)
                {
                    MoneyManager.Instance.AddMoney((int)OrderController.Instance.FirstActiveOrder.Cost);
                    RatingManager.Instance.AddRating((int)OrderController.Instance.FirstActiveOrder.Rating);
                    OrderController.Instance.FirstActiveOrder = null;
                }
                else
                {
                    OrderController.Instance.SecondActiveOrder = null;
                    MoneyManager.Instance.AddMoney((int)OrderController.Instance.SecondActiveOrder.Cost);
                    RatingManager.Instance.AddRating((int)OrderController.Instance.SecondActiveOrder.Rating);
                }
            }
        }
    }

    public override void CancelAction()
    {
        takeOrderButton.gameObject.SetActive(true);
        timer.ContinueTimer();
    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }
}