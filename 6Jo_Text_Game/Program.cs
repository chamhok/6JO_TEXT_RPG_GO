﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;



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

    public void RemoveItem(Item item)
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

    public Attribute Attribute { get; private set; }

    public Skill() { }

    public Skill(string name, float attack, Attribute attribute)
    {
        this.Name = name;
        this.Attack = attack;
        this.Attribute = attribute;
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

        //StartScreen startScreen = new StartScreen();

        new Monster();
        Character character = new Character("ㅇㅇ", 100, 100, 100, 10, 100, 200, Job.가디언, 10, 10, Attribute.풍);
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
        try
        {
            string[] fileContents = File.ReadAllLines(directory, Encoding.UTF8);
            foreach (string line in fileContents)
            {


                Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length, 3);
                Console.Write(line);
                if (!Console.KeyAvailable) Thread.Sleep(450);


            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading the file: {ex.Message}");
        }
        Console.Clear();

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
        /*var info = ConsoleHelper.GetCurrentFont();

        Console.SetWindowPosition(0, 0);
        ConsoleHelper.SetCurrentFont("양재블럭체", 100);

        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

        // Set the console buffer size to the same dimensions
        Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);*/
        Console.ReadLine();

        string charName;
        Job? charJob;
        StartScreenText();
        // ConsoleHelper.SetCurrentFont("양재블럭체", 50);

        CharName(out charName);

        CharJob(out charJob);


        Character character = new Character(charName, 1, 10, 10, 10, 100, 500, charJob, 10, 10, Attribute.풍);
        character.Add();
        Console.WriteLine($"\n당신의 이름은 {character.Name}입니다.");
        Console.WriteLine($"\n당신의 직업은 {character.Job}입니다.");


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

            // Get some settings from current font.
            if (!SetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref set))
            {
                var ex = Marshal.GetLastWin32Error();
                throw new System.ComponentModel.Win32Exception(ex);
            }

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