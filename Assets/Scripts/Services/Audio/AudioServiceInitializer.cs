using Zenject;

public class AudioServiceInitializer : IInitializable
{
    [Inject] private AudioProfileSO audioProfileSO;
    [Inject] private AudioService audioService;

    public void Initialize()
    {
        audioService.SetProfileData(audioProfileSO);
    }
}
