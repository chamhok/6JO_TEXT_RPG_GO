
using NAudio.Codecs;
using System.Drawing;
using static System.Formats.Asn1.AsnWriter;

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
    public int Exp {  get; set; }
    public float maxHealth {  get; set; }


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
        this.Exp = exp;
        this.maxHealth = health;
    }

    #region 스테이지 별 몬스터
    public List<Monster> StageMonster(int stage)
    {
        List<Monster> monsters = new List<Monster>();

        switch (stage)
        {
            // Stage 1----------------------------
        case 0: // 경비병 * 3
            monsters.Add(new Guard());
            monsters.Add(new Guard());
            monsters.Add(new Guard());
            break;

            case 1: // 기사 * 3
            monsters.Add(new Knight());
            monsters.Add(new Knight());
            monsters.Add(new Knight());
            break;

        case 2: // 기사*2 + 기사단장
            monsters.Add(new Knight());
            monsters.Add(new Knight());
            monsters.Add(new Commander());
            break;

        // Stage 2 --------------------------
        case 3: // 마녀
            monsters.Add(new Witch());
            break;

        case 4: // 고블린 + 트롤
            monsters.Add(new Troll());
            monsters.Add(new Goblin());
            break;

        case 5: // 자객 (기믹)
            monsters.Add(new Assassin());
            break;

        case 6: // 상어
            monsters.Add(new DesertShark());
            break;

        // Stage 3 ----------------------------
        case 7: // 용암
            //monsters.Add(new Rava());
            break;

        case 8: // 낭떠러지
            monsters.Add(new Cerberus());
            break;

        case 9: // 케르베로스
            monsters.Add(new Cerberus());
            break;

        case 10: // 사천왕
            monsters.Add(new VoidMaster());
            monsters.Add(new ShadowWizard());
            monsters.Add(new FireQueen());
            monsters.Add(new CloudSpiter());
            break;

        default: break;
        }

        return monsters;
    }
    #endregion

    // 데미지를 입는 메서드
    public virtual bool TakeDamage(float damage)
    {
        Health -= damage;
        return false;
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

    public virtual bool isAction()
    {
        // 각 몬스터 별 특수한 공격일경우
        return false;
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



#region 몬스터 생성
// --------------------------------------------------------------------------------
// Stage 1 - 경비병
public class Guard : Monster
{
    public Guard()
        : base("경비병", 1, 3, 1, 1, 20, 100, Species.코볼트, 1, 1, Attribute.수, 20)
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

    public override bool isAction()
    {
        if (GameData.I.GetMonsters().Count() < 3)
        {
            Console.WriteLine("마녀가 서브드래곤을 소환했습니다.");
            new Chick();
        }
        else
        {
            Console.WriteLine("서브드래곤 소환에 실패했습니다.");
        }
        Console.ReadKey();
        return true;
    }
}

public class Chick : Monster
{
    public Chick()
        : base("서브 드래곤", 1, 1, 1, 1, 10, 10, Species.코볼트, 1, 1, Attribute.수, 20)
    { this.Add(); }
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


// --------------------------------------------------------------------------------
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




// 기믹 전투 ==================================================================
#region stage 2 - (기믹) 자객
public class Assassin : Monster
{
    public Assassin()
        : base("자객", 1, 10, 10, 1, 100, 10, Species.기믹, 1, 1, Attribute.수, 20)
    {
        this.Add();
    }

    //---------------------------------------
    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    SoundManager soundManager = new SoundManager();
    Point player = new Point(10, 20, '▲');
    List<Point> enemy = new List<Point>();
    ConsoleKeyInfo key;
    string[] hp = new string[2] {"♥", "♥" };
    int hpCount = 2;
    int score = 0;

    Random rand = new Random();


    public override bool TakeDamage(float damage)
    {
        Console.Clear();
        DrawWall();
        player.Draw();

        enemy.Add(EnemySpawn());

        while (true)
        {
            Console.SetCursorPosition(5, 5);
            Console.Write("자객을 피하라");
            Console.SetCursorPosition(2, 7);
            Console.Write("x를 눌러 시작하세요");
            if (Console.ReadKey().KeyChar == 'x') break; ;
        }
        Console.Clear();
        DrawWall();
        player.Draw();
        DrawInfo();

        while (true)
        {
            // 플레이어 움직임
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
            }
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    player.direction = Direction.LEFT;
                    PlayerMove(ref player);
                    break;
                case ConsoleKey.RightArrow:
                    player.direction = Direction.RIGHT;
                    PlayerMove(ref player);
                    break;
            }
            
            

            // 적 움직임
            if (enemy.Count == 0 || rand.Next(0, 3) % 3 == 0) enemy.Add(EnemySpawn());

            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].Clear();
                enemy[i].y++;
                if (enemy[i].y == 20 && (enemy[i].x == player.x))
                {
                    hp[--hpCount] = "♡";
                    enemy.Remove(enemy[i]);
                    soundManager.CallSound("sound2", 300);
                    Thread.Sleep(500);
                }
                else if (enemy[i].y > 20)
                {
                    score++;
                    enemy.Remove(enemy[i]);
                }
                else enemy[i].Draw();
            }


            DrawInfo();
            if (score == 30 || hpCount == 0) break;
            Thread.Sleep(50);
        }


        if(hpCount == 0)
        {
            soundManager.CallSound("atk2", 1000);
            Console.SetCursorPosition(0, 22);
            Console.WriteLine("Fail");
            Console.WriteLine("Enter키를 눌러주세요");
            while (true) {
                if (Console.ReadKey().Key == ConsoleKey.Enter) break;
            }
            return true;
        }
        else
        {
            soundManager.CallSound("driring", 1000);
            Console.SetCursorPosition(0, 22);
            Console.WriteLine("Clear");
            Console.WriteLine("Enter키를 눌러주세요");
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter) break;
            }
            return false;
        }
        //Health -= damage;
    }

    void DrawWall()
    {
        // 상하 벽 그리기
        for (int i = 0; i < 21; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("#");
            Console.SetCursorPosition(i, 21);
            Console.Write("#");
        }

        // 좌우 벽 그리기
        for (int i = 0; i < 21; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("#");
            Console.SetCursorPosition(21, i);
            Console.Write("#");
        }
    } // 벽 그리기

    void DrawInfo()
    {
        Console.SetCursorPosition(30, 7);
        for(int i=0; i<2; i++)
        {
            Console.Write(hp[i] + " ");
        }
        Console.SetCursorPosition (30, 9);
        Console.WriteLine($"회피 한 자객 수 : {score} / 30");
    }

    class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public char sym { get; set; }
        public Direction direction { get; set; } = Direction.DOWN;

        public Point(int x, int y, char sym)
        {
            this.x = x;
            this.y = y;
            this.sym = sym;
        }

        // 점을 그리는 메서드
        public void Draw()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            Console.Write(sym);

        }
        public void Draw(string sym)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }

        // 점을 지우는 메서드
        public void Clear()
        {
            Draw("  ");
        }

        // 두 점이 같은지 비교하는 메서드
        public bool IsHit(Point p)
        {
            return p.x == x && p.y == y;
        }

    } // 위치 클래스

    void PlayerMove(ref Point p)
    {
        switch (p.direction)
        {
            case Direction.LEFT:
                p.Clear();
                p.x--;
                if (p.x < 1) p.x = 1;
                p.Draw();
                break;

            case Direction.RIGHT:
                p.Clear();
                p.x++;
                if (p.x > 20) p.x = 20;
                p.Draw();
                break;
        }
    } // 플레이어 움직이기 메서드

    Point EnemySpawn()
    {
        int x = rand.Next(1, 20);

        Point p = new Point(x, 2, '★');
        return p;
    } // 적 스폰 메서드
}
#endregion

