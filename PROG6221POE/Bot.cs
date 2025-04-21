using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POE
{
    public static class Bot
    {
        // the loop to type
        public static void Talk(string Name)
        {
            while (true)
            {
                // this code will get the user input and then by trimming it and making it lower case it will match with the options below
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nYou: ");
                string reply = Console.ReadLine()?.Trim().ToLower();

                //this code here check that the input matches a keyword or if its empty and if it is give a response back
                if (string.IsNullOrWhiteSpace(reply))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I didn't quite understand that. Could you rephrase?");
                    continue;
                }
                // all of these else if is just to check for keywords and give a response
                Console.ForegroundColor = ConsoleColor.Green;

                if (reply.Contains("menu"))
                {
                    Menu();
                }
                else if (reply.Contains("phishing"))
                {
                    Console.WriteLine("\nPhishing is dangerous as it will look like an actuall email or link\n but can install viruses on your computor or other devices where\n they can then steal information from you and other things so make sure\n you dont open or click any suspisious links.");
                }
                else if (reply.Contains("password"))
                {
                    Console.WriteLine("\nWhen creating a password make sure its long and nothing smilar to you\n such as a name or year you where born in , make sure your password\n is unique for diffrent things to keep it safer , aswell as adding symbols\n to your password will always be a good extra mesure");
                }
                else if (reply.Contains("safe browsing") || reply.Contains("browsing"))
                {
                    Console.WriteLine("\nTo safely browse sites make sure the sites you use are secure so that\n would be the HTTPS at the top of the browser screen and the S means\n its secure , also dont download anything from unoffical sites and never click\n links on sites that could be a download in desquise, another safe site would be .org.");
                }
                else
                {// this is for input that i didnt have maybe a spelling error or such and will display an error
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I had an issue understanding you please look if you made\n any spelling errors and that you used a key word.");
                }
            }
        }

        // this just displays the menu
        private static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nif you would like some infomation with any of the things below just ask\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Do you need help with phishing , safe browsing or just password security");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nI relay on key words so make sure your spelling is right please \n the keywords would be phishing , password and safe browsing.");
        }
    }
}