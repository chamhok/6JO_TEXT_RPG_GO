﻿using System;
using System.Numerics;

class ScreenManager
{
    Monster createMonster = new Monster();
    Character player;
    PlayerInfo playerInfo;
    MainScreen mainScreen = new MainScreen();
    BattleScreen battleScreen;

    public ScreenManager(Character player, BattleEvent battleEvent)
    {
        this.player = player;
        playerInfo = new PlayerInfo(player);
        battleScreen = new BattleScreen(battleEvent);
    }

    public void ShowMainScreen()
    {
        Console.Clear();
        mainScreen.Display();
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                playerInfo.Display();
                Console.ReadKey();
                ShowMainScreen();
                break;

            case "2":
                Console.Clear();
                battleScreen.BattleStartSecen();
                ShowMainScreen();
                break;

            default:
                Console.Clear();
                mainScreen.Display();
                ShowMainScreen();
                break;
        }
    }

    class MainScreen
    {
        public void Display()
        {
            Head();
            Body();
            Bottom();
        }

        private void Head()
        {
            Console.WriteLine("================================================");
        }

        public void Body()
        {
            Console.WriteLine("1. 정보창");
            Console.WriteLine("2. 전투");
        }
        private void Bottom()
        {
            Console.WriteLine("================================================");
        }
    } // 메인 화면

    class PlayerInfo
    {
        Character player;
        public PlayerInfo(Character player)
        {
            this.player = player;
        }

        public void Display()
        {
            Head();
            Body(player);
            Bottom();
        }

        private void Head()
        {
            Console.WriteLine("================================================");
        }

        public void Body(Character player)
        {
            Console.WriteLine("이름 : " + player.Name);
            Console.WriteLine("직업 : " + player.Job);
            Console.WriteLine("레벨 : " + player.Level);
            Console.WriteLine("공격력 : " + player.Attack);
            Console.WriteLine("방어력 : " + player.Defense);
            Console.WriteLine("직업 : " + player.Speed);
            Console.WriteLine("치명타 : " + player.Crt);
            Console.WriteLine("회피 : " + player.Avoidance);
            Console.WriteLine("Gold : " + player.Gold);
        }

        private void Bottom()
        {
            Console.WriteLine("================================================");
        }
    } // 플레이어 정보 화면
}

class BattleScreen // 전투 진행 화면
{
    BattleEvent battleEvent;

    public BattleScreen(BattleEvent battleEvent)
    {
        this.battleEvent = battleEvent;
    }

    public void BattleStartSecen()
    {
        Console.WriteLine("전투시작.");
        battleEvent.Battles();
    }
}

