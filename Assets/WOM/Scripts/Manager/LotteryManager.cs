using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 유니온 뽑기 , 진화 주사위 뽑기
/// </summary>
public class LotteryManager : MonoBehaviour
{
    public int curSummonGrade = 0; // 소환 등급 데이터 
    public int lottreyRoundCount = 0;     // 뽑기 시도 카운트
    public UnionGambleData curGambleData;
    public SummonGradeData curSummonGradeData;
    public Transform trCardsParent;
    public int cardsBirthCount = 50; // 풀링으로 사용할 카드 이미지 - 50회 연속 뽑기 기능으로 최초 50개 생성

    public LotteryCard prefabLotteryCard;
    public List<LotteryCard> lotteryCards = new List<LotteryCard>();
    public float cardOpenDeayTime = 0.2f;



    // 테스트 용도 데이터
    string[] unionNameNormal = { "normal-1", "normal-2", "normal-3", "normal-4", "normal-5", "normal-6", "normal-7", "normal-8" };
    string[] unionNameHigh = { "high-1", "high-2", "high-3", "high-4", "high-5", "high-6", "high-7", "high-8" };
    string[] unionNameRare = { "rare-1", "rare-2", "rare-3", "rare-4", "rare-5", "rare-6", "rare-7", "rare-8" };
    string[] unionNameHero = { "hero-1", "hero-2", "hero-3", "hero-4", "hero-5", "hero-6", "hero-7", "hero-8" };
    string[] unionNameLegend = { "legend-1", "legend-2", "legend-3", "legend-4", "legend-5", "legend-6", "legend-7", "legend-8" };
    string[] unionNameUnique = { "unique-1", "unique-2", "unique-3", "unique-4", "unique-5", "unique-6", "unique-7", "unique-8" };

    public List<Sprite> unionFaceNormal = new List<Sprite>();
    public List<Sprite> unionFaceHigh = new List<Sprite>();
    public List<Sprite> unionFaceRare = new List<Sprite>();
    public List<Sprite> unionFaceHero = new List<Sprite>();
    public List<Sprite> unionFaceLegend = new List<Sprite>();
    public List<Sprite> unionFaceUnique = new List<Sprite>();
    List<List<Sprite>> unionFaceDatas = new List<List<Sprite>>();


    // 테스트 용도
    string[][] unionNameData;
    float[] randomGradeValues;
    public List<Color> cardColors = new List<Color>();
    public List<EnumDefinition.UnionGradeType> openedUnionTypeCards;


    void Start()
    {
        SetUnionNmaeData();
        SetUnionFaceList();
    }

    void SetUnionFaceList()
    {
        unionFaceDatas.Add(unionFaceNormal);
        unionFaceDatas.Add(unionFaceHigh);
        unionFaceDatas.Add(unionFaceRare);
        unionFaceDatas.Add(unionFaceHero);
        unionFaceDatas.Add(unionFaceLegend);
        unionFaceDatas.Add(unionFaceUnique);
    }

