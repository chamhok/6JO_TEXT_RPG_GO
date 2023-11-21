using System.Security.Cryptography.X509Certificates;

public class Item : IItem
{
    SoundManager soundManager = new SoundManager();
    //아이템 속성 커밋테스트
    public string Name { get; set; }
    public float Akt { get; set; }
    public float Def { get; set; }
    public string ItemDescription { get; set; }
    public bool Stallation { get; set; }
    public bool Buy { get; set; }
    public int Gold { get; set; }
    public static List<Item> Store = new List<Item>();
    public float ItemAbility { get; set; }

    // 기본 생성자
    public Item()
    {
        GameData.I.AddItem(new Item("무쇠갑옷", 0, 2, "무쇠로 만들어져 튼튼한 갑옷입니다.", false, true, 200));
        GameData.I.AddItem(new Item("낡은검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", false, true, 200));
        GameData.I.AddItem(new Item("청동 도끼", 0, 5, "어디선가 사용됐던거 같은 도끼입니다.", false, false, 1500));
        GameData.I.AddItem(new Item("스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", false, false, 0));
        GameData.I.AddItem(new Item("제우스의 번개", 0, 20, "그리스 신 제우스의 무기로, 강력한 번개를 날릴 수 있습니다.", false, false, 5000));
        GameData.I.AddItem(new Item("아레스의 갑옷", 20, 0, "전쟁의 신 아레스가 사용한 갑옷으로, 강력한 방어를 제공합니다.", false, false, 4500));
        GameData.I.AddItem(new Item("아테나의 방패", 10, 10, "지혜의 여신 아테나의 방패로, 공격과 방어에 모두 탁월한 효과를 줍니다.", false, false, 4000));
        GameData.I.AddItem(new Item("우키의 망치", 0, 15, "북유럽 신화의 신 우키의 망치로, 강력한 공격 능력을 지닌 무기입니다.", false, false, 4200));
        GameData.I.AddItem(new Item("오닉스의 목걸이", 5, 5, "그리스 신화의 오닉스의 눈을 상징하는 목걸이로, 공격과 방어를 동시에 향상시킵니다.", false, false, 3800));
        GameData.I.AddItem(new Item("히노카구타", 12, 8, "일본 신화에서 나온 신기한 검으로, 화염을 뿜어내는 능력이 있습니다.", false, false, 4600));
        GameData.I.AddItem(new Item("용의 심장", 8, 12, "한국 신화에서 나온 용을 상징하는 심장으로, 강력한 공격력을 부여합니다.", false, false, 4300));
        GameData.I.AddItem(new Item("해신의 투구", 10, 8, "한국 신화의 바다 신, 해신의 투구로, 물 속에서 강력한 방어 능력을 발휘합니다.", false, false, 4100));
    }
    /// <summary>
    /// 일반 장비아이템 생성입니다.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="atk"></param>
    /// <param name="def"></param>
    /// <param name="ItemDescription"></param>
    /// <param name="stallation"></param>
    /// <param name="buy"></param>
    /// <param name="gold"></param>
    // 매개변수가 있는 생성자
    public Item(string name, int atk, int def, string ItemDescription, bool stallation, bool buy, int gold)
    {
        // 아이템 속성들 초기화
        this.Name = name;
        this.Akt = atk;
        this.Def = def;
        this.ItemDescription = ItemDescription;
        this.Stallation = stallation;
        this.Buy = buy;
        this.Gold = gold;
    }

    /// <summary>
    /// 보상 아이템 생성입니다.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="itemAbility"></param>
    public Item(string name, float itemAbility)
    {
        this.Name = name;
        this.ItemAbility = itemAbility;
    }

    public void Use(Character character)
    {
        // 보상 아이템 사용 시 캐릭터의 능력치 증가 등을 구현
        Apply(character);
        string Message;

        // 출력 형태 수정
        if (this.Name.Contains("Postion"))
        {
            Message = "을(를) 먹었습니다.";
            soundManager.CallSound("sound2", 1);
            Console.WriteLine($"\n{character.Name}이(가) {this.Name}{Message}");
        }
        else
        {
            // 이미 가지고 있는 아이템이라면 가격만큼 골드로 되돌려받기
            if (character.Inventory.Any(x => x.Name == this.Name))
            {
                character.Gold += this.Gold;
                Console.WriteLine($"\n{character.Name}이(가) {this.Name}을(를) 이미 가지고 있습니다. 가격만큼 골드로 환불되었습니다.");
            }
            else
            {
                Message = "을(를) 획득하였습니다.";
                soundManager.CallSound("sound2", 1);
                Console.WriteLine($"\n{character.Name}이(가) {this.Name}{Message}");
                character.Inventory.Add(this); //인벤토리에 아이템 추가
            }    
        }
        Console.ReadKey(true);
    }

    public enum RewardItems
    {
        HealthPostion,
        AttackPostion,
        CrazyWood,
        VeryCrazySword,
        SuperCrazyHammer
    }

    // 아이템마다 다르게 효과 적용
    public void Apply(Character character)
    {
        switch (Enum.Parse(typeof(RewardItems), this.Name))
        {
            case RewardItems.HealthPostion:
                character.Health += this.ItemAbility;
                break;
            case RewardItems.AttackPostion:
                character.Attack += this.ItemAbility;
                break;
            case RewardItems.CrazyWood:
                break;
            case RewardItems.VeryCrazySword:
                break;
            case RewardItems.SuperCrazyHammer:
                break;
            default:
                break;
        }
    }

    // 아이템 제거 메서드
    private void RemoveItem()
    {
        GameData.I.RemoveItem(this);
    }

    // 아이템 추가 메서드
    private void Add(IItem item)
    {
        GameData.I.AddItem(item);
    }

    // 현재 인스턴스의 아이템 추가 메서드
    public void Add()
    {
        GameData.I.AddItem(this);
    }

    // 장착 여부 뒤집기 메서드
    public static void stallationReverse(Item item)
    {
        item.Stallation = item.Stallation == true ? false : true;
    }
    // 구매 여부 뒤집기 메서드
    public static void BuyReverse(Item item)
    {
        item.Buy = item.Buy == true ? false : true;
    }
    // 장착 여부 관리 문자열 반환 메서드
    public static string stallationManagement(Item item)
    {
        return item.Stallation == true ? "[E]" : "   ";
    }
    // 구매 여부 관리 문자열 반환 메서드
    public static string BuyManagement(Item item)
    {
        return item.Buy == true ? "[구매완료]" : "   ";
    }
    // 객체를 문자열로 표현하는 메서드 오버라이드
    public override string ToString()
    {
        return $"{this.Name} " +
                $"{this.Akt} " +
                $"{this.Def} " +
                $"{this.ItemDescription} " +
                $"{this.Stallation} " +
                $"{this.Gold}";
    }
}
