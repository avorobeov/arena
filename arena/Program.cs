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
                        arena.ChoiceHeroes();
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
        protected Random _random = new Random();

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
            int minimumAmountArmor = 1;
            int getPercentages = 100;

            double amountDamage = damage - damage * Armor / getPercentages;

            if (Armor >= minimumAmountArmor)
            {
                Health -= (int)amountDamage;

                Console.WriteLine($"{Name}-Получаю урона {amountDamage}");
            }
            else
            {
                Health -= damage;
            }
        }

        public abstract int GetAttack();

        protected abstract void UseUniqueAbility();
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

        public override int GetAttack()
        {
            int maxValue = 100;
            int dropPercentage = 15;

            if (_random.Next(0, maxValue) < dropPercentage)
            {
                UseUniqueAbility();
            }

            Console.WriteLine($"Нанесенный урон {Damage}");

            return Damage;
        }

        protected override void UseUniqueAbility()
        {
            Health += 30;
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
      
        public override int GetAttack()
        {
            int maxValue = 100;
            int dropPercentage = 20;

            if (_random.Next(0, maxValue) < dropPercentage)
            {
                UseUniqueAbility();
            }

            Console.WriteLine($"{Name}-Нанесенный урон {Damage}");

            return Damage;
        }

        protected override void UseUniqueAbility()
        {
            Health += 10;

            if (Armor >= 1)
            {
                Armor -= 4;
            }

            Damage += 3;
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

        public override void TakeDamage(int damage)
        {
            int damageReduction = 2;

            int amountDamage = (damage / damageReduction) - Armor;

            Health -= amountDamage;

            Console.WriteLine($"{Name}-Получаю урона {amountDamage}");
        }

        public override int GetAttack()
        {
            int maxValue = 100;
            int dropPercentage = 20;

            if (_random.Next(0, maxValue) < dropPercentage)
            {
                UseUniqueAbility();
            }

            Console.WriteLine($"{Name}-Нанесенный урон {Damage}");

            return Damage;
        }

        protected override void UseUniqueAbility()
        {
            int attackIncrease = 2;

            Damage = (Damage / attackIncrease);
            Health += 10;
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

        public override int GetAttack()
        {
            int maxValue = 100;
            int dropPercentage = 20;

            if (_random.Next(0, maxValue) < dropPercentage)
            {
                UseUniqueAbility();
            }

            Console.WriteLine($"{Name}-Нанесенный урон {Damage}");

            return Damage;
        }

        protected override void UseUniqueAbility()
        {
            int increaseLife = 2;

            Health = (Health * increaseLife);
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

        public override int GetAttack()
        {
            int maxValue = 100;
            int dropPercentage = 20;

            if (_random.Next(0, maxValue) < dropPercentage)
            {
                UseUniqueAbility();
            }

            Console.WriteLine($"{Name}-Нанесенный урон {Damage}");

            return Damage;
        }

        protected override void UseUniqueAbility()
        {
            Armor += 12;
            Damage += 7;
        }
    }

    class Arena
    {
        private List<Hero> _heros;

        private Hero _firstHero;
        private Hero _secondHero;

        private bool _isFightersReady;

        public void ChoiceHeroes()
        {
            _heros = new List<Hero> { new Mage("Mage",60,20,35),
                                      new Warrior("Warrior",120,35,20) ,
                                      new Undead("Undead",100,25,19),
                                      new Zombie("Zombie",75,15,14),
                                      new Barbarian("Barbarian",110,40,16)};

            ShowHeroes();

            _firstHero = GetHero("Ведите номер  первого героя ");

            ShowHeroes();

            _secondHero = GetHero("Ведите номер  первого героя ");

            _isFightersReady = true;
        }

        public void StartFight()
        {
            if (_isFightersReady == true)
            {
                while (_firstHero.Health > 0 && _secondHero.Health > 0)
                {
                    _firstHero.TakeDamage(_secondHero.GetAttack());

                    _secondHero.TakeDamage(_firstHero.GetAttack());

                    if (_firstHero.Health <= 0 && _secondHero.Health <= 0)
                    {
                        ShowMessage("Оба бойца умерло \n", ConsoleColor.Red);
                    }
                    else if (_secondHero.Health <= 0)
                    {
                        ShowMessage($"{_firstHero.Name} победил", ConsoleColor.Green);
                    }
                    else if (_firstHero.Health <= 0)
                    {
                        ShowMessage($"{_firstHero.Name} победил", ConsoleColor.Green);
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

            for (int i = 0; i < _heros.Count; i++)
            {
                ShowMessage($"{i}){_heros[i].Name}", ConsoleColor.Red);
            }
        }

        private Hero GetHero(string text)
        {
            Hero hero = null;

            string inputUser;

            int meaning = 0;

            bool isCorrect = false;

            while (isCorrect == false)
            {
                Console.Write(text);
                inputUser = Console.ReadLine();

                if (Int32.TryParse(inputUser, out meaning))
                {
                    if (meaning <= _heros.Count && meaning >= 0)
                    {
                        hero = _heros[meaning];

                        _heros.RemoveAt(meaning);

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

            return hero;
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
