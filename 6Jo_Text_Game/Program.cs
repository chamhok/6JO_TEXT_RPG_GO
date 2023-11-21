using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;
using static ConsoleHelper;
using System.Numerics;



// 게임 데이터를 저장하는 싱글톤 클래스 정의 
public class GameData
{
    private static GameData Instance;

    // 몬스터 및 캐릭터를 저장하는 데이터 구조
    private Monster monster;
    private List<Monster> monsters;
    private List<Character> characters;
    private List<IItem> items;
    private List<Skill> skills;

    // 인스턴스 생성을 위한 private 생성자
    private GameData()
    {
        monster = new Monster();
        monsters = new List<Monster>();
        characters = new List<Character>();
        items = new List<IItem>();
        skills = new List<Skill>();

        InitializeData(); // 필요시 데이터 초기화 가능
    }

    // 싱글톤 인스턴스에 접근하는 public 메서드
    public static GameData I
    {
        get
        {
            if (Instance == null)
            {
                Instance = new GameData();
            }
            return Instance;
        }
    }

    // 몬스터 및 캐릭터와 상호작용하기 위한 메서드들
    public void AddMonster(Monster monster)
    {
        monsters.Add(monster);
    }

    public void RemoveMonster(Monster monster)
    {
        monsters.RemoveAll(x => x.Name == monster.Name);
    }

    public List<Monster> GetMonsters(Character player)
    {
        monsters.Clear();
        monster.StageMonster(player.WinCount);
        return monsters;
    }
    public List<Monster> GetMonsters()
    {
        return monsters;
    }

    public void AddCharacter(Character character)
    {
        characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        characters.RemoveAll(x => x.Name == character.Name);
    }

    public List<Character> GetCharacters()
    {
        return characters;
    }

    public void AddItem(IItem item)
    {
        items.Add(item);
    }

    public void RemoveItem(IItem item)
    {
        items.RemoveAll(x => x.Name == item.Name);
    }

    public List<IItem> GetItem()
    {
        return items;
    }

    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        skills.RemoveAll(x => x.Name == skill.Name);
    }

    public List<Skill> GetSkill()
    {
        return skills;
    }
    // 필요하다면 더 많은 메서드를 추가할 수 있음

    private void InitializeData()
    {
        // 필요시 몬스터 및 캐릭터 초기화
    }
}

// 캐릭터를 나타내는 인터페이스
public interface ICharacter
{
    string Name { get; }

    void TakeDamage(float damage) { }
}

// 아이템을 나타내는 인터페이스
public interface IItem
{
    string Name { get; }
    void Use(Character character) { }
}

//상점을 나타내는 인터페이스
public interface IStore
{
    string Name { get; }

    void Use(Store store) { }
}

// 스킬을 나타내는 클래스
public class Skill
{
        public string Name { get; }
        public float Attack { get; set; }
        public Job Job { get; set; }
        public Attribute Attribute { get; private set; }
        // public int Level { get; set; }

        public Skill()
        {
                Add(new Skill("스킬1", 1.3f, Job.가디언, Attribute.토));
                Add(new Skill("스킬2", 1.3f, Job.가디언, Attribute.풍));
                Add(new Skill("스킬3", 1.3f, Job.레인저, Attribute.수));
                Add(new Skill("스킬4", 1.3f, Job.레인저, Attribute.화));
                Add(new Skill("스킬5", 1.3f, Job.성직자, Attribute.수));
                Add(new Skill("스킬6", 1.3f, Job.성직자, Attribute.토));
                Add(new Skill("스킬7", 1.3f, Job.위자드, Attribute.화));
                Add(new Skill("스킬8", 1.3f, Job.위자드, Attribute.풍));
        }

        public Skill(string name, float attack, Job job, Attribute attribute)//, int level
        {
                this.Name = name;
                this.Attack = attack;
                this.Job = job;
                this.Attribute = attribute;
                //this.Level = level;
        }

