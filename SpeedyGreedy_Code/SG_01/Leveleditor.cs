// MultiMediaTechnology / FHS
// MultiMediaProjekt 1
// Programming, Art & Music - Hofer Thomas
// Co-Music Producer - Veltman Bob

using System;

namespace SG_01
{
    public class Leveleditor
    {
        static readonly Random rnd = new Random();
        /* 0 = nothing
         * 1 = start
         * 2 = end
         * 3 = enemy
         * 4 = quicksand
         * getlength0 = rows
         * getlength1 = columns
         */
        public static void NewLevelOneArray(int[,] level) // changes level array
        {
            int enemyCounter = 0;
            int holeCounter = 0;
            int enemyChance = 12;
            int holeChance = 20;
            int holeChanceHigh = 100;
            int sumTilesField = 1323;
            int sumTilesReducedField = 989;
            bool neighbourCheck = false;

            for (int row = 0; row < level.GetLength(0); row ++)
            {
                for (int column = 0; column < level.GetLength(1); column++)
                {
                    // start
                    if (column < 5)
                    {
                        level[row, column] = 1;
                    }
                    // end
                    else if (column > 53)
                    {
                        level[row, column] = 2;
                    }
                    // enemy & quicksand
                    else
                    {
                        // enemys
                        if (row > 1 && row < 25 && column > 7 && column < 51 && enemyCounter < 10)
                        {
                            neighbourCheck = false;

                            for (int i = row - 2; i < row + 3; i++) // checks neighboured tiles
                            {
                                for (int j = column - 3; j < column + 4; j++)
                                {
                                    if (level[i, j] == 3)
                                    {
                                        neighbourCheck = true;
                                    }
                                }
                            }

                            if (!neighbourCheck)
                            {
                                if (GetChance(sumTilesReducedField, enemyChance))
                                {
                                    level[row, column] = 3;
                                    enemyCounter++;
                                }
                            }
                        }
                        // quicksand
                        if (column > 4 && column < 54 && level[row, column] != 3 && holeCounter < 50)
                        {
                            if (row < 3 || row > 22) // near row border
                            {
                                if (GetChance(sumTilesField, holeChanceHigh))
                                {
                                    level[row, column] = 4;
                                    holeCounter++;
                                }
                            }
                            else // between
                            {
                                neighbourCheck = false;

                                for (int i = row - 1; i < row + 2; i++)
                                {
                                    for (int j = column - 1; j < column + 2; j++)
                                    {
                                        if (level[i, j] == 4)
                                        {
                                            neighbourCheck = true;
                                        }
                                    }
                                }

                                if (neighbourCheck) // higher chance if neighboured quicksands
                                {
                                    if (GetChance(sumTilesField, holeChanceHigh))
                                    {
                                        level[row, column] = 4;
                                        holeCounter++;
                                    }
                                }
                                else // normal chance if not
                                {
                                    if (GetChance(sumTilesField, holeChance))
                                    {
                                        level[row, column] = 4;
                                        holeCounter++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // returns true if the chance hit, false if not
        public static bool GetChance(int max, int chance)
        {
            int aRandom = rnd.Next(0, max);
            if (aRandom < chance)
            {
                return true;
            }
            return false;
        }

    }
}
