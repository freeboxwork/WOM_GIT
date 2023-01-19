using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnumDefinition;



public class EvolutionManager : MonoBehaviour
{
    // 진화 주사위 돌려서 획득한 데이터 저장
    public DiceEvolutionInGameData diceEvolutionData;


    void Start()
    {
        
    }




    public IEnumerator Init()
    {
        yield return null;             
    }


    public void SetDiceEvolutionData(EvolutionDiceStatType statType, float statValue)
    {
        switch (statType)
        {
            case EvolutionDiceStatType.insectDamage: diceEvolutionData.insectDamage += statValue; break;
            case EvolutionDiceStatType.insectCriticalChance: diceEvolutionData.insectCriticalChance += statValue; break;
            case EvolutionDiceStatType.insectCriticalDamage: diceEvolutionData.insectCriticalDamage += statValue; break;
            case EvolutionDiceStatType.goldBonus: diceEvolutionData.goldBonus += statValue; break;
            case EvolutionDiceStatType.insectMoveSpeed: diceEvolutionData.insectMoveSpeed += statValue; break;
            case EvolutionDiceStatType.insectSpawnTime: diceEvolutionData.insectSpawnTime += statValue; break;
            case EvolutionDiceStatType.insectBossDamage: diceEvolutionData.insectBossDamage += statValue; break;
        }
    }



}
