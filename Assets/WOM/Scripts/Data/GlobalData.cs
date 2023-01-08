using ProjectGraphics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{

    public  static GlobalData instance;
    public DataManager dataManager;
    public InsectManager insectManager;
    public MonsterManager monsterManager;
    public AttackController attackController;
    public CustomTypeDataManager customTypeDataManager;
    public EventController eventController; 
    public UiController uiController;
    public Player player;
    public EffectManager effectManager;
    public BossMonsterChallengeTimer bossChallengeTimer;
    public StageManager stageManager;
    public LotteryManager lotteryManager;
    public SaleManager saleManager;
    public EvolutionManager evolutionManager;
    public GradeAnimationController gradeAnimCont;
    public GlobalPopupController globalPopupController; 
         
    private void Awake()
    {
        SetInstance();
    }

    void Start()
    {
        
    }

    void SetInstance()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

   

}
