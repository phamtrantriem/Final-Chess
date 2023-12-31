using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelTxt;
    [SerializeField] private Image _levelIcon;
    [SerializeField] private List<Sprite> _icons;

    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _manaSlider;
     
    [SerializeField] private Image _hpImage;
    [SerializeField] private Sprite _redBar;

    [SerializeField] private GameObject _tooltip;
    [SerializeField] private TMP_Text _descriptionTxt;

    public void SetLevel(int level)
    {
        _levelTxt.text = level.ToString();
        _levelIcon.sprite = _icons[level - 1];
    }
    
    public void SetSliderBar(TeamID teamID)
    {
        _hpSlider.value =   _hpSlider.maxValue;
        _manaSlider.value = 0;
        if (teamID == TeamID.Red)
        {
            _hpImage.sprite = _redBar;
            
        }
    }

    public void SetSliderInitValue(float maxHp, float maxMana)
    {
        _hpSlider.maxValue = maxHp;
        _hpSlider.value = maxHp;
       
        _manaSlider.maxValue = maxMana;
        _manaSlider.value = 0;
        
    }

    public void SetHpValue(float hp)
    {
        //_hpSlider.value = hp;
        _hpSlider.DOValue(hp, 0.5f);
    }
    
    public void SetManaValue(float mana)
    {
        //_manaSlider.value = mana;
        _manaSlider.DOValue(mana, 0.5f);
    }
    
    public void SetHeroAttribute(HeroStats _stats)
    {
        _tooltip.SetActive(false);
        _descriptionTxt.text = "Name: " + _stats.Name + "\n"
            + "Damage: " + _stats.Dmg + "\n"
            + "HP: " + _stats.Hp + "\n"
            + "Class: " + _stats.Class + "\n"
            + "Species: " + _stats.Species + "\n";
    }

    public void OnOpenTooltip()
    {
        _tooltip.SetActive(true);
    }
    public void OnCloseTooltip()
    {
        _tooltip.SetActive(false);
    }

}
