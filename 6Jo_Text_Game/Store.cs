public class Store : IStore
{
    public static List<Store> Itemlist = new List<Store>();
    public string Name { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public string ItemDescription { get; set; }
    public bool Stallation { get; set; }
    public int Gold { get; set; }
    public bool IsSold { get; set; }  // 판매 여부를 나타내는 속성
    public int RemainingQuantity { get; set; }
    public Store()
    {
        //상점 아이템
        Itemlist.Add(new Store("상점템1", 0, 0, "설명", false, 100));
        Itemlist.Add(new Store("상점템2", 0, 0, "설명", false, 100));
        Itemlist.Add(new Store("상점템3", 0, 0, "설명", false, 100));
        Itemlist.Add(new Store("상점템4", 0, 0, "설명", false, 100));
        Itemlist.Add(new Store("상점템5", 0, 0, "설명", false, 100));
    }
    public Store(string name, int atk, int def, string ItemDescription, bool stallation, int Gold)
    {
        this.Name = name;
        this.Atk = atk;
        this.Def = def;
        this.ItemDescription = ItemDescription;
        this.Stallation = stallation;
        this.Gold = Gold;
        this.IsSold = false;
    }
    //아이템구매시 인벤토리로 이동하게하는?
    public void Add(Store item)
    {
        Itemlist.Add(item);
    }
    //구매한 아이템은 상점에서 삭제
    public static void DisplayStoreItems()
    {
        Console.WriteLine("상점 아이템 목록:");
        foreach (var item in Itemlist)
        {
            if (!item.IsSold)
            {
                Console.WriteLine($"Item: {item.Name}, ATK: {item.Atk}, DEF: {item.Def}, Gold: {item.Gold}");
            }
        }
    }
}