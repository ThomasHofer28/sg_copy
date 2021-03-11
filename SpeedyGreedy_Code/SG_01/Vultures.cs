// MultiMediaTechnology / FHS
// MultiMediaProjekt 1
// Programming, Art & Music - Hofer Thomas
// Co-Music Producer - Veltman Bob

namespace SG_01
{
    class Vultures
    {
        public static float[,] NewVultureArray()
        {
            float[,] vultureArray = new float[4, 2];
            int vultureCounter = 0;
            int maxCounter = 4;
            int startCordX = 176;
            int CordX = startCordX;
            int startCordY = 108;
            int CordY = startCordY;
            int sumTilesField = 1323;
            int tileSize = 32;
            int maxTilesY = 26;
            int maxTilesX = 48;

            do
            {
                CordY = startCordY;
                for (int i = 0; i < maxTilesY; i++, CordY += tileSize)
                {
                    for (int j = 0; j < maxTilesX; j++, CordX += tileSize)
                    {
                        if (Leveleditor.GetChance(sumTilesField, maxCounter) && vultureCounter < maxCounter
                            && i > 1 && i < maxTilesY-1 && j > 1 && j < maxTilesX-1)
                        {
                            vultureArray[vultureCounter, 0] = CordX;
                            vultureArray[vultureCounter, 1] = CordY;
                            vultureCounter++;
                        }
                    }
                    CordX = startCordX;
                }
            }
            while (vultureCounter < maxCounter);
            
            return vultureArray;
        }
    }
}
