using Omegle_SpamBot.Omegle;
using System;

namespace Omegle_SpamBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var bot = new Bot();
                bot.StartSpam();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