// stage 3 - 용암
//public class Rava : Monster
//{
//    public Rava()
//        : base("용암", 1, 1, 1, 1, 10, 10, Species.기믹, 1, 1, Attribute.수, 20)
//    {
//        this.Add();
//    }

//    // -------------------------------------------------------------------------------
//    enum Direction
//    {
//        LEFT,
//        RIGHT
//    }
//    //Timer time = new System.Timers.Timer(cycleTime);
//    //float time = 0.00f;
//    Point player = new Point(10, 10, '▲');

//    public override bool TakeDamage(float damage)
//    {
//        DrawWall();

//        while (true)
//        {
//            Console.SetCursorPosition(2, 5);
//            Console.Write("용암주의! 중심잡기");
//            Console.SetCursorPosition(2, 7);
//            Console.Write("x를 눌러 시작하세요");
//            if (Console.ReadKey().KeyChar == 'x') break; ;
//        }

//        Console.ReadKey();
//        return base.TakeDamage(damage);
//    }

//    // -------------------------------------------------------------------------------
//    void DrawWall()
//    {
//        // 상하 벽 그리기
//        for (int i = 0; i < 21; i++)
//        {
//            Console.SetCursorPosition(i, 0);
//            Console.Write("#");
//            Console.SetCursorPosition(i, 21);
//            Console.Write("#");
//        }

