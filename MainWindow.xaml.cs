using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberChatBot
{
    public partial class MainWindow : Window
    {
        private ChatBotLogic bot = new ChatBotLogic();
        private int currentQuizIndex = -1;
        private bool isQuizRunning = false;
        private int currentScore = 0;

        private enum InputMode { Normal, AddingTaskTitle, AddingTaskDescription, AddingTaskReminder, AnsweringQuiz, AskingName, AskingFavorite }
        private InputMode currentMode = InputMode.AskingName;

        private string tempTaskTitle;
        private string tempTaskDesc;

        public MainWindow()
        {
            InitializeComponent();
            AddBotMessage("hey welcome to the chat bot whats your name?");
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Send_Click(this, new RoutedEventArgs());
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput)) return;

            AddUserMessage(userInput);
            UserInput.Clear();

            switch (currentMode)
            {
                case InputMode.AskingName:
                    bot.UserName = userInput;
                    AddBotMessage($"its nice to meet you, {bot.UserName}. what is your favorite topic in cyber security?");
                    currentMode = InputMode.AskingFavorite;
                    return;

                case InputMode.AskingFavorite:
                    if (!string.IsNullOrWhiteSpace(userInput))
                    {
                        bot.FavoriteTopic = userInput;
                        AddBotMessage($"Awesome! I'll keep an eye out for tips on {bot.FavoriteTopic}. If you need help, just type 'help'.");
                        currentMode = InputMode.Normal;
                        return;
                    }
                    else
                    {
                        AddBotMessage("Please tell me your favorite topic by saying 'My favorite topic is ...'");
                        return;
                    }

                case InputMode.AddingTaskTitle:
                    tempTaskTitle = userInput;
                    currentMode = InputMode.AddingTaskDescription;
                    AddBotMessage("Enter a description for the task:");
                    return;

                case InputMode.AddingTaskDescription:
                    tempTaskDesc = userInput;
                    currentMode = InputMode.AddingTaskReminder;
                    AddBotMessage("Enter a reminder (e.g., 2025-12-31 or '7 days') or type 'none':");
                    return;

                case InputMode.AddingTaskReminder:
                    DateTime? reminderDate = null;
                    if (userInput.ToLower().Contains("day"))
                    {
                        var parts = userInput.Split(' ');
                        if (int.TryParse(parts[0], out int days))
                            reminderDate = DateTime.Today.AddDays(days);
                    }
                    else if (DateTime.TryParse(userInput, out DateTime parsedDate))
                    {
                        reminderDate = parsedDate;
                    }
                    bot.AddTask(tempTaskTitle, tempTaskDesc, reminderDate);
                    AddBotMessage("Task saved.");
                    currentMode = InputMode.Normal;
                    return;

                case InputMode.AnsweringQuiz:
                    HandleQuizAnswer(userInput);
                    return;
            }

            HandleNormalInput(userInput);
        }

        private void HandleNormalInput(string input)
        {
            input = input.ToLower();

            if (input == "exit" || input == "stop")
            {
                Close();
                return;
            }

            if (input.Contains("help"))
            {
                AddBotMessage(" You can ask me to:\n- Add a task \n-  Start a quiz \n-  View log \n-  View tasks\n-  Type a topic like 'phishing' or 'passwords' for tips.");
                return;
            }

            if (input == "menu")
            {
                AddBotMessage($"Hey {bot.UserName}, if you want some information on these topics just type them out and I'll give you some tips: {string.Join(", ", bot.TopicTips.Keys)}");
                return;
            }

            if (input.Contains("add task") || input.Contains("remind me") || input.Contains("set reminder") || input.Contains("remember"))
            {
                currentMode = InputMode.AddingTaskTitle;
                AddBotMessage("enter a name for the task:");
                return;
            }

            if (input.Contains("start quiz") || input.Contains("quiz") || input.Contains("begin quiz") || input.Contains("test me"))
            {
                StartQuiz_Click(this, new RoutedEventArgs());
                return;
            }

            if (input.Contains("log") || input.Contains("summary") || input.Contains("update") || input.Contains("progress"))
            {
                Log_Click(this, new RoutedEventArgs());
                return;
            }

            if (input.Contains("view tasks") || input.Contains("show tasks") || input.Contains("my tasks"))
            {
                ViewTasks_Click(this, new RoutedEventArgs());
                return;
            }

            if (Regex.IsMatch(input, @"^\d+\s+(done|delete)$", RegexOptions.IgnoreCase))
            {
                var parts = input.Split(' ');
                int taskNum = int.Parse(parts[0]) - 1;
                string action = parts[1].ToLower();

                if (action == "done")
                {
                    if (bot.MarkTaskCompleted(taskNum))
                        AddBotMessage($"Task {taskNum + 1} marked as complete.");
                    else
                        AddBotMessage("Invalid task number.");
                }
                else if (action == "delete")
                {
                    if (bot.DeleteTask(taskNum))
                        AddBotMessage($"Task {taskNum + 1} deleted.");
                    else
                        AddBotMessage("Invalid task number.");
                }
                return;
            }

            var responses = bot.GetResponses(input);
            if (responses.Count > 0)
            {
                foreach (var r in responses)
                {
                    AddBotMessage(r);
                }
            }
            else
            {
                AddBotMessage("i dont understand, try typing 'menu', 'help', or use the buttons below.");
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            currentMode = InputMode.AddingTaskTitle;
            AddBotMessage("enter a name for the task:");
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            isQuizRunning = true;
            currentScore = 0;
            currentQuizIndex = 0;
            currentMode = InputMode.AnsweringQuiz;
            AskNextQuizQuestion();
        }

        private void AskNextQuizQuestion()
        {
            var question = bot.GetQuizQuestion(currentQuizIndex);
            AddBotMessage($"\nQuestion {currentQuizIndex + 1}: {question.Question}");
            foreach (var opt in question.Options)
            {
                AddBotMessage(opt);
            }
        }

        private void HandleQuizAnswer(string input)
        {
            var question = bot.GetQuizQuestion(currentQuizIndex);
            if (input.Trim().Equals(question.Answer, StringComparison.OrdinalIgnoreCase))
            {
                AddBotMessage("Correct! " + question.Explanation);
                currentScore++;
            }
            else
            {
                AddBotMessage($"Incorrect. The correct answer was {question.Answer}. {question.Explanation}");
            }

            currentQuizIndex++;
            if (currentQuizIndex < bot.QuizCount)
            {
                AskNextQuizQuestion();
            }
            else
            {
                AddBotMessage($"Quiz complete! Final Score: {currentScore}/{bot.QuizCount}");
                AddBotMessage(currentScore > 7 ? "well done your on the right track" : "keep trying so you stay safe online.");
                currentMode = InputMode.Normal;
                isQuizRunning = false;
                bot.IncrementQuizAttempts();
            }
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            AddBotMessage(bot.GetLog());
        }

        private void ViewTasks_Click(object sender, RoutedEventArgs e)
        {
            var tasks = bot.GetTasks();

            if (tasks.Count == 0)
            {
                AddBotMessage("there are no tasks .");
                return;
            }

            AddBotMessage("Your tasks:");
            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                AddBotMessage($"{i + 1}: {task}");
            }

            AddBotMessage("To mark a task as done, type the number followed by done and \nto delete a task, type the number followed by delete.");
        }

        private void AddUserMessage(string message)
        {
            TextBlock text = new TextBlock
            {
                Text = "> " + message,
                Foreground = Brushes.LightGreen,
                FontSize = 14,
                Margin = new Thickness(0, 2, 0, 2)
            };
            ChatPanel.Children.Add(text);
        }

        private void AddBotMessage(string message)
        {
            TextBlock text = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                FontSize = 14,
                Margin = new Thickness(0, 2, 0, 2),
                TextWrapping = TextWrapping.Wrap
            };
            ChatPanel.Children.Add(text);
        }
    }
}