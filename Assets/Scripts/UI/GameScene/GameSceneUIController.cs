using System;
using UnityEngine;
using Zenject;

public class GameSceneUIController : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;
}
