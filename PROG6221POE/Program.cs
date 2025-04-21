namespace PROG6221POE
{
    class Program
    {
        static void Main(string[] args)
        {

            //this code will display the art and play the audio
            UI.Audio();
            UI.AsciiART();

            //gets the username
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nWhats your name? ");
            string name = Console.ReadLine();
            //checks to see input
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Still need your name before we start . ");
                name = Console.ReadLine();
            }
            // after confirming a name the screen will be cleared , this then shows the Asci Art again because it was cleared 
            Console.Clear();
            UI.AsciiART();
            Console.WriteLine($"\nHey, {name} ill be the one to help you stay safe on the internet.");
            Console.WriteLine("type 'menu' and that will bring up some options to ask me.\n");

            // this will start the loop
            Bot.Talk(name);
        }
    }
}
