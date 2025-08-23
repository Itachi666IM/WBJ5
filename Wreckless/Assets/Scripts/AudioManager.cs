using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void ManageSingleton()
    {
        int instance = FindObjectsOfType<AudioManager>().Length;
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
        ManageSingleton();
    }
}
