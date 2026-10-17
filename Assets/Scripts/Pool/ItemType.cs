using UnityEngine;

public enum ItemType
{
    NotSpecified,

    [InspectorName("Breads/Burger Bread")]
    BurgerBread = 10,
    [InspectorName("Breads/Toast Bread")]
    ToastBread,

    [InspectorName("Ingredients/Tomato")]
    Tomato = 20,
    [InspectorName("Ingredients/Lettuce")]
    Lettuce,
    [InspectorName("Ingredients/Cheese")]
    Cheese,
    [InspectorName("Ingredients/Burger Meat")]
    BurgerMeat,
    [InspectorName("Ingredients/Mushroom")]
    Mushroom,
    [InspectorName("Ingredients/Onion")]
    Onion,
    [InspectorName("Ingredients/Shrimp")]
    Shrimp,
    [InspectorName("Ingredients/Egg")]
    Egg,
    [InspectorName("Ingredients/Bacon")]
    Bacon,

    [InspectorName("Containers/Plate")]
    Plate = 50,
    [InspectorName("Containers/Pan")]
    Pan,
    [InspectorName("Containers/Pot")]
    Pot
}
