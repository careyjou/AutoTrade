﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeUtility
{
    public class StrategyFile
    {

        const string Strategy_File_Name = "Strategy.txt";

        const string Config_Dir = "Config";//設定檔目錄

        static StrategyFile strategyInstance = null;

        string strategyFilePath = "";

        string strategyFileName = "";

        TradeFile strategyFile ;

        Dictionary<int, int> loseLine;  //認賠的底線

        Dictionary<int, int> winLine;  //停利的底線

        int maxStrategyCount = 0; //停損停利規則最大有幾種

        public void close()
        {
            strategyFile.close();
        }

        public int getMaxStrategyCount()
        {
            return maxStrategyCount;
        }


        public static StrategyFile getInstance()
        {

            if (strategyInstance == null)
            {
                strategyInstance = new StrategyFile();
            }

            return strategyInstance;
        }

        public Dictionary<int, int> getLoseLine()
        {
            return loseLine;
        }

        public Dictionary<int, int> getWinLine()
        {
            return winLine;
        }

        public Boolean dealStrategyRule(string appDir)
        {
            return dealStrategyRule(appDir,Strategy_File_Name);
        }


        public Boolean dealStrategyRule(string appDir ,string fileName)//讀取停損停利規則檔
        {
            try
            {

                strategyFileName = fileName;                     

                strategyFilePath = appDir + "\\" + Config_Dir + "\\" + fileName;

                strategyFile = new TradeFile(strategyFilePath);

                strategyFile.prepareReader();

                loseLine = new Dictionary<int, int>();

                winLine = new Dictionary<int, int>();

                int strategyCount = 1;//讀取停損停利規則檔案的行數

                int losePoint;//停損點範圍

                int winPoint;//停利點範圍

                String tmpLine = "";

                String[] tmpData = new String[2];

                while (strategyFile.hasNext())
                {

                    tmpLine = strategyFile.getLine();

                    tmpData = tmpLine.Split(',');

                    losePoint = int.Parse(tmpData[0]);

                    winPoint = int.Parse(tmpData[1]);

                    loseLine.Add(strategyCount, losePoint);

                    winLine.Add(strategyCount, winPoint);

                    strategyCount++;
                }

                maxStrategyCount = --strategyCount;

                for (int i = 1; i <= maxStrategyCount; i++)
                {
                    Console.WriteLine("loseLine[" + i + "] : " + loseLine[i]);
                    Console.WriteLine("winLine[" + i + "] : " + winLine[i]);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }

    }
}
