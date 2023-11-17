public class Store : IStore
{
    public static List<Store> Itemlist = new List<Store>();
    public string Name { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public string ItemDescription { get; set; }
    public bool Stallation { get; set; }
    public int Gold { get; set; }
    public bool Buy { get; set; }  // 구매 여부를 나타내는 속성
    public int RemainingQuantity { get; set; } //만약 수량이 필요한 아이템을 만들면 사용

    public Store()
    {
        //상점 아이템
        Itemlist.Add(new Store("상점템1", 0, 0, "설명", false, 100, false));
        Itemlist.Add(new Store("상점템2", 0, 0, "설명", false, 100, false));
        Itemlist.Add(new Store("상점템3", 0, 0, "설명", false, 100, false));
        Itemlist.Add(new Store("상점템4", 0, 0, "설명", false, 100, false));
        Itemlist.Add(new Store("상점템5", 0, 0, "설명", false, 100, false));
    }
    //상점 아이템 생성자
    public Store(string name, int atk, int def, string ItemDescription, bool stallation, int Gold, bool buy)
    {
        this.Name = name;
        this.Atk = atk;
        this.Def = def;
        this.ItemDescription = ItemDescription;
        this.Stallation = stallation;
        this.Gold = Gold;
        this.Buy = buy;
    }
    public void DisplayStore()
    {
        Console.WriteLine("상점에 오신 것을 환영합니다!");
        Console.WriteLine("판매 중인 아이템:");

        foreach (var item in Itemlist)
        {
            Console.WriteLine($"{item.Name} - {item.Gold} 골드");
        }
    }
    //아이템을 구매하는 메서드
    public void BuyItem(Character character, Item item)
    {
        if (character.Gold >= item.Gold)
        {
            character.Gold -= item.Gold;
            Console.WriteLine($"{character.Name}님이 {item.Name}을(를) {item.Gold} 골드에 구매했습니다.");
            // 아이템을 인벤토리에 추가
        }
        else
        {
            Console.WriteLine("아이템을 구매하기에 골드가 부족합니다.");
        }
    }
}