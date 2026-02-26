using System.Collections.Generic;
using UnityEngine;

public struct DisplayItemContextSignal
{
    public List<Sprite> icons;

    public DisplayItemContextSignal(Sprite icon)
    {
        icons = new();
        icons.Add(icon);
    }

    public DisplayItemContextSignal(List<Sprite> icons)
    {
        this.icons = icons;
    }
}