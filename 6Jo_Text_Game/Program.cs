using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;



// 게임 데이터를 저장하는 싱글톤 클래스 정의 
public class GameData
{
    private static GameData Instance;

    // 몬스터 및 캐릭터를 저장하는 데이터 구조
    private List<Monster> monsters;
    private List<Character> characters;
    private List<IItem> items;
    private List<Skill> skills;

    // 인스턴스 생성을 위한 private 생성자
    private GameData()
    {
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

        StartScreen startScreen = new StartScreen();

        new Monster();
        Character character = new Character("ㅇㅇ", 100, 100, 100, 10, 100, 200, Job.가디언, 10, 10, Attribute.풍, 0, 100);
        BattleEvent battleEvent = new BattleEvent(character);
        ScreenManager screenManager = new ScreenManager(character, battleEvent);
        character.Add();
        GameData.I.GetCharacters().Select(x => x.ToString()).ToList().ForEach(Console.WriteLine);
        screenManager.Prologue();
        screenManager.ShowMainScreen();

    }
}
public class StartScreen
{

    private void StartScreenText()
    {
        int y = 1;
        /*
         . (점): 현재 디렉토리를 나타냅니다.
         ..(점 두 개): 상위 디렉토리를 나타냅니다.
         / (슬래시): 디렉토리를 구분합니다.
        */
        string directory = "../../../StartScreenText.txt";
        string directory2 = "../../../StartScreenText(원본).txt";
        try
        {
            string[] fileContents = File.ReadAllLines(directory, Encoding.UTF8);
            foreach (string line in fileContents)
            {


                Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length, 3);
                Console.Write(line);
                if (!Console.KeyAvailable) Thread.Sleep(450);
                else break;
                Console.Clear();
            }
            Console.Clear();
            string[] fileContents2 = File.ReadAllLines(directory2, Encoding.UTF8);
            foreach (string line in fileContents2)
            {


                Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length, y);
                Console.Write(line);
                y++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading the file: {ex.Message}");
        }

    }
    public void CharName(out string charName)
    {
        foreach (var item in "\n당신의 이름을 입력하십시오. ")
        {
            Console.Write(item);
            Thread.Sleep(50);
        };
        while (true)
        {
            Console.Write(">> ");
            charName = Console.ReadLine();
            if (!(charName == ""))
            {
                break;
            }
        }
    }
    public void CharJob(out Job? job)
    {
        int y = 1;
        Console.Clear();
        string directory = "../../../StartScreenJobText.txt";
        try
        {
            string[] fileContents = File.ReadAllLines(directory, Encoding.UTF8);
            foreach (string line in fileContents)
            {

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

        Console.Write("\n당신은 어떠한 무기를 선택하실 겁니까?  \n>> ");
        int input = CheckValidInput(1, 4);

        switch (input)
        {
            case 1:
                job = Job.가디언;
                break;
            case 2:
                job = Job.위자드;
                break;
            case 3:
                job = Job.레인저;
                break;
            case 4:
                job = Job.성직자;
                break;
            default:
                job = null;
                break;
        }

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
        Character character = new Character(charName, 1, 10, 10, 10, 100, 500, charJob, 10, 10, Attribute.풍, 0, 100);
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
// 게임 화면을 콘솔에 그리는 역할을 하는 Renderer 클래스입니다.
public static class Renderer
{
    // 콘솔 창의 높이와 길이를 저장하는 변수들입니다.
    private static readonly int inputAreaHeight = 1;
    private static readonly string inputAreaString = ">> ";
    private static readonly int printMargin = 2;

    private static int width;
    private static int height;

    // 게임 화면 초기화 메서드입니다.
    public static void Initialize(string gameName)
    {
        Console.Title = gameName;
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
    }

    // 게임 화면에 테두리를 그리는 메서드입니다.
    public static void DrawBorder(string title = "")
    {
        Console.Clear();
        width = Console.WindowWidth;
        height = Console.WindowHeight;

        Console.SetCursorPosition(0, 0);
        Console.Write(new string('=', width));

        for (int i = 1; i < height - inputAreaHeight - 2; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write('║');
            Console.SetCursorPosition(width - 1, i);
            Console.Write('║');
        }

        if (!string.IsNullOrEmpty(title))
        {
            Console.SetCursorPosition(0, 2);
            Console.Write(new string('=', width));
            int correctLength = GetPrintingLength(title);
            int horizontalStart = (width - correctLength) / 2;
            if (horizontalStart < 0) horizontalStart = 3;
            Console.SetCursorPosition(horizontalStart, 1);
            Console.WriteLine(title);
        }

        Console.SetCursorPosition(0, height - inputAreaHeight - 2);
        Console.Write(new string('=', width));
    }

    // 문자열의 출력 길이를 계산하는 메서드입니다.
    private static int GetPrintingLength(string line) => line.Sum(c => IsKorean(c) ? 2 : 1);

    // 주어진 문자가 한글인지 확인하는 메서드입니다.
    private static bool IsKorean(char c) => '가' <= c && c <= '힣';

    // 화면 중앙에 문자열을 출력하는 메서드입니다.
    public static void PrintCenter(string[] lines)
    {
        int textHeight = lines.Length;
        int verticalStart = (height - textHeight) / 2;

        for (int i = 0; i < lines.Length; i++)
        {
            int correctLength = GetPrintingLength(lines[i]);
            int horizontalStart = (width - correctLength) / 2;
            if (horizontalStart < 0) horizontalStart = 0;
            Console.SetCursorPosition(horizontalStart, verticalStart + i);
            Console.WriteLine(lines[i]);
        }
    }

    // 지정된 행에 문자열을 출력하는 메서드입니다.
    public static void Print(int line, string content)
    {
        Console.SetCursorPosition(printMargin, line);
        Console.WriteLine(content);
    }

    // 화면에 테이블을 그리는 메서드입니다.
    public static void DrawTable(Table table, int startLine, int selectedRow)
    {
        var types = table.GetTypes();
        int currentLine = startLine;

        // 헤더 행을 그립니다.
        StringBuilder header = new StringBuilder("| ");
        foreach (var type in types)
        {
            header.Append(type.name.PadRight(type.length));
            header.Append(" | ");
        }
        Print(currentLine++, header.ToString());

        // 수평 선을 그립니다.
        StringBuilder horizontalLine = new StringBuilder("+");
        foreach (var type in types)
        {
            horizontalLine.Append(new string('-', type.length + 2));
            horizontalLine.Append("+");
        }
        Print(currentLine++, horizontalLine.ToString());

        // 데이터 행을 그립니다.
        int dataCount = table.GetDataCount();
        for (int i = 0; i < dataCount; i++)
        {
            StringBuilder row = new StringBuilder("| ");
            if (i == selectedRow)
            {
                row.Append("▶");
            }
            else
            {
                row.Append("  ");
            }
            var rowData = table.GetRow(i);
            for (int j = 0; j < types.Length; j++)
            {
                string data = rowData[j];
                row.Append(data.PadRight(types[j].length));
                row.Append(" | ");
            }
            Print(currentLine++, row.ToString());
        }
    }

    // 화면에 입력 영역을 그리는 메서드입니다.
    public static void DrawInputArea()
    {
        Console.SetCursorPosition(printMargin, height - inputAreaHeight - 1);
        Console.Write(inputAreaString);
    }
}

// 테이블의 데이터 타입을 정의하는 구조체입니다.
public struct TableDataType
{
    public string name;
    public int length;
    public bool center;

    public TableDataType(string name, int length, bool center = false)
    {
        this.name = name;
        this.length = length;
        this.center = center;
    }
}

// 테이블을 나타내는 클래스입니다.
public class Table
{
    private Dictionary<string, TableDataType> dataTypes = new();
    private Dictionary<string, List<string>> datas = new();

    // 데이터 타입을 추가하는 메서드입니다.
    public bool AddType(string name, int length, bool center = false)
    {
        if (dataTypes.ContainsKey(name))
            return false;

        dataTypes[name] = new TableDataType(name, length, center);
        return true;
    }

    // 데이터를 추가하는 메서드입니다.
    public void AddData(string name, string content)
    {
        if (!datas.TryGetValue(name, out List<string>? list))
        {
            list = new List<string>();
            datas[name] = list;
        }

        list.Add(content);
    }

    // 모든 데이터 타입을 가져오는 메서드입니다.
    public TableDataType[] GetTypes() => dataTypes.Values.ToArray();

    // 지정된 행의 데이터를 가져오는 메서드입니다.
    public string[] GetRow(int row)
    {
        string[] result = new string[dataTypes.Count];
        int index = 0;

        foreach (string key in dataTypes.Keys)
        {
            result[index] = datas[key][row];
            index++;
        }

        return result;
    }

    // 데이터의 행 수를 가져오는 메서드입니다.
    public int GetDataCount() => datas.First().Value.Count;
}
public static class ConsoleHelper
{
    private const int FixedWidthTrueType = 54;
    private const int StandardOutputHandle = -11;

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern bool GetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool MaximumWindow, ref FontInfo ConsoleCurrentFontEx);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD size);

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;
    }

    private static readonly IntPtr ConsoleOutputHandle = GetStdHandle(StandardOutputHandle);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FontInfo
    {
        internal int cbSize;
        internal int FontIndex;
        internal short FontWidth;
        public short FontSize;
        public int FontFamily;
        public int FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FontName;
    }

    public static FontInfo GetCurrentFont()
    {
        FontInfo before = new FontInfo
        {
            cbSize = Marshal.SizeOf<FontInfo>()
        };

        if (!GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref before))
        {
            var er = Marshal.GetLastWin32Error();
            throw new System.ComponentModel.Win32Exception(er);
        }
        return before;
    }

    public static FontInfo[] SetCurrentFont(string font, short fontSize = 0)
    {
        FontInfo before = new FontInfo
        {
            cbSize = Marshal.SizeOf<FontInfo>()
        };

        if (GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref before))
        {
            FontInfo set = new FontInfo
            {
                cbSize = Marshal.SizeOf<FontInfo>(),
                FontIndex = 0,
                FontFamily = FixedWidthTrueType,
                FontName = font,
                FontWeight = 400,
                FontSize = fontSize > 0 ? fontSize : before.FontSize
            };

            if (!SetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref set))
            {
                var ex = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(ex);
            }

            // Reset console buffer size and window size
            SetConsoleScreenBufferSize(ConsoleOutputHandle, new COORD { X = 80, Y = 300 });

            FontInfo after = new FontInfo
            {
                cbSize = Marshal.SizeOf<FontInfo>()
            };
            GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref after);

            return new[] { before, set, after };
        }
        else
        {
            var er = Marshal.GetLastWin32Error();
            throw new System.ComponentModel.Win32Exception(er);
        }
    }
}