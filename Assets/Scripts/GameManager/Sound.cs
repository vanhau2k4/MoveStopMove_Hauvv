using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioSource[] audioSources; // Gán tất cả các AudioSource vào đây
    private bool isMuted = false; // Trạng thái âm thanh

    public Button sousceOn;
    public Button sousceOff;
    void Start()
    {
        // Đảm bảo tất cả audioSources được gán tự động
        audioSources = FindObjectsOfType<AudioSource>();
        sousceOn.onClick.AddListener(SousceON);
        sousceOff.onClick.AddListener(SousceOFF);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        foreach (AudioSource source in audioSources)
        {
            source.mute = isMuted;
        }
    }

    private void SousceON()
    {
        isMuted = false;
        sousceOff.gameObject.SetActive(true);
        sousceOn.gameObject.SetActive(false);
        ToggleMute();
    }
    private void SousceOFF()
    {
        isMuted = true;
        sousceOn.gameObject.SetActive(true);
        sousceOff.gameObject.SetActive(false);
        ToggleMute();
    }
}
