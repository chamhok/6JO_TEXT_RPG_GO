using System;
using System.Reflection.Metadata.Ecma335;

public class Quest
{
    Character player;
    List<Quest> quests = GameData.I.GetQuests();
    BattleEvent battle;

    public string Name { get; set; }
    public string QuestInfo { get; set; }
    public int RewardExp { get; set; }
    public string Msg { get; set; }
    public int Mission { get; set; }

    public Quest(BattleEvent battle, Character player)
    {
        GameData.I.AddQuest(new Quest("마을을 위협하는 몬스터 처치", "이봐! 마을 근처에 몬스터들이 너무 많아졌다고 생각하지 않나??\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n자네가 좀 처치해주게!", 25, 5, ""));
        GameData.I.AddQuest(new Quest("퀘스트 이름2", "퀘스트 설명2", 25, 0, ""));
        GameData.I.AddQuest(new Quest("퀘스트 이름3", "퀘스트 설명3", 25, 0, ""));

        this.battle = battle;
        this.player = player;
    }

    public Quest(string name, string questInfo, int rewardExp, int mission, string msg)
    {
        this.Name = name;
        this.QuestInfo = questInfo;
        this.RewardExp = rewardExp;
        this.Msg = msg;
        this.Mission = mission;
    }


    // 디스플레이 화면
    public void DisplayQuest()
    {
        Console.Clear();

        int questIndex = 1;
        Console.WriteLine("===========================================================\n");

        //Gamedata.cs에 있는 quests리스트 표시
        foreach (var quest in quests)
        {
            Console.WriteLine($"{questIndex}. 퀘스트: {quest.Name}{quest.Msg}");
            questIndex++;
        }
        Console.WriteLine("\n===========================================================\n");

        Console.WriteLine($"1 ~ {quests.Count}, 원하시는 퀘스트를 선택해주세요.");
        Console.WriteLine("4, 돌아가기");
        Console.Write(">>");

        int input = CheckValidInput(1, 4);
        // 선택한 퀘스트의 Msg가 "[Clear]"인 경우 처리
        if(input != 4)
        {
            if (quests[input - 1].Msg == "[Clear]")
            {
                Console.WriteLine("이미 완료한 퀘스트입니다. 다른 퀘스트를 선택해주세요.");
                Console.ReadKey();
                DisplayQuest();
                return;
            }
        }

        switch (input)
        {
            case 1:
                Quest1();
                break;
            case 2:
                Quest2();
                break;
            case 3:
                Quest3();
                break;
            case 4:
                break;
        }
    }

