using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Digx7.ThrustPets;
using Digx7.Zygote;

public class LoadingScreenWidget : UIWidget
{
    [Header("Player Skin Data")]
    [SerializeField] List<PlayerSkinData> _playerSkins;
    
    [Header("References")]
    [SerializeField] Image _petImage;
    [SerializeField] TextMeshProUGUI _funFactTMP;

    private void Start() {
        GetRandomFunFact();
    }

    void GetRandomFunFact()
    {
        if (_playerSkins.Count == 0)
            return;

        int index = Random.Range(0, _playerSkins.Count);
        PlayerSkinData randomSkin = _playerSkins[index];
        AnimalFunFact randomFunFact = randomSkin.GetRandomFunFact();

        if (randomFunFact != null)
        {
            _funFactTMP.text = randomFunFact.Fact;
            _petImage.sprite = randomSkin.PhotoImage;
        }
    }
}