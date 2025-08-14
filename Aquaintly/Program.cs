using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Aquaintly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var events = new List<string[]>
            {
                new[] { "CONNECT","Alice", "Bob" },
                new[] { "DISCONNECT","Bob", "Alice" },
                new[] { "CONNECT","Alice", "Charlie" },
                new[] { "CONNECT","Dennis", "Bob" },
                new[] { "CONNECT","Pam", "Dennis" },
                new[] { "DISCONNECT","Pam", "Dennis" },
                new[] { "CONNECT","Pam", "Dennis" },
                new[] { "CONNECT","Edward", "Bob" },
                new[] { "CONNECT","Dennis", "Charlie" },
                new[] { "CONNECT","Alice", "Nicole" },
                new[] { "CONNECT","Pam", "Edward" },
                new[] { "DISCONNECT","Dennis", "Charlie" },
                new[] { "CONNECT","Dennis", "Edward" },
                new[] { "CONNECT","Charlie", "Bob" },
            };

            var result= Grouping(events,3);
            Console.WriteLine(String.Join(",",result.Item1));
            Console.WriteLine(String.Join(",", result.Item2));
        }


        public static (List<string>,List<string>) Grouping(List<string[]> events, int count)
        {
            Dictionary<string, HashSet<string>> connections = new Dictionary<string, HashSet<string>>();

            foreach (var e in events)
            {
                string action = e[0];
                string user1 = e[1];
                string user2 = e[2];

                if(action=="CONNECT")
                {
                    if (!connections.ContainsKey(user1))
                        connections.Add(user1, new HashSet<string>());
                         
                    if (!connections.ContainsKey(user2))
                        connections.Add(user2, new HashSet<string>());
               
                    connections[user2].Add(user1);
                    connections[user1].Add(user2);
                }
                else
                {
                    if(connections.ContainsKey(user1))
                        connections[user1].Remove(user2);

                    if (connections.ContainsKey(user2))
                        connections[user2].Remove(user1);
                }
            }

            List<string> lessthan=new List<string>();
            List<string> greaterthan=new List<string>();

            foreach(var connection in connections)
            {
                if (connection.Value.Count < count)
                    lessthan.Add(connection.Key);
                else
                    greaterthan.Add(connection.Key);
            }

            return (lessthan, greaterthan);
        }

        //public static (List<string>,List<string>) Grouping(List<string[]> events, int count)
        /////public static void Grouping(List<string[]> events, int count)
        //{
        //    var connections=new Dictionary<string, HashSet<string>>();

        //    foreach(var e in events)
        //    {
        //        var connectiontype = e[0];
        //        var user1 = e[1];
        //        var user2 = e[2];

        //        if (connectiontype == "CONNECT")
        //        {
        //            if (!connections.ContainsKey(user1))
        //                connections.Add(user1, new HashSet<string>());

        //            if (!connections.ContainsKey(user2))
        //                connections.Add(user2, new HashSet<string>());

        //            connections[user1].Add(user2);
        //            connections[user2].Add(user1);
        //        }
        //        else
        //        {
        //            if (connections.ContainsKey(user1))
        //                connections.Remove(user2);

        //            if (connections.ContainsKey(user2))
        //                connections.Remove(user1);
        //        }
        //    }
        //    List<string> lessthan = new List<string>();
        //    List<string> greaterthan = new List<string>();

        //    foreach(var connection in connections)
        //    {
        //        if (connection.Value.Count >= count)
        //            greaterthan.Add(connection.Key);
        //        else
        //            lessthan.Add(connection.Key);
        //    }

        //    return (lessthan,greaterthan);
        //}


    }
}
