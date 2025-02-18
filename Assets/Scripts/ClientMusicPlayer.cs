using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class ClientMusicPlayer : Singleton<ClientMusicPlayer>
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip nomAudio;

    public override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayNomSound()
    {
        _audioSource.clip = nomAudio;
        _audioSource.Play();
    }
}
