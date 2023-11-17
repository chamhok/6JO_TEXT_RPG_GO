using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

class BattleEvent
{
    Monster monster;
    Character player;
    private List<Monster> monsters;

    bool playerT = false;
    bool monsterT = false;

    int skillPguard = 1;
    int skillPsmash = 1;

    int skillMguard = 1;
    int skillMsmash = 1;

    int life = 10;
    int wincount = 0;

    public BattleEvent(Character player, Monster monster)
    {
        this.player = player;
        this.monster = monster;
    }

    public void CoinToss() //몬스터와 플레이어의 스피드값 비교후 선공 결정
    {
        if (player.Speed >= monster.Speed)
        {
            playerT = true;
        }
        else
        {
            monsterT = true;
        }
    }

    public void PlayerTurn() // 플레이어의 행동을 입력받는 메소드
    {
        Console.Clear();
        Console.WriteLine("남은 몫숨" + life);
        Console.WriteLine("플레이어의 남은 생명력 : " + player.Health);
        Console.WriteLine("몬스터의 남은 생명력 : " + monster.Health);
        Console.WriteLine("다음 행동을 입력해 주세요!");
        Console.WriteLine("1.공격 2.방어 3.일격준비");
        String input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.WriteLine("공격하였습니다!");
                MonsterResult();
                playerT = false;
                monsterT = true;
                Console.ReadKey();
                break;

            case "2":
                if (skillPguard == 1)
                {
                    Console.WriteLine("방어자세를 취합니다.");
                    skillPguard = 2;
                    playerT = false;
                    monsterT = true;
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("다음 공격을 방어할 준비가 되었습니다!");
                    Console.ReadKey();
                }
                break;

            case "3":
                if (skillPsmash == 1)
                {
                    Console.WriteLine("강력한 일격을 준비합니다!");
                    skillPsmash = 2;
                    playerT = false;
                    monsterT = true;
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("일격 준비가 되었습니다!");
                    Console.ReadKey();
                }
                break;

            default:
                Console.WriteLine("다시입력해주세요!");
                Console.ReadKey();
                break;

        }
    }

    public void MonsterTurn()
    {
        Console.Clear();
        Random random = new Random();
        Console.WriteLine("몬스터가 다음 행동을 준비중입니다....");
        switch (random.Next(1, 4))
        {
            case 1:
                Console.WriteLine("몬스터가 공격합니다!");
                PlayerResult();
                monsterT = false;
                playerT = true;
                Console.ReadKey();
                break;
            case 2:
                if (skillMguard == 1)
                {
                    Console.WriteLine("몬스터가 특정한 자세를 취합니다.");
                    monsterT = false;
                    playerT = true;
                    skillMguard = 2;
                    Console.ReadKey();
                }
                break;
            case 3:
                if (skillMsmash == 1)
                {
                    Console.WriteLine("몬스터가 특정한 자세를 취합니다.");
                    monsterT = false;
                    playerT = true;
                    skillMsmash = 2;
                    Console.ReadKey();
                }
                break;
            default:
                break;
        }
    } //몬스터의 행동을 정하는 메소드

    public void MonsterResult()
    {
        monster.TakeDamage((float)PlayerDmg());
        if (monster.Health <= 0)
            monster.IsDead = false;
    } //몬스터의 피격시 데미지 계산 및 사망처리

    public void PlayerResult()
    {
        player.TakeDamage((float)MonsterDmg());
        if (player.Health <= 0)
            player.IsDead = false;
    } // 플레이어의 피격시 데미지 계산 및 사망처리


    public void Battles()
    {
        CoinToss(); 
        do
        {
            if (playerT == true)
            {
                PlayerTurn();
            }
            else
            {
                MonsterTurn();
            }
        }
        while (player.IsDead == true && monster.IsDead == true);

        if (player.IsDead == false)
        {
            --life; //라이프 기능 아직 구현X 추가적인 상세 게임 스토리라인 결정후 구현
            Console.WriteLine("전투패배!");
            Console.ReadKey();
        }
        else
        {
            ++wincount; //라이프와 동일
            Console.WriteLine("전투승리! 승리횟수: " + wincount);
            Console.ReadKey();
            GetRewards();
        }

    }

    // 보상 단계
    public void GetRewards()
    {
        Console.Clear();
        Console.WriteLine("아래 항목 중에서 원하는 보상 아이템을 고를 수 있습니다.");
        SelectRewardList();
    }

    // 보상 아이템 선택 과정
    public void SelectRewardList()
    {
        // 리스트 생성
        List<IItem> rewardItems = new List<IItem>
        {
            new Item("Health Postion", 1),
            new Item("Attack Postion", 1),
        };

        for(int i = 0; i < rewardItems.Count; i++) Console.WriteLine($"{i+1}. {rewardItems[i].Name}");

        Console.WriteLine("보상 아이템 번호를 입력하세요.");
        while(true)
        {
            if (int.TryParse(Console.ReadLine(), out int selectedNumber) && selectedNumber >= 1 && selectedNumber <= rewardItems.Count)
            {
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
                Console.ReadKey();
                return 0;
            }
            else if (CrtToss() <= monster.Crt)
            {
                Console.WriteLine("치명적 일격! 피해량 : " + damage * 2);
                Console.ReadKey();
                return damage * 2;
            }
            else
            {
                Console.WriteLine("몬스터의 공격을 받았다 피해량 : " + damage);
                Console.ReadKey();
                return damage;
            }

        }
        else
        {
            Console.WriteLine("몬스터의 공격이 너무 약하다!");
            Console.ReadKey();
            return 0;
        }
    } // 몬스터의 공격과 공격 성공유무, 크리티컬 유무 판정( PlayerDmg 주석 참고)

    public float PlayerDmg()
    {
        float damage = (player.Attack * skillPsmash) - (monster.Defense * skillMguard);
        skillPsmash = 1;
        skillMguard = 1;

        if (damage > 0) //데미지가 0과 같거나 작은지 체크
        {
            if (AvoidanceToss() <= monster.Avoidance) // 회피 성공 유무 체크
            {
                Console.WriteLine("몬스터가 회피하였습니다!");
                Console.ReadKey();
                return 0;
            }
            else if (CrtToss() <= player.Crt) // 크리티컬 유무 체크
            {
                Console.WriteLine("치명적 일격! 피해량 : " + damage * 2);
                Console.ReadKey();
                return damage * 2;
            }
            else //위 조건문 모두 false시 기본 피해량만 타격
            {
                Console.WriteLine("공격 성공! 피해량 : " + damage);
                Console.ReadKey();
                return damage;
            }
        }
        else // 데미지가 0과 같거나 작을경우 0으로 데미지 리턴 ( 음수가 될경우 생명력이 회복되는 현상을 막기위한 로직)
        {
            Console.WriteLine("상대가 너무 단단하다!");
            Console.ReadKey();
            return 0;
        }

    } // 플레이어의 공격과 공격 성공유무, 크리티컬 유무 판정



    public int AvoidanceToss() // 회피 성공 주사위
    {
        Random random = new Random();
        return random.Next(1, 100);
    }
    public int CrtToss() //크리티컬 성공 주사위
    {
        Random random = new Random();
        return random.Next(1, 100);
    }


}