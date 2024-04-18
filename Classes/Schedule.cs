namespace TestProject1;

public class Schedule
{
    public List<ScheduleActivity> Activity { get; set; }
    public List<DateTime> ActivityTime { get; set; }
    public List<int> Duration { get; set; }
    public List<Responsible> Responsible { get; set; }
    public bool IsActive { get; set; }
    public bool Disruptions { get; set; }

    public Schedule()
    {
        Activity = new List<ScheduleActivity>
        {
            ScheduleActivity.Checking,
            ScheduleActivity.Feeding,
            ScheduleActivity.Watering, 
            ScheduleActivity.Health,
            ScheduleActivity.Milking,
            ScheduleActivity.Cleaning,
            ScheduleActivity.Breaktime
        };
        ActivityTime = new List<DateTime>
        {
            new DateTime(2024, 4, 19, 5, 0, 0),
            new DateTime(2024, 4, 19, 5, 30, 0),
            new DateTime(2024, 4, 19, 6, 0, 0),
            new DateTime(2024, 4, 19, 6, 30, 0),
            new DateTime(2024, 4, 19, 7, 30, 0),
            new DateTime(2024, 4, 19, 8, 0, 0),
            new DateTime(2024, 4, 19, 10, 0, 0)
        };
        Duration = new List<int> { 30, 30, 30, 90, 150, 120, 220 };
        Responsible = new List<Responsible>
        {
            TestProject1.Responsible.Cattleman,
            TestProject1.Responsible.CalfHouse,
            TestProject1.Responsible.CalfHouse,
            TestProject1.Responsible.Vet,
            TestProject1.Responsible.Operator,
            TestProject1.Responsible.Cattleman,
            TestProject1.Responsible.Vet
        };
        IsActive = false;
        Disruptions = false;
    }

    public Schedule(List<ScheduleActivity> activity, List<DateTime> activityTime, List<int> duration, List<Responsible> responsible, bool isActive, bool disruptions)
    {
        Activity = activity;
        ActivityTime = activityTime;
        Duration = duration;
        Responsible = responsible;
        IsActive = isActive;
        Disruptions = disruptions;
    }

    public bool ActiveChange()
    {
        IsActive = !IsActive;
        return IsActive;
    }
    
    public bool Disrupted()
    {
        Disruptions = true;
        return Disruptions;
    }
    
    public bool Start()
    {
        IsActive = true;
        return IsActive;
    }
    
    public bool Stop()
    {
        IsActive = false;
        return IsActive;
    }
}