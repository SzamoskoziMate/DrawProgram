using System;
using System.Reflection.Metadata.Ecma335;

class Program
{
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
        string[] gombok = { " Létrehozás", "Szerkeszt", " Mentés", " Kilépés" };
        int gombSzélesség = 16;
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
                        ClearButtons(gombok.Length, gombSzélesség, gombMagasság, gombKezdésY, centerX);
                        isRunning = false;
                    }
                    else if(választottGomb == 3)
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
        ConsoleColor[] szinek = new ConsoleColor[]
        {
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Yellow,
            ConsoleColor.Cyan,
            ConsoleColor.Magenta,
            ConsoleColor.White
        };
        int jelenlegiSzín = 0;

        string jelenlegiKarakter = "█";
       

        DrawEdges();

        ButtonNavigation();

        while (true)
        {
       
            // A karakter színváltozása
            Console.ForegroundColor = szinek[jelenlegiSzín];

            // Egy "█" rajzolása a porzícióban
            Console.SetCursorPosition(x, y);
            

            // Gomb lenyomás 
            gombInfo = Console.ReadKey(true);

            // Mozgás és karakterváltás
            switch (gombInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (y > 0) y--;
                    break;
                case ConsoleKey.DownArrow:
                    if (y < Console.WindowHeight - 1) y++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (x > 0) x--;
                    break;
                case ConsoleKey.RightArrow:
                    if (x < Console.WindowWidth - 1) x++;
                    break;
                case ConsoleKey.D:
                    jelenlegiKarakter = "█";
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
                case ConsoleKey.Spacebar:
                    Console.Write(jelenlegiKarakter);
                    break;
                
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    DrawEdges();
                    ButtonNavigation();
                    break;
            }
            // Szinek asszociálása a számokhoz
            if (gombInfo.Key >= ConsoleKey.D1 && gombInfo.Key <= ConsoleKey.D7)
            {
                int választottSzám = gombInfo.Key - ConsoleKey.D1 + 1;

                for (int i = 1; i <= választottSzám; i++)
                {
                    jelenlegiSzín = (i - 1) % szinek.Length;
                }
            }
        }
    }
}