    public void Add(Skill skill)
    {
        GameData.I.AddSkill(skill);
    }

    public void Add()
    {
        GameData.I.AddSkill(this);
    }
}

// 속성을 나타내는 열거형
public enum Attribute
{
    화,
    수,
    토,
    풍
}

// 직업을 나타내는 열거형
public enum Job
{
    가디언,
    위자드,
    레인저,
    성직자
}

// 종족을 나타내는 열거형
public enum Species
{
        기믹,
        고블린,
        가고일,
        코볼트,
        드래곤
}

// 프로그램 진입점 클래스
class Program
{
    static void Main()
    {

        /*  // 캐릭터 생성 및 게임 데이터에 추가 후 목록 출력
            Character character = new Character("ㅇㅇ", 100, 100, 100, 100, 100, 200, Job.가디언,10,10, Attribute.풍);
            character.Add(new Character("ㅇㅇ", 100, 100, 100, 100, 100, 200, Job.가디언, 10, 10, Attribute.풍));
            Monster monster = new Monster("Test", 1, 1, 1, 1, 1, 1, Species.고블린,1,1, Attribute.풍);
            monster.Add(new Monster("Test", 1, 1, 1, 1, 1, 1, Species.고블린, 1, 1, Attribute.풍));
            Monster monsterb = new Monster("Test2", 1000, 1000, 1000, 1000, 1000, 1000, Species.고블린,1,1, Attribute.풍);
            monster.Add(new Monster("Test2", 1000, 1000, 1000, 1000, 1000, 1000, Species.고블린, 1, 1, Attribute.풍));
            BattleEvent battleEvent = new BattleEvent(character);
            ScreenManager screenManager = new ScreenManager(character, battleEvent);
            character.Add();
            GameData.I.GetCharacters().Select(x => x.ToString()).ToList().ForEach(Console.WriteLine);
            screenManager.Prologue();
            Console.ReadKey();
            screenManager.ShowMainScreen();*/
        SoundManager soundManager = new SoundManager();
        soundManager.PlayBackgroundMusicAsync("Raindrop");
        StartScreen startScreen = new StartScreen();
        soundManager.StopMusic();
        Character character = new Character("ㅇㅇ", 100, 100, 100, 10, 100, 200, Job.가디언, 10, 10, Attribute.풍, 0, 100);
        BattleEvent battleEvent = new BattleEvent(character);
        Store store = new Store();
        ScreenManager screenManager = new ScreenManager(character, battleEvent,store);
        character.Add();
        GameData.I.GetCharacters().Select(x => x.ToString()).ToList().ForEach(Console.WriteLine);
        screenManager.Prologue();
        screenManager.ShowMainScreen();

    }
}
public class StartScreen
{
        SoundManager soundManager = new SoundManager();
        int y = 1;
        FontInfo originalFont = ConsoleHelper.GetCurrentFont();

