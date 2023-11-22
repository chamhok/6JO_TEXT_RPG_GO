using System;
using System.Numerics;
using System.IO;
using NAudio.Wave;
using System.Text;

class ScreenManager
{
    Character player;
    PlayerInfo playerInfo;
    MainScreen mainScreen = new MainScreen();
    BattleScreen battleScreen;
    QuestScreen questScreen;
    Store Displaystore;
    bool skipcheck = false;
    SoundManager soundManager = new SoundManager();
    SoundManager soundManager2 = new SoundManager();

    public ScreenManager()
    {
        
    }

    public ScreenManager(Character player, BattleEvent battleEvent, Store store, Quest quest)
    {
        this.player = player;
        playerInfo = new PlayerInfo(player);
        battleScreen = new BattleScreen(battleEvent);
        Displaystore = store;
        questScreen = new QuestScreen(quest);
    }

        public void ShowMainScreen()
        {

                do
                {
                        Console.Clear();
                        soundManager2.PlayBackgroundMusicAsync("BaseBgm");
                        ChapterPicker(player.WinCount);
                        Console.ReadKey();
                        mainScreen.Display();
                        string input = Console.ReadLine();
                        soundManager.CallSound("sound1", 1);
                        switch (input)
                        {
                                case "1":
                                        Console.Clear();
                                        playerInfo.Display();
                                        Console.ReadKey();
                                        soundManager.CallSound("sound1", 1);
                                        break; //캐릭터 정보창 호출 ( 별도의 키지정없이 아무키 입력시 다시 메인 화면으로 이동되게 설정)

                                case "2":
                                        CallInventory();
                                        break;//인벤토리

                case "3":
                    soundManager2.StopMusic();
                    Console.Clear();
                    skipcheck = false;
                    soundManager.PlayBackgroundMusicAsync($"battle{player.WinCount}");
                    battleScreen.BattleStartSecen();
                    soundManager.StopMusic();
                    break;// 전투 화면으로 이동
                case "4":
                    Console.Clear();
                    Displaystore.DisplayStore();
                    break;
                case "5":
                    Console.Clear();
                    questScreen.DisplayQuestList();
                    break;
                case "end":
                    Console.Clear();
                    skipcheck = false;
                    ChapterPicker(11);
                    break;

                                case "n":
                                        player.WinCount += 1;
                                        break;

                                default:
                                        Console.WriteLine("잘못된입력입니다.");
                                        break; //1 과 2가 아닌 입력을 받을시 메인 화면으로 다시 로드

                        }
                } while (true);
        }

        public void CallInventory()
        {
                // 게임 화면 초기화 및 테이블 설정
                Renderer.Initialize("");
                // 테두리와 테이블 그리기
                Renderer.DrawBorder("무기 선택");
                Renderer.Print(3, "무기를 선택해주세요! 당신의 직업을 결정합니다");
                // 입력 영역 그리기
                Renderer.DrawInputArea();
                // 테이블에 데이터 추가
                Table table = new Table();
                table.AddType("Index", 5, false);
                table.AddType("Name", 15, false);
                table.AddType("Description", 40, false);
                table.AddType("Stallation",10 , false);
                table.AddType("Akt", 5, false);
                table.AddType("Def", 5, false);

                foreach (var item in GameData.I.GetCharacters()[0].Inventory)
                {

                        Console.WriteLine(item.Name);
                        table.AddData("Index", (GameData.I.GetCharacters()[0].Inventory.IndexOf(item) + 1).ToString());
                        table.AddData("Name", item.Name);
                        table.AddData("Description", item.ItemDescription);// 무기 1 추가
                        table.AddData("Stallation", Item.stallationManagement(item));
                        table.AddData("Akt", item.Akt.ToString());
                        table.AddData("Def", item.Def.ToString());
                }



                int currentSelection = 0;

                while (true)
                {
                        Renderer.DrawTable(table, 5, currentSelection);
                        Renderer.DrawInputArea();

                        ConsoleKeyInfo key = Console.ReadKey();
                        if (key.Key == ConsoleKey.UpArrow && currentSelection > 0)
                        {
                                currentSelection--;
                        }
                        if (key.Key == ConsoleKey.DownArrow && currentSelection < table.GetDataCount() - 1)
                        {
                                currentSelection++;
                        }
                        if (key.Key == ConsoleKey.Enter)
                        {
                                // 여기서 선택된 행에 대한 처리를 수행합니다.
                                switch (currentSelection)
                                {
                                }
                                break;
                        }
                }


                soundManager.CallSound("sound1", 1);



                /*      bool outcheck = true;
              do
              {
                  Console.Clear();
                  player.DisplayInventory();
                  Console.WriteLine("1, 장착관리");
                  Console.WriteLine("2, 돌아가기");
                  Console.Write(">>");
                  int input = Console.Read();
                  char check = (char)input;
                  soundManager.CallSound("sound1", 1);
                  switch (check)
                  {
                      case '1':
                          Console.WriteLine("장착관리모드 활성화");
                          break;
                      case '2':
                          outcheck = false;
                          break;
                      default:
                          Console.WriteLine("잘못된 입력입니다. 1과 2를 입력해주세요");
                          Console.ReadKey();
                          break;

                  }

              }
              while (outcheck);*/
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
            Console.WriteLine("5. 퀘스트");
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
            CurrentExp();
        }

