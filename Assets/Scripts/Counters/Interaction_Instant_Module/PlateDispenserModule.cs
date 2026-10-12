using System.Collections.Generic;
using UnityEngine;


// This state module manages whether plates are placed or removed.
// A queue structure is used.
//
// Plates appear to be stacked up in the scene.
// When a new plate is placed, it is enqueued into the queue.
// All plates already on top of the counter are moved up slightly,
// and the newly placed plate is added to the bottom of the stack.
//
// When a plate is removed, the module detects it through the
// IInteractor OnItemRemoved event.
// It dequeues the removed plate, then tries to peek at the next plate in the queue
// and sets the peeked plate to the IInteractor.
// If the queue is empty, the IInteractor stays empty.


public class PlateDispenserModule : MonoBehaviour, IStateModule
{
    private IInteractor currentInteractor;

    private Queue<Plate> plateQueue = new();
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Vector3 positionSetOffset = new(0f, 0.1f, 0f);


    public void Initialize(IInteractor interactor)
    {
        currentInteractor = interactor;
        currentInteractor.OnItemPlaced += ItemPlaced;
        currentInteractor.OnItemRemoved += ItemRemoved;

        Debug.Log("it is came as param: " + interactor.ToString() + "plate dispenser module initialize");
    }

    public void Dispose()
    {
        currentInteractor.OnItemPlaced -= ItemPlaced;
        currentInteractor.OnItemRemoved -= ItemRemoved;
        Debug.Log("it is gonna go as param: " + currentInteractor.ToString() + "plate dispenser module dispose");
        currentInteractor = null;
    }

    private void ItemPlaced(IPickable pickable)
    {
        Plate plate = pickable as Plate;
        plateQueue.Enqueue(plate);

        Vector3 offset = plateQueue.Count * positionSetOffset;
        foreach (var p in plateQueue)
        {
            Debug.Log(p.gameObject.name);
            p.transform.position = holdPoint.position + offset;
            offset -= positionSetOffset;
        }

        currentInteractor.CurrentItem = plateQueue.Peek();
    }

    private void ItemRemoved(IPickable pickable)
    {
        plateQueue.Dequeue();

        if (plateQueue.Count > 0)
            currentInteractor.CurrentItem = plateQueue.Peek();
    }
}