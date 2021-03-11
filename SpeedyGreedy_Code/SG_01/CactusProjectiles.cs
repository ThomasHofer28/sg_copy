// MultiMediaTechnology / FHS
// MultiMediaProjekt 1
// Programming, Art & Music - Hofer Thomas
// Co-Music Producer - Veltman Bob

namespace SG_01
{
    public class CactusProjectiles
    {
        public static float[,] NewProjectileArray(int[,] mapArray)
        {

            float[,] projectileArray = new float[40,2];
            int projectileCounter = 0;
            int posX = 16;
            int posY = 108;
            int rows = mapArray.GetLength(0);
            int columns = mapArray.GetLength(1);
            int tileSize = 32;

            for (int i = 0; i < rows; i++, posY += tileSize)
            {
                for (int j = 0; j < columns; j++, posX += tileSize)
                {
                    if (mapArray[i,j] == 3)
                    {
                        // left projectile start
                        projectileArray[projectileCounter, 0] = posX- tileSize;
                        projectileArray[projectileCounter, 1] = posY;
                        projectileCounter++;
                        // up projectile start
                        projectileArray[projectileCounter, 0] = posX;
                        projectileArray[projectileCounter, 1] = posY- tileSize;
                        projectileCounter++;
                        // right projectile start
                        projectileArray[projectileCounter, 0] = posX+ tileSize;
                        projectileArray[projectileCounter, 1] = posY;
                        projectileCounter++;
                        // down projectile start
                        projectileArray[projectileCounter, 0] = posX;
                        projectileArray[projectileCounter, 1] = posY+ tileSize;
                        projectileCounter++;
                    }  
                }
                posX = 16;
            }
            // new 2 dim array, but just as long as needed (no more 0 values inside)
            float[,] returnArray = new float[projectileCounter, 2];

            for (int i = 0; i < projectileCounter; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    returnArray[i, j] = projectileArray[i, j];
                }
            }

            return returnArray;
        }
    }
}
