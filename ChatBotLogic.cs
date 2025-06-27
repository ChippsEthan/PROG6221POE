using System;
using System.Collections.Generic;

namespace CyberChatBot
{
    public class ChatBotLogic
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private List<string> activityLog = new List<string>();
        private int quizAttempts = 0;

        private List<QuizQuestion> quizQuestions = new List<QuizQuestion>
{
    new QuizQuestion
    {
        Question = "what should you do if you receive an email or a message asking for your password?",
        Options = new List<string> { "A) send your password", "B) delete the message or email", "C) report the email or message as phishing", "D) leave it alone" },
        Answer = "C",
        Explanation = "by reporting it you could stop the scams and you could help others."
    },
    new QuizQuestion
    {
        Question = "True or False: it is okay to use the same password for everything.",
        Options = new List<string> { "A) True", "B) False" },
        Answer = "B",
        Explanation = "by using diffrent passwords your accounts will be alot more secure."
    },
    new QuizQuestion
    {
        Question = "What does 2FA stand for?",
        Options = new List<string> { "A) two factor authenticator", "B) two way access", "C) tweanty fast attacks", "D) two hundred accounts" },
        Answer = "A",
        Explanation = "2fa is there to give your account that extra safety ."
    },
    new QuizQuestion
    {
        Question = "what could lead you to belive the website you are on is unsafe?",
        Options = new List<string> { "A) HTTPS", "B) the lock emojie", "C) Strange URL", "D) none of the above" },
        Answer = "C",
        Explanation = "a fake site or phishing site often use really weird URLS."
    },
    new QuizQuestion
    {
        Question = "whats the safest when making a password?",
        Options = new List<string> { "A) your name", "B) following numbers ", "C) use random symbols or phrases ", "D) use personal information" },
        Answer = "C",
        Explanation = "the strongest passwords are those that have no relation to you and are complex."
    },
    new QuizQuestion
    {
        Question = "True or False: using public wifi is safe to accesses your bank account.",
        Options = new List<string> { "A) True", "B) False" },
        Answer = "B",
        Explanation = "public wifi can be dangerous as users could access your data and it may be unsafe."
    },
    new QuizQuestion
    {
        Question = "when or how often should you update your software or antivirus?",
        Options = new List<string> { "A) once each year", "B) never", "C) only if your computor has a virus or is slow", "D) when ever a possible update comes out" },
        Answer = "D",
        Explanation = "an update could be a cruicial patch in keeping you safe."
    },
    new QuizQuestion
    {
        Question = "what is a white hacker?",
        Options = new List<string> { "A) software updates", "B) someone who hacks other hackers", "C) someone who is payed to hack specific systems to find vulnrabilities", "D) none of the above" },
        Answer = "C",
        Explanation = "white hackers or ethical hackers use their skills to help companys stop the bad hackers."
    },
    new QuizQuestion
    {
        Question = "what is something dangerous to do online?",
        Options = new List<string> { "A) update your anti virus", "B) click on a pop up when on a unsucure website", "C) use a VPN", "D) secure your accounts with 2FA" },
        Answer = "B",
        Explanation = "clicking on a pop up could be dangerous."
    },
    new QuizQuestion
    {
        Question = "when you get emailed a link what can you do to check if its safe?",
        Options = new List<string> { "A) hover over the email url", "B) just click it", "C) nothing", "D) delete it" },
        Answer = "A",
        Explanation = "if you hover over an email you can see the url of the adress and from there you can see if its secure."


            }

        };

        private readonly Dictionary<string, string> sentimentResponses = new()
        {
            {"worried", "It's okay to feel that way. I'm here to help."},
            {"frustrated", "I know this can be frustrating. I'm here to help."},
            {"curious", "Being curious is great that way we can learn more."}
        };

        private readonly Dictionary<string, List<string>> topicTips = new()
        {
            { "phishing", new List<string> {
                "Always check the emails you get sent are safe, especially if they ask for personal info.",
            "Never click on suspicious links in emails.",
            "Always verify the sender's address in an email."
            }},
            { "password", new List<string> {
                "Avoid personal info in passwords.",
            "Use symbols and numbers.",
            "Write passwords down physically if needed, not in plain text files."
            }},
            { "browsing", new List<string> {
                "Check for HTTPS in websites.",
            "Avoid downloads from shady sources.",
            "Don't click pop-up ads or unknown links."
            }},
            { "2fa", new List<string> {
                "2FA makes sure your account is secure.",
                "Use authenticator apps instead of SMS when possible.",
                "Enable 2FA on all important accounts."
            }},
            { "updates", new List<string> {
                "make sure your anti virus is updated.",
                "you can set your updates to do it themselves automaticaly.",
                "updates stop scammers and hackers from using old vulrnabilities."
            }},
            { "wifi", new List<string> {
                "when in public you can use a VPN.",
                "dont do anything important on public wifi.",
                "dont join unsecure wifis."
            }}
        };

        public string UserName { get; set; } = "";
        public string FavoriteTopic { get; set; } = "";

        public Dictionary<string, List<string>> TopicTips => topicTips;
        public Dictionary<string, string> SentimentResponses => sentimentResponses;

        public void AddTask(string title, string description, DateTime? reminder)
        {
            tasks.Add(new TaskItem
            {
                Title = title,
                Description = description,
                Reminder = reminder,
                IsCompleted = false
            });

            string reminderText = reminder.HasValue ? $" (Reminder set for {reminder.Value.ToShortDateString()})" : "";
            activityLog.Add($"Task added: '{title}'{reminderText}.");
        }

        public bool MarkTaskCompleted(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                tasks[index].IsCompleted = true;
                activityLog.Add($"task is completed: '{tasks[index].Title}'.");
                return true;
            }
            return false;
        }

        public bool DeleteTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
                var removed = tasks[index].Title;
                tasks.RemoveAt(index);
                activityLog.Add($"Task deleted: '{removed}'.");
                return true;
            }
            return false;
        }

        public List<TaskItem> GetTasks()
        {
            return tasks;
        }

        public void IncrementQuizAttempts()
        {
            quizAttempts++;
            activityLog.Add($"Quiz started - {quizQuestions.Count} questions answered.");
        }

        public QuizQuestion GetQuizQuestion(int index)
        {
            return quizQuestions[index];
        }

        public int QuizCount => quizQuestions.Count;

        public string GetLog()
        {
            if (activityLog.Count == 0)
                return "No activity logged yet.";

            string log = "Here's a summary of recent actions:\n";
            int counter = 1;
            foreach (var entry in activityLog)
            {
                log += $"{counter++}. {entry}\n";
            }
            return log.Trim();
        }

        public List<string> GetResponses(string userInput)
        {
            var responses = new List<string>();

            foreach (var s in sentimentResponses)
            {
                if (userInput.Contains(s.Key, StringComparison.OrdinalIgnoreCase))
                {
                    responses.Add(s.Value);
                }
            }

            foreach (var topic in topicTips.Keys)
            {
                if (userInput.Contains(topic, StringComparison.OrdinalIgnoreCase))
                {
                    var tips = topicTips[topic];
                    if (tips.Count > 0)
                    {
                        var rnd = new Random();
                        var tip = tips[rnd.Next(tips.Count)];
                        responses.Add(tip);
                    }
                }
            }

            return responses;
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Reminder { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "[✓]" : "[ ]";
            string reminder = Reminder.HasValue ? $" (Reminder: {Reminder.Value.ToShortDateString()})" : "";
            return $"{status} {Title} - {Description}{reminder}";
        }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string Answer { get; set; }
        public string Explanation { get; set; }
    }
}