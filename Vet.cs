namespace TestProject1;

public class Vet
{
    public bool IsWorking { get; set; }
    public bool IsWorkingDay { get; set; }
    public bool IsHealthy { get; set; }
    public bool InActivity { get; set; }
    public ActivityType Activity { get; set; }

    public Vet(bool isWorking, bool isWorkingDay, bool isHealthy, bool inActivity, ActivityType activity)
    {
        IsWorking = isWorking;
        IsWorkingDay = isWorkingDay;
        IsHealthy = isHealthy;
        InActivity = inActivity;
        Activity = activity;
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
    
    public bool ActivityChg()
    {
        InActivity = !InActivity;
        return InActivity;
    }
    
    public void ActivityTypeChg(ActivityType activityType)
    {
        Activity = activityType;
    }
    
    public bool CheckHealth(Cow cow)
    {
        return cow.Health == CowHealth.Healthy;
    }
    
    public bool Heal(Cow cow)
    {
        switch (cow.Health)
        {
            case CowHealth.Sick:
            {
                cow.Location = CowLocation.CowFlipper;
                var rand = new Random();
                var a = rand.Next(2);
                if (a == 1)
                {
                    cow.Health = CowHealth.Healthy;
                    cow.Location = CowLocation.Stall;
                    return true;
                }
                else
                {
                    cow.Location = CowLocation.Stall;
                    Foreman.Schedule.Disruptions = true;
                    Foreman.AcceptWork(false);
                    return false;
                }
            }
            case CowHealth.Dead:
                Foreman.Schedule.Disruptions = true;
                Foreman.AcceptWork(false);
                return false;
            case CowHealth.Healthy:
            default:
                return true;
        }
    }
    
    public bool UseEq(Equipment eq)
    {
        eq.IsUsing = true;
        eq.UsageTime += 100;
        return eq.IsUsing;
    }
}