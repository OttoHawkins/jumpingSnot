using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject infoPanel;
    public GameObject mainMenuPanel;
    public Toggle soundToggle;
    public Slider volumeSlider;

    public AudioClip buttonClickSound; // ���� ������
    private AudioSource audioSource;    // ������ �� AudioSource � ������ �������

    public GameObject soundManager;    // ����� ������ ��� ���������� ������

    private void Awake()
    {
        // �������� ������ �� AudioSource � ������� SoundManager
        audioSource = soundManager.GetComponent<AudioSource>();
    }

    void Start()
    {
        settingsPanel.SetActive(false);
        infoPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        // �������� ��������� �����
        soundToggle.isOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;

        AudioListener.volume = soundToggle.isOn ? savedVolume : 0f;

        soundToggle.onValueChanged.AddListener(delegate { UpdateSoundState(); });
        volumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
    }

    private void UpdateSoundState()
    {
        if (soundToggle.isOn)
        {
            AudioListener.volume = volumeSlider.value;
        }
        else
        {
            AudioListener.volume = 0f;
        }

        PlayerPrefs.SetInt("Sound", soundToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateVolume()
    {
        if (soundToggle.isOn)
        {
            AudioListener.volume = volumeSlider.value;
        }

        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    private void PlayButtonSound()
    {
        if (soundToggle.isOn && buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void PlayGame()
    {
        PlayButtonSound();
        Invoke(nameof(LoadGameScene), 0.2f); // �������� ��� �����
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenSettings()
    {
        PlayButtonSound();
        mainMenuPanel.SetActive(false);  // ��������� ������ ������ ����
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        PlayButtonSound();
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true); // �������� ������ ���� �������
    }

    public void OpenInfo()
    {
        PlayButtonSound();
        mainMenuPanel.SetActive(false);
        infoPanel.SetActive(true);
    }

    public void CloseInfo()
    {
        PlayButtonSound();
        infoPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        PlayButtonSound();
        Application.Quit();
    }
}
