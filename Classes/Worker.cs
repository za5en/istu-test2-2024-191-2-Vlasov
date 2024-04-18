namespace TestProject1;

public class Worker
{
    public bool IsWorking { get; set; }
    public bool IsWorkingDay { get; set; }
    public bool IsHealthy { get; set; }
    public WorkType WorkType { get; set; }
    public WorkerLocation Location { get; set; }
    public WorkerPost Post { get; set; }
    public bool IsAccepted { get; set; }

    public Worker()
    {
        IsWorking = true;
        IsWorkingDay = true;
        IsHealthy = true;
        WorkType = WorkType.Milking;
        Location = WorkerLocation.MilkingHall;
        Post = WorkerPost.Operator;
        IsAccepted = false;
    }
    
    public Worker(bool isWorking, bool isWorkingDay, bool isHealthy, WorkType workType, WorkerLocation location, WorkerPost post, bool isAccepted)
    {
        IsWorking = isWorking;
        IsWorkingDay = isWorkingDay;
        IsHealthy = isHealthy;
        WorkType = workType;
        Location = location;
        Post = post;
        IsAccepted = isAccepted;
    }

    public bool WorkCondChange()
    {
        IsWorking = !IsWorking;
        return IsWorking;
    }
    
    public bool HealthChange()
    {
        IsHealthy = !IsHealthy;
        IsWorkingDay = IsHealthy;
        return IsHealthy;
    }
    
    public Product? GetMilk(Worker worker, Cow cow, Equipment eq)
    {
        WorkType = WorkType.Milking;
        worker.WorkType = WorkType.Milking;
        UseEq(eq);
        var prod = cow.GiveMilk();
        return prod;
    }
    
    public Food MakeFood(FoodType foodType)
    {
        var food = new Food(inStock: true, isOrdered: false, isTransported: true, type: foodType, location: FoodLocation.Stall, readyToUse: true);
        return food;
    }
    
    public bool FeedCow(Worker worker, Cow cow)
    {
        WorkType = WorkType.Feeding;
        worker.WorkType = WorkType.Feeding;
        cow.IsHungry = false;
        return true;
    }
    
    public bool WaterCow(Worker worker, Cow cow)
    {
        WorkType = WorkType.Watering;
        worker.WorkType = WorkType.Watering;
        cow.IsThirsty = false;
        return true;
    }
    
    public bool CleanBld(Worker worker, Building bld)
    {
        WorkType = WorkType.Cleaning;
        worker.WorkType = WorkType.Cleaning;
        bld.IsClean = true;
        return bld.IsClean;
    }
    
    public bool CleanEq(Worker worker, Equipment eq)
    {
        WorkType = WorkType.Cleaning;
        worker.WorkType = WorkType.Cleaning;
        eq.IsClean = true;
        return eq.IsClean;
    }
    
    public bool UseEq(Equipment eq)
    {
        eq.IsUsing = true;
        eq.UsageTime += 100;
        return eq.IsUsing;
    }
    
    public bool CheckEq(Equipment eq)
    {
        if (eq.UsageTime > 10000)
        {
            eq.ServiceRequired = true;
        }
        else if (eq.UsageTime > 20000)
        {
            eq.IsBroken = true;
        }
        return !eq.IsBroken;
    }
    
    public bool RepairEq(Worker worker, Equipment eq)
    {
        WorkType = WorkType.Fixing;
        worker.WorkType = WorkType.Fixing;
        if (eq.IsBroken)
        {
            var rand = new Random();
            var a = rand.Next(2);
            if (a == 1)
            {
                eq.IsBroken = false;
                eq.UsageTime = 0;
                eq.ServiceRequired = false;
                return true;
            }
            // Foreman.DeleteEq(eq);
            Foreman.Schedule.Disruptions = true;
            Foreman.AcceptWork(false);
            return false;
        }
        if (!eq.ServiceRequired) return true;
        eq.UsageTime = 0;
        eq.ServiceRequired = false;
        return true;
    }
}