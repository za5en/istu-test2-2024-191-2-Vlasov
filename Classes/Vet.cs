namespace TestProject1;

public class Vet
{
    public bool IsWorking { get; set; }
    public bool IsWorkingDay { get; set; }
    public bool IsHealthy { get; set; }
    public bool InActivity { get; set; }
    public ActivityType Activity { get; set; }

    public static bool CheckHealthRequest = false;

    public Vet()
    {
        IsWorking = true;
        IsWorkingDay = true;
        IsHealthy = true;
        InActivity = true;
        Activity = ActivityType.Treatment;
    }
    
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

    public void HealthChange()
    {
        IsHealthy = !IsHealthy;
        IsWorkingDay = IsHealthy;
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
        Activity = ActivityType.Prevention;
        return cow.Health == CowHealth.Healthy;
    }

    public bool Heal(Cow cow, bool fail = false)
    {
        Activity = ActivityType.Treatment;
        switch (cow.Health)
        {
            case CowHealth.Sick:
            {
                cow.Location = CowLocation.CowFlipper;
                if (!fail)
                {
                    var rand = new Random();
                    var a = rand.Next(2);
                    if (a == 1)
                    {
                        cow.Health = CowHealth.Healthy;
                        cow.Location = CowLocation.Stall;
                        return true;
                    }
                    cow.Location = CowLocation.Stall;
                    Foreman.Schedule.Disruptions = true;
                    Foreman.AcceptWork(false);
                    return false;
                }
                cow.Location = CowLocation.Stall;
                Foreman.Schedule.Disruptions = true;
                return false;
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

    public void UseEq(Equipment eq)
    {
        eq.IsUsing = true;
        eq.UsageTime += 100;
    }
}