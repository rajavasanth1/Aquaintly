using System.Reflection.Metadata;

namespace BadgedAccess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var records1 = new List<string[]>
          {
              new[] {"Paul","enter"},
              new[] {"Pauline","exit"},
              new[] {"Paul","enter"},
              new[] {"Paul","exit"},
              new[] {"Martha","exit"},
              new[] {"Joe","enter"},
              new[] {"Martha","enter"},
              new[] {"Steve","enter"},
              new[] {"Martha","exit" },
              new[] {"Jennifer","enter"},
              new[] {"Joe","enter"},
              new[] {"Curtis","exit" },
              new[] {"Curtis", "enter"},
              new[] {"Joe","exit"},
              new[] { "Martha", "enter"},
              new[] { "Martha", "exit"},
              new[] { "Jennifer", "exit"},
              new[] { "Joe", "enter"},
              new[] { "Joe", "enter"},
              new[] { "Martha", "exit"},
              new[] { "Joe", "exit"},
              new[] { "Joe", "exit"},
          };

            var records2 = new List<string[]>
            {
                new[] {"Paul","enter"},
                new[] {"Paul","exit"}
            };

            var records3 = new List<string[]>
            {
                new[] {"Paul","enter"},
                new[] {"Paul","enter"},
                new[] {"Paul","exit"},
                new[] {"Paul","exit"}
            };

            var records4 = new List<string[]>
            {
                new[] {"Raj","enter"},
                new[] {"Paul","enter"},
                new[] {"Paul","exit"},
                new[] {"Paul","exit"},
                new[] {"Paul","enter"},
                new[] {"Raj","enter"},
            };

            Printresults(records1);
            Printresults(records2);
            Printresults(records3);
            Printresults(records4);
        }

        public static void Printresults(List<string[]> records)
        {
            List<string> notexited = new List<string>();
            List<string> notentered = new List<string>();
            

            Dictionary<string,List<string>> userentry = new Dictionary<string,List<string>>();

            foreach (var record in records)
            {
                var user = record[0];
                var entryorexit= record[1];

                //if (entryorexit == "exit" && !notentered.Contains(user))
                //    notentered.Add(user);

                //if (entryorexit == "enter" && !notentered.Contains(user))
                //    notexited.Add(user);
                if (!userentry.ContainsKey(user))
                    userentry.Add(user, new List<string>());

                userentry[user].Add(entryorexit);
                     
            }

            foreach(var user in userentry)
            {
                if(user.Value.Contains("enter"))
                {
                    var entercount = user.Value.Where(x => x == "enter").Count();
                    var exitcount = user.Value.Where(x => x == "exit").Count();

                    if(entercount > exitcount)
                        notexited.Add(user.Key);
                    else if (exitcount > entercount) 
                        notentered.Add(user.Key);
                }

            }

            Console.WriteLine(string.Join(", ", notexited));
            Console.WriteLine(string.Join(", ", notentered));
        }
    }
}
