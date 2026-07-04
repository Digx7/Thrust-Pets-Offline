using UnityEngine;
using UnityEngine.UI;
using Digx7.Zygote;
using Digx7.ThrustPets;
using TMPro;
using System.Collections;

public class RunPowerUpUIElement : UIElement 
{
    [Header("Reffernces")]
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _count;
    [SerializeField] Slider _slider;
    [SerializeField] Button _button;

    [SerializeField] PowerUpData currentPowerUp;

    public void Start()
    {
        Refreash();
    }

    public void Refreash()
    {
        currentPowerUp.OnSuccessfullyUse.RemoveListener(OnPowerUpUse);

        // Setsup listeners to events of new powerup
        currentPowerUp.OnSuccessfullyUse.AddListener(OnPowerUpUse);
        
        if(currentPowerUp.MenuImage != null)
        {
            SetIcon(currentPowerUp.MenuImage);
        }
    }

    public void OnPowerUpUse()
    {
        // SetCount(currentPowerUp.UsesLeft);
        StartCoroutine(CooldownSliderRoutine(currentPowerUp.CooldownTime));
    }

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void SetCount(int currentCount)
    {
        _count.text = currentCount.ToString();
    }

    IEnumerator CooldownSliderRoutine(float cooldownTime)
    {
        _button.interactable = false;
        _slider.value = 1f;
        
        float currentTime = cooldownTime;
        while(currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            _slider.value = currentTime / cooldownTime;
            yield return null;
        }

        _button.interactable = true;
        _slider.value = 0f;
    }
}