namespace TestProject1;

public class Foreman
{
    public bool IsWorking { get; set; }
    public bool IsWorkingDay { get; set; }
    public bool IsHealthy { get; set; }
    public CheckType CheckType { get; set; }
    public bool IsChecking { get; set; }
    public static bool ChartRequest = false;
    public static bool EqUpdate = false;
    public List<Cow> Cows = new List<Cow>();
    public List<Worker> Workers = new List<Worker>();
    public List<Equipment> Eqs = new List<Equipment>();
    public static Schedule Schedule = new Schedule();

    public Foreman(Schedule schedule, bool isWorking, bool isWorkingDay, bool isHealthy, CheckType checkType, bool isChecking)
    {
        Schedule = schedule;
        IsWorking = isWorking;
        IsWorkingDay = isWorkingDay;
        IsHealthy = isHealthy;
        CheckType = checkType;
        IsChecking = isChecking;
    }

    public Foreman()
    {
        Schedule = new Schedule();
        IsWorking = true;
        IsWorkingDay = true;
        IsHealthy = true;
        CheckType = CheckType.Schedule;
        IsChecking = false;
    }

    public void WorkCondChange()
    {
        IsWorking = !IsWorking;
    }
    
    public void HealthChange()
    {
        IsHealthy = !IsHealthy;
        IsWorkingDay = IsHealthy;
    }
    
    public void CheckTypeChange(CheckType checkType)
    {
        CheckType = checkType;
    }
    
    public void CheckChange()
    {
        IsChecking = !IsChecking;
    }
    
    public void Order(Food food)
    {
        food.InStock = true;
        food.Location = FoodLocation.Warehouse;
        food.ReadyToUse = false;
        food.IsTransported = false;
        food.IsOrdered = true;
    }
    
    public void SetChart()
    {
        List<ScheduleActivity> scheduleActivities = new List<ScheduleActivity>
        {
            ScheduleActivity.Checking,
            ScheduleActivity.Feeding,
            ScheduleActivity.Watering, 
            ScheduleActivity.Health,
            ScheduleActivity.Milking,
            ScheduleActivity.Cleaning,
            ScheduleActivity.Breaktime
        };
        List<DateTime> dateTimes = new List<DateTime>
        {
            new DateTime(2024, 4, 19, 5, 0, 0),
            new DateTime(2024, 4, 19, 5, 30, 0),
            new DateTime(2024, 4, 19, 6, 0, 0),
            new DateTime(2024, 4, 19, 6, 30, 0),
            new DateTime(2024, 4, 19, 7, 30, 0),
            new DateTime(2024, 4, 19, 8, 0, 0),
            new DateTime(2024, 4, 19, 10, 0, 0)
        };
        List<int> durations = new List<int> { 30, 30, 30, 90, 150, 120, 240 };
        List<Responsible> responsible = new List<Responsible>
        {
            Responsible.Cattleman,
            Responsible.CalfHouse,
            Responsible.CalfHouse,
            Responsible.Vet,
            Responsible.Operator,
            Responsible.Cattleman,
            Responsible.Vet
        };
        Schedule.Activity = scheduleActivities;
        Schedule.ActivityTime = dateTimes;
        Schedule.Duration = durations;
        Schedule.Responsible = responsible;
        Schedule.IsActive = false;
        Schedule.Disruptions = false;
        Schedule.Start();
    }
    
    public Equipment BuyEq(EqType type)
    {
        var eq = new Equipment(type: type, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 0, isClean: true);
        Eqs.Add(eq);
        return eq;
    }
    
    public List<Equipment> DeleteEq(Equipment eq, List<Equipment> eqs, bool buy = true)
    {
        var type = eq.Type;
        foreach (var equip in Eqs.Where(equip => eq.Type == equip.Type).ToList())
        {
            Eqs.Remove(equip);
            eqs.Remove(eq);
            eq.IsBroken = false;
            eq.IsClean = true;
            eq.IsUsing = false;
            eq.UsageTime = 0;
            eq.ServiceRequired = false;
        }
        Schedule.Disruptions = true;

        if (!buy) return eqs;
        var newEq = BuyEq(type);
        eqs.Add(newEq);
        return eqs;
    }
    
    public static bool AcceptWork(bool accept)
    {
        if (Schedule.Disruptions)
        {
            accept = false;
            Schedule.Disruptions = false;
        }
        Schedule.Stop();
        return accept;
    }
}