using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Vertex
    {
        public Vertex(char name, int id)
        {
            Name = name;
            Id = id;
            IsVisited = name == '.';
        }
        public bool IsVisited { get; set; }
        public char Name { get; private set; }
        public int Id { get; private set; }
        public List<int> Vertices { get; set; }
    }
    public class Map
    {
        private static List<char> _map = new List<char>();
        private static Graph _graph;

        public static List<string> MapValidateShort(string[] args)
        {
            int setCount = Convert.ToInt32(args[0]);
            List<string> output = new List<string>();
            int offset = 1;
            for (int p = 0; p < setCount; p++)
            {
                _map = new List<char>();
                List<int> input = args[offset++].Split(' ').Select(x => int.Parse(x)).ToList();
                int rows = input[0];
                int symbols = input[1];
                List<List<string>> vertices = new List<List<string>>();

                for(int i = 0; i < rows; i++)
                {
                    char[] row = args[offset++].ToArray();
                    for(int j = 0; j < symbols; j++)
                    {
                        if (row[j] == '.')
                            continue;

                    }

                }
           
            }
            return output;
        }
        public static List<string> MapValidate(string[] args)
        {
            int setCount = Convert.ToInt32(args[0]);
            List<string> output = new List<string>();
            int offset = 1;
            for (int p = 0; p < setCount; p++)
            {
                _map = new List<char>();
                List<int> input = args[offset++].Split(' ').Select(x => int.Parse(x)).ToList();
                int rows = input[0];
                int symbols = input[1];
                _graph = new Graph(rows, symbols);

                for (int i = 0; i < _graph.Length; i += symbols)
                {
                    char[] q = args[offset++].ToArray();
                    for (int j = 0; j < q.Length; j++)
                    {
                        if (!_map.Contains(q[j]) && q[j] != '.')
                            _map.Add(q[j]);
                        _graph.AddVertex(q[j], i + j);
                    }
                }
                _graph.CreateEdges();
                //GraphTraversal(graph.Vertices);
                foreach (var color in _map)
                {
                    Vertex v = _graph.Vertices.First(x => x.Name == color);
                    DiscoverArea(color, v.Id);
                }
                bool remaind = _graph.Vertices.Any(x => x.IsVisited == false);
                output.Add(remaind ? "NO" : "YES");
            }
            return output;
        }

        private static void DiscoverArea(char area, int id)
        {
            if (_graph.Vertices[id].IsVisited)
                return;
            if (_graph.Vertices[id].Name != area)
                return;
            _graph.Vertices[id].IsVisited = true;
            foreach (var i in _graph.Vertices[id].Vertices)
            {
                DiscoverArea(area, i);
            }
        }
    }

    internal class Graph
    {
        public int Length { get; private set; }
        public Vertex[] Vertices;
        private int _rows;
        private int _symbols;
        private bool _isSymbolsEven;
        public Graph(int rows, int symbols)
        {
            Length = rows * symbols;
            Vertices = new Vertex[Length];
            _rows = rows;
            _symbols = symbols;
            _isSymbolsEven = _symbols % 2 == 0;
        }
        public void AddVertex(char name, int id)
        {
            Vertices[id] = new Vertex(name, id);
        }
        private bool _isEvenRow = false;
        private int _currentRow;
        public void CreateEdges()
        {
            _currentRow = 0;

            for (int i = 0; i < Length; i += _symbols)
            {
                _currentRow++;
                _isEvenRow = _currentRow % 2 == 0;
                for (int k = 0; k < _symbols; k++)
                {
                    bool isEvenSymbol = (k + 1) % 2 == 0;
                    if ((_isEvenRow && !isEvenSymbol) || (!_isEvenRow && isEvenSymbol))
                        continue;
                    bool isLeft = _isEvenRow ? k == 1 : k == 0;
                    bool isRight;
                    if (!_isSymbolsEven)
                        isRight = _isEvenRow ? k == _symbols - 2 : _symbols - 1 == k;
                    else
                        isRight = !_isEvenRow ? k == _symbols - 2 : _symbols - 1 == k;

                    int[] neighbours = new int[]
                    {
                        CheckDownLeft(i+k,isLeft),
                        CheckDownRight(i+k,isRight),
                        CheckRight(i+k,isRight),
                        CheckUpRight(i+k,isRight),
                        CheckUpLeft(i+k,isLeft),
                        CheckLeft(i+k,isLeft)
                    };
                    Vertices[i + k].Vertices = new List<int>();
                    foreach (int n in neighbours)
                    {
                        if (n >= 0)
                            Vertices[i + k].Vertices.Add(n);
                    }
                }
            }
        }

        private int CheckRight(int v, bool isRight)
        {
            if (isRight)
                return -1;
            return v + 2;
        }
        private int CheckUpRight(int v, bool isRight)
        {
            if (_currentRow < 2)
                return -1;
            if (_isSymbolsEven)
            {
                if (_isEvenRow && isRight)
                    return -1;
                return (v - _symbols) + 1;
            }
            else
            {
                if (!_isEvenRow && isRight)
                    return -1;
                return (v - _symbols) + 1;
            }

        }
        private int CheckUpLeft(int v, bool isLeft)
        {
            if (_currentRow < 2)
                return -1;
            if (!_isEvenRow && isLeft)
                return -1;
            return (v - _symbols) - 1;
        }
        private int CheckLeft(int v, bool isLeft)
        {
            if (isLeft)
                return -1;
            return v - 2;
        }
        private int CheckDownRight(int v, bool isRight)
        {
            if (_currentRow >= _rows)
                return -1;
            if (_isSymbolsEven)
            {
                if (_isEvenRow && isRight)
                    return -1;
                return (v + _symbols) + 1;
            }
            else
            {
                if (!_isEvenRow && isRight)
                    return -1;
                return (v + _symbols) + 1;
            }
        }
        private int CheckDownLeft(int v, bool isLeft)
        {
            if (_currentRow >= _rows)
                return -1;
            if (!_isEvenRow && isLeft)
                return -1;
            return (v + _symbols) - 1;
        }
    }
}
