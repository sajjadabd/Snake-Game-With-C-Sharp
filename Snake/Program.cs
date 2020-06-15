using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class snake
    {

        static void printBorder(int screenwidth, int screenheight)
        {
            string pattern = "#";
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(pattern);
            }
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write(pattern);
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(pattern);
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write(pattern);
            }
        }


        static void printSnakeSize(int size)
        {
            Console.SetCursorPosition(5, 27);
            Console.Write("size : " + size + "  ");
        }


        static void printPause(Boolean pause)
        {
            if (pause == true)
            {
                Console.SetCursorPosition(25, 27);
                Console.Write("pause");
            }
            else if (pause == false)
            {
                Console.SetCursorPosition(25, 27);
                Console.Write("        ");
            }
        }


        static int returnWidth(int defaultWidth)
        {
            int returnWidth = 0;
            Console.WriteLine("Enter Width : ");
            string width = Console.ReadLine();
            if (Int32.TryParse(width, out returnWidth)) // convert string to int
            {
                if (returnWidth > defaultWidth)
                {
                    return defaultWidth;
                }
                else if (returnWidth < defaultWidth / 2)
                {
                    return defaultWidth;
                }
                else
                {
                    return returnWidth;
                }

            }
            else
            {
                return Console.WindowWidth - 10;
            }
        }

        static int returnHeight(int defaultHeight)
        {
            int returnHeight = 0;
            Console.WriteLine("Enter Height : ");
            string height = Console.ReadLine();
            if (Int32.TryParse(height, out returnHeight))
            {
                if (returnHeight > defaultHeight)
                {
                    return defaultHeight;
                }
                else if (returnHeight < defaultHeight / 2)
                {
                    return defaultHeight;
                }
                else
                {
                    return returnHeight;
                }

            }
            else
            {
                return Console.WindowHeight - 5;
            }
        }

        static Boolean IsAppleInSnake(List<int> snakeXposition, List<int> snakeYposition, int appleX, int appleY)
        {
            for (int i = 0; i < snakeXposition.Count(); i++)
            {
                if (snakeXposition[i] == appleX && snakeYposition[i] == appleY)
                {
                    return true; // Apple in Snake
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            int screenwidth = Console.WindowWidth - 10;
            int screenheight = Console.WindowHeight - 10;


            //Console.WriteLine("Welcome To Sanke Game");
            //screenwidth = returnWidth(screenwidth);
            //screenheight = returnHeight(screenheight);


            //use Random Class to generate Random number
            Random randomNumber = new Random();

            int score = 3;
            int gameover = 0;

            pixel snakeHead = new pixel();

            snakeHead.xpos = screenwidth / 2;
            snakeHead.ypos = screenheight / 2;

            // default movement is RIGHT
            string movement = "RIGHT";

            // speed of snake
            int speed = 100;

            // use list to save x and y position of snake
            List<int> snakeXposition = new List<int>();
            List<int> snakeYposition = new List<int>();

            //x position and y position of apple(berry)
            int appleX = randomNumber.Next(0 + 5, screenwidth - 5);
            int appleY = randomNumber.Next(0 + 5, screenheight - 5);

            // use Datetime to change speed of snake
            DateTime timeBeforeWhile = DateTime.Now;
            DateTime timeInWhile = DateTime.Now;

            string buttonpressed = "no";

            Boolean pause = false;

            //print the border

            //Console.BackgroundColor = ConsoleColor.White;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            printBorder(screenwidth, screenheight);
            while (true)
            {
                //clear the screen
                //Console.Clear();

                //check that snake hit the border or not
                if (snakeHead.xpos == screenwidth - 1 ||
                snakeHead.xpos == 0 ||
                snakeHead.ypos == screenheight - 1 ||
                snakeHead.ypos == 0)
                {
                    gameover = 1;
                }



                //change the color of snake
                Console.ForegroundColor = ConsoleColor.Green;

                //check snake eat the apple
                if (appleX == snakeHead.xpos && appleY == snakeHead.ypos)
                {
                    score++;

                    do
                    {
                        appleX = randomNumber.Next(0 + 5, screenwidth - 5);
                        appleY = randomNumber.Next(0 + 5, screenheight - 5);
                    } while (IsAppleInSnake(snakeXposition, snakeYposition, appleX, appleY));
                }



                //print snake
                for (int i = 0; i < snakeXposition.Count(); i++)
                {
                    Console.SetCursorPosition(snakeXposition[i], snakeYposition[i]);
                    Console.Write("0");
                    if (snakeXposition[i] == snakeHead.xpos && snakeYposition[i] == snakeHead.ypos)
                    {
                        gameover = 1;
                    }
                }


                // print sanke head
                Console.SetCursorPosition(snakeHead.xpos, snakeHead.ypos);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("@");


                // if gameover==1 go outside of the while loop
                if (gameover == 1)
                {
                    break;
                }


                // print the apple
                Console.SetCursorPosition(appleX, appleY);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("@");


                timeBeforeWhile = DateTime.Now;
                buttonpressed = "no";

                while (true && buttonpressed == "no")
                {
                    // change speed of snake
                    //printStatistics(snakeHead.xpos, snakeHead.ypos);
                    timeInWhile = DateTime.Now;

                    if (timeInWhile.Subtract(timeBeforeWhile).TotalMilliseconds > speed)
                    {
                        break;
                    }

                    //check keyboard press
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo inputKey = Console.ReadKey(true);

                        //Console.WriteLine(inputKey.Key.ToString());
                        if (inputKey.Key.Equals(ConsoleKey.Escape) && buttonpressed == "no")
                        {
                            gameover = 1;
                            buttonpressed = "yes";
                        }
                        if (inputKey.Key.Equals(ConsoleKey.Spacebar) && buttonpressed == "no")
                        {
                            pause = !pause;
                            buttonpressed = "yes";
                        }
                        if ((inputKey.Key.Equals(ConsoleKey.UpArrow) || inputKey.Key.Equals(ConsoleKey.W)) && movement != "DOWN" && buttonpressed == "no")
                        {
                            movement = "UP";
                            buttonpressed = "yes";
                        }
                        if ((inputKey.Key.Equals(ConsoleKey.DownArrow) || inputKey.Key.Equals(ConsoleKey.S)) && movement != "UP" && buttonpressed == "no")
                        {
                            movement = "DOWN";
                            buttonpressed = "yes";
                        }
                        if ((inputKey.Key.Equals(ConsoleKey.LeftArrow) || inputKey.Key.Equals(ConsoleKey.A)) && movement != "RIGHT" && buttonpressed == "no")
                        {
                            movement = "LEFT";
                            buttonpressed = "yes";
                        }
                        if ((inputKey.Key.Equals(ConsoleKey.RightArrow) || inputKey.Key.Equals(ConsoleKey.D)) && movement != "LEFT" && buttonpressed == "no")
                        {
                            movement = "RIGHT";
                            buttonpressed = "yes";
                        }
                    }
                }


                if (!pause)
                {
                    //add the new direction to move the snake
                    snakeXposition.Add(snakeHead.xpos);
                    snakeYposition.Add(snakeHead.ypos);

                    switch (movement)
                    {
                        case "UP":
                            snakeHead.ypos--;
                            break;
                        case "DOWN":
                            snakeHead.ypos++;
                            break;
                        case "LEFT":
                            snakeHead.xpos--;
                            break;
                        case "RIGHT":
                            snakeHead.xpos++;
                            break;
                    }


                    if (snakeXposition.Count() > score)
                    {
                        //printStatistics(snakeXposition.First<int>() ,snakeYposition.First<int>());
                        int xlast = snakeXposition.First<int>();
                        int ylast = snakeYposition.First<int>();

                        printSnakeSize(snakeXposition.Count());

                        Console.SetCursorPosition(xlast, ylast);
                        Console.Write(" ");

                        snakeXposition.RemoveAt(0);
                        snakeYposition.RemoveAt(0);
                    }

                    printPause(false);
                }
                else
                {
                    printPause(true);
                }

            }

            Console.SetCursorPosition(25, 27);
            score++;
            Console.Write("Game over, Score: " + score);
            //Console.SetCursorPosition(25, 27);


            Console.ReadLine();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }

        class pixel
        {
            public int xpos;
            public int ypos;
        }

    }
}