        private void StartScreenText()
        {
        /*
         . (점): 현재 디렉토리를 나타냅니다.
         ..(점 두 개): 상위 디렉토리를 나타냅니다.
         / (슬래시): 디렉토리를 구분합니다.
        */
                string directory = "../../../StartScreenText.txt";
                string directory2 = "../../../StartScreenText(원본).txt";
                try
                {
                        ConsoleHelper.SetCurrentFont("EBS주시경 Medium", 100);
                        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                        Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                        Console.WindowWidth = Console.LargestWindowWidth;
                        Console.WindowHeight = Console.LargestWindowHeight;
                        string[] fileContents = File.ReadAllLines(directory, Encoding.UTF8);
                        Console.ForegroundColor = ConsoleColor.Red;
                        foreach (string line in fileContents)
                        {
                                Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length, 4);
                                Console.Write(line);
                                if (!Console.KeyAvailable) Thread.Sleep(450);
                                else break;
                                Console.Clear();
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.WriteLine("x를 누르십시오.");
                        while (true)
                        {
                                ConsoleKeyInfo key = Console.ReadKey();
                                if (key.KeyChar == 'x' || key.KeyChar == 'ㅌ')
                                {
                                soundManager.CallSound("sound1", 100);
                                 break;
                                }
                        }
                        Console.Clear();

                        ConsoleHelper.SetCurrentFont("EBS주시경 Medium", 30);
                        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                        Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                        Console.WindowWidth = Console.LargestWindowWidth;
                        Console.WindowHeight = Console.LargestWindowHeight;

                        string[] fileContents2 = File.ReadAllLines(directory2, Encoding.UTF8);
                        foreach (string line in fileContents2)
                        {

                                // 두 번째 foreach 루프에서 좌표 설정 부분 수정
                                int left2 = Math.Max((Console.WindowWidth / 2) - line.Length, 0);
                                Console.SetCursorPosition(left2, y);
                                Console.Write(line);
                                y++;
                        }
                }
                catch (Exception ex)
                {
                        Console.WriteLine($"Error reading the file: {ex.Message}");
                }
                Console.SetCursorPosition((Console.WindowWidth) / 2 - "x를 누르시오.".Length, y + 5);
                Console.WriteLine("x를 누르시오.");
                while (true)
                {
                        ConsoleKeyInfo key = Console.ReadKey();
                        if (key.KeyChar == 'x' || key.KeyChar == 'ㅌ')
                        {
                        soundManager.CallSound("sound1", 100);
                        break;
                        }
                }

        }
        public void CharName(out string charName)
        {
                Console.Clear();
                ConsoleHelper.SetCurrentFont("EBS주시경 Medium", 100);
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.WindowWidth = Console.LargestWindowWidth;
                Console.WindowHeight = Console.LargestWindowHeight;
                foreach (var item in "\n당신의 이름을 입력하십시오. ")
                {
                        Console.Write(item);
                        Thread.Sleep(50);
                };
                Console.WriteLine();
                while (true)
                {
                        Console.Write(">> ");
                        charName = Console.ReadLine();
                        if (!(charName == ""))
                        {
                        soundManager.CallSound("sound1", 100);
                        break;
                        }
                }
        }
        public void CharJob(out Job? job)
        {
                int y = 1;
                Console.Clear();
                ConsoleHelper.SetCurrentFont("EBS주시경 Medium", 30);
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.WindowWidth = Console.LargestWindowWidth;
                Console.WindowHeight = Console.LargestWindowHeight;
                string directory = "../../../StartScreenJobText.txt";
                try
                {
                        string[] fileContents = File.ReadAllLines(directory, Encoding.UTF8);

                        foreach (string line in fileContents)
                        {
                                Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length, y);
                                foreach (char cha in line)
                                {
                                        Console.Write(cha);
                                        if (!Console.KeyAvailable) Thread.Sleep(50);
                                }
                                Console.WriteLine();
                                y++;
                        }

                }
                catch (Exception ex)
                {
                        Console.WriteLine($"Error reading the file: {ex.Message}");
                }
                Console.WriteLine();
                Console.WriteLine("x를 누르십시오");
                while (true)
                {
                        ConsoleKeyInfo key = Console.ReadKey();
                        if (key.KeyChar == 'x' || key.KeyChar == 'ㅌ')
                        {
                            soundManager.CallSound("sound1", 100);
                            break;
                        }
                }
                Console.Clear();
                ConsoleHelper.SetCurrentFont(originalFont.ToString(), 30);
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.WindowWidth = Console.LargestWindowWidth;
                Console.WindowHeight = Console.LargestWindowHeight;
                // 게임 화면 초기화 및 테이블 설정
                Renderer.Initialize("");
                // 테두리와 테이블 그리기
                Renderer.DrawBorder("무기 선택");
                Renderer.Print(3, "무기를 선택해주세요! 당신의 직업을 결정합니다");
                // 입력 영역 그리기
                Renderer.DrawInputArea();
                // 테이블에 데이터 추가
                Table table = new Table();
                table.AddType("index", 8, false);
                table.AddType("name", 14, false);
                table.AddType("class", 14, false);
                table.AddType("Description", 40, false);

