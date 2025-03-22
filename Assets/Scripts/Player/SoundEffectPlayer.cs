using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioSource Src;
    public AudioClip SfxBouffe;

    public void Bouffing()
    {
        Src.clip = SfxBouffe;
        Src.Play();
    }

}
