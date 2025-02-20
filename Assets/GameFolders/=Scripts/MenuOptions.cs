using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject _soundON;
    [SerializeField] private GameObject _soundOFF;
    [SerializeField] private GameObject _musicON;
    [SerializeField] private GameObject _musicOFF;
    [SerializeField] private GameObject _vibroON;
    [SerializeField] private GameObject _vibroOFF;

    [SerializeField] private AudioSource _soundManager;
    [SerializeField] private AudioSource _musicManager;

    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioClip _buy;
    [SerializeField] private AudioClip _fail;

    private float _vibration;

    private void Start()
    {
        Vibration.Init();

        _soundManager.volume = PlayerPrefs.GetFloat("SoundVolume", 1);
        if (_soundManager.volume != 1)
        {
            _soundOFF.SetActive(true);
            _soundON.SetActive(false);
        }
        else
        {
            _soundON.SetActive(true);
            _soundOFF.SetActive(false);
        }

        _musicManager.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
        if (_musicManager.volume != 1)
        {
            _musicOFF.SetActive(true);
            _musicON.SetActive(false);
        }
        else
        {
            _musicON.SetActive(true);
            _musicOFF.SetActive(false);
        }

        _vibration = PlayerPrefs.GetFloat("VibroStatus", 1);

        if (_vibration != 1)
        {
            _vibroOFF.SetActive(true);
            _vibroON.SetActive(false);
        }
        else
        {
            _vibroON.SetActive(true);
            _vibroOFF.SetActive(false);
        }
    }

    public void DisableSound()
    {
        _soundON.SetActive(false);
        _soundOFF.SetActive(true);
        _soundManager.volume = 0;
        PlayerPrefs.SetFloat("SoundVolume", 0);
    }

    public void EnableSound()
    {
        _soundOFF.SetActive(false);
        _soundON.SetActive(true);
        _soundManager.volume = 1;
        PlayerPrefs.SetFloat("SoundVolume", 1);
    }

    public void DisableMusic()
    {
        _musicON.SetActive(false);
        _musicOFF.SetActive(true);
        _musicManager.volume = 0;
        PlayerPrefs.SetFloat("MusicVolume", 0);
    }

    public void EnableMusic()
    {
        _musicOFF.SetActive(false);
        _musicON.SetActive(true);
        _musicManager.volume = 1;
        PlayerPrefs.SetFloat("MusicVolume", 1);
    }

    public void DisableVibro()
    {
        _vibroON.SetActive(false);
        _vibroOFF.SetActive(true);
        PlayerPrefs.SetFloat("VibroStatus", 0);
    }

    public void EnableVibro()
    {
        _vibroOFF.SetActive(false);
        _vibroON.SetActive(true);
        PlayerPrefs.SetFloat("VibroStatus", 1);
    }

    public void PlayClickSound()
    {
        if (_vibration == 1) Vibration.VibratePop();
        _soundManager.PlayOneShot(_click);
    }

    public void PlayBuySound()
    {
        if (_vibration == 1) Vibration.VibratePeek();
        _soundManager.PlayOneShot(_buy);
    }

    public void PlayFailSound()
    {
        if (_vibration == 1) Vibration.VibrateNope();
        _soundManager.PlayOneShot(_fail);
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("MenuScene");
    }
}