﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;
using static ConsoleHelper;
using System.Collections;



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
        private static int _itemsTopPostion = 7;

        private static int _categoryTopPostion = 3;

        private  static int _goldLeftPostion = 64;

        private static int _goldTopPostion = 3;
        public static void PrintGold()
        {
                var currentCursor = Console.GetCursorPosition();

                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion);
                Console.Write("┌─────────────────────────┐");
                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion + 1);
                Console.Write($"│ 소지금│ {GameData.I.GetCharacters().First().Gold.ToString().PadLeft(14, ' ')} G│");
                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion + 2);
                Console.Write("┴─────────────────────────┴");

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
        }

        public static void PrintDef()
        {
                var currentCursor = Console.GetCursorPosition();
                var player = GameData.I.GetCharacters().First();

                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion);
                Console.Write("┌─────────────────────────┐");
                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion + 1);
                Console.Write($"│ 방어력│ {player.Defense + 50 }");
                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion + 2);
                Console.Write("┴─────────────────────────┤");

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
        }
        public static void PrintLevel()
        {
                Console.Clear();

                var currentCursor = Console.GetCursorPosition();
                var player = GameData.I.GetCharacters().First();

                int fillExpBar = (int)(8 * (float)player.CurrentExp / (player.Level * player.Level * 100));
                if (fillExpBar >= 8) fillExpBar = 8;

                Console.SetCursorPosition(0, 3);
                Console.Write("┌───────┬─────────────────┐");
                Console.SetCursorPosition(0, 3 + 1);
                Console.Write($"│ Lv {player.Level.ToString().PadLeft(3, ' ')}│ ");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write("".PadRight(fillExpBar, '　'));
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write("".PadRight(8 - fillExpBar, '　'));
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.SetCursorPosition(0, 3 + 2);
                Console.Write("├───────┴─────────────────┴");

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
        }
        private static void MakeUIContainer(int left, int top, int right, int bottom)
        {   //┌ ─ ┐ └ ┘ │
                if (left < 0 || top < 0 || --right > Console.WindowWidth || --bottom > Console.WindowHeight) return;

                Console.SetCursorPosition(left, top);
                Console.Write("┏".PadRight(right - left - 1, '━'));
                Console.Write("┓");

                for (int i = top + 1; i < bottom; i++)
                {
                        Console.SetCursorPosition(left, i);
                        Console.Write("┃".PadRight(right - left - 1, ' '));

                        Console.SetCursorPosition(right - 1, i);
                        Console.Write("┃");
                }


                Console.SetCursorPosition(left, bottom);
                Console.Write("┗".PadRight(right - left - 1, '━'));
                Console.Write("┛");
        }
        public static void MakeTab()
        {
                var currentCursor = Console.GetCursorPosition();

                int left = 2, top = 1, right = 90, bottom = 28;
                MakeUIContainer(left, top, right, bottom);

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
        }
        public static void PrintSkills()
        {
                var currentCursor = Console.GetCursorPosition();

                var skillList = GameData.I.GetSkill();

                Console.SetCursorPosition(5, 2);
                Console.Write("SKILL");

                Console.SetCursorPosition(80, 2);
                Console.Write("x. 닫기");
                Console.SetCursorPosition(4, 3);
                Console.Write("┏━━━━━━━━━━━━━━━┳━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");

                Console.SetCursorPosition(4, 4);
                Console.Write("┃   스킬명      ┃ 소모 마나┃                        스킬 설명                     ┃");

                Console.SetCursorPosition(4, 5);
                Console.Write("┗━━━━━━━━━━━━━━━┻━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

                for (int i = 0; i < skillList.Count && i < 21; i++)
                {
                        Skill skill = skillList[i];
                        Console.SetCursorPosition(4, 6 + i);
                        Console.Write($" {(i + 1).ToString().PadRight(4, ' ')}{skill.Name}");
                        Console.SetCursorPosition(20, 6 + i);
                        Console.Write($"ㅣ{"ddd".PadLeft(9, ' ')}ㅣ");
                        Console.Write($"{"뭐 어쨋드,ㄴ 졸라쌥니다 시바".PadLeft(20, ' ')} ");
                        int value = 0;
                      /*  switch (skill.ValueType)
                        {
                                case ValueTypeEnum.PROPOTIONAL:
                                        value = (int)(GameManager.Instance.DataManager.Player.GetStatValue(skill.Stat) * skill.Value / 100f);
                                        break;
                                case ValueTypeEnum.FIXED:
                                        value = skill.Value;
                                        break;
                        }*/

                       /* string aoe = skill.IsAoE ? "(광역기)" : "";
                        switch (skill.SkillType)
                        {
                                case SkillType.DAMAGE:
                                        if (skill.Duration > 1)
                                        {
                                                Console.Write($"{aoe}{skill.Duration}턴 동안 ");
                                                if (value > 0)
                                                {
                                                        Console.Write($"대상의 체력을 {value}씩 회복합니다.");
                                                }
                                                else if (value <= 0)
                                                {
                                                        Console.Write($"대상에게 {-value}의 지속 피해를 입힙니다.");
                                                }
                                        }
                                        else
                                        {
                                                if (value > 0)
                                                {
                                                        Console.Write($"{aoe}대상의 체력을 {value}만큼 회복합니다.");
                                                }
                                                else if (value <= 0)
                                                {
                                                        Console.Write($"{aoe}대상에게 {-value}의 피해를 입힙니다.");
                                                }
                                        }
                                        break;
                                case SkillType.BUFF:
                                        string str = "";
                                        switch (skill.Stat)
                                        {
                                                case Stats.MAXHP:
                                                        str = "체력";
                                                        break;
                                                case Stats.MAXMP:
                                                        str = "마나";
                                                        break;
                                                case Stats.ATK:
                                                        str = "공격력";
                                                        break;
                                                case Stats.DEF:
                                                        str = "방어력";
                                                        break;
                                                case Stats.CRITICALCHANCE:
                                                        str = "치명률";
                                                        break;
                                                case Stats.CRITICALDAMAGE:
                                                        str = "치명타";
                                                        break;
                                                case Stats.DODGECHANCE:
                                                        str = "회피율";
                                                        break;

                                        }
                                        if (value > 0)
                                        {
                                                Console.Write($"{aoe}대상의 {str}을(를) {skill.Duration}턴 동안 {value}만큼 증가시킵니다.");
                                        }
                                        else if (value <= 0)
                                        {
                                                Console.Write($"{aoe}대상의 {str}을(를) {skill.Duration}턴 동안 {value}만큼 감소시킵니다.");
                                        }
                                        break;
                        }*/
                }
                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
        }
        public void ShowMonsterCard(List<Monster> monsters)
        {
                var currentCursor = Console.GetCursorPosition();
                int size = monsters.Count;
                int top = 6, bottom = 19;
                int width = 21;

                List<int> leftPosition = new List<int>();

                switch (size)
                {
                        case 1:
                                leftPosition.Add(36);
                                break;
                        case 2:
                                leftPosition.Add(15);
                                leftPosition.Add(61);
                                break;
                        case 3:
                                leftPosition.Add(5);
                                leftPosition.Add(35);
                                leftPosition.Add(65);
                                break;
                        case 4:
                                leftPosition.Add(2);
                                leftPosition.Add(24);
                                leftPosition.Add(46);
                                leftPosition.Add(68);
                                break;
                }

                for (int i = 0; i < leftPosition.Count; i++)
                {
                        // 몬스터 카드 틀 생성
                        Console.ResetColor();
                        MakeUIContainer(leftPosition[i], top, leftPosition[i] + width, bottom);
                }

                for (int i = 0; i < size; i++)
                {
                        // 몬스터 Info 출력
                        Monster monster = monsters[i];

                        Console.SetCursorPosition(leftPosition[i] + 9, top + 1);
                        Console.Write($"Lv{monster.Level}");
                        Console.SetCursorPosition(leftPosition[i] + 2, top + 2);
                        int paddingSize = (17 - monster.Name.Length * 2) / 2;
                        if (monster.Name.IndexOf(' ') > 0)
                                paddingSize++;
                        Console.Write("".PadLeft(paddingSize, ' ') + monster.Name);

                        int fillHpBar = (int)(7 * (float)monster.Health / monster.Health + 0.9f);//현재 체력과 최대 체력이 필요한데 그냥 이렇게함
                        if (fillHpBar >= 7) fillHpBar = 7;

                        Console.SetCursorPosition(leftPosition[i] + 2, top + 4);
                        string atkString = $"공격력 : {monsters[i].Attack}";
                        paddingSize = (17 - (atkString.Length + 3)) / 2;
                        Console.Write("".PadLeft(paddingSize, ' ') + atkString);
                        Console.SetCursorPosition(leftPosition[i] + 2, top + 5);
                        string defString = $"방어력 : {monsters[i].Defense}";
                        paddingSize = (17 - (defString.Length + 3)) / 2;
                        Console.Write("".PadLeft(paddingSize, ' ') + defString);
                        Console.SetCursorPosition(leftPosition[i] + 2, top + 10);
                        string hpString = $"체력 : {monsters[i].Health}/{monsters[i].Health}";//현재 체력과 최대 체력이 필요한데 그냥 이렇게함
                        paddingSize = (17 - (hpString.Length + 2)) / 2;
                        Console.Write("".PadLeft(paddingSize, ' ') + hpString);

                        if (monsters[i].IsDead)
                        {
                                Console.SetCursorPosition(leftPosition[i] + 2, top + 7);
                                paddingSize = (17 - 4) / 2;
                                Console.Write("".PadLeft(paddingSize, ' '));

                                var color = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("사망");
                                Console.ForegroundColor = color;
                                Console.Write(" ");
                        }


                        Console.SetCursorPosition(leftPosition[i] + 2, top + 11);
                        Console.Write("HP ");
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("".PadRight(fillHpBar, '　'));
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("".PadRight(7 - fillHpBar, '　'));
                        Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);

        }
        static void Main()
        {
                //ShowMonsterCard();
              

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

                /*   StartScreen startScreen = new StartScreen();

           new Monster();
           Character character = new Character("ㅇㅇ", 100, 100, 100, 10, 100, 200, Job.가디언, 10, 10, Attribute.풍, 0, 100);
           BattleEvent battleEvent = new BattleEvent(character);
           ScreenManager screenManager = new ScreenManager(character, battleEvent);
           character.Add();
           GameData.I.GetCharacters().Select(x => x.ToString()).ToList().ForEach(Console.WriteLine);
           screenManager.Prologue();
           screenManager.ShowMainScreen();*/

        }
}
public class StartScreen
{
        int y = 1;
        FontInfo originalFont = ConsoleHelper.GetCurrentFont();

        public void ss()
        {
                ConsoleHelper.SetCurrentFont("궁서", 100);
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.WindowWidth = Console.LargestWindowWidth;
                Console.WindowHeight = Console.LargestWindowHeight;

                string line = "THE $GAME";
                Console.SetCursorPosition((Console.WindowWidth) / 2 - line.Length, 4);
                Console.WriteLine(line);
        }

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



        }
        public StartScreen()
        {
                /* var info = ConsoleHelper.GetCurrentFont();


                 Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

                 // Set the console buffer size to the same dimensions
                 Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);*/

                ss();
                Console.ReadLine();

                string charName;
                Job? charJob;
                StartScreenText();

                CharName(out charName);

                CharJob(out charJob);

                Console.Clear();
                Character character = new Character(charName, 1, 10, 10, 10, 100, 500, charJob, 10, 10, Attribute.풍, 0, 115);
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
