﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10CharpT
{
    public delegate void FireEventHandler(object sender, FireEventArgs e);
    public delegate void GasEventHandler(object sender, GasEventArgs e);
    public delegate void RobberyEventHandler(object sender, RobberyEventArgs e);
    public delegate void MurderEventHandler(object sender, MurderEventArgs e);


    public class NewTown
    {
        // Нашi данi про життя мiста: назва, будинок та день
        string townname;
        int buildings;
        int days;

        // Служби екстренного виклику
        Police policeman;
        Ambulance ambulanceman;
        FireDetect fireman;

        // Об'єкт класу Random
        private Random rnd = new Random();

        // Це оголошення подiї з назвою "Fire"
        public event FireEventHandler Fire;
        public event GasEventHandler Gas;
        public event RobberyEventHandler Robbery;
        public event MurderEventHandler Murder;

        // Це оголошення масиву рядкiв для зберiгання вiдповiдi служб
        string[] resultservice;

        // Це оголошення масиву рядкiв для зберiгання вiдповiдi служб
        string[] resultservice1;

        // Це оголошення масиву рядкiв для зберiгання вiдповiдi служб
        string[] resultservice2;

        // Це оголошення масиву рядкiв для зберiгання вiдповiдi служб
        string[] resultservice3;

        // Змiнна для зберiгання ймовiрностi виникнення пожежi
        double fireprobability;

        // Змiнна для зберiгання ймовiрностi виникнення витiку газу
        double gasLeakageProbability;

        double robberyProbability;
        double murderProbability;

        // Конструктор з 3 параметрами для створення мiста 
        public NewTown(string townname, int buildings, int days)
        {
            this.townname = townname;
            this.buildings = buildings;
            this.days = days;

            // Присвоюємо ймовiрность виникнення пожежi
            fireprobability = 1e-3;

            // Присвоюємо ймовiрность виникнення витоку газу
            gasLeakageProbability = 1e-3;

            robberyProbability = 1e-3;
            murderProbability = 1e-3;

            //Створення служб
            policeman = new Police(this);
            ambulanceman = new Ambulance(this);
            fireman = new FireDetect(this);

            //Пiдключення до спостереження за подiями
            policeman.On();
            ambulanceman.On();
            fireman.On();
        }

        protected virtual void OnFire(FireEventArgs e)
        {
            Console.WriteLine(string.Format("\nУ мiстi {0} пожежа! Будинок {1}. День {2}-й", townname, e.Building, e.Day));
            if (Fire != null)
            {
                // Метод GetInvocationList() повертає масив делегатiв, що представляють всi методи, якi будуть викликанi при спрацюваннi подiї Fire
                Delegate[] eventhandlers = Fire.GetInvocationList();

                // Записує у наш масив розмiр
                resultservice = new string[eventhandlers.Length];
                int k = 0;
                foreach (FireEventHandler evhandler in eventhandlers)
                {
                    evhandler(this, e);
                    resultservice[k++] = e.Result;
                }
            }
        }

        protected virtual void OnGasLeakage(GasEventArgs e)
        {
            Console.WriteLine(string.Format("\nУ мiстi {0} витiк газу! Будинок {1}. День {2}-й", townname, e.Building, e.Day));
            if (Gas != null)
            {
                // Метод GetInvocationList() повертає масив делегатiв, що представляють всi методи, якi будуть викликанi при спрацюваннi подiї Fire
                Delegate[] eventhandlers = Gas.GetInvocationList();

                // Записує у наш масив розмiр
                resultservice1 = new string[eventhandlers.Length];
                int k = 0;
                foreach (GasEventHandler evhandler in eventhandlers)
                {
                    evhandler(this, e);
                    resultservice1[k++] = e.Result;
                }
            }
        }

        protected virtual void OnRobbery(RobberyEventArgs e)
        {
            Console.WriteLine(string.Format("\nУ мiстi {0} вiдбулося пограбування! Будинок {1}. День {2}-й", townname, e.Building, e.Day));
            if (Robbery != null)
            {
                Delegate[] eventhandlers = Robbery.GetInvocationList();
                resultservice2 = new string[eventhandlers.Length];
                int k = 0;
                foreach (RobberyEventHandler evhandler in eventhandlers)
                {
                    evhandler(this, e);
                    resultservice2[k++] = e.Result;
                }
            }
        }

        protected virtual void OnMurder(MurderEventArgs e)
        {
            Console.WriteLine(string.Format("\nУ мiстi {0} вiдбулося вбивство! Будинок {1}. День {2}-й", townname, e.Building, e.Day));
            if (Murder != null)
            {
                Delegate[] eventhandlers = Murder.GetInvocationList();
                resultservice3 = new string[eventhandlers.Length];
                int k = 0;
                foreach (MurderEventHandler evhandler in eventhandlers)
                {
                    evhandler(this, e);
                    resultservice3[k++] = e.Result;
                }
            }
        }

        public void LifeOurTown()
        {
            const string OK = "У мiстi {0} усi спокiйно! Пожеж не було.";
            bool wasfire = false;
            bool wasGasLeakage = false;
            bool wasRobbery = false;
            bool wasMurder = false;

            for (int day = 1; day <= days; day++)
            {
                for (int building = 1; building <= buildings; building++)
                {
                    if (rnd.NextDouble() < fireprobability)
                    {
                        FireEventArgs e = new FireEventArgs(building, day);
                        OnFire(e);
                        wasfire = true;
                        for (int i = 0; i < resultservice.Length; i++)
                            Console.WriteLine(resultservice[i]);
                    }
                    else if (rnd.NextDouble() < gasLeakageProbability)
                    {
                        GasEventArgs e = new GasEventArgs(building, day);
                        OnGasLeakage(e);
                        wasGasLeakage = true;
                        for (int i = 0; i < resultservice1.Length; i++)
                            Console.WriteLine(resultservice1[i]);
                    }
                    else if (rnd.NextDouble() < robberyProbability)
                    {
                        RobberyEventArgs e = new RobberyEventArgs(building, day);
                        OnRobbery(e);
                        wasRobbery = true;
                        for (int i = 0; i < resultservice2.Length - 2; i++)
                            Console.WriteLine(resultservice2[i]);
                    }
                    else if (rnd.NextDouble() < murderProbability)
                    {
                        MurderEventArgs e = new MurderEventArgs(building, day);
                        OnMurder(e);
                        wasMurder = true;
                        for (int i = 0; i < resultservice3.Length - 2; i++)
                            Console.WriteLine(resultservice3[i]);
                    }
                }
            }
            if (!wasfire || !wasGasLeakage || !wasRobbery || !wasMurder)
            {
                Console.WriteLine(string.Format(OK, townname));
            }
        }
    }
    public abstract class Receiver
    {
        protected NewTown town;
        protected Random rnd = new Random();
        public Receiver(NewTown town) { this.town = town; }
        public void On()
        {
            town.Fire += new FireEventHandler(It_is_Fire);
            town.Gas += new GasEventHandler(It_is_Gas);
            town.Robbery += new RobberyEventHandler(It_is_Robbery);
            town.Murder += new MurderEventHandler(It_is_Murder);
        }
        public void Off()
        {
            town.Fire -= new FireEventHandler(It_is_Fire);
            town.Gas -= new GasEventHandler(It_is_Gas);
            town.Robbery -= new RobberyEventHandler(It_is_Robbery);
            town.Murder -= new MurderEventHandler(It_is_Murder);

        }
        public abstract void It_is_Fire(object sender, FireEventArgs e);
        public abstract void It_is_Gas(object sender, GasEventArgs e);
        public abstract void It_is_Robbery(object sender, RobberyEventArgs e);
        public abstract void It_is_Murder(object sender, MurderEventArgs e);
    }

    public class Police : Receiver
    {
        public Police(NewTown town) : base(town) { }
        public override void It_is_Fire(object sender, FireEventArgs e)
        {
            const string OK = "Мiлiцiя знайшла винних!";
            const string NOK = "Мiлiцiя не знайшла винних! Наслiдок триває.";
            if (rnd.Next(0, 10) > 6)
                e.Result = OK;
            else e.Result = NOK;
        }
        public override void It_is_Gas(object sender, GasEventArgs e)
        {
            const string OK = "Мiлiцiя знайшла винних!";
            const string NOK = "Мiлiцiя не знайшла винних! Наслiдок триває.";
            if (rnd.Next(0, 10) > 6)
                e.Result = OK;
            else e.Result = NOK;
        }
        public override void It_is_Robbery(object sender, RobberyEventArgs e)
        {
            const string OK = "Мiлiцiя затримала злочинцiв!";
            const string NOK = "Мiлiцiя не знайшла злочинцiв! Розшук триває.";
            if (rnd.Next(0, 10) > 3)
                e.Result = OK;
            else e.Result = NOK;
        }
        public override void It_is_Murder(object sender, MurderEventArgs e)
        {
            const string OK = "Мiлiцiя розкрила вбивство!";
            const string NOK = "Мiлiцiя не знайшла вбивцю! Розшук триває.";
            if (rnd.Next(0, 10) > 3)
                e.Result = OK;
            else e.Result = NOK;
        }
    }

    public class FireDetect : Receiver
    {
        public FireDetect(NewTown town) : base(town) { }
        public override void It_is_Fire(object sender, FireEventArgs e)
        {
            const string OK = "Пожежнi згасили пожежу!";
            const string NOK = "Пожежа триває! Потрiбна допомога.";
            if (rnd.Next(0, 10) > 4)
                e.Result = OK;
            else e.Result = NOK;
        }
        public override void It_is_Gas(object sender, GasEventArgs e)
        {
            const string OK = "Рятувальники знайшли де витiкає газ!";
            const string NOK = "Рятувальники не знайшли де витiкає газ! Потрiбна допомога.";
            if (rnd.Next(0, 10) > 4)
                e.Result = OK;
            else e.Result = NOK;
        }
        public override void It_is_Robbery(object sender, RobberyEventArgs e) 
        { 

        }
        public override void It_is_Murder(object sender, MurderEventArgs e) 
        { 

        }
    }

    public class Ambulance : Receiver
    {
        public Ambulance(NewTown town) : base(town) { }
        public override void It_is_Fire(object sender, FireEventArgs e)
        {
            const string OK = "Швидка надала допомогу!";
            const string NOK = "Є постраждалi! Потрiбнi лiки.";
            if (rnd.Next(0, 10) > 2)
                e.Result = OK;
            else e.Result = NOK;
        }
        public override void It_is_Gas(object sender, GasEventArgs e)
        {
            const string OK = "Швидка надала допомогу!";
            const string NOK = "Є постраждалi! Потрiбнi лiки.";
            if (rnd.Next(0, 10) > 2)
                e.Result = OK;
            else e.Result = NOK;
        }
        public override void It_is_Robbery(object sender, RobberyEventArgs e)
        {
            const string OK = "Швидка надала допомогу!";
            const string NOK = "Є постраждалi! Потрiбнi лiки.";
            if (rnd.Next(0, 10) > 5)
                e.Result = OK;
            else e.Result = NOK;
        }
        public override void It_is_Murder(object sender, MurderEventArgs e) 
        { 

        }
    }

    public class FireEventArgs : EventArgs
    {
        int building;
        int day;
        string result;
        public int Building { get { return building; } }
        public int Day { get { return day; } }
        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        public FireEventArgs(int building, int day)
        {
            this.building = building; this.day = day;
        }
    }

    public class GasEventArgs : EventArgs
    {
        int building;
        int day;
        string result;
        public int Building { get { return building; } }
        public int Day { get { return day; } }
        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        public GasEventArgs(int building, int day)
        {
            this.building = building; this.day = day;
        }
    }

    public class RobberyEventArgs : EventArgs
    {
        int building;
        int day;
        string result;
        public int Building { get { return building; } }
        public int Day { get { return day; } }
        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        public RobberyEventArgs(int building, int day)
        {
            this.building = building; this.day = day;
        }
    }

    public class MurderEventArgs : EventArgs
    {
        int building;
        int day;
        string result;
        public int Building { get { return building; } }
        public int Day { get { return day; } }
        public string Result
        {
            get { return result; }
            set { result = value; }
        }
        public MurderEventArgs(int building, int day)
        {
            this.building = building; this.day = day;
        }
    }
}
