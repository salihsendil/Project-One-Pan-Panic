using System.Collections;

public interface ICuttableItem 
{
    public CuttingState CurrentState { get; set; }
    public void StartCut();
    public IEnumerator CuttingProgress();
    public void OnCutComplete();
}