        private void CurrentExp()
        {
            // 현재 경험치 퍼센트 계산
            double expPercentage = (player.CurrentExp / player.MaxExp * 100) / 2;

            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;

            // 변수에 경험치를 표시
            for (int i = 0; i < (int)expPercentage; i++)
            {
                Console.Write("█");
            }

            Console.ForegroundColor = ConsoleColor.Gray;

            // 남은 부분은 공백으로 채우기
            for (int i = (int)expPercentage; i < 50; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine($"] {player.CurrentExp}/{player.MaxExp} Exp");
        }

        private void Bottom()
        {
            Console.WriteLine("================================================");
        }
    } // 플레이어 정보 화면

        public void Prologue()
        {
                ConsoleHelper.SetCurrentFont("빛의 계승자 Bold", 30);
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.WindowWidth = Console.LargestWindowWidth;
                Console.WindowHeight = Console.LargestWindowHeight;
                string readTxt = "../../../Story/Prologue.txt";
                int yCount = 0;
                try
                {
                        Console.Clear();

                        try
                        {
                                string[] fileContents = File.ReadAllLines(readTxt, Encoding.UTF8)
                                                                 .Select(x =>
                                                               x.Replace("주인공:", $"\u001b[94m주인공:\u001b[0m")
                                                                 .Replace("왕:", $"\u001b[36m왕:\u001b[0m")).ToArray();

                                foreach (string line in fileContents)
                                {
                                        if (line.Contains("주인공:") || line.Contains("왕:")) Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length + 15, yCount++);
                                        else Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length, yCount++);
                                        foreach (char cha in line)
                                        {
                                                Console.Write(cha);
                                                if (!Console.KeyAvailable) Thread.Sleep(50);
                                        }
                                        Console.WriteLine();
                                }
                                Console.SetCursorPosition((Console.WindowWidth) / 2 - "x를 누르십시오".Length, yCount + 3);
                                Console.WriteLine("x를 누르십시오");


                        }
                        catch (Exception ex)
                        {
                                Console.WriteLine($"Error reading the file: {ex.Message}");
                        }
                        while (true)
                        {
                                ConsoleKeyInfo key = Console.ReadKey();
                                if (key.KeyChar == 'x' || key.KeyChar == 'ㅌ')
                                {
                                        soundManager.CallSound("sound1", 1);
                                        break;
                                }
                        }
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
                                ChapterPicker("Chapter12");
                                break;
                        case 11:
                                skipcheck = false;
                                ChapterPicker("Ending");
                                skipcheck = false;
                                Console.WriteLine();
                                Console.WriteLine("x를 누르십시오");
                                while (true)
                                {
                                        ConsoleKeyInfo key = Console.ReadKey();
                                        if (key.KeyChar == 'x' || key.KeyChar == 'ㅌ')
                                        {
                                                soundManager.CallSound("sound1", 1);
                                                break;
                                        }
                                }
                                Console.Clear();
                                soundManager2.StopMusic();
                                soundManager.StopMusic();
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
                int yCount = 3;

