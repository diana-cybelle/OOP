using System;
using System.Collections.Generic;

// 1. Interface: defines what every battery must do. 
interface IBattery
{
    int RemainingEnergy { get; }
    void Consume(int amount);
    void Recharge(int amount);
}

// 2. LithiumBattery class
class LithiumBattery : IBattery
{
    private int _remainingEnergy;

    public int RemainingEnergy
    {
        get { return _remainingEnergy; }
    }

    public LithiumBattery(int startingEnergy)
    {
        _remainingEnergy = Math.Clamp(startingEnergy, 0, 100);
    }

    public void Consume(int amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Energy amount cannot be negative.");
            return;
        }

        _remainingEnergy = Math.Max(0, _remainingEnergy - amount);
    }

    public void Recharge(int amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Recharge amount cannot be negative.");
            return;
        }

        _remainingEnergy = Math.Min(100, _remainingEnergy + amount);
    }
}

// 3. SolarBattery class
class SolarBattery : IBattery
{
    private int _remainingEnergy;

    public int RemainingEnergy
    {
        get { return _remainingEnergy; }
    }

    public SolarBattery(int startingEnergy)
    {
        _remainingEnergy = Math.Clamp(startingEnergy, 0, 100);
    }

    public void Consume(int amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Energy amount cannot be negative.");
            return;
        }

        _remainingEnergy = Math.Max(0, _remainingEnergy - amount);
    }

    public void Recharge(int amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Recharge amount cannot be negative.");
            return;
        }

        _remainingEnergy = Math.Min(100, _remainingEnergy + amount);
    }
}

// 4. Abstract parent class: Robot
abstract class Robot
{
    private readonly IBattery _battery;

    public string Name { get; set; }

    public int RemainingEnergy
    {
        get { return _battery.RemainingEnergy; }
    }

    protected Robot(string name, IBattery battery)
    {
        Name = name;
        _battery = battery;
    }

    public void Recharge(int amount)
    {
        _battery.Recharge(amount);
        Console.WriteLine($"{Name} recharged by {amount}. Energy: {RemainingEnergy}%");
    }

    protected void UseEnergy(int amount)
    {
        _battery.Consume(amount);
        Console.WriteLine($"{Name} used {amount} energy. Energy left: {RemainingEnergy}%");
    }

    public abstract void PerformTask();
}

// 5. CleaningRobot inherits from Robot
class CleaningRobot : Robot
{
    public CleaningRobot(string name, IBattery battery) : base(name, battery)
    {
    }

    public override void PerformTask()
    {
        Console.WriteLine($"{Name} is cleaning.");
        UseEnergy(10);
    }
}

// 6. DeliveryRobot inherits from Robot
class DeliveryRobot : Robot
{
    public DeliveryRobot(string name, IBattery battery) : base(name, battery)
    {
    }

    public override void PerformTask()
    {
        Console.WriteLine($"{Name} is delivering a package.");
        UseEnergy(25);
    }
}

// 7. Main program
class Program
{
    static void Main()
    {
        // Inject a different battery type into each robot.
        Robot cleaningRobot = new CleaningRobot("CleanBot", new LithiumBattery(80));
        Robot deliveryRobot = new DeliveryRobot("DeliveryBot", new SolarBattery(60));

        // Polymorphism: store different robot types in one List<Robot>.
        List<Robot> robots = new List<Robot>
        {
            cleaningRobot,
            deliveryRobot
        };

        Console.WriteLine("=== ROBOT TASKS ===");

        foreach (Robot robot in robots)
        {
            robot.PerformTask();
            Console.WriteLine();
        }

        Console.WriteLine("=== RECHARGING ===");
        foreach (Robot robot in robots)
        {
            robot.Recharge(15);
        }

        Console.WriteLine("\n=== FINAL ENERGY ===");
        foreach (Robot robot in robots)
        {
            Console.WriteLine($"{robot.Name}: {robot.RemainingEnergy}%");
        }
    }
}
