using System;
using System.Numerics;
using System.IO;

class ScreenManager
{
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
        ChapterPicker();
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
                Console.Clear();
                battleScreen.BattleStartSecen();
                ShowMainScreen();
                break;// 전투 화면으로 이동

            default:
                Console.WriteLine("잘못된입력입니다.");
                Console.Clear();
                mainScreen.Display();
                ShowMainScreen();
                break; //1 과 2가 아닌 입력을 받을시 메인 화면으로 다시 로드
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

    public void Prologue()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string readTxt = Path.Combine(currentDirectory,"Prologue.txt");

        try
        {
            string getTxt = File.ReadAllText(readTxt);
            Console.WriteLine(getTxt);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"파일을 읽어오는 중 오류 발생: {ex.Message}");
        }
    }


    public void ChapterPicker() // 플레이어의 현재 승리 카운트에 따라 스토리 챕터를 불러오는 함수
    {

        switch (player.WinCount)
        {
            case 0:
                ChapterPicker("Chapter1"); ;
                break;
            case 1:
                ChapterPicker("Chapter2");
                break;
            default:
                Console.WriteLine("아직 스토리가 없습니다.");
                break;
        }
    }

    void ChapterPicker(string filename)
    {
        string currentDirectory = Directory.GetCurrentDirectory();// 현재 프로세스가 실행중인 파일의 경로 불러오기

        string readTxt = Path.Combine(currentDirectory, $"{filename}.txt");
        try
        {
            string getTxt = File.ReadAllText(readTxt);
            Console.WriteLine(getTxt);
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



