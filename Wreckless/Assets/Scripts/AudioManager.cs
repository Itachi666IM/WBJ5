using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    AudioSource myAudio;
    [SerializeField] Slider volumeSlider;
    void ManageSingleton()
    {
        int instance = FindObjectsByType<AudioManager>(FindObjectsSortMode.None).Length;
        if(instance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        myAudio = GetComponent<AudioSource>();
        ManageSingleton();
    }

    public void SetMusicVolume()
    {
        myAudio.volume = volumeSlider.value;
    }
}
