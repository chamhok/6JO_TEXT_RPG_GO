﻿using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Linq;

class BattleEvent
{
    Monster monster;
    Character player;
    List<Monster> monsters;
    SoundManager soundManager = new SoundManager();
    UiManager uiManager = new UiManager();

    int skillPguard = 1;
    bool skillPsmash = false;
    float skillPDamage = 1;

    int skillMguard = 1;
    int skillMsmash = 1;

    int life = 10;
    
    

    public BattleEvent(Character player)
    {
        this.player = player;
    }

    // ------------------------------------------------------------------------

    public void Battles()  // 배틀 시작
    {
        if (monsters != null) this.monsters.Clear();
        this.monsters = GameData.I.GetMonsters(player);
        this.monster = monsters[0];

        int turnSpeed = 10;
        

        // 턴제전투 (누구 하난 죽을때까지)
        do
        {
            Console.Clear();

            Message();

            if (monsters[0].Species == Species.기믹)
            {
                player.IsDead = monsters[0].TakeDamage(0);
                break;
            }

            if (player.Speed == turnSpeed) PlayerTurn();
            else
            {
                for(int i=0; i < monsters.Count; i++)
                {
                    if (monsters[i].Speed == turnSpeed)
                    {
                        monster = monsters[i];
                        MonsterTurn();
                    }
                }
            }

            turnSpeed--;
            turnSpeed = turnSpeed == 0 ? 10 : turnSpeed;
        }
        while (player.IsDead == false && monster.IsDead == false);


        // 플레이어 사망시
        if (player.IsDead == true)
        {
            //--life; //라이프 기능 아직 구현X 추가적인 상세 게임 스토리라인 결정후 구현
            Console.WriteLine("전투패배!");
        }
        // 플레이어 생존시
        else
        {
            player.WinCount++; //라이프와 동일
            Console.WriteLine("전투승리! 승리횟수: ");
            GetRewards();
        }

        Console.ReadKey();
    }


    public void Message()
    {
        Console.WriteLine($"이름 : {player.Name} ");
        Console.WriteLine("------------------------------------------------------------\n");
        foreach(var x in monsters)
        {
            Console.WriteLine(x.ToString());
            Console.WriteLine("------------------------------------------------------------\n");
        }
    }



    public void PlayerTurn() // 플레이어의 행동을 입력받는 메소드 -------------------------------------------------------------------------------------------------------
    {
        //Console.Clear();
        //Console.WriteLine("남은 목숨" + life);
        Console.WriteLine("플레이어의 남은 생명력 : " + player.Health);
        Console.WriteLine("다음 행동을 입력해 주세요!\n");
        Console.WriteLine("1.공격 2.방어 3.스킬");
        Console.WriteLine("------------------------------------------------\n");


        int input;
        while (true)
        {
            int.TryParse(Console.ReadLine(), out input);
            if (input > 0 && input < 4) break;
            else Console.WriteLine("다시 입력헤주세요");
        }
        switch (input)
        {
            case 1: // 일반 공격
                SelectMonster();
                break;

            case 2: // 방어
                if (skillPguard == 1)
                {
                    Console.WriteLine("방어자세를 취합니다.");
                    skillPguard = 2;
                }
                else
                {
                    Console.WriteLine("다음 공격을 방어할 준비가 되었습니다!");
                }
                Console.ReadKey();
                break;

            case 3: // 스킬 공격
                
                if (player.SkillList.Count > 1)
                {
                    Console.WriteLine("스킬을 골라주세요");
                    for (int i = 0; i < player.SkillList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {player.SkillList[i].Name}");
                    }

                    int sel;
                    while (true)
                    {
                        int.TryParse(Console.ReadLine(), out sel);
                        if (sel > 0 && sel < player.SkillList.Count) break;
                        else Console.WriteLine("다시 입력헤주세요");
                    }
                    this.skillPDamage = player.SkillList[sel].Attack;
                    this.skillPsmash = true;
                }

                SelectMonster();
                this.skillPDamage = 1;
                this.skillPsmash = false;

                break;

            default:
                Console.WriteLine("다시입력해주세요!");
                break;
        }

    }