                // 무기 1 추가
                table.AddData("index", "1");
                table.AddData("name", "루미나소드");
                table.AddData("class", Job.가디언.ToString());
                table.AddData("Description", "투명한 검신에는 검은 일자문양이 나있다.");

                // 무기 1 추가
                table.AddData("index", "2");
                table.AddData("name", "리코더러스");
                table.AddData("class", Job.위자드.ToString());
                table.AddData("Description", "지팡이에서 나오는 소리는 음악과도 같다.");
                // 무기 1 추가
                table.AddData("index", "3");
                table.AddData("name", "고므즈르");
                table.AddData("class", Job.레인저.ToString());
                table.AddData("Description", "활의 탄성은 마치 슬라임과 비견된다.");
                // 무기 1 추가
                table.AddData("index", "4");
                table.AddData("name", "도오더그성서");
                table.AddData("class", Job.성직자.ToString());
                table.AddData("Description", "읽기만 해도 도덕적인 사람이 될 수 있다.");

            
                int currentSelection = 0;
                Console.WriteLine(table.GetDataCount());


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
                                        case 0:
                                                job = Job.가디언;
                                                break;
                                        case 1:
                                                job = Job.위자드;
                                                break;
                                        case 2:
                                                job = Job.레인저;
                                                break;
                                        case 3:
                                                job = Job.성직자;
                                                break;
                                        default:
                                                job = null;
                                                break;
                                }
                                break;
                        }

                }
                soundManager.CallSound("driring", 100);



    }
        public StartScreen()
        {
                /* var info = ConsoleHelper.GetCurrentFont();


                 Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

                 // Set the console buffer size to the same dimensions
                 Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);*/
                Console.ReadLine();

        string charName;
        Job? charJob;
        StartScreenText();

        CharName(out charName);

        CharJob(out charJob);

                Console.Clear();
                Character character = new Character(charName, 1, 10, 10, 10, 100, 500, charJob, 10, 10, Attribute.풍,0,10);
                character.Add();

                // 게임 화면 초기화 및 테이블 설정
                Renderer.Initialize("게임 제목");
                // 테두리와 테이블 그리기
                Renderer.DrawBorder("캐릭터 선택");
                Renderer.Print(3, "플레이할 캐릭터를 선택해주세요!");
                // 입력 영역 그리기
                Renderer.DrawInputArea();
                // 테이블에 데이터 추가
                Table table = new Table();
                table.AddType("index", 7, false);
                table.AddType("name", 20, false);
                table.AddType("class", 20, false);
                table.AddType("hp", 15, false);
                table.AddType("damage", 15, false);
                table.AddType("defense", 15, false);

                table.AddData("index", "1");
                table.AddData("name", $"{character.Name}");
                table.AddData("class", $"{character.Job}");
                table.AddData("hp", $"{character.Health}");
                table.AddData("damage", $"{character.Attack}");
                table.AddData("defense", $"{character.Defense}");


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
                        else if (key.Key == ConsoleKey.DownArrow && currentSelection < table.GetDataCount() - 1)
                        {
                                currentSelection++;
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                                // 여기서 선택된 행에 대한 처리를 수행합니다.
                                break;
                        }
                }
                Console.Clear();
                ConsoleHelper.SetCurrentFont("EBS주시경 Medium", 30);
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.WindowWidth = Console.LargestWindowWidth;
                Console.WindowHeight = Console.LargestWindowHeight;

    }
    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine() ?? " ";

                        bool parseSuccess = int.TryParse(input, out var ret);
                        if (parseSuccess)
                        {
                                if (ret >= min && ret <= max)
                                        return ret;
                        }
                }

        }
}
