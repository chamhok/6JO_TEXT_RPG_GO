using System;
using System.Numerics;

delegate void StoryPick();
class ScreenManager
{
    StoryPick storyPick;
    Monster createMonster = new Monster();
    Character player;
    PlayerInfo playerInfo;
    MainScreen mainScreen = new MainScreen();
    BattleScreen battleScreen;

    public ScreenManager(Character player, BattleEvent battleEvent)
    {
        this.player = player;
        playerInfo = new PlayerInfo(player);
        battleScreen = new BattleScreen(battleEvent);
    }

    public void ShowMainScreen()
    {
        Console.Clear();
        StoryChanger();
        mainScreen.Display();
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Clear();
                playerInfo.Display();
                Console.ReadKey();
                ShowMainScreen();
                break; //캐릭터 정보창 호출 ( 별도의 키지정없이 아무키 입력시 다시 메인 화면으로 이동되게 설정)

            case "2": 
                Console.Clear();
                battleScreen.BattleStartSecen();
                ShowMainScreen();
                break;// 전투 화면으로 이동

            default:
                Console.WriteLine("잘못된입력입니다.");
                Console.Clear();
                mainScreen.Display();
                ShowMainScreen();
                break; //1 과 2가 아닌 입력을 받을시 메인 화면으로 다시 로드
        }
    }

    class MainScreen
    {
        public void Display()
        {
            Head();
            Body();
            Bottom();
        }

        private void Head()
        {
            Console.WriteLine("================================================");
        }

        public void Body()
        {
            Console.WriteLine("1. 정보창");
            Console.WriteLine("2. 전투");
        }
        private void Bottom()
        {
            Console.WriteLine("================================================");
        }
    } // 메인 화면

    class PlayerInfo
    {
        Character player;
        public PlayerInfo(Character player)
        {
            this.player = player;
        }

        public void Display()
        {
            Head();
            Body(player);
            Bottom();
        }

        private void Head()
        {
            Console.WriteLine("================================================");
        }

        public void Body(Character player)
        {
            Console.WriteLine("이름 : " + player.Name);
            Console.WriteLine("직업 : " + player.Job);
            Console.WriteLine("레벨 : " + player.Level);
            Console.WriteLine("공격력 : " + player.Attack);
            Console.WriteLine("방어력 : " + player.Defense);
            Console.WriteLine("직업 : " + player.Speed);
            Console.WriteLine("치명타 : " + player.Crt);
            Console.WriteLine("회피 : " + player.Avoidance);
            Console.WriteLine("Gold : " + player.Gold);
        }

        private void Bottom()
        {
            Console.WriteLine("================================================");
        }
    } // 플레이어 정보 화면

    public void Prologue()
    {
        Console.WriteLine("왕궁 내부, 크고 차가운 조명이 비춰진 회의실.\r\n왕은 위엄있게 앉아 있고, 기사로 차려입은 주인공은 머리를 숙인 채 왕 앞에 서 있다.\r\n왕: \"기사, 네 충성을 알아주노라. 나는 네 노고를 자랑스러워하노라. 오늘 네게 맡길 임무는 매우 중대하다.\"\r\n기사: \"메리의 명복을 위하여, 왕의 명령에 헌신하겠습니다.\"\r\n왕: \"제국은 나를 크게 우려하며 전쟁에 대비하고 있다. 정보에 의하면 그 과정을 상세하게 담은 기밀 문서가 있다고 한다. 그 기밀 문서를 찾아 가져오거라.\"\r\n기사: \"왕의 명령에 따라, 제 최선을 다하겠습니다. 이 임무를 완수하겠습니다.\"\r\n왕: \"기억하라, 네 충성은 나의 영광이 되며, 네 행동은 우리 왕국의 운명을 결정짓게 될 것이다. 이리와라, 네 검이 우리 왕국을 지킬 것이다.\"\r\n기사는 머리를 숙이고, 자신의 이름을 왕에게 전달한 뒤 궁전을 떠난다. 이제 모험이 시작된다.");
        Console.ReadKey();
    }

    public void StoryChanger()
    {
        switch(player.Wincount)
        {
            case 0:
                storyPick=Chapter1;
                storyPick();
                break;
            case 1:
                storyPick=Chapter2;
                storyPick();
                break;
            default:
                Console.WriteLine("아직 스토리가 없습니다.");
                break;
        }
    }

    void Chapter1()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("경비병: ");
        Console.ResetColor();
        Console.WriteLine("여기에 어떻게 왔냐?");
        // 나머지 대사는 흰색으로 출력
        Console.ForegroundColor = ConsoleColor.White; // 텍스트 색을 흰색으로 변경
        Console.WriteLine("하지만 경비병의 분위기는 약간 바뀐 것 같고, 눈에 비친 빛이 번쩍이고 있다.");
        Console.WriteLine("기사는 미소를 지으며 말한다.");
        Console.WriteLine("기사: \"오랜 여행을 떠났다보니, 여러 길을 통해 이곳까지 왔습니다. 이곳은 참 아름다운 곳이로군요.\"");
        Console.WriteLine("경비병은 고개를 가볍게 끄덕이면서 눈에 가려져 있는 투구를 들쓰고 기사를 자세히 들여다본다.");
        Console.WriteLine("경비병: \"그래 이 나라는 아름다운 곳이지.. 그런데 자네는 아름다운 곳이라고 말하면서도 주변을 살피지 않더군.\"");
        Console.WriteLine("그의 안광이 빛을 뿜어 내고 있다.");
        Console.WriteLine("주인공은 방심한 자신을 탓하고도 여전히 미소를 유지하며 말한다.");
        Console.WriteLine("기사: \"눈을 뗄수가 없었더군요. 자, 여기 상인회의 증서를 가져왔습니다.\"");
        Console.WriteLine("주인공이 내민것은 제국령의 상인회 증명문서, 어지간한 것들보다 확실하게 신분을 증명할 수 있을 만한 물건이다.");
        Console.WriteLine("그랬을것이다..만..");
        Console.WriteLine("경비병의 표정이 갑자기 변한다. 그의 눈에는 의심의 그림자가 번쩍였다.");
        Console.WriteLine("경비병: \"기다려봐. \"");
        Console.WriteLine("주인공은 싸늘한 그 한마디에 온몸에서 식은땀이 새어 나왔다.");
        Console.WriteLine("이득고 경비병은 증서의 한 부분을 가리키며 말한다.");
        Console.WriteLine("경비병: \"이 문장은 타국의 스파이를 위해 심어둔 가짜 문장이다.\"");
        Console.WriteLine("이 순간, 두 사람 사이에 긴장된 분위기가 고조된다.");
    }

    void Chapter2()
        {
            Console.WriteLine("경비를 물리친 주인공은 미끄러운 듯이 빠르게 나아갔다.\r\n이미 시작된 전투에서, 이 임무의 성공 가능성은 극히 낮아졌다.\r\n성벽 내부에 대한 정확한 정보가 이미 알려져 있었지만,\r\n미로처럼 얽힌 골목과 강력한 경비 시스템으로 둘러싸인 성 내부를 헤쳐 나가는 것은 쉽지 않았다.\r\n주인공은 세간를 뚫고 나아가는 듯한 기세로 전진했다.\r\n그가 향한 곳은 기밀 문서가 보관되어 있는 요새였다. 다른 이들이 길을 막기 전에 임무를 완수해야 한다.\r\n그러나 요새 안에서 한 기사가 주인공을 발견하고 다가왔다.\r\n기사: \"너 누구냐! 여기서 뭐하는 거냐?\"\r\n주인공: \"젠장.\"\r\n기사는 허리에 달린 나팔을 즉시 불렀다.\r\n나팔 소리에 함께, 요새에서 기사들이 나와 주변을 채우기 시작했다.");
        }

}

class BattleScreen // 전투 진행 화면
{
    BattleEvent battleEvent;

    public BattleScreen(BattleEvent battleEvent)
    {
        this.battleEvent = battleEvent;
    }

    public void BattleStartSecen()
    {
        Console.WriteLine("전투시작.");
        battleEvent.Battles();
    }
}



