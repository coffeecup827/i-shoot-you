using System;

namespace Tools;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"=================================================");
        Console.WriteLine($"[GAME TOOLS] Initialising");
        Console.WriteLine($"=================================================");

        AudioInitialiser.Run();
        
        Console.WriteLine("=================================================");
        Console.WriteLine("[GAME TOOLS] All Pipeline Operations Completed.");
        Console.WriteLine("=================================================");
    }
}