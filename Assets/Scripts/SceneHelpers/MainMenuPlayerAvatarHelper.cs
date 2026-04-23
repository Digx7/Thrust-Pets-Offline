using UnityEngine;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class MainMenuPlayerAvatarHelper : MonoBehaviour 
{
    public void Start()
    {
        UpdateDisplayedPlayerSkin(PlayerDataManager.Instance.PlayerSkin);
    }
    
    public void UpdateDisplayedPlayerSkin(PlayerSkinData playerSkinData)
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(playerSkinData.RuntimePrefab, this.transform);
    }
}