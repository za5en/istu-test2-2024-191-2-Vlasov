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
    
    public void GetMilk(Cow cow)
    {
        WorkType = WorkType.Milking;
        // UseEq();
        cow.GiveMilk();
    }
    
    public Food MakeFood(FoodType foodType)
    {
        var food = new Food(inStock: true, isOrdered: false, isTransported: true, type: foodType, location: FoodLocation.Stall, readyToUse: true);
        return food;
    }
    
    public bool FeedCow(Cow cow)
    {
        WorkType = WorkType.Feeding;
        cow.IsHungry = false;
        return true;
    }
    
    public bool WaterCow(Cow cow)
    {
        WorkType = WorkType.Watering;
        cow.IsThirsty = false;
        return true;
    }
    
    public bool CleanBld(Building bld)
    {
        WorkType = WorkType.Clearing;
        bld.IsClean = true;
        return !IsWorking;
    }
    
    public bool CleanEq(Equipment eq)
    {
        WorkType = WorkType.Clearing;
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
    
    public bool RepairEq(Equipment eq)
    {
        WorkType = WorkType.Fixing;
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