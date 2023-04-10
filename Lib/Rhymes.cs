using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Rhymes
    {
        public static List<string> Calculate(List<string> dict, List<string> queries)
        {
            var output = new List<string>();
            PrefixTree tree = new PrefixTree();
            dict.Sort();
            foreach (string s in dict)
            {
                string reversed = new StringBuilder().Append(s.Reverse().ToArray()).ToString();
                tree.Add(reversed);
            }
            for (int i = 0; i < queries.Count; i++)
            {
                string name = queries[i];
                string reversedInput = new StringBuilder().Append(name.Reverse().ToArray()).ToString();
                string result = "";
                List<string> values = new List<string>();

                while (!string.IsNullOrEmpty(reversedInput))
                {
                    values.AddRange(GetValues(tree, reversedInput, name));
                    if (values.Count > 0)
                    {
                        result = values[0];
                        break;
                    }
                    else
                    {
                        reversedInput = reversedInput.Remove(reversedInput.Length - 1);
                    }
                }
                if (result.Length == 0)
                {
                    if (dict[0] != name)
                        result = dict[0];
                    else
                        result = dict[1];
                }

                output.Add(result);
            }
            return output;
        }

 

        private static List<string> GetValues(PrefixTree tree, string reversedInput, string name)
        {
            List<string> reversedValues = tree.GetValuesByPrefix(reversedInput);
            List<string> values = new List<string>();

            if (string.IsNullOrEmpty(reversedInput))
                return values;

            foreach (string rev in reversedValues)
            {
                string s = new StringBuilder().Append(rev.Reverse().ToArray()).ToString();
                values.Add(s);
            }
            values.RemoveAll(x => x == name);
            return values;
        }


    }


    internal struct TreeNode
    {
        public SortedDictionary<char, TreeNode> Childs { get; set; }
        public Char Value { get; set; }
        public bool IsLeaf { get; set; }
        public TreeNode(char val)
        {
            Value = val;
            Childs = new SortedDictionary<char, TreeNode>();
            IsLeaf = false;
        }
    }

    class PrefixTree
    {
        public PrefixTree()
        {
            TreeNode tree = new TreeNode();
            tree.Childs = new SortedDictionary<char, TreeNode>();
            Root = tree;
        }
        TreeNode Root;

        public void Add(string key)
        {
            TreeNode current = Root;

            for (int i = 0; i < key.Length; i++)
            {
                if (!current.Childs.ContainsKey(key[i]))
                {
                    TreeNode treeNode = new TreeNode(key[i]);
                    if (i + 1 == key.Length)
                        treeNode.IsLeaf = true;
                    current.Childs.Add(key[i], treeNode);
                }

                //if (i + 1 == key.Length)
                //    current.Childs[key[i]].IsLeaf = true;

                current = current.Childs[key[i]];
            }
        }


        List<string> _allSuffixes = new List<string>();
        public List<string> GetValuesByPrefix(string prefix)
        {

            List<string> values = new List<string>();
            _allSuffixes = new List<string>();
            TreeNode current = Root;
            string commonPrefix = "";
            for (int i = 0; i < prefix.Length; i++)
            {
                if (i == 0 && !current.Childs.ContainsKey(prefix[i]))
                    return values;

                bool isKeyExist = current.Childs.ContainsKey(prefix[i]);
                if (isKeyExist)
                {
                    commonPrefix += current.Childs[prefix[i]].Value;
                    current = current.Childs[prefix[i]];
                }
                else
                {
                    break;
                }

            }
            SetAllSuffixes(current, "");
            foreach (string suffix in _allSuffixes)
            {
                values.Add(commonPrefix + suffix);
            }
            return values;
        }

        private void SetAllSuffixes(TreeNode root, string s)
        {
            if (root.Childs.Count == 0 || root.IsLeaf)
            {
                _allSuffixes.Add(s);
            }

            if (_allSuffixes.Count > 10)
                return;

            foreach (var tree in root.Childs)
            {
                string res = s + tree.Value.Value;
                SetAllSuffixes(tree.Value, res);
            }
        }
    }
}