//        // 좌우 벽 그리기
//        for (int i = 0; i < 21; i++)
//        {
//            Console.SetCursorPosition(0, i);
//            Console.Write("#");
//            Console.SetCursorPosition(21, i);
//            Console.Write("#");
//        }

//        // 중간선 그리기
//        for (int i = 1; i < 21; i++)
//        {
//            Console.SetCursorPosition(10, i);
//            Console.Write("│");
//        }

//        // 한계선 그리기
//        for (int i = 1; i < 21; i++)
//        {
//            Console.SetCursorPosition(7, i);
//            Console.Write("┃");
//            Console.SetCursorPosition(12, i);
//            Console.Write("┃");
//        }

//    } // 벽 그리기

//    void DrawInfo()
//    {
//        Console.SetCursorPosition(10, 2);
//        //Console.Write(time);

//        Console.SetCursorPosition(30, 7);
//        Console.WriteLine($"현재 위치 : {player.x} / 30");

//        Console.SetCursorPosition(30, 9);
//        Console.WriteLine($"한계 위치 : 8 ~ 12");
//    }


//    class Point
//    {
//        public int x { get; set; }
//        public int y { get; set; }
//        public char sym { get; set; }
//        public Direction direction { get; set; }

//        public Point(int x, int y, char sym)
//        {
//            this.x = x;
//            this.y = y;
//            this.sym = sym;
//        }

//        // 점을 그리는 메서드
//        public void Draw()
//        {
//            Console.CursorVisible = false;
//            Console.SetCursorPosition(x, y);
//            Console.Write(sym);

//        }
//        public void Draw(string sym)
//        {
//            Console.CursorVisible = false;
//            Console.SetCursorPosition(x, y);
//            Console.Write(sym);
//        }

//        // 점을 지우는 메서드
//        public void Clear()
//        {
//            Draw("  ");
//        }

//        // 두 점이 같은지 비교하는 메서드
//        public bool IsHit(Point p)
//        {
//            return p.x == x && p.y == y;
//        }

//    } // 위치 클래스

//    void PlayerMove(ref Point p)
//    {
//        switch (p.direction)
//        {
//            case Direction.LEFT:
//                p.Clear();
//                p.x--;
//                if (p.x < 1) p.x = 1;
//                p.Draw();
//                break;

//            case Direction.RIGHT:
//                p.Clear();
//                p.x++;
//                if (p.x > 20) p.x = 20;
//                p.Draw();
//                break;
//        }
//    } // 플레이어 움직이기 메서드

    


//}
#endregion
