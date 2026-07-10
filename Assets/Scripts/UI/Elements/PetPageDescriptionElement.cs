using UnityEngine;
using TMPro;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PetPageDescriptionElement : UIElement 
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI description;

    public void Start()
    {
        SetText(PlayerDataManager.Instance.PlayerSkin);
    }

    public void SetText(PlayerSkinData playerSkinData)
    {
        description.text = playerSkinData.MenuDescription;
    }
}