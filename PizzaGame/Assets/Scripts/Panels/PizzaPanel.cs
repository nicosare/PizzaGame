using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PizzaPanel : MonoBehaviour
{
    [SerializeField] private Image pizzaIcon;
    [SerializeField] private TextMeshProUGUI pizzaName;
    [SerializeField] private IngredientPanel ingredientPanel;
    [SerializeField] private Transform ingredientsField;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Transform starsPanel;
    [SerializeField] private TextMeshProUGUI ratingText;
    [SerializeField] private float rating;
    [SerializeField] private GameObject ratingPanel;
    public float ActualCost;
    private Pizza pizza;
    private Order order;
    private int pizzaLevel;

    public void LoadData(Pizza pizza)
    {
        ActualCost = pizza.Cost;
        SetLevelPizza();
        ratingText.text = $"+{rating}";
        this.pizza = pizza;
        pizzaIcon.sprite = pizza.Icon;
        pizzaName.text = pizza.nameOfObject;
        cost.text = ActualCost.ToString();
        foreach (var ingredient in pizza.ingredients)
        {
            var newIngredientPanel = Instantiate(ingredientPanel, ingredientsField);
            newIngredientPanel.LoadData(ingredient);
        }
    }

    public void LoadCleanData(Pizza pizza)
    {
        ratingPanel.SetActive(false);
        starsPanel.gameObject.SetActive(false);
        this.pizza = pizza;
        pizzaIcon.sprite = pizza.Icon;
        pizzaName.text = pizza.nameOfObject;
        cost.text = pizza.Cost.ToString();
        foreach (var ingredient in pizza.ingredients)
        {
            var newIngredientPanel = Instantiate(ingredientPanel, ingredientsField);
            newIngredientPanel.LoadData(ingredient);
        }
        GetComponent<Button>().interactable = false;
    }

    private void SetLevelPizza()
    {
        switch (transform.GetSiblingIndex())
        {
            case 0:
                ActualCost *= 0.8f;
                pizzaLevel = 1;
                rating = 0;
                break;
            case 1:
                pizzaLevel = 2;
                break;
            case 2:
                ActualCost *= 1.2f;
                pizzaLevel = 3;
                rating *= 1.5f;
                break;
            default:
                break;
        }
        for (var i = 0; i < pizzaLevel; i++)
            starsPanel.GetChild(i).gameObject.SetActive(true);
    }

    public void TakeOrder()
    {
        var window = transform.parent.parent.GetComponent<PizzaWindow>();

        window.TakeOrder();
        order = new Order(pizza, window.ActionObjectCallBack as Customer, ActualCost, rating, pizzaLevel);
        OrderController.Instance.Orders.Add(order);

        WindowsController.Instance.CloseWindow(window);
    }
}
