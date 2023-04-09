using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Friends
    {
        public static List<string> FriendsRecomendations(int userInputCount, int pairCount, List<string> pairs)
        {
            SortedDictionary<int, List<int>> usersInput = new SortedDictionary<int, List<int>>();

            Dictionary<int, List<int>> usersWithFriends = new Dictionary<int, List<int>>();

            Dictionary<string, SameFriend> sameFriends = new Dictionary<string, SameFriend>();

            for (int i = 1; i <= userInputCount; i++)
            {
                usersWithFriends.Add(i, new List<int>());
            }

            List<string> outputs = new List<string>();

            long validate = 0;
            long getIntersect = 0;
            long getMax = 0;

            return outputs;
        }

        public static List<string> FriendsRecomendations1(int userInputCount, int pairCount, List<string> pairs)
        {
            SortedDictionary<int, List<int>> usersInput = new SortedDictionary<int, List<int>>();

            Dictionary<int, List<int>> usersWithFriends = new Dictionary<int, List<int>>();

            Dictionary<string, SameFriend> sameFriends = new Dictionary<string, SameFriend>();

            for (int i = 1; i <= userInputCount; i++)
            {
                usersWithFriends.Add(i, new List<int>());
            }

            foreach(var pair in pairs)
            {
                string[] parts = pair.Split(' ');
                int user = Convert.ToInt32(parts[0]);
                int hisFriend = Convert.ToInt32(parts[1]);
                CheckPair(user, hisFriend, usersInput);
                CheckPair(hisFriend, user, usersInput);
            }
            foreach (var user in usersInput)
            {
                string s = "";
                foreach (int i in user.Value)
                {
                    s += "," + i;
                }
                if (!sameFriends.ContainsKey(s))
                    sameFriends.Add(s, new SameFriend(user.Value) { Users = new List<int>() { user.Key } });
                else
                    sameFriends[s].Users.Add(user.Key);
            }
            List<string> outputs = new List<string>();

            long validate = 0;
            long getIntersect = 0;
            long getMax = 0;

            foreach (var outputUser in usersWithFriends)
            {
                Dictionary<int, List<int>> commonsFriends = new Dictionary<int, List<int>>();
                List<int> validUsers = new List<int>();

                var k = usersInput.Where(x => x.Value.Contains(outputUser.Key));

                for (int i = 1; i <= userInputCount; i++)
                {
                    if (outputUser.Key == i) //если имя пользователя такое же
                        continue;
                    if (!usersInput.ContainsKey(i) || !usersInput.ContainsKey(outputUser.Key)) // если ввод не содержит пользователя
                        continue;
                    if (usersInput[outputUser.Key].Contains(i)) // если пользователь не является другом
                        continue;

                    int common = usersInput[outputUser.Key].Intersect(usersInput[i]).ToList().Count();
                    if (common == 0)
                        continue;

                    if (commonsFriends.ContainsKey(common))
                    {
                        commonsFriends[common].Add(i);
                    }
                    else
                    {
                        commonsFriends.Add(common, new List<int> { i });
                    }
                }

                if (commonsFriends.Count > 0)
                {
                    int maxKey = commonsFriends.Max(x => x.Key);
                    outputUser.Value.AddRange(commonsFriends[maxKey]);
                }

                if (outputUser.Value.Count == 0)
                    outputs.Add("0");
                else
                {
                    string s = "";
                    foreach (int i in outputUser.Value)
                    {
                        s += i.ToString() + " ";
                    }
                    outputs.Add(s.Trim(' '));
                }
            }
             return outputs;
        }

        private static void CheckPair(int user, int hisFriend, SortedDictionary<int, List<int>> pairs)
        {
            if (!pairs.ContainsKey(user))
            {
                pairs.Add(user, new List<int>() { hisFriend });
            }
            else
            {
                if (!pairs[user].Contains(hisFriend))
                    pairs[user].Add(hisFriend);
            }
        }
    }

    public class SameFriend
    {
        public SameFriend(List<int> sameFriends)
        {
            SameFriends = sameFriends;
        }
        public List<int> SameFriends { get; private set; }
        public List<int> Users { get; set; }
    }
}
