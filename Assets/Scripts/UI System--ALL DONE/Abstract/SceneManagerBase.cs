using UnityEngine;

public abstract class SceneManagerBase : MonoBehaviour
{
    public virtual void TogglePausePanel() { }

    public virtual void ToggleOptionsPanel() { }

    public virtual void ToggleMusic(UIActionButtonSO buttonData) { }

    public virtual void ToggleSound(UIActionButtonSO buttonData) { }
}
