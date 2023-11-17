using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;



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

                StartScreen startScreen = new StartScreen();

                /* BattleEvent battleEvent = new BattleEvent(character);
                 ScreenManager screenManager = new ScreenManager(character, battleEvent);
                 character.Add();
                 GameData.I.GetCharacters().Select(x => x.ToString()).ToList().ForEach(Console.WriteLine);
                 //screenManager.Prologue();
                 screenManager.ShowMainScreen();*/
        }
}
public class StartScreen
{
        public string DirectoryGetParent(string directoryInfo)
        {
                return Directory.GetParent(Directory.GetParent(Directory.GetParent(directoryInfo).ToString()).ToString()).ToString();
        }
        private void StartScreenText()
        {
                string currentDirectory = DirectoryGetParent(Directory.GetCurrentDirectory());
                // Specify the file name (change it to your actual file name)
                string fileName = "StartScreenText.txt";

                // Combine the current directory and file name to get the full path
                string filePath = Path.Combine(currentDirectory, fileName);

                try
                {
                        // Read the contents of the file
                        string[] fileContents = File.ReadAllLines(filePath, Encoding.UTF8);
                        foreach (string line in fileContents)
                        {
                                foreach (char cha in line)
                                {
                                        Console.Write(cha);
                                        if (!Console.KeyAvailable) Thread.Sleep(50);
                                }
                                Console.WriteLine();
                        }
                        // Display the contents

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
                Console.Clear();
                string weapon =
                          $"1. 올곧은 빛의 검 루미나소드\r\n" +
                          $"검은 마치 어둠을 가를 것처럼 보입니다.\r\n" +
                          $"투명한 검날에서 빛나는 힘은 마치 마법의 속삭임처럼 들리며,\r\n" +
                          $"일정한 폭으로 검은 선들이 새겨져 있는 문양은 신비롭습니다.\r\n" +
                          $"이것으로 나아가면 세계의 비밀스러운 모험을 만날 것입니다.\r\n" +
                          $"검의 끝에는 지혜와 결단이 서려 있으며,\r\n" +
                          $"모든 어려움을 극복할 수 있는 힘이 내재되어 있습니다.\r\n" +
                          $"이 무기를 휘두르며, 나만의 운명을 쓰러뜨리고새로운 세계로 나아갈 것입니다. \n" +
                          $"\n" +
                          $"2. 노래하는 환상의 완드 리코드로스\r\n" +
                          $"오래된 마법 지팡이, \r\n" +
                          $"그 반짝이는 지팡이는 마법의 힘을 담고 있습니다. \r\n" +
                          $"누군가 그 지팡이를 휘두르면, \r\n" +
                          $"공중에 특별한 음향의 고요한 빛이 퍼져나가 마법의 소리로 변합니다. \r\n" +
                          $"이 마법 지팡이는 마법의 세계에서 흘러나오는 고대의 지식을 기록하고, \r\n" +
                          $"그 속에서 다양한 이야기와 모험을 펼칠 수 있는 유용한 도구입니다.\n" +
                          $"" +
                          $"3.  자비없는 필멸의 활 " +
                          $"4. 그저 그는 도덕책";
                foreach (var item in weapon)
                {
                        Console.Write(item);
                        if (!Console.KeyAvailable) Thread.Sleep(50);
                }
                Console.Write("\n당신은 어떠한 무기를 선택하실 겁니까? : \n>> ");
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
                string charName;
                Job? charJob;
                StartScreenText();

                CharName(out charName);
                CharJob(out charJob);


                Character character = new Character(charName, 1, 10, 10, 10, 100, 500, Job.가디언, 10, 10, Attribute.풍);
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

                        Console.WriteLine("잘못된 입력입니다.");
                }
        }
}
