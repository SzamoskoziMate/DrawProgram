using System;
using System.Reflection.Metadata.Ecma335;

class Program
{
    
    static List<ConsoleCharacter> savedContent = new List<ConsoleCharacter>(99);
    static void DrawEdges()
    {
        Console.SetCursorPosition(0, 0);
        Console.Write("╔");
        Console.SetCursorPosition(Console.WindowWidth - 1, 0);
        Console.Write("╗");
        Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 1);
        Console.Write("╝");
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        Console.Write("╚");

        int keretX = Console.WindowWidth - 2;
        int keretY = Console.WindowHeight - 2;
        Console.SetCursorPosition(keretX, keretY);

        for (int i = 0; i < Console.WindowWidth - 2; i++)
        {
            Console.SetCursorPosition(i + 1, 0);
            Console.Write("═");

        }
        for (int i = 0; i < Console.WindowWidth - 2; i++)
        {
            Console.SetCursorPosition(i + 1, Console.WindowHeight - 1);
            Console.Write("═");

        }

        for (int j = 1; j < Console.WindowHeight - 1; j++)
        {
            Console.SetCursorPosition(Console.WindowWidth - 1, j);
            Console.Write("║");

        }

        for (int j = 1; j < Console.WindowHeight - 1; j++)
        {
            Console.SetCursorPosition(0, j);
            Console.Write("║");

        }
    }
    static void ButtonNavigation()
    {
        ConsoleKeyInfo gombInfo;
        int választottGomb = 0;
        string[] gombok = { "Létrehozás", " Szerkesztés", "Kilépés" };
        int gombSzélesség = 15;
        int gombMagasság = 3;

       
        Console.CursorVisible = false;
        int consoleSzélesség = Console.WindowWidth;
        int consoleMagasság = Console.WindowHeight;

        
        int gombKezdésY = (consoleMagasság - (gombMagasság + 1) * gombok.Length) / 2;
        int centerX = (consoleSzélesség - (gombSzélesség + 2)) / 2;

        
        DrawButtons(gombok, választottGomb, gombSzélesség, gombMagasság, gombKezdésY, centerX);

        
        bool isRunning = true;
        while (isRunning)
        {
            gombInfo = Console.ReadKey(true);

            int előzőGomb = választottGomb;
            switch (gombInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    választottGomb--;
                    if (választottGomb < 0)
                    {
                        választottGomb = gombok.Length - 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    választottGomb++;
                    if (választottGomb >= gombok.Length)
                    {
                        választottGomb = 0;
                    }
                    break;
                case ConsoleKey.Enter:
                    if(választottGomb == 0)
                    {
                        savedContent.Clear();
                        ClearButtons(gombok.Length, gombSzélesség, gombMagasság, gombKezdésY, centerX);
                        isRunning = false;
                    }
                    else if (választottGomb == 1)
                    {
                        ClearButtons(gombok.Length, gombSzélesség, gombMagasság, gombKezdésY, centerX);
                        isRunning = false;
                        LoadContent();
                    }
                    else if(választottGomb == 2)
                    {
                        Environment.Exit(0);
                    }
                    break;
               
            }

            if (előzőGomb != választottGomb)
            {
                DrawButtons(gombok, választottGomb, gombSzélesség, gombMagasság, gombKezdésY, centerX);
            }
        }

        Console.SetCursorPosition(0, gombKezdésY + gombok.Length * (gombMagasság + 1));
        Console.CursorVisible = true;
    }


    static void DrawButtons(string[] gombok, int választottGomb, int gombSzélesség, int gombMagasság, int startY, int centerX)
    {
        for (int i = 0; i < gombok.Length; i++)
        {
            DrawButton(gombok[i], i == választottGomb, startY + i * (gombMagasság + 1), gombSzélesség, centerX);
        }
    }

    static void DrawButton(string text, bool isSelected, int topOffset, int gombSzélesség, int centerX)
    {
        Console.SetCursorPosition(centerX, topOffset);
        if (isSelected)
        {
            Console.Write('█' + new string('█', gombSzélesség) + '█');
        }
        else
        {
            Console.Write('┌' + new string('─', gombSzélesség) + '┐');
        }
        Console.SetCursorPosition(centerX, topOffset + 1);
        if (isSelected)
        {
            Console.Write('█' + CenterText(text, gombSzélesség) + '█');
        }
        else
        {
            Console.Write('│' + CenterText(text, gombSzélesség) + '│');
        }
        Console.SetCursorPosition(centerX, topOffset + 2);
        if (isSelected)
        {
            Console.Write('█' + new string('█', gombSzélesség) + '█');
        }
        else
        {
            Console.Write('└' + new string('─', gombSzélesség) + '┘');
        }     
    }

    class ConsoleCharacter
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Karakter { get; set; }
        public ConsoleColor Szín { get; set; }
    }

    static void SaveCharacter(int x, int y, string character, ConsoleColor color)
    {
        
        savedContent.Add(new ConsoleCharacter
        {
            X = x,
            Y = y,
            Karakter = character[0],
            Szín = color
        });
        
    }

    static void LoadContent()
    {
        Console.Clear();
        DrawEdges(); 

        foreach (var item in savedContent)
        {
            Console.SetCursorPosition(item.X, item.Y);
            Console.ForegroundColor = item.Szín;
            Console.Write(item.Karakter);
        }
    }
    static void ClearButtons(int gombokSzáma, int gombSzélesség, int gombMagasság, int startY, int centerX)
    {
        for (int i = 0; i < gombokSzáma; i++)
        {

            for (int j = 0; j < gombMagasság; j++)
            {
                Console.SetCursorPosition(centerX, startY + i * (gombMagasság + 1) + j);
                Console.Write(new string(' ', gombSzélesség + 2));
            }
        }
    }

    static string CenterText(string text, int szélesség)
    {
        int padding = (szélesség - text.Length) / 2;
        return new string(' ', padding) + text + new string(' ', szélesség - padding - text.Length);
    }
    static void Main(string[] args)
    { 
        int x = Console.WindowWidth / 2;
        int y = Console.WindowHeight / 2;
        ConsoleKeyInfo gombInfo;
        string jelenlegiKarakter = "█";
        string szín = "White";

        DrawEdges();

        ButtonNavigation();


        bool IsRunningDrawing = true;


        while (IsRunningDrawing)
        {
            
            Console.SetCursorPosition(Console.WindowLeft + 2, 1);
            Console.WriteLine($"A karaktered: {jelenlegiKarakter}");
            Console.SetCursorPosition(Console.WindowLeft + 2, 3);
            Console.Write($"A szined: {szín}");
            Console.SetCursorPosition(x, y);
            

 
            gombInfo = Console.ReadKey(true);
            
    
            switch (gombInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (y > 0)
                    {
                        y--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (y < Console.WindowHeight - 1)
                    {
                        y++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (x > 0)
                    {
                        x--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (x < Console.WindowWidth - 1)
                    {
                        x++;
                    }
                    break;
                case ConsoleKey.D1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    szín = ConsoleColor.Red.ToString();
                    break;
                case ConsoleKey.D2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    szín = ConsoleColor.Green.ToString();
                    break;
                case ConsoleKey.D3:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    szín = ConsoleColor.Blue.ToString();
                    break;
                case ConsoleKey.D4:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    szín = ConsoleColor.Yellow.ToString();
                    break;
                case ConsoleKey.D5:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    szín = ConsoleColor.Cyan.ToString();
                    break;
                case ConsoleKey.D6:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    szín = ConsoleColor.Magenta.ToString();
                    break;
                case ConsoleKey.D7:
                    Console.ForegroundColor = ConsoleColor.White;
                    szín = ConsoleColor.White.ToString();
                    break;
                case ConsoleKey.D8:
                    jelenlegiKarakter = "▓";
                    break;
                case ConsoleKey.D9:
                    jelenlegiKarakter = "▒";
                    break;
                case ConsoleKey.D0:
                    jelenlegiKarakter = "░";
                    break;
                case ConsoleKey.D:
                    jelenlegiKarakter = "█";
                    break;
                case ConsoleKey.Spacebar:
                    Console.Write(jelenlegiKarakter);
                    SaveCharacter(x, y, jelenlegiKarakter, Console.ForegroundColor);
                    break;
                case ConsoleKey.Escape:
                    IsRunningDrawing = false;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    DrawEdges();
                    ButtonNavigation();
                    break;
                case ConsoleKey.M:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    DrawEdges();
                    ButtonNavigation();
                    break;
            }
        }
    }
}
