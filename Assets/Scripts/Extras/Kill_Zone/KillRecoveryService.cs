using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class KillRecoveryService : MonoBehaviour
{
    [Inject] private AdvancedPoolManager poolManager;

    private HashSet<ItemSocket> stoveCountersSockets = new();
    [SerializeField] private PlayerRespawner playerRespawner;

    private void Awake()
    {
        StoveCounter[] stoves = FindObjectsByType<StoveCounter>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var stove in stoves)
        {
            if (!stove.TryGetComponent(out ItemSocket itemSocket)) continue;

            stoveCountersSockets.Add(itemSocket);
        }
    }

    public void Recover(PlayerController player)
    {
        player.gameObject.SetActive(false);

        if (player.TryGetComponent(out ItemSocket itemSocket) && itemSocket.HasItem)
        {
            IPickable pickable = itemSocket.RemoveItem();
            pickable.Transform.gameObject.SetActive(false);

            if (pickable.Transform.TryGetComponent(out IPoolable poolable))
            {
                poolManager.Despawn(poolable);

                if (poolable is CookwareItem)
                {
                    CookwareItem cookwareItem = poolManager.Spawn<CookwareItem>(poolable.ItemType);

                    foreach (var socket in stoveCountersSockets)
                    {
                        if (!socket.HasItem)
                        {
                            socket.SetItem(cookwareItem);
                            break;
                        }
                    }
                }
            }

            else
                Destroy(pickable.Transform.gameObject);
        }

        playerRespawner.Respawn(player);
    }
}