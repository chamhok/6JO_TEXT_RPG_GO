public class Character : ICharacter
{
        // 체력 변경 시 호출될 콜백
        public Action<float> ChangedCallback;

        // 인벤토리 리스트
        public List<Item> Inventory = new List<Item>();
        public List<Skill> SkillList = new List<Skill>();
        public void DisplayInventory()
        {

                Console.WriteLine("인벤토리:");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");

        // 인벤토리에 있는 아이템을 출력
                foreach (var item in Inventory)
                {
                Console.WriteLine($"{item.Name} - 공격력: {item.Akt}, 방어력: {item.Def}");
                Console.WriteLine($"              설명: {item.ItemDescription}");
                Console.WriteLine($"              장착 여부: {item.Stallation}, 구매 여부: {item.Buy}");
                Console.WriteLine($"              가격: {item.Gold}");
                Console.WriteLine();
                }
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

    }

    // 체력 속성
    private float health;

        // 체력 프로퍼티
        public float Health
        {
                get { return health; }
                set
                {
                        // 체력 값을 설정하고 변경 콜백 호출
                        this.health = value;
                        ChangedCallback?.Invoke(health);
                }
        }

    // 캐릭터의 이름, 레벨, 공격력, 방어력, 속도, 사망 여부, 골드 등의 속성
    public string Name { get; set; }
    public float Level { get; set; }
    public double CurrentExp { get; set; }
    public double MaxExp { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float Speed { get; set; }
    public bool IsDead { get; set; } = false;
    public int Gold { get; set; }
    public float Avoidance { get; set; }
    public float Crt { get; set; }
    public int WinCount { get; set; } = 0;
    public bool IsQuest { get; set; }


        // 직업과 속성
        public Job? Job { get; set; }
        public Attribute? Attribute { get; set; }

        // 기본 생성자
        public Character()
        {
        }

    // 모든 속성을 초기화하는 생성자
    public Character(string name, float level, float attack, float defense, float speed, float health, int gold, Job? job, float crt, float avoidance, Attribute? attribute, double currentexp, double maxexp, bool isquest = false)
    {
        this.Name = name;
        this.Level = level;
        this.CurrentExp = currentexp;
        this.MaxExp = maxexp;
        this.Attack = attack;
        this.Defense = defense;
        this.Speed = speed;
        this.Health = health;
        this.Gold = gold;
        this.Avoidance = avoidance;
        this.Crt = Crt;
        this.Job = job;
        this.Attribute = attribute;
        this.IsQuest = isquest;
    }

        // 데미지를 입는 메서드
        public void TakeDamage(float damage)
        {
                Health -= damage;
        }

        // 체력 변경 콜백 설정 메서드
        public void SetChangedCallback(Action<float> callback)
        {
                this.ChangedCallback = callback;
        }

        // 캐릭터를 게임 데이터에 추가하는 메서드
        public void Add(Character character)
        {
                GameData.I.AddCharacter(character);
        }

        // 자기 자신을 게임 데이터에 추가하는 메서드
        public void Add()
        {
                GameData.I.AddCharacter(this);
        }
        public void SetSkill(Skill skill)
        { 
               SkillList.Add(skill);
        }
       
        // 객체를 문자열로 표현하는 메서드
        public override string ToString()
        {
                return $"{this.Name} " +
                         $"{this.Level} " +
                         $"{this.Attack} " +
                         $"{this.Defense} " +
                         $"{this.Speed} " +
                         $"{this.Health} " +
                         $"{this.Job} " +
                         $"{this.Attribute}";
        }


}