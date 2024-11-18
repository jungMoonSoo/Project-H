using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LobbyManager : Singleton<LobbyManager>
{
    public void EnergyPay() //에너지 구매창으로 이동?
    {
        Debug.Log("에너지 추가 구매");
    }
    public void GoldPay() //골드 구매창으로 이동?
    {
        Debug.Log("골드 추가 구매");
    }
    public void DiamondPay() //다이아 구매창으로 이동?
    {
        Debug.Log("다이아 추가 구매");
    }

    public void AlarmWindow() //알람창으로 이동
    {
        Debug.Log("알람창으로 이동");
    }

    public void MassegeWindow() //메세지창으로 이동
    {
        Debug.Log("메세지창으로 이동");
    }

    public void OptionWindow() //옵션창으로 이동
    {
        Debug.Log("옵션창으로 이동");
    }

    public void QeustWindow() //퀘스트창으로 이동
    {
        Debug.Log("퀘스트창으로 이동");
    }

    public void AdveristingWindow() //광고창으로 이동
    {
        Debug.Log("광고창으로 이동");
    }

    public void AnnouncementWindow() //공지창으로 이동
    {
        Debug.Log("공지창으로 이동");
    }

    public void EventWindow() //이벤트창으로 이동
    {
        Debug.Log("이벤트창으로 이동");
    }

    public void LeaderWindow() //리더창으로 이동
    {
        Debug.Log("리더창으로 이동");
    }

    public void ColleagueWindow() //동료창으로 이동
    {
        Debug.Log("동료창으로 이동");
    }

    public void StoreWindow() //상점창으로 이동
    {
        Debug.Log("상점창으로 이동");
    }

    public void PickUpWindow() //모집창으로 이동
    {
        Debug.Log("모집창으로 이동");
    }

    public void GameStart()//게임 시작 이미지 함수 
    {
        Debug.Log("게임 시작 버튼을 눌렸습니다.");
    }

    public void LockOne()//Lock1 이미지 함수 
    {
        Debug.Log("잠긴 이미지1 입니다.");
    }

    public void LockTwo()//Lock2 이미지 함수 
    {
        Debug.Log("잠긴 이미지2 입니다.");
    }
}
