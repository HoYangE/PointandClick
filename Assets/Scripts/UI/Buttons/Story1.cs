using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story1 : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(Talk());
    }

    #region Start

    IEnumerator Talk()
    {
        var data = CSVReader.Read("Story1TextScript");
        foreach (var t in data)
        {
            switch (int.Parse(t["Talk"].ToString()))
            {
                case 7:
                    yield return StartCoroutine(NPCTextUI.Instance.FadeInCoroutine(0));
                    break;
                case 10:
                    yield return StartCoroutine(NPCTextUI.Instance.FadeOutCoroutine(0));
                    break;
            }

            yield return StartCoroutine(NPCTextUI.Instance.NormalChat(t["Name"].ToString(), t["Text"].ToString()));
            yield return new WaitForSeconds(float.Parse(t["Delay"].ToString()));
        }
    }
    #endregion
    
    #region Object
    public void Object1()
    {
        // 오른쪽 책장에 튀어나와 있는 책 클릭
        // 오래된 종이 이미지 출력
        // 아무 클릭이나 하면 이미지 사라짐
    }
    public void Object2()
    {
        // 책상 위에 올라와 있는 인형 클릭
        // 똑똑 거리는 사운드 출력
    }
    public void Object3()
    {
        // 침대 머리 쪽 클릭
        // "평소에 눕는 침대지만 지금은 눕고 싶지 않아."
    }
    public void Object4()
    {
        // 침대 위에 있는 인형 클릭
        // 사운드와 침대 밑에 기괴한 손 이미지 나왔다가 사라짐
    }
    public void Object5()
    {
        // 떨어져 있는 책 클릭
        // 기괴한 얼굴 이미지 나왔다가 사라짐
    }
    #endregion Object

    #region Door
    public void Door1()
    {
        // 1번째 문 클릭
        // "열리지 않는다. 무슨 일이지?"
    }
    public void Door2()
    {
        // 2번째 문 클릭
        // 문 긁는 소리 출력
    }
    public void Door3()
    {
        // 3번째 문 클릭
        // 크게 두드리는 소리 출력, 문에 피 튀기는 이미지 출력
    }
    public void Door4()
    {
        // 4번째 문 클릭
        // 첫번째 인형 빠르게 화면 지나감
    }
    public void Door5()
    {
        // 5번째 문 클릭
        // 문이 열리고 두번째 인형의 얼굴이 나왔다가 사라짐
    }
    #endregion Door

    #region MoreEvent
    public void Object6()
    {
        // 두 번째로 책장을 클릭
        // 알 수 없는 소리 출력
    }
    public void Object7()
    {
        // 두 번째로 책상 위 인형을 클릭
        // 벽에 피 튀기는 이미지 출력
    }
    public void Object8()
    {
        // 두 번째로 침대 위 인형을 클릭
        // "이 인형은 처음 보는 인형인데..."
    }
    public void Object9()
    {
        // 책상 다리 클릭 시
        // 악 소리 출력
    }
    public void Object10()
    {
        // 두 번째로 책상 다리 클릭 시
        // 귀신 얼굴 나왔다가 사라짐
    }    
    #endregion MoreEvent
}
