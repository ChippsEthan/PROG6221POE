using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace PROG6221POE
{
    public static class Bot
    {
        // Stores name
        private static string userName = "";

        // Stores  topic
        private static string favoriteTopic = "";

        // Stores the last topic 
        private static string lastTopic = "";

        // Random  generator tips
        private static Random rnd = new Random();

        // List of tips for topics
        private static readonly List<string> phishingTips = new()
        {
            "Always check the emails you get sent are safe espicialy if they ask for personal info as scammers hide themselves using real looking mails.",
            "Never click on a link you recived in mail if the link is from an unknown person and still then be safe as these emails with links are most likely dangerous.",
            "Always check who sent you the email as sometimes the sender could be pretending to be someone else and their email will liik very simular."
        };
        
        private static readonly List<string> passwordTips = new()
        {
            "try not to use any personal details when making a password and make sure your passwords are unique for each site.",
            "if you want to increase the strength of your password i would use lots of diffrent numbers and symbols.",
            "when it comes to passwords i would also write the important ones down on paper and do not save it to sites."
        };

        private static readonly List<string> safeBrowsingTips = new()
        {
            "make sure on each site you enter information the bar at the top has a HTTP'S' the S means it secure.",
            "dont download anything from a site you dont trust or doesnt seem to be secure.",
            "links and ads may pop up when you are browsing make sure to never click them as it could be a virus."
        };

        private static readonly List<string> twoFactorAuthTips = new()
        {
            "two factor authentication is where a code will be sent to another device to detrmine it is you.",
            "you should use 2fa on important apps such as banking apps and social media apps.",
            "dont always use just an sms service as if your sim is stolen there is an issue rather use a app for 2fa."
        };

        private static readonly List<string> updateTips = new()
        {
            "when ever there is an update regarding your os sytem or other security mesures such as an anti virus mnake sure you do it to stay safe.",
            "check often if the updates are availabe as sometimes they may go unoticed and could be inportant.",
            "if you struggle or not, to make your life easier turn on automatic updates."
        };

        private static readonly List<string> wifiTips = new()
        {
            "when ever in public use your own data or hotspot when logging into personal things as public wifi is often unsecure.",
            "if you need to use a public wifi or even a home wifi a VPN will keep you safe and secure.",
            "dont ever join a open hotspot as people often create an open malicious hotspot for those in public areas to join."
        };

        // Keywords for emtion responses
        private static readonly Dictionary<string, string> sentimentKeywords = new()
        {
            {"worried", "its okay to feel that way im here to help."},
            {"frustrated", "i know this can be frustrating at times , im here to help."},
            {"curious", "being curious is good as this will help you stay safe online."}
        };

        // method to speak with the bot
        public static void Talk(string Name)
        {
            userName = Name;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nHi {userName}! whats your favorite topic in cyber security?");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nYou: ");
            favoriteTopic = Console.ReadLine()?.Trim();

            // Save favourite topic
            if (!string.IsNullOrWhiteSpace(favoriteTopic))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nGreat! I'll remember that you're interested in {favoriteTopic}.");
            }
            else
            {
                favoriteTopic = null;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nDont stress if you do rember your favorite topic just say 'My favorite topic is ...'.");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nAsk me anything about cybersecurity or type 'menu' to see available topics. Type 'exit' to leave.\n");

            // Main chat loop
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nYou: ");
                string reply = Console.ReadLine()?.Trim().ToLower();

                // Exit condition
                if (reply != null && (reply.Contains("exit") || reply.Contains("stop")))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nThank you for chatting! Stay safe online, {userName}!");
                    break;
                }

                // input errors from user blank
                if (string.IsNullOrWhiteSpace(reply))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I didn't quite understand that. Could you rephrase?");
                    continue;
                }

                // reads each input looking for sentimental keywords
                foreach (var sentiment in sentimentKeywords)
                {
                    if (reply.Contains(sentiment.Key))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(sentiment.Value);
                        break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;

                // user doesn't understand last topic and tip
                if (reply.Contains("i don't understand") || (reply.Contains("i dont understand"))  || (reply.Contains("could you explain more")))
                {
                    if (!string.IsNullOrWhiteSpace(lastTopic))
                    {
                        RespondRandom(GetTipsListByTopic(lastTopic));
                        Console.WriteLine("\nDoes this help you understand better? yes/no");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("You: ");
                        string satisfaction = Console.ReadLine()?.Trim().ToLower();

                        Console.ForegroundColor = ConsoleColor.Green;

                        if (satisfaction == "yes")
                        {
                            lastTopic = "";
                        }
                        else if (satisfaction == "no")
                        {
                            RespondLast(GetTipsListByTopic(lastTopic)); // shows last tip
                            lastTopic = "";
                        }
                        else
                        {
                            Console.WriteLine("I didn't understand that. Try asking about another topic.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Let's start with a topic first. Type 'menu' to choose.");
                    }
                    continue;
                }

                // Show menu
                if (reply.Contains("menu"))
                {
                    Menu();
                }

                // Topic matching
                else if (reply.Contains("phishing"))
                {
                    RespondRandom(phishingTips);
                    lastTopic = "phishing";
                }
                else if (reply.Contains("password"))
                {
                    RespondRandom(passwordTips);
                    lastTopic = "password";
                }
                else if (reply.Contains("safe browsing") || reply.Contains("browsing"))
                {
                    RespondRandom(safeBrowsingTips);
                    lastTopic = "browsing";
                }
                else if (reply.Contains("2fa") || reply.Contains("two factor") || reply.Contains("authentication"))
                {
                    RespondRandom(twoFactorAuthTips);
                    lastTopic = "2fa";
                }
                else if (reply.Contains("update") || reply.Contains("updates") || reply.Contains("patch"))
                {
                    RespondRandom(updateTips);
                    lastTopic = "updates";
                }
                else if (reply.Contains("wifi") || reply.Contains("public network") || reply.Contains("hotspot"))
                {
                    RespondRandom(wifiTips);
                    lastTopic = "wifi";
                }

                // change the fav topic
                else if (reply.StartsWith("my favorite topic is"))
                {
                    string newTopic = reply.Replace("my favorite topic is", "").Trim();
                    if (!string.IsNullOrWhiteSpace(newTopic))
                    {
                        favoriteTopic = newTopic;
                        Console.WriteLine($"\nokay cool you are interested in {favoriteTopic}. I'll remember that.");
                    }
                }

                // Retrieve favourite topic
                else if (reply.Contains("i am interested in") || (reply.Contains("what am i interested in")))
                {
                    if (!string.IsNullOrWhiteSpace(favoriteTopic))
                    {
                        Console.WriteLine($"\nAs someone interested in {favoriteTopic}, you could learn more about it if its in my menu \n ps you can just type 'menu'.");
                    }
                    else
                    {
                        Console.WriteLine("\nYou haven't told me your favourite topic yet. Just say: 'My favorite topic is ...'");
                    }
                }

                // error getting topic or misspelled
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I'm not sure I understand. Try asking about a topic or type 'menu'.");
                }
            }
        }

        // responds with tip from topic
        private static void RespondRandom(List<string> responses)
        {
            int index = rnd.Next(responses.Count);
            Console.WriteLine("\n" + responses[index]);
        }

        // this will display the last tip
        private static void RespondLast(List<string> responses)
        {
            if (responses.Count > 0)
            {
                Console.WriteLine("\n" + responses[^1]);
            }
        }

        // menu for the user
        private static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTopics you can ask about:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" - phishing\n - password\n - safe browsing\n - two-factor authentication (2FA)\n - software updates\n - public Wi-Fi");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nJust type a keyword to begin (e.g., 'Tell me about phishing').");
        }

        // gets list for topic chosen
        private static List<string> GetTipsListByTopic(string topic)
        {
            return topic switch
            {
                "phishing" => phishingTips,
                "password" => passwordTips,
                "browsing" => safeBrowsingTips,
                "2fa" => twoFactorAuthTips,
                "updates" => updateTips,
                "wifi" => wifiTips,
                _ => new List<string> { "Sorry, I don't have more tips on that topic." }
            };
        }
    }
}