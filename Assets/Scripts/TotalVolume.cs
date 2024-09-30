using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalVolume : MonoBehaviour
{
    public Slider _allSlider;
    public Sprite newImageNhac; // Ảnh khi tắt âm thanh 
    public Sprite oldImageNhac;     // Ảnh khi mở âm thanh
    private Image buttonImageNhac; // Biến lưu trữ component Image của nút Nhạc
    // Start is called before the first frame update
    void Start()
    {
        bool isMuted = PlayerPrefs.GetInt("AllMuted", 0) == 1;
        AudioManager.Instance.sfxSource.mute = isMuted;
        buttonImageNhac = GetComponent<Image>();
        _allSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f); // Tải giá trị âm lượng đã lưu hoặc đặt mặc định là 0.5
        AllVolume(); // Áp dụng giá trị âm lượng
        UpdateButtonImage();
    }

    public void ToggleMusic()
    {
        // AudioManager.Instance.PlaySFX("ClickButton");
        bool isMuted = !AudioManager.Instance.musicSource.mute;
        AudioManager.Instance.sfxSource.mute = isMuted;
        PlayerPrefs.SetInt("SfxMuted", isMuted ? 1 : 0);
        AudioManager.Instance.musicSource.mute = isMuted;
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.SetInt("AllMuted", isMuted ? 1 : 0);
        AudioManager.Instance.PlaySFX("ClickButton");
        PlayerPrefs.Save();
        UpdateButtonImage();
    }
    private void UpdateButtonImage()
    {
        // Cập nhật hình ảnh của button dựa trên trạng thái mute
        buttonImageNhac.sprite = AudioManager.Instance.musicSource.mute ? newImageNhac : oldImageNhac;
    }
    public void AllVolume()
    {
        float volume = _allSlider.value;
        AudioManager.Instance.MusicVolume(volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
        AudioManager.Instance.SFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("AllVolume", volume);
        PlayerPrefs.Save();
    }
}
