using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject _onSound;
    [SerializeField] private GameObject _offSound;
    [SerializeField] private GameObject _onMusic;
    [SerializeField] private GameObject _offMusic;
    [SerializeField] private GameObject _onVibro;
    [SerializeField] private GameObject _offVibro;

    [SerializeField] private AudioSource _soundController;
    [SerializeField] private AudioSource _musicController;

    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _buySound;
    [SerializeField] private AudioClip _failSound;

    private float _vibration;

    private void Start()
    {
        Vibration.Init();

        _soundController.volume = PlayerPrefs.GetFloat("SoundVolume", 1);
        if (_soundController.volume == 1)
        {
            _offSound.SetActive(true);
            _onSound.SetActive(false);
        }
        else
        {
            _onSound.SetActive(true);
            _offSound.SetActive(false);
        }

        _musicController.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        if (_musicController.volume == 1)
        {
            _offMusic.SetActive(true);
            _onMusic.SetActive(false);
        }
        else
        {
            _onMusic.SetActive(true);
            _offMusic.SetActive(false);
        }

        _vibration = PlayerPrefs.GetFloat("VibroStatus", 1);
    }

    public void DisableSound()
    {
        _onSound.SetActive(true);
        _offSound.SetActive(false);
        _soundController.volume = 0;
        PlayerPrefs.SetFloat("SoundVolume", 0);
    }

    public void EnableSound()
    {
        _offSound.SetActive(true);
        _onSound.SetActive(false);
        _soundController.volume = 1;
        PlayerPrefs.SetFloat("SoundVolume", 1);
    }

    public void DisableMusic()
    {
        _onMusic.SetActive(true);
        _offMusic.SetActive(false);
        _musicController.volume = 0;
        PlayerPrefs.SetFloat("MusicVolume", 0);
    }

    public void EnableMusic()
    {
        _offMusic.SetActive(true);
        _onMusic.SetActive(false);
        _musicController.volume = 1;
        PlayerPrefs.SetFloat("MusicVolume", 1);
    }

    public void DisableVibro()
    {
        _onVibro.SetActive(true);
        _offVibro.SetActive(false);
        PlayerPrefs.SetFloat("VibroStatus", 0);
    }

    public void EnableVibro()
    {
        _offVibro.SetActive(true);
        _onVibro.SetActive(false);
        PlayerPrefs.SetFloat("VibroStatus", 1);
    }

    public void PlayClickSound()
    {
        if (_vibration == 1) Vibration.VibratePop();
        _soundController.PlayOneShot(_clickSound);
    }

    public void PlayBuySound()
    {
        if (_vibration == 1) Vibration.VibratePeek();
        _soundController.PlayOneShot(_buySound);
    }

    public void PlayFailSound()
    {
        if (_vibration == 1) Vibration.VibrateNope();
        _soundController.PlayOneShot(_failSound);
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("MenuScene");
    }
}