    // 1번 퀘스트
    public void Quest1()
    {
        // 플레이어가 아직 퀘스트를 수락하지 않은 경우
        if (!player.IsQuest)
        {
            Console.Clear();
            Console.WriteLine("===========================================================\n");
            Console.WriteLine($"{quests[0].Name}\n\n{quests[0].QuestInfo}\n");
            Console.WriteLine($"- 몬스터 {quests[0].Mission}마리 처치 ({battle.deadMonsterCnt}/{quests[0].Mission})");
            Console.WriteLine("\n- 보상 -");
            Console.WriteLine($"+{quests[0].RewardExp}Exp");
            Console.WriteLine("\n===========================================================\n");

            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
            Console.Write(">>");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    SelecetedQuest(1);
                    break;
                case 2:
                    DeclineQuest();
                    break;
            }
        }
        // 이미 퀘스트를 수락한 경우
        else
        {
            if(battle.deadMonsterCnt < 5)
            {
                    Console.Clear();
                    Console.WriteLine("===========================================================\n");
                    Console.WriteLine($"{quests[0].Name}\n\n{quests[0].QuestInfo}\n");
                    Console.WriteLine($"- 몬스터 {quests[0].Mission}마리 처치 ({battle.deadMonsterCnt}/{quests[0].Mission})");
                    Console.WriteLine("\n- 보상 -");
                    Console.WriteLine($"+{quests[0].RewardExp}Exp");
                    Console.WriteLine("\n===========================================================\n");
                    Console.WriteLine("1. 돌아가기");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");

                    int input = CheckValidInput(1, 1);
                    switch (input)
                    {
                        case 1:
                            DisplayQuest();
                            break;
                    }

            }
            else
            {
                Console.Clear();
                Console.WriteLine("===========================================================\n");
                Console.WriteLine($"{quests[0].Name}\n\n{quests[0].QuestInfo}\n");
                Console.WriteLine($"- 몬스터 {quests[0].Mission}마리 처치 ({battle.deadMonsterCnt}/{quests[0].Mission})");
                Console.WriteLine("\n- 보상 -");
                Console.WriteLine($"+{quests[0].RewardExp}Exp");
                Console.WriteLine("\n===========================================================\n");
                Console.WriteLine("1. 보상 받기");
                Console.WriteLine("2. 돌아가기");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int input = CheckValidInput(1, 2);
                switch (input)
                {
                    case 1:
                        FinishQuest(1);
                        break;
                    case 2:
                        DisplayQuest();
                        break;
                }
            }
        }
    }

    public void Quest2()
    {
        Console.Clear();
        Console.WriteLine("===========================================================\n");
        Console.WriteLine($"{quests[1].Name}\n\n{quests[1].QuestInfo}\n");
        Console.WriteLine("\n- 보상 -");
        Console.WriteLine($"+{quests[1].RewardExp}Exp");
        Console.WriteLine("\n===========================================================\n");

        Console.WriteLine("1. 수락");
        Console.WriteLine("2. 거절");
        Console.Write(">>");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                SelecetedQuest(2);
                break;
            case 2:
                DeclineQuest();
                break;
        }
    }

    public void Quest3()
    {
        Console.Clear();
        Console.WriteLine("===========================================================\n");
        Console.WriteLine($"{quests[2].Name}\n\n{quests[2].QuestInfo}\n");
        Console.WriteLine("\n- 보상 -");
        Console.WriteLine($"+{quests[2].RewardExp}Exp");
        Console.WriteLine("\n===========================================================\n");

        Console.WriteLine("1. 수락");
        Console.WriteLine("2. 거절");
        Console.Write(">>");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                SelecetedQuest(3);
                break;
            case 2:
                DeclineQuest();
                break;
        }
    }

    // 퀘스트 수락
    public void SelecetedQuest(int index)
    {
        if (!player.IsQuest)
        {
            player.IsQuest = true;
            quests[index -1].Msg = "[...ING]";
            Console.WriteLine("퀘스트를 시작합니다.");
            Console.ReadKey();
            DisplayQuest();
        }
        else
        {
            Console.WriteLine("플레이어가 이미 다른 퀘스트를 진행 중입니다.");
            Console.ReadKey();
            DisplayQuest();
        }
    }

    // 퀘스트 거절
    public void DeclineQuest()
    {
        Console.WriteLine("퀘스트를 거절합니다.");
        Console.ReadKey();
        DisplayQuest();
    }

    // 퀘스트 완료
    public void FinishQuest(int Index)
    {
        Console.WriteLine($"퀘스트를 완료하였습니다. 보상이 지급되었습니다.");
        player.CurrentExp += quests[Index-1].RewardExp;
        Console.WriteLine($"현재 경험치: {player.CurrentExp} / {player.MaxExp}");

        if (player.CurrentExp >= player.MaxExp)
        {
            player.Level += 1;
            Console.WriteLine($"레벨업! Lv.{player.Level}");
            player.CurrentExp -= player.MaxExp;
            player.MaxExp += (int)(player.MaxExp * 0.2);
        }

        player.IsQuest = false;
        quests[0].Msg = "[Clear]";
        Console.ReadKey();
    }

    // input값의 범위 확인
    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}
