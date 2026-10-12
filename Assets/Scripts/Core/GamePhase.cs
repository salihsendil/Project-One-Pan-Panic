using UnityEngine;

public enum GamePhase
{
    [InspectorName("DebugOnly/None")]
    None = 0,

    [InspectorName("DebugOnly/Preparation")]
    Preparation,

    Countdown = 5,
    Play,

    [InspectorName("DebugOnly/Finish")]
    Finish,
}