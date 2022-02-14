using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arena
{
    class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();

            string userInput;
            bool isExit = false;

            while (isExit == false)
            {
                ShowMenu();

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        arena.PlayerChoice();
                        break;

                    case "2":
                        arena.StartFight();
                        break;

                    case "3":
                        isExit = false;
                        break;
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\nДля выбора героев нажмите 1\n" +
                              "\nДля начала сражения нажмите 2\n" +
                              "");
        }
    }

    abstract class Hero
    {
        protected Random random = new Random();

        public string Name { get; protected set; }

        public int Health { get; protected set; }

        public int Armor { get; protected set; }

        public int Damage { get; protected set; }

        public Hero(string name, int health, int armor, int damage)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public virtual void TakeDamage(int damage)
        {
            double amountDamage = damage - damage * Armor / 100;

            if (Armor >= 1)
            {
                Health -= (int)amountDamage;

                Console.WriteLine($"{Name}-Получаю урона {amountDamage}");
            }
            else
            {
                Health -= damage;
            }
        }

        public abstract int Attack();
    }

    class Mage : Hero
    {
        public Mage(string name, int health, int armor, int damage) : base(name, health, armor, damage)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        private void Pray()
        {
            Health += 30;
        }

        public override int Attack()
        {
            int maxValue = 100;
            int dropPercentage = 15;

            if (random.Next(0, maxValue) < dropPercentage)
            {
                Pray();
            }

            Console.WriteLine($"Нанесенный урон {Damage}");

            return Damage;
        }
    }

    class Warrior : Hero
    {
        public Warrior(string name, int health, int armor, int damage) : base(name, health, armor, damage)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public void Confession()
        {
            Health += 10;

            if (Armor >= 1)
            {
                Armor -= 4;
            }

            Damage += 3;
        }

        public override int Attack()
        {
            int maxValue = 100;
            int dropPercentage = 20;

            if (random.Next(0, maxValue) < dropPercentage)
            {
                Confession();
            }

            Console.WriteLine($"{Name}-Нанесенный урон {Damage}");

            return Damage;
        }
    }

    class Undead : Hero
    {
        public Undead(string name, int health, int armor, int damage) : base(name, health, armor, damage)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public void Anger()
        {
            int attackIncrease = 2;

            Damage = (Damage / attackIncrease);
            Health += 10;
        }

        public override void TakeDamage(int damage)
        {
            int damageReduction = 2;

            int amountDamage = (damage / damageReduction) - Armor;

            Health -= amountDamage;

            Console.WriteLine($"{Name}-Получаю урона {amountDamage}");
        }

        public override int Attack()
        {
            int maxValue = 100;
            int dropPercentage = 20;

            if (random.Next(0, maxValue) < dropPercentage)
            {
                Anger();
            }

            Console.WriteLine($"{Name}-Нанесенный урон {Damage}");

            return Damage;
        }
    }

    class Zombie : Hero
    {
        public Zombie(string name, int health, int armor, int damage) : base(name, health, armor, damage)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public void Decay()
        {
            int increaseLife = 2;

            Health = (Health * increaseLife);
        }

        public override int Attack()
        {
            int maxValue = 100;
            int dropPercentage = 20;

            if (random.Next(0, maxValue) < dropPercentage)
            {
                Decay();
            }

            Console.WriteLine($"{Name}-Нанесенный урон {Damage}");

            return Damage;
        }
    }

    class Barbarian : Hero
    {
        public Barbarian(string name, int health, int armor, int damage) : base(name, health, armor, damage)
        {
            Name = name;
            Health = health;
            Armor = armor;
            Damage = damage;
        }

        public void Shout()
        {
            Armor += 12;
            Damage += 7;
        }

        public override int Attack()
        {
            int maxValue = 100;
            int dropPercentage = 20;

            if (random.Next(0, maxValue) < dropPercentage)
            {
                Shout();
            }

            Console.WriteLine($"{Name}-Нанесенный урон {Damage}");

            return Damage;
        }
    }

    class Arena
    {
        private Hero[] _heros;

        private int _indexFirstHero;
        private int _indexSecondHero;

        private bool _isFightersReady;

        public void PlayerChoice()
        {
            _heros = new Hero[] { new Mage("Mage",60,20,35),
                                  new Warrior("Warrior",120,35,20) ,
                                  new Undead("Undead",100,25,19),
                                  new Zombie("Zombie",75,15,14),
                                  new Barbarian("Barbarian",110,40,16)};

            ShowHeroes();

            _indexFirstHero = СheckСhoiceHero("Ведите номер  первого героя ");

            _indexSecondHero = СheckСhoiceHero("Ведите номер  первого героя ");

            _isFightersReady = true;
        }

        public void StartFight()
        {
            if (_isFightersReady == true)
            {
                bool isEndFight = false;

                Hero firstHero = _heros[_indexFirstHero];
                Hero secondHero = _heros[_indexSecondHero];

                while (isEndFight == false)
                {
                    if (firstHero.Health > 0 && secondHero.Health > 0)
                    {
                        firstHero.TakeDamage(secondHero.Attack());

                        secondHero.TakeDamage(firstHero.Attack());
                    }
                    else if (firstHero.Health <= 0 && secondHero.Health <= 0)
                    {
                        ShowMessage("Оба бойца умерло \n", ConsoleColor.Red);

                        isEndFight = true;
                    }
                    else if (secondHero.Health <= 0)
                    {
                        ShowMessage($"{firstHero.Name} победил", ConsoleColor.Green);

                        isEndFight = true;
                    }
                    else if (firstHero.Health <= 0)
                    {
                        ShowMessage($"{firstHero.Name} победил", ConsoleColor.Green);

                        isEndFight = true;
                    }
                }
            }
            else
            {
                ShowMessage("Бойцы не готовы", ConsoleColor.Red);
            }

            _isFightersReady = false;
        }

        private void ShowHeroes()
        {
            ShowMessage("||||||||||||| Список героев |||||||||||||\n\n", ConsoleColor.Yellow);

            for (int i = 0; i < _heros.Length; i++)
            {
                ShowMessage($"{i}){_heros[i].Name}", ConsoleColor.Red);
            }
        }

        private int СheckСhoiceHero(string text)
        {
            string inputUser;

            int meaning = 0;

            bool isCorrect = false;

            while (isCorrect == false)
            {
                Console.Write(text);
                inputUser = Console.ReadLine();

                if (Int32.TryParse(inputUser, out meaning))
                {
                    if (meaning <= _heros.Length)
                    {
                        isCorrect = true;
                    }
                    else
                    {
                        ShowMessage("Такого бойца нет в списке", ConsoleColor.Red);
                    }
                }
                else
                {
                    ShowMessage("Вы вели вместо числа строку", ConsoleColor.Red);
                }
            }

            return meaning;
        }

        private void ShowMessage(string message, ConsoleColor color)
        {
            ConsoleColor preliminaryColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message);

            Console.ForegroundColor = preliminaryColor;
        }
    }
}
