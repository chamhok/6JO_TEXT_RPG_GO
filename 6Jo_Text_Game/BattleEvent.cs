using System.Numerics;
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

    public void CoinToss()
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

    public void PlayerTurn()
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
    }

    public void MonsterResult()
    {
        monster.TakeDamage((float)PlayerDmg());
    }

    public void PlayerResult()
    {
        player.TakeDamage((float)MonsterDmg());
    }


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
            player.IsDead = false;
            Console.WriteLine("전투패배!");
            Console.ReadKey();
        }
        else
        {
            monster.IsDead = false;
            Console.WriteLine("전투승리! 승리횟수: " + wincount);
            Console.ReadKey();
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
    }

    public float PlayerDmg()
    {
        float damage = (player.Attack * skillPsmash) - (monster.Defense * skillMguard);
        skillPsmash = 1;
        skillMguard = 1;

        if (damage > 0)
        {
            if (AvoidanceToss() <= monster.Avoidance)
            {
                Console.WriteLine("몬스터가 회피하였습니다!");
                Console.ReadKey();
                return 0;
            }
            else if (CrtToss() <= player.Crt)
            {
                Console.WriteLine("치명적 일격! 피해량 : " + damage * 2);
                Console.ReadKey();
                return damage * 2;
            }
            else
            {
                Console.WriteLine("공격 성공! 피해량 : " + damage);
                Console.ReadKey();
                return damage;
            }
        }
        else
        {
            Console.WriteLine("상대가 너무 단단하다!");
            Console.ReadKey();
            return 0;
        }

    }



    public int AvoidanceToss()
    {
        Random random = new Random();
        return random.Next(1, 100);
    }
    public int CrtToss()
    {
        Random random = new Random();
        return random.Next(1, 100);
    }


}