    void SelectMonster()
    {
        if (monsters.Count > 1)
        {
            Console.WriteLine("공격 대상을 골라주세요");
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {monsters[i].Name}");
            }

            int sel;
            while (true)
            {
                int.TryParse(Console.ReadLine(), out sel);
                if (sel > 0 && sel <= monsters.Count) break;
                else Console.WriteLine("다시 입력헤주세요");
            }
            Console.WriteLine("공격하였습니다!");
            MonsterResult(monsters[sel - 1]);
        }
        else
        {
            Console.WriteLine("공격하였습니다!");
            MonsterResult();
        }
    }

    public void MonsterTurn()  // 몬스터의 행동을 입력받는 메소드 -------------------------------------------------------------------------------------------------------
    {
        Console.Clear();
        Random random = new Random();
        Console.WriteLine($"{monster.Name}이(가) 다음 행동을 준비중입니다....");
        switch (random.Next(1, 4))
        {
            case 1:
                if (!monster.isAction())
                {
                    Console.WriteLine($"{monster.Name}이(가) 공격합니다!");
                    PlayerResult();
                }
                
                break;
            case 2:
                if (skillMguard == 1)
                {
                    Console.WriteLine($"{monster.Name}이(가) 방어 자세를 취합니다.");
                    skillMguard = 2;
                }
                Console.ReadKey();
                break;
            case 3:
                if (skillMsmash == 1)
                {
                    Console.WriteLine($"{monster.Name}이(가) 공격 자세를 취합니다.");
                    skillMsmash = 2;
                }
                Console.ReadKey();
                break;
            default:
                Console.ReadKey();
                break;
        }


    }



    // 몬스터의 피격시 데미지 계산 및 사망처리 -------------------------------------------------------------------------------------------
    public void MonsterResult(Monster monster) // 다수의 몬스터 처리
    {
        monster.TakeDamage((float)PlayerDmg());
        if (monster.Health <= 0)
        {
            monster.IsDead = true;
            monsters.Remove(monster);
            Console.WriteLine($"{monster.Name}가 사망했습니다.");
            AcquireExp(monster);
            this.monster = monsters[0] != null? monsters[0] : monster;
            Console.ReadKey();
        }
    } 

    public void MonsterResult() // 단일 몬스터 처리
    {
        monsters[0].TakeDamage((float)PlayerDmg());
        if (monsters[0].Health <= 0)
        {
            monsters[0].IsDead = true;
            Console.WriteLine($"{monsters[0].Name}이(가) 사망했습니다.");
            AcquireExp(monsters[0]);
            Console.ReadKey();
        }
    }

    // 플레이어의 피격시 데미지 계산 및 사망처리 -------------------------------------------------------------------------------------------
    public void PlayerResult()
    {
        player.TakeDamage((float)MonsterDmg());
        if (player.Health <= 0)
        {
            player.IsDead = true;
            Console.WriteLine($"{player.Name}가 사망했습니다.");
            Console.ReadKey();
        }
    }

    // 몬스터 처치 시, 경험치 획득
    public void AcquireExp(Monster monster)
    {
        Console.WriteLine($"\n+{monster.Exp}경험치를 획득하였습니다.");
        player.CurrentExp += monster.Exp;
        if (player.CurrentExp >= player.MaxExp)
        {
            player.Level += 1;
            Console.WriteLine($"레벨업! Lv.{player.Level}");
            player.CurrentExp -= player.MaxExp;
            player.MaxExp += (float)(player.MaxExp * 0.2);
        }
        Console.WriteLine($"현재 경험치: {player.CurrentExp}, 다음 레벨업 까지 남은 경험치: {player.MaxExp - player.CurrentExp}");
    }

    // 몬스터의 공격과 공격 성공유무, 크리티컬 유무 판정( PlayerDmg 주석 참고) -------------------------------------------------------------
    public float MonsterDmg()
    {
        float damage = (monster.Attack * skillMsmash) - (player.Defense * skillPguard);
        skillMsmash = 1;
        skillPguard = 1;

        if (damage > 0)
        {
            if (AvoidanceToss() <= player.Avoidance)
            {
                Console.WriteLine("플레이어가 회피하였습니다!");
                soundManager.CallSound("avod", 100);
                Console.ReadKey();
                return 0;
            }
            else if (CrtToss() <= monster.Crt)
            {
                Console.WriteLine("치명적 일격! 피해량 : " + damage * 2);
                soundManager.CallSound("atk2", 100);
                Console.ReadKey();
                return damage * 2;
            }
            else
            {
                Console.WriteLine($"{monster.Name}의 공격을 받았다 피해량 : " + damage);
                soundManager.CallSound("atk", 100);
                Console.ReadKey();
                return damage;
            }

        }
        else
        {
            Console.WriteLine($"{monster.Name}의 공격이 너무 약하다!");
            soundManager.CallSound("block", 100);
            Console.ReadKey();
            return 0;
        }
    }

    // 플레이어의 공격과 공격 성공유무, 크리티컬 유무 판정 -----------------------------------------------------------------------------------
    public float PlayerDmg()
    {
        float playerAttack = player.Attack;
        if(skillPsmash) playerAttack *= skillPDamage;
        float damage = (player.Attack) - (monster.Defense * skillMguard);
        damage *= CompareAttribute(player.Attribute.Value, monster.Attribute);
        damage = (float)Math.Round(damage, 2);

        //skillPsmash = 1;
        skillMguard = 1;

        if (damage > 0) //데미지가 0과 같거나 작은지 체크
        {
            if (AvoidanceToss() <= monster.Avoidance) // 회피 성공 유무 체크
            {
                Console.WriteLine($"{monster.Name}이(가) 회피하였습니다!");
                soundManager.CallSound("avod", 100);
                Console.ReadKey();
                return 0;
            }
            else if (CrtToss() <= player.Crt) // 크리티컬 유무 체크
            {
                Console.WriteLine("치명적 일격! 피해량 : " + damage * 2);
                soundManager.CallSound("atk2", 100);
                Console.ReadKey();
                return damage * 2;
            }
            else //위 조건문 모두 false시 기본 피해량만 타격
            {
                Console.WriteLine("공격 성공! 피해량 : " + damage);
                soundManager.CallSound("atk", 100);
                Console.ReadKey();
                return damage;
            }
        }
        else // 데미지가 0과 같거나 작을경우 0으로 데미지 리턴 ( 음수가 될경우 생명력이 회복되는 현상을 막기위한 로직)
        {
            Console.WriteLine("상대가 너무 단단하다!");
            soundManager.CallSound("block", 100);
            Console.ReadKey();
            return 0;
        }
    } 

    public float CompareAttribute(Attribute a, Attribute b)
    {
        switch (a)
        {
            case Attribute.수:
                if (b == Attribute.화) return 1.3f;
                else if (b == Attribute.토) return 0.8f;
                break;

            case Attribute.화:
                if (b == Attribute.풍) return 1.3f;
                else if (b == Attribute.수) return 0.8f;
                break;

            case Attribute.풍:
                if (b == Attribute.토) return 1.3f;
                else if (b == Attribute.화) return 0.8f;
                break;

            case Attribute.토:
                if (b == Attribute.수) return 1.3f;
                else if (b == Attribute.풍) return 0.8f;
                break;
        }

        return 1.0f;
    }


    // 회피 및 크리티컬 주사위 ------------------------------------------------------------------------------------------------------------------
    public int AvoidanceToss() // 회피
    {
        Random random = new Random();
        return random.Next(1, 100);
    }
    public int CrtToss() //크리티컬
    {
        Random random = new Random();
        return random.Next(1, 100);
    }

    // 보상 단계 ---------------------------------------------------------------------------------------------------------------------------------
    public void GetRewards()
    {
        Console.Clear();
        Console.WriteLine("아래 항목 중에서 원하는 보상 아이템을 고를 수 있습니다.\n");
        SelectRewardList();
    }

    // 보상 아이템 선택 과정
    public void SelectRewardList()
    {
        // 리스트 생성
        List<IItem> rewardItems = new List<IItem>
        {
            new Item("HealthPostion", 1),
            new Item("AttackPostion", 1),
            new Item("CrazyWood", 10,10,"dd",false,false,100),
            new Item("VeryCrazySword", 10,10,"dd",false,false,100),
            new Item("SuperCrazyHammer", 10,10,"dd",false,false,100),
        };

        string questionMark = "???";
        // 아이템 랜덤으로 섞기
        Random random = new Random();
        rewardItems = rewardItems.OrderBy(x => random.Next()).ToList();

        for (int i = 0; i < rewardItems.Count; i++) Console.WriteLine($"{i + 1}. {questionMark}");

        Console.WriteLine("\n보상 아이템 번호를 입력하세요.");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int selectedNumber) && selectedNumber >= 1 && selectedNumber <= rewardItems.Count)
            {
                Console.Clear();
                Console.WriteLine("과연 ... 보상 결과는 ?\n");
                Thread.Sleep(500);
                for (int i = 0; i < rewardItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {rewardItems[i].Name}");
                    Thread.Sleep(500);
                }
                IItem selectedReward = rewardItems[selectedNumber - 1];
                selectedReward.Use(player);
                
                break;
            }
            else
            {
                Console.WriteLine("올바른 번호를 입력하세요.");
            }
        }
    }

}