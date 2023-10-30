using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;

Random rand = new Random();
float rounds = 10_000f;
int attack_team = 1000; // team 1
int defense_team = 500; // team 2

monteCarlo();

int roll()
    => rand.Next(6) + 1;

void monteCarlo()
{
    int attack_rounds = 0;
    int defense_rounds = 0;
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    for(int i = 0; i < rounds; i++)
    {
        if(Round(attack_team, defense_team) == 1)
            attack_rounds++;
        else
            defense_rounds++;
    }

    stopwatch.Stop();
    Console.WriteLine("Attack: {0}", attack_rounds / rounds);
    Console.WriteLine("Defense: {0}", defense_rounds / rounds);
    Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
}

int Round(int team1, int team2)
{
    int results = 2;

    int attack_points = 0;
    int defense_points = 0;

    int attack_qntd = team1;
    int defense_qntd = team2;
    int rolls_qntd = 3;

    List<int> attack_rolls = new List<int>();
    List<int> defense_rolls = new List<int>();

    for(int i = 0; attack_qntd != 1 || defense_qntd != 0; i++)
    {
        if(attack_qntd < 3 || defense_qntd < 3)
            rolls_qntd = 2;
        if(defense_qntd < 2)
            rolls_qntd = 1;

        attack_rolls.Clear();
        defense_rolls.Clear();

        for(int j = 0; j < 3; j++)
        {
            attack_rolls.Add(roll());
            defense_rolls.Add(roll());
        }

        attack_rolls.Sort();
        defense_rolls.Sort();

        int attack_vitories = 0;
        int defense_vitories = 0;
        
        for(int j = 0; j < 3; j++)
        {
            if(attack_rolls[j] < defense_rolls[j] || attack_rolls[0] == defense_rolls[0])
            {
                defense_vitories++;
                attack_qntd--;
            }
            else
            {
                attack_vitories++;
                defense_qntd--;
            }
        }

        if(attack_vitories > defense_vitories)
            results = 1;
    }
    
    return results;
}