
public class Monster : ICharacter
{
        // 체력 변경 시 호출될 콜백
        public Action<float> ChangedCallback;

        // 몬스터의 인벤토리 리스트
        public List<Item> Inventory = new List<Item>();

        // 몬스터의 체력 속성
        private float health;

        // 몬스터의 체력 프로퍼티
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

        // 몬스터의 이름, 레벨, 공격력, 방어력, 속도, 사망 여부, 골드 등의 속성
        public string Name { get; set; }
        public float Level { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Speed { get; set; }
        public bool IsDead { get; set; }
        public int Gold { get; set; }

        // 몬스터의 종족과 속성
        public Species Species { get; set; }
        public Attribute Attribute { get; set; }

        // 몬스터 기본 생성자
        public Monster()
        {
        }

        // 모든 속성을 초기화하는 생성자
        public Monster(string name, float level, float attack, float defense, float speed, float health, int gold, Species species, Attribute attribute)
        {
                this.Name = name;
                this.Level = level;
                this.Attack = attack;
                this.Defense = defense;
                this.Speed = speed;
                this.Health = health;
                this.Gold = gold;

                this.Species = species;
                this.Attribute = attribute;
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

        // 몬스터를 게임 데이터에 추가하는 메서드
        public void Add(Monster monster)
        {
                GameData.I.AddMonster(monster);
        }

        // 자기 자신을 게임 데이터에 추가하는 메서드
        public void Add()
        {
                GameData.I.AddMonster(this);
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
                       $"{this.Gold} " +
                       $"{this.Species} " +
                       $"{this.Attribute}";
        }
}
