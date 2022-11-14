using System;
using System.Collections.Generic;
using FactsAboutMeDatabase.Data;
using FactsAboutMeDatabase.Models;
using Xamarin.Forms;

namespace FactsAboutMeDatabase
{
    public partial class App : Application
    {
        static FactsDatabase database;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static FactsDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FactsDatabase();
                    prefillDatabase();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        static void prefillDatabase()
        {
            database.ClearAllAsync();
            List<FactsAboutMe> list = new List<FactsAboutMe>();
            list.Add(new FactsAboutMe() { TheFact = "Sunflowers are my favorite flower", ShortFact = "Sunflowers" });
            list.Add(new FactsAboutMe() { TheFact = "I have 8 siblings", ShortFact = "Siblings" });
            list.Add(new FactsAboutMe() { TheFact = "I was born and raised in California", ShortFact = "California" });
            list.Add(new FactsAboutMe() { TheFact = "Autumn is my favorite season", ShortFact = "Autumn" });
            list.Add(new FactsAboutMe() { TheFact = "I like to play video games", ShortFact = "Spare Time" });
            list.Add(new FactsAboutMe() { TheFact = "I enjoy crocheting because it keeps my hands busy", ShortFact = "Hobby" });
            list.Add(new FactsAboutMe() { TheFact = "Elephants are my favorite animals", ShortFact = "Favorite Animal" });
            database.InsertList(list);
        }
    }
}
