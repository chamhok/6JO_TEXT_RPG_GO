using System;
using System.Numerics;
using System.IO;
using NAudio.Wave;

class ScreenManager
{
    Character player;
    PlayerInfo playerInfo;
    MainScreen mainScreen = new MainScreen();
    BattleScreen battleScreen;
    Store Displaystore;
    bool skipcheck = false;
    SoundManager soundManager = new SoundManager();

    public ScreenManager(Character player, BattleEvent battleEvent,Store store)
    {
        this.player = player;
        playerInfo = new PlayerInfo(player);
        battleScreen = new BattleScreen(battleEvent);
        Displaystore = store;
    }

    public void ShowMainScreen()
    {
        Console.Clear();
        ChapterPicker(player.WinCount);
        Console.ReadKey();
        mainScreen.Display();
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                playerInfo.Display();
                Console.ReadKey();
                ShowMainScreen();
                break; //캐릭터 정보창 호출 ( 별도의 키지정없이 아무키 입력시 다시 메인 화면으로 이동되게 설정)

            case "2":
                CallInventory();
                break;

            case "3":
                Console.ReadKey();
                soundManager.PlayBackgroundMusicAsync($"Battle{player.WinCount}");
                Console.Clear();
                skipcheck = false;
                battleScreen.BattleStartSecen();
                soundManager.StopMusic();
                ShowMainScreen();
                break;// 전투 화면으로 이동
            case "4":
                Console.Clear();
                Displaystore.DisplayStore();
                break;

            case "end":
                soundManager.StopMusic();
                Console.Clear();
                skipcheck = false;
                ChapterPicker(11);
                break;

            default:
                Console.WriteLine("잘못된입력입니다.");
                Console.Clear();
                mainScreen.Display();
                ShowMainScreen();
                break; //1 과 2가 아닌 입력을 받을시 메인 화면으로 다시 로드
        }
    }

    void CallInventory()
    {
        do
        {
            Console.Clear();
            player.DisplayInventory();
            Console.WriteLine("1, 장착관리");
            Console.WriteLine("2, 돌아가기");
            Console.Write(">>");
            int input = Console.Read();
            char check = (char)input;
            switch (check)
            {
                case '1':
                    Console.WriteLine("장착관리모드 활성화");
                    break;
                case '2':
                    ShowMainScreen();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 1과 2를 입력해주세요");
                    CallInventory();
                    break;

            }

        }
        while (true);
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
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 전투");
            Console.WriteLine("4. 상점");
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

    public void Prologue()
    {
        string readTxt = "../../../Story/Prologue.txt";

        try
        {
            Console.Clear();
            string getTxt = File.ReadAllText(readTxt)
                .Replace("주인공", $"\u001b[34m주인공\u001b[0m")
                .Replace("왕:", $"\u001b[36m왕:\u001b[0m");
            foreach (char c in getTxt)
            {
                Console.Write(c);
                Thread.Sleep(100); // 출력 간격 조절 (밀리초 단위)
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    soundManager.CallSound("sound1", 50);
                    Console.Clear() ;
                    Console.WriteLine(getTxt);
                    Console.ReadKey();
                    break;
                } //플레이어가 Enter 입력시 바로 모든 문자열 출력
            }
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"파일을 읽어오는 중 오류 발생: {ex.Message}");
        }
    }


    public void ChapterPicker(int input) // 플레이어의 현재 승리 카운트에 따라 스토리 챕터를 불러오는 함수
    {

        switch (input)
        {
            case 0:
                ChapterPicker("Chapter1");
                break;
            case 1:
                ChapterPicker("Chapter2");
                break;
            case 2:
                ChapterPicker("Chapter3");
                break;
            case 3:
                ChapterPicker("Chapter4");
                break;
            case 4:
                ChapterPicker("Chapter5");
                break;
            case 5:
                ChapterPicker("Chapter6");
                break;
            case 6:
                ChapterPicker("Chapter7");
                break;
            case 7:
                ChapterPicker("Chapter8");
                break;
            case 8:
                ChapterPicker("Chapter9");
                break;
            case 9:
                ChapterPicker("Chapter10");
                break;
            case 10:
                ChapterPicker("Chapter11");
                break;
            case 11:
                ChapterPicker("Ending");
                soundManager.PlayBackgroundMusicAsync("end");
                ChapterPicker("Ending2");
                Console.WriteLine("아무키를 누르면 종료됩니다!");
                Console.ReadKey();
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("아직 스토리가 없습니다.");
                break;
        }
    }

    void ChapterPicker(string filename)
    {
        try
        {

            string getTxt = File.ReadAllText($"../../../Story/{filename}.txt")
                .Replace("주인공", $"\u001b[34m주인공\u001b[0m")
                .Replace("경비병", $"\u001b[31m경비병\u001b[0m")
                .Replace("기사", $"\u001b[31m기사\u001b[0m")
                .Replace("단장", $"\u001b[31m단장\u001b[0m")
                .Replace("마녀", $"\u001b[35m마녀\u001b[0m")
                .Replace("고블린", $"\u001b[32m고블린\u001b[0m")
                .Replace("트롤", $"\u001b[32m트롤\u001b[0m")
                .Replace("상어", $"\u001b[31m상어\u001b[0m")
                .Replace("케르베로스", $"\u001b[31m케르베로스\u001b[0m")
                .Replace("공허의 마스터", $"\u001b[35m공허의 마스터\u001b[0m")
                .Replace("불의 여왕", $"\u001b[31m불의 여왕\u001b[0m")
                .Replace("그림자 마법사", $"\u001b[30m그림자 마법사\u001b[0m")
                .Replace("무자비한 검사", $"\u001b[31;무자비한 검사\u001b[0m")
                ;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[ {filename} ]");
            Console.ResetColor();

            foreach (char c in getTxt)
            {
                if (skipcheck == true)
                {
                    Console.WriteLine(getTxt);
                    break;
                }
                Console.Write(c);
                Thread.Sleep(100); // 출력 간격 조절 (밀리초 단위)
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    soundManager.CallSound("sound1",50);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[ {filename} ]");
                    Console.ResetColor();
                    Console.WriteLine(getTxt);
                    skipcheck = true;
                    break;
                }

            }
            Console.WriteLine(); // 문단 잘리는 현상 수정
            skipcheck = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"파일을 읽어오는 중 오류 발생: {ex.Message}");
        }
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
}



