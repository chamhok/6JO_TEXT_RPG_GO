internal class UiManager
{

        private static int _categoryTopPostion = 3;

        private static int _goldLeftPostion = 64;

    private static int _goldTopPostion = 3;
    private static int _itemsTopPostion = 7;

    private char[,] buffer;

    public void MakeTab()
    {
        var currentCursor = Console.GetCursorPosition();

                int left = 2, top = 1, right = 90, bottom = 28;
                MakeUIContainer(left, top, right, bottom);

        Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
    }
    public void BattleMakeTab(int left, int top, int right, int bottom)
    {
        var currentCursor = Console.GetCursorPosition();

        MakeBattleContainer(left, top, right, bottom);

        Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
    }

    public void BattleClearTab(int left, int top, int right, int bottom)
    {
        var currentCursor = Console.GetCursorPosition();

        ClearBattleContainer(left, top, right, bottom);

        Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
    }
        private static void MakeBattleContainer(int left, int top, int right, int bottom)
        {   //┌ ─ ┐ └ ┘ │
                if (left < 0 || top < 0 || --right > Console.WindowWidth || --bottom > Console.WindowHeight) return;
                Console.SetCursorPosition(left, top);
                Console.Write("┌".PadRight(right - left - 1, '─'));
                Console.Write("┐");
                for (int i = top + 1; i < bottom; i++)
                {
                        Console.SetCursorPosition(left, i);
                        Console.Write("│".PadRight(right - left - 1, ' '));
                        Console.SetCursorPosition(right - 1, i);
                        Console.Write("│");
                }
                Console.SetCursorPosition(left, bottom);
                Console.Write("└".PadRight(right - left - 1, '─'));
                Console.Write("┘");
        }
        private static void ClearBattleContainer(int left, int top, int right, int bottom)
        {   //┌ ─ ┐ └ ┘ │
                if (left < 0 || top < 0 || --right > Console.WindowWidth || --bottom > Console.WindowHeight) return;
                Console.SetCursorPosition(left, top);
                for (int i = top + 1; i < bottom; i++)
                {
                        for (int j = left; j < right; j++)
                        {
                                Console.SetCursorPosition(j, i);
                                Console.Write(' ');
                        }
                }
        }
        public static void PrintDef()
    {
        var currentCursor = Console.GetCursorPosition();
        var player = GameData.I.GetCharacters().First();

                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion);
                Console.Write("┌─────────────────────────┐");
                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion + 1);
                Console.Write($"│ 방어력│ {player.Defense + 50}");
                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion + 2);
                Console.Write("┴─────────────────────────┤");

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
        }
        public void PrintGold()
        {
                var currentCursor = Console.GetCursorPosition();

                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion);
                Console.Write("┌─────────────────────────┐");
                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion + 1);
                Console.Write($"│ 소지금│ {GameData.I.GetCharacters().First().Gold.ToString().PadLeft(14, ' ')} G│");
                Console.SetCursorPosition(_goldLeftPostion, _goldTopPostion + 2);
                Console.Write("┴─────────────────────────┴");

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
        }
        public void PrintLevel()
        {
                Console.Clear();

                var currentCursor = Console.GetCursorPosition();
                var player = GameData.I.GetCharacters().First();

                int fillExpBar = (int)(8 * (float)player.CurrentExp / (player.Level * player.Level * 100));
                if (fillExpBar >= 8) fillExpBar = 8;

                Console.SetCursorPosition(0, 3);
                Console.Write("┌───────┬─────────────────┐");
                Console.SetCursorPosition(0, 3 + 1);
                Console.Write($"│ Lv {player.Level.ToString().PadLeft(3, ' ')}│ ");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write("".PadRight(fillExpBar, '　'));
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write("".PadRight(8 - fillExpBar, '　'));
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.SetCursorPosition(0, 3 + 2);
                Console.Write("├───────┴─────────────────┴");

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
        }
        public void PrintSkills()
        {
                var currentCursor = Console.GetCursorPosition();

                var skillList = GameData.I.GetSkill();

                Console.SetCursorPosition(5, 2);
                Console.Write("SKILL");

                Console.SetCursorPosition(80, 2);
                Console.Write("x. 닫기");
                Console.SetCursorPosition(4, 3);
                Console.Write("┏━━━━━━━━━━━━━━━┳━━━━━━━━━━┳━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");

                Console.SetCursorPosition(4, 4);
                Console.Write("┃   스킬명      ┃ 소모 마나┃                        스킬 설명                     ┃");

                Console.SetCursorPosition(4, 5);
                Console.Write("┗━━━━━━━━━━━━━━━┻━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

                for (int i = 0; i < skillList.Count && i < 21; i++)
                {
                        Skill skill = skillList[i];
                        Console.SetCursorPosition(4, 6 + i);
                        Console.Write($" {(i + 1).ToString().PadRight(4, ' ')}{skill.Name}");
                        Console.SetCursorPosition(20, 6 + i);
                        Console.Write($"ㅣ{"ddd".PadLeft(9, ' ')}ㅣ");
                        Console.Write($"{"뭐 어쨋드,ㄴ 졸라쌥니다 시바".PadLeft(20, ' ')} ");
                        int value = 0;
                        /*  switch (skill.ValueType)
                          {
                                  case ValueTypeEnum.PROPOTIONAL:
                                          value = (int)(GameManager.Instance.DataManager.Player.GetStatValue(skill.Stat) * skill.Value / 100f);
                                          break;
                                  case ValueTypeEnum.FIXED:
                                          value = skill.Value;
                                          break;
                          }*/

                        /* string aoe = skill.IsAoE ? "(광역기)" : "";
                         switch (skill.SkillType)
                         {
                                 case SkillType.DAMAGE:
                                         if (skill.Duration > 1)
                                         {
                                                 Console.Write($"{aoe}{skill.Duration}턴 동안 ");
                                                 if (value > 0)
                                                 {
                                                         Console.Write($"대상의 체력을 {value}씩 회복합니다.");
                                                 }
                                                 else if (value <= 0)
                                                 {
                                                         Console.Write($"대상에게 {-value}의 지속 피해를 입힙니다.");
                                                 }
                                         }
                                         else
                                         {
                                                 if (value > 0)
                                                 {
                                                         Console.Write($"{aoe}대상의 체력을 {value}만큼 회복합니다.");
                                                 }
                                                 else if (value <= 0)
                                                 {
                                                         Console.Write($"{aoe}대상에게 {-value}의 피해를 입힙니다.");
                                                 }
                                         }
                                         break;
                                 case SkillType.BUFF:
                                         string str = "";
                                         switch (skill.Stat)
                                         {
                                                 case Stats.MAXHP:
                                                         str = "체력";
                                                         break;
                                                 case Stats.MAXMP:
                                                         str = "마나";
                                                         break;
                                                 case Stats.ATK:
                                                         str = "공격력";
                                                         break;
                                                 case Stats.DEF:
                                                         str = "방어력";
                                                         break;
                                                 case Stats.CRITICALCHANCE:
                                                         str = "치명률";
                                                         break;
                                                 case Stats.CRITICALDAMAGE:
                                                         str = "치명타";
                                                         break;
                                                 case Stats.DODGECHANCE:
                                                         str = "회피율";
                                                         break;

                             }
                             if (value > 0)
                             {
                                     Console.Write($"{aoe}대상의 {str}을(를) {skill.Duration}턴 동안 {value}만큼 증가시킵니다.");
                             }
                             else if (value <= 0)
                             {
                                     Console.Write($"{aoe}대상의 {str}을(를) {skill.Duration}턴 동안 {value}만큼 감소시킵니다.");
                             }
                             break;
             }*/
        }
        Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
    }

    public void PrintHp()
    {
        var currentCursor = Console.GetCursorPosition();
        var player = GameData.I.GetCharacters().First();

        int rate = 20;
        int fillExpBar = (int)(rate * (float)player.Health / 100);
        if (fillExpBar >= rate) fillExpBar = rate;

        Console.SetCursorPosition(0, _goldTopPostion);
        Console.Write("┌──────┬───────────────┬───────┬──────────────────┬─────────┐");
        Console.SetCursorPosition(0, _goldTopPostion + 1);
        Console.Write($"│ 이 름│{player.Name, 14} │ 직 업 │{player.Job, 14} │ {player.Attribute,2}속성 │");
        Console.SetCursorPosition(0, _goldTopPostion + 2);
        Console.Write("├──────┴───────────────┴───────┴──────────────────┴─────────┴");



        Console.SetCursorPosition(64, _goldTopPostion);
        Console.Write("┌──────┬──────────────────────────────────────────┐");
        Console.SetCursorPosition(64, _goldTopPostion + 1);
        Console.Write($"│ 체 력│  ");
        Console.BackgroundColor = ConsoleColor.Red;
        Console.Write("".PadRight(fillExpBar, '　'));
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.Write("".PadRight(rate - fillExpBar, '　'));
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write("│");
        Console.SetCursorPosition(64, _goldTopPostion + 2);
        Console.Write("┴──────┴──────────────────────────────────────────┴");

        Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);
    }




    public void ShowMonsterCard(List<Monster> monsters)
    {
        var currentCursor = Console.GetCursorPosition();
        int size = monsters.Count;
        int top = 6, bottom = 19;
        int width = 21;

                List<int> leftPosition = new List<int>();

                switch (size)
                {
                        case 1:
                                leftPosition.Add(36);
                                break;
                        case 2:
                                leftPosition.Add(15);
                                leftPosition.Add(61);
                                break;
                        case 3:
                                leftPosition.Add(5);
                                leftPosition.Add(35);
                                leftPosition.Add(65);
                                break;
                        case 4:
                                leftPosition.Add(2);
                                leftPosition.Add(24);
                                leftPosition.Add(46);
                                leftPosition.Add(68);
                                break;
                }

                for (int i = 0; i < leftPosition.Count; i++)
                {
                        // 몬스터 카드 틀 생성
                        Console.ResetColor();
                        MakeUIContainer(leftPosition[i], top, leftPosition[i] + width, bottom);
                }

                for (int i = 0; i < size; i++)
                {
                        // 몬스터 Info 출력
                        Monster monster = monsters[i];

                        Console.SetCursorPosition(leftPosition[i] + 9, top + 1);
                        Console.Write($"Lv{monster.Level}");
                        Console.SetCursorPosition(leftPosition[i] + 2, top + 2);
                        int paddingSize = (17 - monster.Name.Length * 2) / 2;
                        if (monster.Name.IndexOf(' ') > 0)
                                paddingSize++;
                        Console.Write("".PadLeft(paddingSize, ' ') + monster.Name);

            int fillHpBar = (int)(7 * (float)monster.Health / monster.maxHealth + 0.9f);//현재 체력과 최대 체력이 필요한데 그냥 이렇게함
            if (fillHpBar >= 7) fillHpBar = 7;

            Console.SetCursorPosition(leftPosition[i] + 2, top + 4);
            string atkString = $"공격력 : {monsters[i].Attack}";
            paddingSize = (17 - (atkString.Length + 3)) / 2;
            Console.Write("".PadLeft(paddingSize, ' ') + atkString);
            Console.SetCursorPosition(leftPosition[i] + 2, top + 5);
            string defString = $"방어력 : {monsters[i].Defense}";
            paddingSize = (17 - (defString.Length + 3)) / 2;
            Console.Write("".PadLeft(paddingSize, ' ') + defString);
            Console.SetCursorPosition(leftPosition[i] + 2, top + 10);
            Console.SetCursorPosition(leftPosition[i] + 2, top + 6);
            string attributeString = $"속성 : {monsters[i].Attribute}속성";
            paddingSize = (17 - (attributeString.Length + 3)) / 2;
            Console.Write("".PadLeft(paddingSize, ' ') + attributeString);
            Console.SetCursorPosition(leftPosition[i] + 2, top + 10);
            string hpString = $"체력 : {monsters[i].Health}/{monsters[i].maxHealth}";//현재 체력과 최대 체력이 필요한데 그냥 이렇게함
            paddingSize = (17 - (hpString.Length + 2)) / 2;
            Console.Write("".PadLeft(Math.Max(paddingSize, 0), ' ') + hpString);


                        if (monsters[i].IsDead)
                        {
                                Console.SetCursorPosition(leftPosition[i] + 2, top + 7);
                                paddingSize = (17 - 4) / 2;
                                Console.Write("".PadLeft(paddingSize, ' '));

                                var color = Console.ForegroundColor;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("사망");
                                Console.ForegroundColor = color;
                                Console.Write(" ");
                        }


                        Console.SetCursorPosition(leftPosition[i] + 2, top + 11);
                        Console.Write("HP ");
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("".PadRight(fillHpBar, '　'));
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("".PadRight(7 - fillHpBar, '　'));
                        Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);

        }
        public void MakeUIContainer(int left, int top, int right, int bottom)
        {   //┌ ─ ┐ └ ┘ │
                if (left < 0 || top < 0 || --right > Console.WindowWidth || --bottom > Console.WindowHeight) return;

                Console.SetCursorPosition(left, top);
                Console.Write("┏".PadRight(right - left - 1, '━'));
                Console.Write("┓");

                for (int i = top + 1; i < bottom; i++)
                {
                        Console.SetCursorPosition(left, i);
                        Console.Write("┃".PadRight(right - left - 1, ' '));

                        Console.SetCursorPosition(right - 1, i);
                        Console.Write("┃");
                }


                Console.SetCursorPosition(left, bottom);
                Console.Write("┗".PadRight(right - left - 1, '━'));
                Console.Write("┛");
        }
        public void MakeOptionBox(List<string>? option)
        {
                var currentCursor = Console.GetCursorPosition();

                int left = 0, top = 20, right = 92, bottom = 30;

                MakeUIContainer(left, top, right, bottom);

                if (option != null)
                {
                        int tempLeft = left, tempTop = top;

                        for (int i = 0; i < option.Count; i++)
                        {
                                Console.SetCursorPosition(tempLeft + 2, tempTop + 2);
                                Console.Write(option[i].ToString());

                                tempLeft += right / 3;
                                if (tempLeft >= right - 5)
                                {
                                        tempLeft = left;
                                        tempTop += 2;
                                }

                        }
                }

                Console.SetCursorPosition(left + 2, bottom - 4);
                Console.Write("".PadRight(right - left - 4, '-'));

                Console.SetCursorPosition(left + 2, bottom - 2);
                Console.Write(">> ");

                Console.SetCursorPosition(currentCursor.Left, currentCursor.Top);

        }
        public void MakeStartBox(List<string>? option)
        {
                Console.ForegroundColor = ConsoleColor.White;

                int left = 100, top = 40, right = 140, bottom = 48;

                MakeUIContainer(left, top, right, bottom);

                if (option != null)
                {
                        int tempLeft = left, tempTop = top;
                        int selectedIndex = 0; // 선택된 텍스트의 인덱스

                        // 텍스트 출력
                        for (int i = 0; i < option.Count; i++)
                        {
                                Console.SetCursorPosition(tempLeft + 2, tempTop + 2);
                                Console.Write("   " + option[i].ToString());
                                tempTop += 2;
                        }

                        // 방향키 입력 처리
                        ConsoleKeyInfo keyInfo;
                        do
                        {
                                Console.ForegroundColor = ConsoleColor.White;

                                // 선택된 텍스트 강조 출력
                                Console.SetCursorPosition(tempLeft + 2, top + 2 + selectedIndex * 2);
                                Console.Write("▶ " + option[selectedIndex]);

                                keyInfo = Console.ReadKey(true);
                                switch (keyInfo.Key)
                                {

                                        case ConsoleKey.UpArrow:
                                                if (selectedIndex > 0)
                                                {
                                                        Console.ForegroundColor = ConsoleColor.White;

                                                        // 이전에 선택된 텍스트 강조 해제
                                                        Console.SetCursorPosition(tempLeft + 2, top + 2 + selectedIndex * 2);
                                                        Console.Write("   " + option[selectedIndex]);

                                                        selectedIndex--;
                                                }
                                                break;
                                        case ConsoleKey.DownArrow:
                                                if (selectedIndex < option.Count - 1)
                                                {
                                                        Console.ForegroundColor = ConsoleColor.White;

                                                        // 이전에 선택된 텍스트 강조 해제
                                                        Console.SetCursorPosition(tempLeft + 2, top + 2 + selectedIndex * 2);
                                                        Console.Write("   " + option[selectedIndex]);

                                                        selectedIndex++;
                                                }
                                                break;
                                }

                        } while (keyInfo.Key != ConsoleKey.Enter);

                        // 선택된 텍스트 처리
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                                switch (selectedIndex)
                                {
                                        case 0:
                                                break;
                                        case 1:
                                               
                                                break;
                                        default:
                                                break;
                                }
                        }

                }
        }


}