using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSpriteAnimation : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite[] sprites;

    [Header("�ʴ� ������")]
    public float samplelate = 12;
    private float frameTime = 0;
    private float t = 0;

    private int currentIndex = 0;

    public delegate void EffectAnimation(int index);
    public EffectAnimation EffectEvent;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        t = 0;
        currentIndex = 0;   
    }

    private void Update()
    {
        //Ȱ��ȭ ���� �ʾ����� ������Ʈ ������ ����.
        if(gameObject.activeSelf == false) return;

        //�ʴ� ������ �׷����� ��������
        frameTime = 1 / samplelate;

        //������ Ȯ���ϱ�
        t += Time.deltaTime;
        if(t >= frameTime)
        {
            rend.sprite = sprites[currentIndex];

            if(EffectEvent != null) EffectEvent.Invoke(currentIndex);

            t = 0;
            currentIndex = (currentIndex >= sprites.Length - 1) ? 0 : currentIndex + 1;
        }
    }

    public void SetSprite(Sprite[] s)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = s[i];
        }
    }
}
