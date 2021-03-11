// MultiMediaTechnology / FHS
// MultiMediaProjekt 1
// Programming, Art & Music - Hofer Thomas
// Co-Music Producer - Veltman Bob

using System;

namespace SG_01
{
    public class HelperMethods
    {
        public static void FillArrayRandomSingleOccurrence(int[] myArray)
        {
            Random rnd = new Random();
            int index = 0;
            while (index < myArray.Length)
            {
                int newNumber = rnd.Next(1, myArray.Length+1);
                if (HelperMethods.CheckNumberInArray(myArray, newNumber))
                {
                    myArray[index] = newNumber;
                    index++;
                }
            }
        }

        public static bool CheckNumberInArray(int[] myArray, int numberToCheck)
        {
            for (int i = 0; i < myArray.Length; i++)
            {
                if (myArray[i] == numberToCheck)
                {
                    return false;
                }
            }
            return true;
        }

        public static void StartPositionY(int[] newStartingPositions)
        {
            int yPosFirst = 380;
            int yPosSecond = 476;
            int yPosThird = 572;
            int yPosFourth = 668;

            for (int i = 0; i < newStartingPositions.Length; i++)
            {
                if (newStartingPositions[i] == 1)
                {
                    newStartingPositions[i] = yPosFirst;
                }
                if (newStartingPositions[i] == 2)
                {
                    newStartingPositions[i] = yPosSecond;
                }
                if (newStartingPositions[i] == 3)
                {
                    newStartingPositions[i] = yPosThird;
                }
                if (newStartingPositions[i] == 4)
                {
                    newStartingPositions[i] = yPosFourth;
                }
            }
        }

    }
}
