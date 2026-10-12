

// It represents the current stage of ingredient items.
// The variables update some states: at the start, after processing, and after over-processing.


public enum PreparationStage
{
    Raw = 0,

    Chopped = 10,

    PanCook = 20,
    Boiled = 30,
    Fried = 40,

    Ready = 90,

    Burnt = 100
}