using UnityEngine;
using UnityEngine.UI;

public class LabelManager : MonoBehaviour
{

    public Text FightExpLabel, CraftExpLabel, EnchantingExpLabel, TimberExpLabel, MiningExpLabel;
    // Start is called before the first frame update

    public Text FightExpSprite, CraftExpSprite, EnchantingExpSprite, TimberExpSprite, MiningExpSprite;



    private void Update()
    {
        if (transform.gameObject.activeSelf)
        {
            UpdateUI();
            return;
        }
        else
        {
            return;
        }
    }

    private void UpdateUI()
    {
        FightExpLabel.text = $"Fighting\n exp: {SkillSystemManager.Instance.GetExp(SkillSet.FIGHTING)}";
        CraftExpLabel.text = $"Crafting\n exp: {SkillSystemManager.Instance.GetExp(SkillSet.CRAFTING)}";
        EnchantingExpLabel.text = $"Enchanting\n exp: {SkillSystemManager.Instance.GetExp(SkillSet.ENCHANTING)}";
        TimberExpLabel.text = $"Timber\n exp: {SkillSystemManager.Instance.GetExp(SkillSet.TIMBER)}";
        MiningExpLabel.text = $"Minig\n exp: {SkillSystemManager.Instance.GetExp(SkillSet.MINING)}";

        FightExpSprite.text = $"lvl: {SkillSystemManager.Instance.getLvl(SkillSet.FIGHTING)}";
        CraftExpSprite.text = $"lvl: {SkillSystemManager.Instance.getLvl(SkillSet.CRAFTING)}";
        EnchantingExpSprite.text = $"lvl: {SkillSystemManager.Instance.getLvl(SkillSet.ENCHANTING)}";
        TimberExpSprite.text = $"lvl: {SkillSystemManager.Instance.getLvl(SkillSet.TIMBER)}";
        MiningExpSprite.text = $"lvl: {SkillSystemManager.Instance.getLvl(SkillSet.MINING)}";
    }
}