    // 테스트 용도
    void SetUnionNmaeData()
    {
        unionNameData = new string[6][];
        unionNameData[0] = unionNameNormal;
        unionNameData[1] = unionNameHigh;
        unionNameData[2] = unionNameRare;
        unionNameData[3] = unionNameHero;
        unionNameData[4] = unionNameLegend;
        unionNameData[5] = unionNameUnique;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            //StartCoroutine(CardOpen(10));
        }
    }

    public void SetGambleData(UnionGambleData unionGambleData)
    {
        curGambleData = unionGambleData;
    }
    public void SetSummonGradeData(SummonGradeData summonGradeData)
    {
        curSummonGradeData = summonGradeData;
    }

    /// <summary> 뽑기 필요 데이터 최초 세팅 </summary>
    public IEnumerator Init(int gradeIndex)
    {
        SetSummonGradeData(GlobalData.instance.dataManager.GetSummonGradeDataByLevel(gradeIndex));
        SetGambleData(GlobalData.instance.dataManager.GetUnionGambleDataBySummonGrade(gradeIndex));
        randomGradeValues = GetRandomArrayValue();
        CreateCards();
        yield return null;
    }
        
    // 풀링으로 사용할 카드 생성
    void CreateCards()
    {
        for (int i = 0; i < cardsBirthCount; i++)
        {
            var card = Instantiate(prefabLotteryCard, trCardsParent);
            card.SetTxtName("name");
            card.gameObject.SetActive(false);
            lotteryCards.Add(card);
        }
    }

    public void LotteryStart(int roundCount, UnityAction gameEndEvent)
    {
        StartCoroutine(CardOpen(roundCount, gameEndEvent));
    }

    public IEnumerator CardOpen(int roundCount, UnityAction gameEndEvent)
    {
        yield return StartCoroutine(MakeCardOption(roundCount));
        
        yield return new WaitForSeconds(0.3f); // 05초 대기 ( 연출 )

        yield return StartCoroutine(CardsOpenEffect());
        
        gameEndEvent.Invoke();
    }

    void LotteryClose()
    {

    }

    public IEnumerator MakeCardOption(int gameCount)
    {
        openedUnionTypeCards = new List<EnumDefinition.UnionGradeType>();
        for (int i = 0; i < gameCount; i++)
        {
            var union = (EnumDefinition.UnionGradeType)UtilityMethod.GetWeightRandomValue(randomGradeValues);
            openedUnionTypeCards.Add(union);
            lottreyRoundCount++;
            yield return null;
        }
    }


    public IEnumerator CardsOpenEffect()
    {
        for (int i = 0; i < openedUnionTypeCards.Count; i++)
        {
            var unionTypeIndex = (int)openedUnionTypeCards[i];
            var faceIndex = GetRandomFaceIndex();
            // name , color , sprite image index
            var name = unionNameData[unionTypeIndex][faceIndex];
            var face = unionFaceDatas[unionTypeIndex][faceIndex];

            // INDEX 로 전달 하도록 수정 
            // https://docs.google.com/spreadsheets/d/1gsiAKac3UtyWZNIKr3T-RxqNeR6ZfgjRdC4_VC_49gA/edit#gid=548996693

            //var color = cardColors[faceIndex];
            //card.imgCard.color = color;
            var card = lotteryCards[i];
            card.SetTxtName(name);
            card.SetCardFace(face);
            card.gameObject.SetActive(true);
            card.Effect(openedUnionTypeCards[i]);
            
            // 딜레이가 필요한경우 코루틴
            // yield return StartCoroutine(card.EffectCor(openedUnionTypeCards[i]));
            
            yield return new WaitForSeconds(cardOpenDeayTime);
        }
    }

    int GetRandomFaceIndex()
    {
        return Random.Range(0, 7);
    }

    // curGambleData 의 각 랜덤 범위 적용 
    float[] GetRandomArrayValue()
    {
        var data = curGambleData;
        float[] array = new float[6];
        array[0] = data.normal;
        array[1] = data.high;
        array[2] = data.rare;
        array[3] = data.hero;
        array[4] = data.legend;
        array[5] = data.unique;
        return array;
    }

    //float ChooseUnionType(float[] probs)
    //{
    //    float total = 0;
    //    foreach (float elem in probs)
    //    {
    //        total += elem;
    //    }
    //    float randomPoint = Random.value * total;

    //    for (int i = 0; i < probs.Length; i++)
    //    {
    //        if (randomPoint < probs[i])
    //        {
    //            return i;
    //        }
    //        else
    //        {
    //            randomPoint -= probs[i];
    //        }
    //    }
    //    return probs.Length - 1;
    //}


    private void OnDisable()
    {
        ResetValues();
    }

    void ResetValues()
    {
       
    }

         
}
