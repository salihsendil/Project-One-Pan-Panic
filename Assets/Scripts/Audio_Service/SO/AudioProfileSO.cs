using UnityEngine;

[CreateAssetMenu(fileName = "New AudioProfileSO", menuName = "Scriptable Objects/New Audio Profile SO")]
public class AudioProfileSO : ScriptableObject
{
    public AudioClip Music;
    public AudioClip Ambience;
}