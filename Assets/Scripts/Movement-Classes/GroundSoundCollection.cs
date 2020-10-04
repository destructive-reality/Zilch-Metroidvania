using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New GroundSoundCollection", menuName = "Sound/Ground Sound Collection")]
public class GroundSoundCollection : ScriptableObject
{
    public List<AudioClip> walkSoundAudioClips;
}
