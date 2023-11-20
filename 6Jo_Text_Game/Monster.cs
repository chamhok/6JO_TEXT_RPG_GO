
public enum MonsterStage
{
    Guard = 0,
    Knight = 1,
    Commander = 2,

    // stage 2
    Witch = 3, 
    Goblin = 4,
    Troll = 4,
    DesertShark = 5,
    //new Assassin,

    // stage 3
    Cerberus = 6,
    VoidMaster = 7,
    FireQueen = 8,
    ShadowWizard = 9,
    CloudSpiter = 10
}

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
    public bool IsDead { get; set; } = false;
    public int Gold { get; set; }
    public float Avoidance { get; set; }
    public float Crt { get; set; }
    public int exp {  get; set; }



    // 몬스터의 종족과 속성
    public Species Species { get; set; }
    public Attribute Attribute { get; set; }

    // 몬스터 기본 생성자
    public Monster()
    {
        
    }

    // 모든 속성을 초기화하는 생성자
    public Monster(string name, float level, float attack, float defense, float speed, float health, int gold, Species species, float avoidance, float crt, Attribute attribute, int exp)
    {
        this.Name = name;
        this.Level = level;
        this.Attack = attack;
        this.Defense = defense;
        this.Speed = speed;
        this.Health = health;
        this.Gold = gold;
        this.Avoidance = avoidance;
        this.Crt = crt;

        this.Species = species;
        this.Attribute = attribute;
        this.exp = exp;
    }

    /// <summary>
    /// 스테이지 별 몬스터
    /// </summary>
    /// <param name="stage"></param>
    /// <returns></returns>

    public List<Monster> StageMonster(int stage)
    {
        List<Monster> monsters = new List<Monster>();

        switch (stage)
        {
            // Stage 1 -------------------------
            case 0:
                monsters.Add(new Guard());
                monsters.Add(new Guard());
                monsters.Add(new Guard());
                break;

            case 1:
                monsters.Add(new Guard());
                monsters.Add(new Guard());
                monsters.Add(new Knight());
                break;

            case 2:
                monsters.Add(new Knight());
                monsters.Add(new Knight());
                monsters.Add(new Commander());
                break;

            // Stage 2 --------------------------
            case 3:
                monsters.Add(new Witch());
                break;

            case 4:
                monsters.Add(new Troll());
                monsters.Add(new Goblin());
                break;

            case 5:
                monsters.Add(new DesertShark());
                break;

            // Stage 3 ----------------------------
            case 6:
                monsters.Add(new Cerberus());
                break;

            case 7:
                monsters.Add(new VoidMaster());
                monsters.Add(new ShadowWizard());
                monsters.Add(new FireQueen());
                monsters.Add(new CloudSpiter());
                break;

        }

        return monsters;
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
        return $"이름 : {this.Name} \n" +
               $"LV   : {this.Level} \n" +
               $"ATK  : {this.Attack} \n" +
               $"DEF  : {this.Defense} \n" +
               $"SPD  : {this.Speed} \n" +
               $"H P  : {this.Health} \n" +
               $"GOLD : {this.Gold} \n" +
               $"SPC  : {this.Species} \n" +
               $"ATB  : {this.Attribute}";
    }
}



/// <summary>
///  몬스터 생성 클래스
/// </summary>
// --------------------------------------------------------------------------------
// Stage 1 - 경비병
public class Guard : Monster
{
    public Guard()
        : base("경비병", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}

// stage 1 - 기사
public class Knight : Monster
{
    public Knight()
        : base("기사", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}

// stage 1 - 기사단장
public class Commander : Monster
{
    public Commander()
        : base("기사단장", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}

// -------------------------------------------------------------------------------
// stage 2 - 마녀
public class Witch : Monster
{
    public Witch()
        : base("마녀", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}

// stage 2 - 고블린
public class Goblin : Monster
{
    public Goblin()
        : base("고블린", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}

// sgate 2 - 트롤
public class Troll : Monster
{
    public Troll()
        : base("트롤", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}

// stage 2 - 사막상어
public class DesertShark : Monster
{
    public DesertShark()
        : base("사막상어", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}

// stage 2 - (기믹) 자객
//public class Assassin : Monster
//{
//    public Assassin()
//        : base("Assassin", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수)
//    {
//        this.Add();
//    }
//}


// --------------------------------------------------------------------------------
// stage 3 - 용암

// stage 3 - 낭떠러지

// stage 3 - 케르베로스 (Cerberus)
public class Cerberus : Monster
{
    public Cerberus()
        : base("케르베로스", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}

// stage 3 - 사천왕1 허공의 마스터
public class VoidMaster : Monster
{
    public VoidMaster()
        : base("공허의 마스터", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}
// stage 3 - 사천왕2 화염의 여왕
public class FireQueen : Monster
{
    public FireQueen()
        : base("불의 여왕", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}
// stage 3 - 사천왕3 그림자 마법사
public class ShadowWizard : Monster
{
    public ShadowWizard()
        : base("그림자 마법사", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}
// stage 3 - 사천왕4 무자비한 검사
public class CloudSpiter : Monster
{
    public CloudSpiter()
        : base("무자비한 검사", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }
}


