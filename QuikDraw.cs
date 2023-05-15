using System.Diagnostics;
Random random = new Random();
const string Menu = @" 
Quick Draw 
Face your opponent and wait for the signal. Once the signal is given, shoot your opponent by pressing [space]
before they shoot you. It's all about your reaction time. 
Choose Your Opponent: 
[1] Easy....1000 milliseconds 
[2] Medium...500 milliseconds 
[3] Hard.....250 milliseconds 
[4] Harder...125 milliseconds";
const string Wait = @" 
 O                  O
|/|_      wait      |\| 
/\                    /\ 
/ |                  | \ 
------------------------------------------------------";
const string Fire = @" 
        ******** 
        * FIRE * 
 O     ********     O 
|/|                _|\| 
/\      spacebar      /\ 
/ |                  | \ 
------------------------------------------------------";
const string LoseTooSlow = @" 
                        > ╗__O 
//         Too Slow        / \ 
O//\     You Lose       /\ 
     \                   | \ 
------------------------------------------------------";
const string LoseTooFast = @" 
            Too Fast    > ╗__O 
//         You Missed       / \ 
O//\      You Lose       /\
\                         | \ 
------------------------------------------------------";
const string Win = @" 
O__╔ < 
/ \                         \\ 
/\          You Win      /\__\O 
/ |                      / 
------------------------------------------------------";
while (true)
{
    Console.Clear();
    Console.WriteLine(Menu);
    TimeSpan requiredReactionTime;
    string playerInput = Console.ReadLine();
    switch (playerInput)
    {
        case "1":
            requiredReactionTime = TimeSpan.FromMilliseconds(1000);
            break;
        case "2":
            requiredReactionTime = TimeSpan.FromMilliseconds(0500);
            break;
        case "3":
            requiredReactionTime = TimeSpan.FromMilliseconds(0250);
            break;
        case "4":
            requiredReactionTime = TimeSpan.FromMilliseconds(0125);
            break;
        default:
            continue;
    }
    random.Next();
    Console.Clear();
    TimeSpan signal = TimeSpan.FromMilliseconds(random.Next(5000, 10000));
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Restart();
    Console.WriteLine(Wait);
    bool isToofast = false;
    while (stopwatch.Elapsed < signal && !isToofast)
    {
        if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
        {
            isToofast = true;
        }
    }
    Console.Clear();
    if (isToofast)
    {
        Console.WriteLine(LoseTooFast);
    }
    else
    {
        Console.WriteLine(Fire);
        stopwatch.Restart();
        bool isTooSlow = true;
        TimeSpan reactionTime = default;
        while (stopwatch.Elapsed < requiredReactionTime && isTooSlow)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Spacebar)
                {
                    isTooSlow = false;
                    reactionTime = stopwatch.Elapsed;
                }
            }
        }
        Console.Clear();
        if (isTooSlow)
        {
            Console.WriteLine(LoseTooSlow);
        }
        else
        {
            Console.WriteLine(Win);
            Console.WriteLine($"Reaction time: " + $"{reactionTime.TotalMilliseconds} milliseconds");
        }
    }
    Console.Write("Press [1] to Play Gain or [2] to quit: ");
    string playOrQuit = Console.ReadLine();
    if (playOrQuit == "2")
    {
        Console.Clear();
        Console.Write("Quick Draw was closed.");
        break;
    }
}