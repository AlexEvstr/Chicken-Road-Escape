using UnityEngine;

public class GameSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _soundController;
    [SerializeField] private AudioSource _musicController;

    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _win;
    [SerializeField] private AudioClip _lose;
    [SerializeField] private AudioClip _bomb;
    [SerializeField] private AudioClip _resize;
    [SerializeField] private AudioClip _spikes;
    private float _vibration;

    private void Start()
    {
        Vibration.Init();
        _soundController.volume = PlayerPrefs.GetFloat("SoundVolume", 1);
        _musicController.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        _vibration = PlayerPrefs.GetFloat("VibroStatus", 1);
    }

    public void PlayClickSound()
    {
        _soundController.PlayOneShot(_click);
        if (_vibration == 1) Vibration.VibratePop();
    }

    public void PlayWinSound()
    {
        _soundController.PlayOneShot(_win);
        if (_vibration == 1) Vibration.Vibrate();
    }

    public void PlayLoseSound()
    {
        _soundController.PlayOneShot(_lose);
        if (_vibration == 1) Vibration.Vibrate();
    }

    public void PlayBombSound()
    {
        _soundController.PlayOneShot(_bomb);
        if (_vibration == 1) Vibration.VibrateNope();
    }

    public void PlayResizeSound()
    {
        _soundController.PlayOneShot(_resize);
        if (_vibration == 1) Vibration.VibratePop();
    }

    public void PlaySpikesSound()
    {
        _soundController.PlayOneShot(_spikes);
        if (_vibration == 1) Vibration.VibratePeek();
    }

    public void StopAllMusic()
    {
        _musicController.Stop();
    }
}