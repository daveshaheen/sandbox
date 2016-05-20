<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Drawing.dll</Reference>
</Query>

/*
   Copyright 2016 Dave Shaheen

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

void Main()
{
    decimal height = (5 * 12) + 8,  // inches
            age = 36,               // years
            weight = 175;           // lbs

    var person = new Person(Gender.Male, ActivityLevel.Sedentary, height, age, weight);

    person.Dump();
}

enum Gender
{
    Female = 0,
    Male = 1
}

enum ActivityLevel
{
    Sedentary = 20,         // you don't move
    Light = 30,             // you're lightly active
    Moderate = 40,          // exercise most days in a week
    Intense = 50,           // intense exercise daily for prolonged periods
    ProTraining = 60        // athletic training
}

class Person
{
    public ActivityLevel Activity { get; set; }
    public Gender Gender { get; set; }
    public decimal Age { get; set; }                // years
    public decimal Height { get; set; }             // inches
    public decimal Weight { get; set; }             // lbs
    public decimal CarbPercentage { get; set; }     // percent of diet for carbs as a decimal
    public decimal ProteinPercentage { get; set; }  // percent of diet for protein as a decimal
    public decimal FatPercentage { get; set; }      // percent of diet for fat as a decimal

    public Person(Gender bodyType, ActivityLevel activity, decimal height, decimal age, decimal weight, decimal? carbPercentage = 0.45m, decimal? proteinPercentage = 0.25m, decimal? fatPercentage = 0.30m)
    {
        CarbPercentage = (carbPercentage ?? 0);
        ProteinPercentage = (proteinPercentage ?? 0);
        FatPercentage = (fatPercentage ?? 0);

        var totalPercentage = this.CarbPercentage + this.ProteinPercentage + this.FatPercentage;

        if (totalPercentage > 1 || totalPercentage < 1)
        {
            throw new Exception("Total percentage must add up to 1.0");
        }

        Activity = activity;
        Gender = bodyType;
        Age = age;
        Height = height;
        Weight = weight;
    }

    public decimal BMR
    {
        get
        {
            decimal magicNumber,
                    weightMultiplier,
                    heightMultiplier,
                    ageMultiplier;

            if (this.Gender == Gender.Male)
            {
                magicNumber = 66.0m;
                weightMultiplier = 6.3m;
                heightMultiplier = 12.9m;
                ageMultiplier = 6.8m;
            }
            else
            {
                magicNumber = 655.0m;
                weightMultiplier = 4.3m;
                heightMultiplier = 4.7m;
                ageMultiplier = 4.7m;
            }

            return (magicNumber + (weightMultiplier * Weight) + (heightMultiplier * Height) - (ageMultiplier * Age));
        }
    }

    public decimal CaloriesTotal
    {
        get
        {
            return (BMR + (BMR * ((decimal)this.Activity / 100.0m)));
        }
    }

    public decimal CaloriesCarbs
    {
        get
        {
            return (this.CaloriesTotal * this.CarbPercentage);
        }
    }

    public decimal CaloriesProtein
    {
        get
        {
            return (this.CaloriesTotal * this.ProteinPercentage);
        }
    }

    public decimal CaloriesFat
    {
        get
        {
            return (this.CaloriesTotal * this.FatPercentage);
        }
    }

    public decimal GramsCarbs
    {
        get
        {
            return (this.CaloriesCarbs / 4.0m);
        }
    }

    public decimal GramsProtein
    {
        get
        {
            return (this.CaloriesProtein / 4.0m);
        }
    }

    public decimal GramsFat
    {
        get
        {
            return (this.CaloriesFat / 9.0m);
        }
    }
}