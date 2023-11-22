using System.Text;
// 게임 화면을 콘솔에 그리는 역할을 하는 Renderer 클래스입니다.
public static class Renderer
{
        // 콘솔 창의 높이와 길이를 저장하는 변수들입니다.
        private static readonly int inputAreaHeight = 1;
        private static readonly string inputAreaString = ">> ";
        private static readonly int printMargin = 2;

        private static int width;
        private static int height;

        // 게임 화면 초기화 메서드입니다.
        public static void Initialize(string gameName)
        {
                Console.Title = gameName;
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.OutputEncoding = Encoding.UTF8;
        }

        // 게임 화면에 테두리를 그리는 메서드입니다.
        public static void DrawBorder(string title = "")
        {
                Console.Clear();
                width = Console.LargestWindowWidth;
                height = Console.LargestWindowHeight;

                Console.SetCursorPosition(0, 0);
                Console.Write(new string('━', width));

                for (int i = 1; i < height - inputAreaHeight - 2; i++)
                {
                        Console.SetCursorPosition(0, i);
                        Console.Write('┃');
                        Console.SetCursorPosition(width - 1, i);
                        Console.Write('┃');
                }

                if (!string.IsNullOrEmpty(title))
                {
                        Console.SetCursorPosition(0, 2);
                        Console.Write(new string('━', width));
                        int correctLength = GetPrintingLength(title);
                        int horizontalStart = (width - correctLength) / 2;
                        if (horizontalStart < 0) horizontalStart = 3;
                        Console.SetCursorPosition(horizontalStart, 1);
                        Console.WriteLine(title);
                }

                Console.SetCursorPosition(0, height - inputAreaHeight - 2);
                Console.Write(new string('━', width));
        }

        // 문자열의 출력 길이를 계산하는 메서드입니다.
        private static int GetPrintingLength(string line) => line.Sum(c => IsKorean(c) ? 1 : 0);

        // 주어진 문자가 한글인지 확인하는 메서드입니다.
        private static bool IsKorean(char c) => '가' <= c && c <= '힣';



        // 지정된 행에 문자열을 출력하는 메서드입니다.
        public static void Print(int line, string content)
        {
                Console.SetCursorPosition(printMargin, line);
                Console.WriteLine(content);
        }

        // 화면에 테이블을 그리는 메서드입니다.
        public static void DrawTable(Table table, int startLine, int selectedRow)
        {
                var types = table.GetTypes();
                int currentLine = startLine;

                // 헤더 행을 그립니다.
                StringBuilder header = new StringBuilder("┃   ");
                foreach (var type in types)
                {
                        header.Append(type.name.PadRight(type.length));
                        header.Append(" ┃ ");
                }
                Print(currentLine++, header.ToString());

                // 수평 선을 그립니다.
                StringBuilder horizontalLine = new StringBuilder("╋━━");
                foreach (var type in types)
                {
                        horizontalLine.Append(new string('━', type.length + 2));
                        horizontalLine.Append("╋");
                }
                Print(currentLine++, horizontalLine.ToString());

                int dataCount = table.GetDataCount();
                for (int i = 0; i < dataCount; i++)
                {
                        StringBuilder row = new StringBuilder("┃ ");
                        if (i == selectedRow)
                        {
                                row.Append("▶");
                        }
                        else
                        {
                                row.Append("　");
                        }
                        var rowData = table.GetRow(i);
                        for (int j = 0; j < types.Length; j++)
                        {
                                string data = rowData[j];
                                int dataLength = GetPrintingLength(data);
                                row.Append(data.PadRight(types[j].length - dataLength));
                                row.Append(" ┃ ");
                        }
                        Print(currentLine++, row.ToString());
                }
        }

        // 화면에 입력 영역을 그리는 메서드입니다.
        public static void DrawInputArea()
        {
                Console.SetCursorPosition(printMargin, height - inputAreaHeight - 1);
                Console.Write(inputAreaString);
        }
        // 화면에 입력 영역을 그리는 메서드입니다.
        public static void DrawInputInventoryArea()
        {
                Console.SetCursorPosition(printMargin, height - inputAreaHeight - 1);
                Console.Write("        Z를 누르면 나갑니다.");
        }
}
