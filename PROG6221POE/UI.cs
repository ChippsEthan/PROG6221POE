using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221POE
{
    public static class UI
    {
        // this will play my recording
        public static void Audio()
        {
            try
            {
                using (SoundPlayer sound = new SoundPlayer("Greeting.wav"))
                {
                    sound.Load(); // Loads the file with the recording
                    // this waits till the recording is done
                    sound.PlaySync();
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error with the voice line");
            }
        }
        // this shows the ascii art required
        public static void AsciiART()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
  _____  ____  _____   ___  ______  __ __     __  __ __  ____     ___  ____  
 / ___/ /    T|     | /  _]|      T|  T  T   /  ]|  T  T|    \   /  _]|    \ 
(   \_ Y  o  ||   __j/  [_ |      ||  |  |  /  / |  |  ||  o  ) /  [_ |  D  )
 \__  T|     ||  l_ Y    _]l_j  l_j|  ~  | /  /  |  ~  ||     TY    _]|    / 
 /  \ ||  _  ||   _]|   [_   |  |  l___, |/   \_ l___, ||  O  ||   [_ |    \ 
 \    ||  |  ||  T  |     T  |  |  |     !\     ||     !|     ||     T|  .  Y
  \___jl__j__jl__j  l_____j  l__j  l____/  \____jl____/ l_____jl_____jl__j\_j
                                                                                                                       
        Welcome to the Safty guide for the internet
        ");
            Console.ResetColor();
        
        }
    }
}