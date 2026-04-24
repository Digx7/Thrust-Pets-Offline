using UnityEngine;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PetPageTitleElement : UIElement 
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI title;

    public void Start()
    {
        SetText(PlayerDataManager.Instance.PlayerSkin);
    }

    public void SetText(PlayerSkinData playerSkinData)
    {
        title.text = playerSkinData.DisplayName;
    }
}