                try
                {


                        string[] readTxt = File.ReadAllLines($"../../../Story/{filename}.txt")
                                                       .Select(x => x.Replace("주인공", $"\u001b[94m주인공\u001b[0m")
                                                       .Replace("엄마", $"\u001b[36m엄마\u001b[0m")
                                                       .Replace("경비병", $"\u001b[91m경비병\u001b[0m")
                                                       .Replace("반장", $"\u001b[91m반장\u001b[0m")
                                                       .Replace("기사", $"\u001b[91m기사\u001b[0m")
                                                       .Replace("형드리", $"\u001b[91m형드리\u001b[0m")
                                                       .Replace("단장", $"\u001b[91m단장\u001b[0m")
                                                       .Replace("선생님", $"\u001b[91m선생님\u001b[0m")
                                                       .Replace("마녀", $"\u001b[95m마녀\u001b[0m")
                                                       .Replace("할머니", $"\u001b[95m할머니\u001b[0m")
                                                       .Replace("고블린", $"\u001b[92m고블린\u001b[0m")
                                                       .Replace("축구공", $"\u001b[92m축구공\u001b[0m")
                                                       .Replace("트롤", $"\u001b[92m트롤\u001b[0m")
                                                       .Replace("야구공", $"\u001b[92m야구공\u001b[0m")
                                                       .Replace("상어", $"\u001b[91m상어\u001b[0m")
                                                       .Replace("케르베로스", $"\u001b[91m케르베로스\u001b[0m")
                                                       .Replace("무서운개", $"\u001b[91m무서운개\u001b[0m")
                                                       .Replace("용암", $"\u001b[94m용암\u001b[0m")
                                                       .Replace("경찰과 도둑 놀이", $"\u001b[91m경찰과 도둑 놀이\u001b[0m")
                                                       .Replace("횡단보도", $"\u001b[94m횡단보도\u001b[0m")
                                                       .Replace("형누나", $"\u001b[95m형누나\u001b[0m")
                                                       .Replace("공허의 마스터", $"\u001b[95m공허의 마스터\u001b[0m")
                                                       .Replace("불의 여왕", $"\u001b[91m불의 여왕\u001b[0m")
                                                       .Replace("그림자 마법사", $"\u001b[90m그림자 마법사\u001b[0m")
                                                       .Replace("무자비한 검사", $"\u001b[91;1m무자비한 검사\u001b[0m"))
                                                       .ToArray();


                        if (filename != "Ending2")
                        {
                                ConsoleHelper.SetCurrentFont("빛의 계승자 Bold", 30);
                                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                                Console.WindowWidth = Console.LargestWindowWidth;
                                Console.WindowHeight = Console.LargestWindowHeight;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"[ {filename} ]");
                                Console.ResetColor();
                        }
                        else
                        {
                                yCount = 0;
                                ConsoleHelper.SetCurrentFont("UhBee Rami BOLD", 33);
                                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                                Console.WindowWidth = Console.LargestWindowWidth;
                                Console.WindowHeight = Console.LargestWindowHeight;
                        }
                        foreach (string line in readTxt)
                        {
                                if (line.Contains("주인공") ||
                                    line.Contains("경비병") ||
                                    line.Contains("기사") ||
                                    line.Contains("단장") ||
                                    line.Contains("마녀") ||
                                    line.Contains("고블린") ||
                                    line.Contains("트롤") ||
                                    line.Contains("상어") ||
                                    line.Contains("축구공") ||
                                    line.Contains("야구공") ||
                                    line.Contains("선생님") ||
                                    line.Contains("엄마") ||
                                    line.Contains("반장") ||
                                    line.Contains("할머니") ||
                                    line.Contains("횡단보도") ||
                                    line.Contains("형드리") ||
                                    line.Contains("무서운개") ||
                                    line.Contains("케르베로스왕") ||
                                    line.Contains("공허의 마스터") ||
                                    line.Contains("그림자 마법사") ||
                                    line.Contains("경찰과 도둑 놀이") ||
                                    line.Contains("무자비한 검사")) Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length + 10, yCount++);
                                else Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length, yCount++);
                                foreach (char cha in line)
                                {
                                        Console.Write(cha);
                                        if (!Console.KeyAvailable) Thread.Sleep(40);
                                }
                                Console.WriteLine();
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

    // 퀘스트 디스플레이 화면
    class QuestScreen
    {
        Quest quest;

        public QuestScreen(Quest quest)
        {
            this.quest = quest;
        }

        public void DisplayQuestList()
        {
            Console.Clear();
            quest.DisplayQuest();
        }
    }
}



