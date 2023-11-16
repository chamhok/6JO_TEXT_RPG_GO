using System;
using System.Collections.Generic;

//선건우

// 게임 데이터를 저장하는 싱글톤 클래스 정의 
public class GameData
{
        private static GameData Instance;

        // 몬스터 및 캐릭터를 저장하는 데이터 구조
        private List<Monster> monsters;
        private List<Character> characters;
        private List<Item> items;
        private List<Skill> skills;

        // 인스턴스 생성을 위한 private 생성자
        private GameData()
        {
                monsters = new List<Monster>();
                characters = new List<Character>();
                items = new List<Item>();
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

        public void AddItem(Item item)
        {
                items.Add(item);
        }

        public void RemoveItem(Item item)
        {
                items.RemoveAll(x => x.Name == item.Name);
        }

        public List<Item> GetItem()
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
interface IItem
{
        string Name { get; }

        void Use(Character character) { }
}

// 체력 포션을 나타내는 클래스
public class HealthPotion : IItem
{
        public string Name { get; }

        void Use(Character character)
        {
                character.Health += 50;
        }
}

// 힘 포션을 나타내는 클래스
public class StrengthPotion : IItem
{
        public string Name { get; }

        void Use(Character character)
        {
                character.Attack += 20;
        }
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
                // 캐릭터 생성 및 게임 데이터에 추가 후 목록 출력
                Character character = new Character("ㅇㅇ", 1, 1, 1, 1, 1, 2, Job.가디언, Attribute.풍);
                character.Add(new Character("ㅇㅇ", 1, 1, 1, 1, 1, 2, Job.가디언, Attribute.풍));
                character.Add();
                GameData.I.GetCharacters().Select(x => x.ToString()).ToList().ForEach(Console.WriteLine);
        Console.WriteLine("TT"      );
        Console.WriteLine();
        Console.WriteLine("어렵다 어려워 되나되나?");
        
    }
}
