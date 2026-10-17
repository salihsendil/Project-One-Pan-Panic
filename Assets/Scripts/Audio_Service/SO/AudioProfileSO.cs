using UnityEngine;

[CreateAssetMenu(fileName = "New AudioProfileSO", menuName = "Scriptable Objects/Audio/New Audio Profile SO")]
public class AudioProfileSO : ScriptableObject
{
    public AudioClip Music;
    public AudioClip Ambience;
}