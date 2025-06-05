using System.Collections;

public interface ICookableItem
{
    public CookingState CurrentState { get; set; }
    public void StartCook();
    public IEnumerator CookingProgress();
    public void OnCookComplete();
}
