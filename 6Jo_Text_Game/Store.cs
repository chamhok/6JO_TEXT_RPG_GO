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
    public Store()
    {
        Itemlist.Add(new Store("상점템1", 0, 0, "설명", false, 100, false));
        Itemlist.Add(new Store("상점템2", 0, 0, "설명", false, 100, false));
    }
    public void Add(Store item)
    {
        Itemlist.Add(item);
    }

    public void DisplayStore()
    {
        Console.WriteLine("상점에 오신 것을 환영합니다!");

        Console.WriteLine("1. 구매하기");
        Console.WriteLine("2. 판매하기");
        Console.WriteLine("3. 나가기");

        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        int input = CheckValidInput(1, 3);
        switch (input)
        {
            case 1:
                BuyItem();
                break;

            case 2:
                SellItem();
                break;

            case 3:
                
                break;
        }
    }
    //아이템을 구매
    public void BuyItem()
    {
        Console.Clear();

        Console.WriteLine("구매하기");
        Console.WriteLine("판매 중인 아이템을 구매할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine($"[보유 골드 : 골드 ]");
        Console.WriteLine();
        Console.WriteLine($"[아이템 목록]");
        Console.WriteLine();
        foreach (var item in Item.Store)
        {
            Console.WriteLine(item.Name);
            string a = ((Item.Store.IndexOf(item) + 1).ToString().Length) > 1 ? "" : " ";
            Console.WriteLine($"- {Item.Store.IndexOf(item) + 1}" +
                    $"{a}" +
                    $"{Item.stallationManagement(item)}" +
                    $"{item.Name + new string('　', 10 - item.Name.Length)}" +
                    $"{item.Akt} " +
                    $"{item.Def} " +
                    $"{item.ItemDescription}" +
                    $"{item.Gold}G " +
                    $"{Item.BuyManagement(item)}");
        }
        Console.WriteLine("0. 나가기");
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                Console.Clear();
                DisplayStore();
                break;
        }
    }
    //아이템을 판매
    public void SellItem()
    {
        Console.Clear();

        Console.WriteLine("판매하기");
        Console.WriteLine("판매 중인 아이템을 판매할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine($"[보유 골드 : 골드 ]");
        Console.WriteLine();
        Console.WriteLine($"[아이템 목록]");
        Console.WriteLine();
        foreach (var item in Item.Store)
        {
            Console.WriteLine(item.Name);
            string a = ((Item.Store.IndexOf(item) + 1).ToString().Length) > 1 ? "" : " ";
            Console.WriteLine($"- {Item.Store.IndexOf(item) + 1}" +
                    $"{a}" +
                    $"{Item.stallationManagement(item)}" +
                    $"{item.Name + new string('　', 10 - item.Name.Length)}" +
                    $"{item.Akt} " +
                    $"{item.Def} " +
                    $"{item.ItemDescription}" +
                    $"{item.Gold}G " +
                    $"{Item.BuyManagement(item)}");
        }
        Console.WriteLine("0. 나가기");
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");
        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                Console.Clear();
                DisplayStore();
                break;
        }
    }

        static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }
            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}