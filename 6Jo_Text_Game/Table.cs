
// 테이블을 나타내는 클래스입니다.
public class Table
{
        private Dictionary<string, TableDataType> dataTypes = new();
        private Dictionary<string, List<string>> datas = new();

        // 데이터 타입을 추가하는 메서드입니다.
        public bool AddType(string name, int length, bool center = false)
        {
                if (dataTypes.ContainsKey(name))
                        return false;

                dataTypes[name] = new TableDataType(name, length, center);
                return true;
        }

        // 데이터를 추가하는 메서드입니다.
        public void AddData(string name, string content)
        {
                
                if (!datas.TryGetValue(name, out List<string>? list))
                {
                        list = new List<string>();
                        datas[name] = list;
                }

                list.Add(content);
        }

        // 모든 데이터 타입을 가져오는 메서드입니다.
        public TableDataType[] GetTypes() => dataTypes.Values.ToArray();

        // 지정된 행의 데이터를 가져오는 메서드입니다.
        public string[] GetRow(int row)
        {
                string[] result = new string[dataTypes.Count];
                int index = 0;

                foreach (string key in dataTypes.Keys)
                {
                        result[index] = datas[key][row];
                        index++;
                }

                return result;
        }

        // 데이터의 행 수를 가져오는 메서드입니다.
        public int GetDataCount() => datas.First().Value.Count;
}
// 테이블의 데이터 타입을 정의하는 구조체입니다.
public struct TableDataType
{
        public string name;
        public int length;
        public bool center;

        public TableDataType(string name, int length, bool center = false)
        {
                this.name = name;
                this.length = length;
                this.center = center;
        }
}
