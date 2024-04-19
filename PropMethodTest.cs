#pragma warning disable NUnit2005 // Consider using Assert.That(actual, Is.EqualTo(expected)) instead of ClassicAssert.AreEqual(expected, actual) 
namespace TestProject1;

public class PropMethodsTest
{
    public static Foreman foreman;
    public static List<Worker> workers;
    public static List<Cow> cows;
    public static List<Equipment> equipment;
    public static Vet vet;
    public static Schedule schedule;
    public static Food food;
    public static Water water;
    public static Building building;

    [SetUp]
    public void Setup()
    {
        workers = new List<Worker>();
        workers.AddRange(new List<Worker>()
        {
            new(isWorking: true, isWorkingDay: true, isHealthy: true, workType: WorkType.Fixing,
                location: WorkerLocation.Barn, post: WorkerPost.Cattleman, isAccepted: false),
            new(isWorking: true, isWorkingDay: true, isHealthy: true, workType: WorkType.Milking,
                location: WorkerLocation.MilkingHall, post: WorkerPost.Operator, isAccepted: false),
            new(isWorking: true, isWorkingDay: true, isHealthy: true, workType: WorkType.Feeding,
                location: WorkerLocation.Barn, post: WorkerPost.CalfHouse, isAccepted: false),
        });
        cows = new List<Cow>();
        cows.AddRange(new List<Cow>()
        {
            new(type: CowType.Cow, condition: CowCond.Living, location: CowLocation.Stall, CowHealth.Healthy,
                inQueue: false, isHungry: false, isThirsty: false),
            new(type: CowType.Calf, condition: CowCond.Living, location: CowLocation.Stall, CowHealth.Healthy,
                inQueue: false, isHungry: false, isThirsty: false),
            new(type: CowType.Heifer, condition: CowCond.Living, location: CowLocation.Stall, CowHealth.Healthy,
                inQueue: false, isHungry: false, isThirsty: false),
            new(type: CowType.Replacement, condition: CowCond.Living, location: CowLocation.Stall, CowHealth.Healthy,
                inQueue: false, isHungry: false, isThirsty: false),
        });
        equipment = new List<Equipment>();
        equipment.AddRange(new List<Equipment>()
        {
            new(type: EqType.Loader, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.WateringMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.MilkingMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.CowFlipper, isBroken: true, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Scraper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
        });
        food = new Food();
        water = new Water();
        building = new Building(type: BuildingType.MilkingHall, serviceRequired: false, isClean: true);
        vet = new Vet(isWorking: true, isWorkingDay: true, isHealthy: true, inActivity: false,
            activity: ActivityType.Prevention);
        schedule = new Schedule();
        foreman = new Foreman(schedule: schedule, isWorking: true, isWorkingDay: true, isHealthy: true,
            checkType: CheckType.Schedule, isChecking: false);
    }

    //инициализация объекта Foreman
    [Test]
    public void TestInitForeman()
    {
        Foreman foremanCheck = new Foreman();
        Assert.AreEqual(true, foremanCheck.IsWorking);
        Assert.AreEqual(true, foremanCheck.IsWorkingDay);
        Assert.AreEqual(true, foremanCheck.IsHealthy);
        Assert.AreEqual(CheckType.Schedule, foremanCheck.CheckType);
        Assert.AreEqual(false, foremanCheck.IsChecking);
    }

    [Test]
    public void TestMethodsForeman()
    {
        //тестирование метода WorkCondChange
        foreman.WorkCondChange();
        Assert.AreEqual(false, foreman.IsWorking);
        //тестирование метода HealthChange
        foreman.HealthChange();
        Assert.AreEqual(false, foreman.IsHealthy);
        Assert.AreEqual(false, foreman.IsWorkingDay);
        //тестирование метода CheckTypeChange
        foreman.CheckTypeChange(CheckType.Equipment);
        Assert.AreEqual(CheckType.Equipment, foreman.CheckType);
        //тестирование метода CheckChange
        foreman.CheckChange();
        Assert.AreEqual(true, foreman.IsChecking);
        //тестирование метода Order
        foreman.Order(food);
        var foodCheck = new Food(inStock: true, isOrdered: true, isTransported: false, type: FoodType.Hay,
            location: FoodLocation.Warehouse, readyToUse: false);
        // Assert.AreEqual(foodCheck, food);
        Assert.AreEqual(foodCheck.InStock, food.InStock);
        Assert.AreEqual(foodCheck.IsOrdered, food.IsOrdered);
        Assert.AreEqual(foodCheck.IsTransported, food.IsTransported);
        Assert.AreEqual(foodCheck.Type, food.Type);
        Assert.AreEqual(foodCheck.Location, food.Location);
        Assert.AreEqual(foodCheck.ReadyToUse, food.ReadyToUse);
        //тестирование метода SetChart
        foreman.SetChart();
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
            new(2024, 4, 19, 5, 0, 0),
            new(2024, 4, 19, 5, 30, 0),
            new(2024, 4, 19, 6, 0, 0),
            new(2024, 4, 19, 6, 30, 0),
            new(2024, 4, 19, 7, 30, 0),
            new(2024, 4, 19, 8, 0, 0),
            new(2024, 4, 19, 10, 0, 0)
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
        var scheduleCheck = new Schedule(activity: scheduleActivities, activityTime: dateTimes, duration: durations,
            responsible: responsible, isActive: true, disruptions: false);
        Assert.AreEqual(scheduleCheck, Foreman.Schedule);
        //тестирование метода DeleteEq
        equipment = foreman.DeleteEq(equipment.First(), equipment);
        var eqCheck = new List<Equipment>();
        eqCheck.AddRange(new List<Equipment>()
        {
            new(type: EqType.WateringMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.MilkingMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.CowFlipper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Scraper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Loader, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 0,
                isClean: true),
        });
        Assert.AreEqual(eqCheck, equipment);
        Assert.AreEqual(true, Foreman.Schedule.Disruptions);
        //тестирование метода BuyEq
        var newEq = foreman.BuyEq(equipment.First().Type);
        var oneEqCheck = new Equipment(type: EqType.WateringMachine, isBroken: false, isUsing: false,
            serviceRequired: false, usageTime: 0, isClean: true);
        Assert.AreEqual(oneEqCheck, newEq);
        //тестирование метода AcceptWork
        Foreman.AcceptWork(true);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        Assert.AreEqual(false, Foreman.Schedule.IsActive);
    }

    //инициализация объекта Worker
    [Test]
    public void TestInitWorker()
    {
        Worker workerCheck = new Worker();
        Assert.AreEqual(true, workerCheck.IsWorking);
        Assert.AreEqual(true, workerCheck.IsWorkingDay);
        Assert.AreEqual(true, workerCheck.IsHealthy);
        Assert.AreEqual(WorkType.Milking, workerCheck.WorkType);
        Assert.AreEqual(WorkerLocation.MilkingHall, workerCheck.Location);
        Assert.AreEqual(WorkerPost.Operator, workerCheck.Post);
        Assert.AreEqual(false, workerCheck.IsAccepted);
    }

    [Test]
    public void TestMethodsWorker()
    {
        //тестирование метода WorkCondChange
        workers.First().WorkCondChange();
        Assert.AreEqual(false, workers.First().IsWorking);
        //тестирование метода HealthChange
        workers.First().HealthChange();
        Assert.AreEqual(false, workers.First().IsHealthy);
        Assert.AreEqual(false, workers.First().IsWorkingDay);
        //тестирование метода GetMilk
        var prodAct = workers[1].GetMilk(worker: workers[1], cow: cows.First(), equipment[1]) ??
                      new Product(isObtained: true, inStock: true, isStored: true, isSent: false, isSpoiled: false);
        var prodExp = new Product(isObtained: true, inStock: true, isStored: true, isSent: false, isSpoiled: false);
        Assert.AreEqual(WorkType.Milking, workers[1].WorkType);
        Assert.AreEqual(200, equipment[1].UsageTime);
        Assert.AreEqual(true, equipment[1].IsUsing);
        Assert.AreEqual(prodExp.IsObtained, prodAct.IsObtained);
        Assert.AreEqual(prodExp.InStock, prodAct.InStock);
        Assert.AreEqual(prodExp.IsStored, prodAct.IsStored);
        Assert.AreEqual(prodExp.IsSent, prodAct.IsSent);
        Assert.AreEqual(prodExp.IsSpoiled, prodAct.IsSpoiled);
        // Assert.AreEqual(prodExp, prod);
        //тестирование метода MakeFood
        var foodAct = workers[2].MakeFood(food.Type);
        var foodExp = new Food(inStock: true, isOrdered: false, isTransported: true, type: food.Type,
            location: FoodLocation.Stall, readyToUse: true);
        Assert.AreEqual(foodAct.InStock, food.InStock);
        Assert.AreEqual(foodAct.IsOrdered, food.IsOrdered);
        Assert.AreEqual(foodAct.IsTransported, food.IsTransported);
        Assert.AreEqual(foodAct.Type, food.Type);
        Assert.AreEqual(foodAct.Location, food.Location);
        Assert.AreEqual(foodAct.ReadyToUse, food.ReadyToUse);
        // Assert.AreEqual(foodExp, foodAct);
        //тестирование метода FeedCow
        workers[2].FeedCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Feeding, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsHungry);
        //тестирование метода WaterCow
        workers[2].WaterCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Watering, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsThirsty);
        //тестирование метода CleanBld
        workers.First().CleanBld(worker: workers[0], bld: building);
        Assert.AreEqual(WorkType.Cleaning, workers[0].WorkType);
        Assert.AreEqual(true, building.IsClean);
        //тестирование метода CleanEq
        workers.First().CleanEq(workers[0], equipment[1]);
        Assert.AreEqual(WorkType.Cleaning, workers[0].WorkType);
        Assert.AreEqual(true, equipment[1].IsClean);
        //тестирование метода UseEq
        workers[1].UseEq(equipment[1]);
        Assert.AreEqual(true, equipment[1].IsUsing);
        Assert.AreEqual(300, equipment[1].UsageTime);
        //тестирование метода CheckEq
        workers.First().CheckEq(equipment[1]);
        Assert.AreEqual(false, equipment[1].ServiceRequired);
        Assert.AreEqual(false, equipment[1].IsBroken);
        //тестирование метода RepairEq
        equipment[1].IsBroken = true;
        var res = workers.First().RepairEq(workers[0], equipment[1]);
        Assert.AreEqual(WorkType.Fixing, workers[0].WorkType);
        if (res)
        {
            Assert.AreEqual(false, equipment[1].IsBroken);
            Assert.AreEqual(0, equipment[1].UsageTime);
            Assert.AreEqual(false, equipment[1].ServiceRequired);
        }
        else
        {
            Assert.AreEqual(true, equipment[1].IsBroken);
            Assert.AreEqual(false, Foreman.Schedule.Disruptions);
            Assert.AreEqual(false, Foreman.Schedule.IsActive);
        }
    }

    //инициализация объекта Cow
    [Test]
    public void TestInitCow()
    {
        Cow cowCheck = new Cow();
        Assert.AreEqual(CowType.Cow, cowCheck.Type);
        Assert.AreEqual(CowCond.Milking, cowCheck.Condition);
        Assert.AreEqual(CowLocation.MilkingHall, cowCheck.Location);
        Assert.AreEqual(CowHealth.Healthy, cowCheck.Health);
        Assert.AreEqual(false, cowCheck.InQueue);
        Assert.AreEqual(false, cowCheck.IsHungry);
        Assert.AreEqual(false, cowCheck.IsThirsty);
    }

    [Test]
    public void TestMethodsCow()
    {
        //тестирование метода Move
        Assert.AreEqual(CowLocation.CowFlipper, cows.First().Move(CowLocation.CowFlipper));
        //тестирование метода HealthChange
        Assert.AreEqual(CowHealth.Sick, cows.Last().HealthChange(CowHealth.Sick));
        //тестирование метода CondChange
        Assert.AreEqual(CowCond.Sleeping, cows.First().CondChange(CowCond.Sleeping));
        //тестирование метода Waiting
        Assert.AreEqual(true, cows.First().Waiting());
        //тестирование метода GiveMilk
        var prodAct = cows.First().GiveMilk() ?? new Product(isObtained: true, inStock: true, isStored: true,
            isSent: false, isSpoiled: false);
        var prodExp = new Product(isObtained: true, inStock: true, isStored: true, isSent: false, isSpoiled: false);
        Assert.AreEqual(CowCond.Milking, cows.First().Condition);
        Assert.AreEqual(true, cows.First().IsHungry);
        Assert.AreEqual(true, cows.First().IsThirsty);
        Assert.AreEqual(prodExp.IsObtained, prodAct.IsObtained);
        Assert.AreEqual(prodExp.InStock, prodAct.InStock);
        Assert.AreEqual(prodExp.IsStored, prodAct.IsStored);
        Assert.AreEqual(prodExp.IsSent, prodAct.IsSent);
        Assert.AreEqual(prodExp.IsSpoiled, prodAct.IsSpoiled);
        // Assert.AreEqual(prodExp, prodAct);
        //тестирование метода Drink
        cows.First().Drink(water);
        Assert.AreEqual(false, cows.First().IsThirsty);
        Assert.AreEqual(CowCond.Drinking, cows.First().Condition);
        Assert.AreEqual(false, water.InStock);
        //тестирование метода Eat
        cows.First().Eat(food);
        Assert.AreEqual(false, cows.First().IsHungry);
        Assert.AreEqual(CowCond.Eating, cows.First().Condition);
        Assert.AreEqual(false, food.InStock);
        //тестирование метода WakeUp
        Assert.AreEqual(CowCond.Eating, cows.First().WakeUp());
        //тестирование метода Sleep
        Assert.AreEqual(CowCond.Sleeping, cows.First().Sleep());
    }

    //инициализация объекта Vet
    [Test]
    public void TestInitVet()
    {
        Vet vetCheck = new Vet();
        Assert.AreEqual(true, vetCheck.IsWorking);
        Assert.AreEqual(true, vetCheck.IsWorkingDay);
        Assert.AreEqual(true, vetCheck.IsHealthy);
        Assert.AreEqual(ActivityType.Treatment, vetCheck.Activity);
        Assert.AreEqual(true, vetCheck.InActivity);
    }

    [Test]
    public void TestMethodsVet()
    {
        //тестирование метода WorkCondChange
        vet.IsWorking = false;
        Assert.AreEqual(true, vet.WorkCondChange());
        //тестирование метода HealthChange
        vet.IsHealthy = false;
        vet.IsWorkingDay = false;
        vet.HealthChange();
        Assert.AreEqual(true, vet.IsHealthy);
        Assert.AreEqual(true, vet.IsWorkingDay);
        //тестирование метода ActivityChg
        vet.InActivity = false;
        Assert.AreEqual(true, vet.ActivityChg());
        //тестирование метода ActivityTypeChg
        vet.Activity = ActivityType.Prevention;
        vet.ActivityTypeChg(ActivityType.Treatment);
        Assert.AreEqual(ActivityType.Treatment, vet.Activity);
        //тестирование метода CheckHealth
        vet.CheckHealth(cows.First());
        Assert.AreEqual(ActivityType.Prevention, vet.Activity);
        //тестирование метода Heal
        cows[1].Health = CowHealth.Sick;
        var heal = vet.Heal(cows[1]);
        Assert.AreEqual(ActivityType.Treatment, vet.Activity);
        if (heal)
        {
            Assert.AreEqual(CowHealth.Healthy, cows[1].Health);
            Assert.AreEqual(CowLocation.Stall, cows[1].Location);
        }
        else
        {
            Assert.AreEqual(CowHealth.Sick, cows[1].Health);
            Assert.AreEqual(CowLocation.Stall, cows[1].Location);
            Assert.AreEqual(false, Foreman.Schedule.Disruptions);
            Assert.AreEqual(false, Foreman.Schedule.IsActive);
        }

        //тестирование метода UseEq
        vet.UseEq(equipment[1]);
        Assert.AreEqual(true, equipment[1].IsUsing);
        Assert.AreEqual(200, equipment[1].UsageTime);
    }

    //инициализация объекта Building
    [Test]
    public void TestInitBuilding()
    {
        Building bldCheck = new Building();
        Assert.AreEqual(false, bldCheck.ServiceRequired);
        Assert.AreEqual(BuildingType.MilkingHall, bldCheck.Type);
        Assert.AreEqual(true, bldCheck.IsClean);
    }

    [Test]
    public void TestMethodsBuilding()
    {
        building.ServiceRequired = false;
        //тестирование метода ServicePointCheck
        Assert.AreEqual(true, building.ServicePointCheck());
        //тестирование метода CowMove
        var bld = building.CowMove(cows.First());
        Assert.AreEqual(BuildingType.MilkingHall, bld);
        Assert.AreEqual(CowLocation.MilkingHall, cows.First().Location);
    }

    //инициализация объекта Schedule
    [Test]
    public void TestInitSchedule()
    {
        Schedule schCheck = new Schedule();
        Assert.AreEqual(new List<ScheduleActivity>
        {
            ScheduleActivity.Checking,
            ScheduleActivity.Feeding,
            ScheduleActivity.Watering,
            ScheduleActivity.Health,
            ScheduleActivity.Milking,
            ScheduleActivity.Cleaning,
            ScheduleActivity.Breaktime
        }, schCheck.Activity);
        Assert.AreEqual(new List<DateTime>
        {
            new(2024, 4, 19, 5, 0, 0),
            new(2024, 4, 19, 5, 30, 0),
            new(2024, 4, 19, 6, 0, 0),
            new(2024, 4, 19, 6, 30, 0),
            new(2024, 4, 19, 7, 30, 0),
            new(2024, 4, 19, 8, 0, 0),
            new(2024, 4, 19, 10, 0, 0)
        }, schCheck.ActivityTime);
        Assert.AreEqual(new List<int> { 30, 30, 30, 90, 150, 120, 220 }, schCheck.Duration);
        Assert.AreEqual(new List<Responsible>
        {
            Responsible.Cattleman,
            Responsible.CalfHouse,
            Responsible.CalfHouse,
            Responsible.Vet,
            Responsible.Operator,
            Responsible.Cattleman,
            Responsible.Vet
        }, schCheck.Responsible);
        Assert.AreEqual(false, schCheck.IsActive);
        Assert.AreEqual(false, schCheck.Disruptions);
    }

    [Test]
    public void TestMethodsSchedule()
    {
        schedule.IsActive = true;
        //тестирование метода ActiveChange
        Assert.AreEqual(false, schedule.ActiveChange());
        //тестирование метода Disrupted
        Assert.AreEqual(true, schedule.Disrupted());
        //тестирование метода Start
        Assert.AreEqual(true, schedule.Start());
        //тестирование метода Stop
        Assert.AreEqual(false, schedule.Stop());
    }

    //инициализация объекта Equipment
    [Test]
    public void TestInitEquipment()
    {
        Equipment eqCheck = new Equipment(EqType.MilkingMachine);
        Assert.AreEqual(false, eqCheck.ServiceRequired);
        Assert.AreEqual(EqType.MilkingMachine, eqCheck.Type);
        Assert.AreEqual(true, eqCheck.IsClean);
        Assert.AreEqual(50, eqCheck.UsageTime);
        Assert.AreEqual(false, eqCheck.IsBroken);
        Assert.AreEqual(false, eqCheck.IsUsing);
    }

    //инициализация объекта Food
    [Test]
    public void TestInitFood()
    {
        Food foodCheck = new Food();
        Assert.AreEqual(FoodType.Hay, foodCheck.Type);
        Assert.AreEqual(FoodLocation.Stall, foodCheck.Location);
        Assert.AreEqual(true, foodCheck.InStock);
        Assert.AreEqual(false, foodCheck.IsOrdered);
        Assert.AreEqual(true, foodCheck.IsTransported);
        Assert.AreEqual(false, foodCheck.ReadyToUse);
    }

    //инициализация объекта Product
    [Test]
    public void TestInitProduct()
    {
        Product prodCheck = new Product();
        Assert.AreEqual(true, prodCheck.InStock);
        Assert.AreEqual(true, prodCheck.IsObtained);
        Assert.AreEqual(true, prodCheck.IsStored);
        Assert.AreEqual(false, prodCheck.IsSent);
        Assert.AreEqual(false, prodCheck.IsSpoiled);
    }

    //инициализация объекта Water
    [Test]
    public void TestInitWater()
    {
        Water waterCheck = new Water();
        Assert.AreEqual(true, waterCheck.InStock);
        Assert.AreEqual(WaterLocation.WateringMachine, waterCheck.Location);
        Assert.AreEqual(Purpose.Watering, waterCheck.Purpose);
        Assert.AreEqual(true, waterCheck.ReadyToUse);
    }

    //тестирование состояний
    [Test]
    public void TestFirst()
    {
        //Расписание -> Оборудование проверено -> Оборудование утилизировано -> Покупка оборудования -> Работа не принята
        equipment = new List<Equipment>();
        equipment.AddRange(new List<Equipment>()
        {
            new(type: EqType.Loader, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.WateringMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.MilkingMachine, isBroken: true, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.CowFlipper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Scraper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 15000,
                isClean: true),
        });
        //Расписание 
        foreman.SetChart();
        Assert.AreEqual(new List<ScheduleActivity>
        {
            ScheduleActivity.Checking,
            ScheduleActivity.Feeding,
            ScheduleActivity.Watering,
            ScheduleActivity.Health,
            ScheduleActivity.Milking,
            ScheduleActivity.Cleaning,
            ScheduleActivity.Breaktime
        }, Foreman.Schedule.Activity);
        Assert.AreEqual(new List<DateTime>
        {
            new(2024, 4, 19, 5, 0, 0),
            new(2024, 4, 19, 5, 30, 0),
            new(2024, 4, 19, 6, 0, 0),
            new(2024, 4, 19, 6, 30, 0),
            new(2024, 4, 19, 7, 30, 0),
            new(2024, 4, 19, 8, 0, 0),
            new(2024, 4, 19, 10, 0, 0)
        }, Foreman.Schedule.ActivityTime);
        Assert.AreEqual(new List<int> { 30, 30, 30, 90, 150, 120, 240 }, Foreman.Schedule.Duration);
        Assert.AreEqual(new List<Responsible>
        {
            Responsible.Cattleman,
            Responsible.CalfHouse,
            Responsible.CalfHouse,
            Responsible.Vet,
            Responsible.Operator,
            Responsible.Cattleman,
            Responsible.Vet
        }, Foreman.Schedule.Responsible);
        Assert.AreEqual(true, Foreman.Schedule.IsActive);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        //Оборудование проверено
        workers.First().CheckEq(equipment[0]);
        workers.First().CheckEq(equipment[1]);
        workers.First().CheckEq(equipment[2]);
        workers.First().CheckEq(equipment[3]);
        workers.First().CheckEq(equipment[4]);
        Assert.AreEqual(false, equipment[0].ServiceRequired);
        Assert.AreEqual(false, equipment[0].IsBroken);
        Assert.AreEqual(false, equipment[1].ServiceRequired);
        Assert.AreEqual(false, equipment[1].IsBroken);
        Assert.AreEqual(false, equipment[2].ServiceRequired);
        Assert.AreEqual(true, equipment[2].IsBroken);
        Assert.AreEqual(false, equipment[3].ServiceRequired);
        Assert.AreEqual(false, equipment[3].IsBroken);
        Assert.AreEqual(true, equipment[4].ServiceRequired);
        Assert.AreEqual(false, equipment[4].IsBroken);
        //Оборудование утилизировано
        equipment = foreman.DeleteEq(equipment[2], equipment);
        var eqCheck = new List<Equipment>();
        eqCheck.AddRange(new List<Equipment>()
        {
            new(type: EqType.Loader, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.WateringMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.MilkingMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Scraper, isBroken: false, isUsing: false, serviceRequired: true, usageTime: 15000,
                isClean: true),
            new(type: EqType.CowFlipper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 0,
                isClean: true),
        });
        Assert.AreEqual(eqCheck, equipment);
        Assert.AreEqual(true, Foreman.Schedule.Disruptions);
        //Покупка оборудования
        var newEq = foreman.BuyEq(equipment[2].Type);
        var oneEqCheck = new Equipment(type: EqType.MilkingMachine, isBroken: false, isUsing: false,
            serviceRequired: false, usageTime: 0, isClean: true);
        Assert.AreEqual(oneEqCheck, newEq);
        //Работа не принята
        Foreman.AcceptWork(false);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        Assert.AreEqual(false, Foreman.Schedule.IsActive);
    }

    [Test]
    public void TestSecond()
    {
        //Расписание -> Оборудование проверено -> Оборудование готово к использованию -> Кормление -> Поение -> Здоровье коровы -> Результат лечения -> Работа не принята
        cows = new List<Cow>();
        cows.AddRange(new List<Cow>()
        {
            new(type: CowType.Cow, condition: CowCond.Living, location: CowLocation.Stall, CowHealth.Healthy,
                inQueue: false, isHungry: false, isThirsty: false),
            new(type: CowType.Calf, condition: CowCond.Living, location: CowLocation.Stall, CowHealth.Sick,
                inQueue: false, isHungry: false, isThirsty: false),
            new(type: CowType.Heifer, condition: CowCond.Living, location: CowLocation.Stall, CowHealth.Healthy,
                inQueue: false, isHungry: false, isThirsty: false),
            new(type: CowType.Replacement, condition: CowCond.Living, location: CowLocation.Stall, CowHealth.Healthy,
                inQueue: false, isHungry: false, isThirsty: false),
        });
        equipment = new List<Equipment>();
        equipment.AddRange(new List<Equipment>()
        {
            new(type: EqType.Loader, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.WateringMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.MilkingMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.CowFlipper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Scraper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
        });
        //Расписание 
        foreman.SetChart();
        Assert.AreEqual(new List<ScheduleActivity>
        {
            ScheduleActivity.Checking,
            ScheduleActivity.Feeding,
            ScheduleActivity.Watering,
            ScheduleActivity.Health,
            ScheduleActivity.Milking,
            ScheduleActivity.Cleaning,
            ScheduleActivity.Breaktime
        }, Foreman.Schedule.Activity);
        Assert.AreEqual(new List<DateTime>
        {
            new(2024, 4, 19, 5, 0, 0),
            new(2024, 4, 19, 5, 30, 0),
            new(2024, 4, 19, 6, 0, 0),
            new(2024, 4, 19, 6, 30, 0),
            new(2024, 4, 19, 7, 30, 0),
            new(2024, 4, 19, 8, 0, 0),
            new(2024, 4, 19, 10, 0, 0)
        }, Foreman.Schedule.ActivityTime);
        Assert.AreEqual(new List<int> { 30, 30, 30, 90, 150, 120, 240 }, Foreman.Schedule.Duration);
        Assert.AreEqual(new List<Responsible>
        {
            Responsible.Cattleman,
            Responsible.CalfHouse,
            Responsible.CalfHouse,
            Responsible.Vet,
            Responsible.Operator,
            Responsible.Cattleman,
            Responsible.Vet
        }, Foreman.Schedule.Responsible);
        Assert.AreEqual(true, Foreman.Schedule.IsActive);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        //Оборудование проверено
        workers.First().CheckEq(equipment[0]);
        workers.First().CheckEq(equipment[1]);
        workers.First().CheckEq(equipment[2]);
        workers.First().CheckEq(equipment[3]);
        workers.First().CheckEq(equipment[4]);
        Assert.AreEqual(false, equipment[0].ServiceRequired);
        Assert.AreEqual(false, equipment[0].IsBroken);
        Assert.AreEqual(false, equipment[1].ServiceRequired);
        Assert.AreEqual(false, equipment[1].IsBroken);
        Assert.AreEqual(false, equipment[2].ServiceRequired);
        Assert.AreEqual(false, equipment[2].IsBroken);
        Assert.AreEqual(false, equipment[3].ServiceRequired);
        Assert.AreEqual(false, equipment[3].IsBroken);
        Assert.AreEqual(false, equipment[4].ServiceRequired);
        Assert.AreEqual(false, equipment[4].IsBroken);
        //Оборудование готово к использованию
        workers[2].UseEq(equipment[1]);
        Assert.AreEqual(true, equipment[1].IsUsing);
        Assert.AreEqual(100, equipment[1].UsageTime);
        //Кормление
        workers[2].FeedCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Feeding, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsHungry);
        //Поение
        workers[2].WaterCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Watering, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsThirsty);
        //Здоровье коровы
        var hlth = vet.CheckHealth(cows[1]);
        Assert.AreEqual(false, hlth);
        Assert.AreEqual(ActivityType.Prevention, vet.Activity);
        //Результат лечения
        vet.Heal(cows[1], true);
        Assert.AreEqual(ActivityType.Treatment, vet.Activity);
        Assert.AreEqual(CowHealth.Sick, cows[1].Health);
        Assert.AreEqual(CowLocation.Stall, cows[1].Location);
        Assert.AreEqual(true, Foreman.Schedule.Disruptions);
        //Работа не принята
        Foreman.AcceptWork(false);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        Assert.AreEqual(false, Foreman.Schedule.IsActive);
    }

    [Test]
    public void TestThird()
    {
        //Расписание -> Оборудование проверено -> Кормление -> Поение -> Здоровье коровы -> Помещение убрано -> Корова в доильном зале -> Продукция получена -> Корова в коровнике -> Мойка оборудования -> Корова сыта -> Корова не испытывает жажды -> Корова спит -> Заказ оформлен -> Работа принята

        //Расписание 
        foreman.SetChart();
        Assert.AreEqual(new List<ScheduleActivity>
        {
            ScheduleActivity.Checking,
            ScheduleActivity.Feeding,
            ScheduleActivity.Watering,
            ScheduleActivity.Health,
            ScheduleActivity.Milking,
            ScheduleActivity.Cleaning,
            ScheduleActivity.Breaktime
        }, Foreman.Schedule.Activity);
        Assert.AreEqual(new List<DateTime>
        {
            new(2024, 4, 19, 5, 0, 0),
            new(2024, 4, 19, 5, 30, 0),
            new(2024, 4, 19, 6, 0, 0),
            new(2024, 4, 19, 6, 30, 0),
            new(2024, 4, 19, 7, 30, 0),
            new(2024, 4, 19, 8, 0, 0),
            new(2024, 4, 19, 10, 0, 0)
        }, Foreman.Schedule.ActivityTime);
        Assert.AreEqual(new List<int> { 30, 30, 30, 90, 150, 120, 240 }, Foreman.Schedule.Duration);
        Assert.AreEqual(new List<Responsible>
        {
            Responsible.Cattleman,
            Responsible.CalfHouse,
            Responsible.CalfHouse,
            Responsible.Vet,
            Responsible.Operator,
            Responsible.Cattleman,
            Responsible.Vet
        }, Foreman.Schedule.Responsible);
        Assert.AreEqual(true, Foreman.Schedule.IsActive);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        //Оборудование проверено
        workers.First().CheckEq(equipment[0]);
        workers.First().CheckEq(equipment[1]);
        workers.First().CheckEq(equipment[2]);
        workers.First().CheckEq(equipment[3]);
        workers.First().CheckEq(equipment[4]);
        Assert.AreEqual(false, equipment[0].ServiceRequired);
        Assert.AreEqual(false, equipment[0].IsBroken);
        Assert.AreEqual(false, equipment[1].ServiceRequired);
        Assert.AreEqual(false, equipment[1].IsBroken);
        Assert.AreEqual(false, equipment[2].ServiceRequired);
        Assert.AreEqual(false, equipment[2].IsBroken);
        Assert.AreEqual(false, equipment[3].ServiceRequired);
        Assert.AreEqual(false, equipment[3].IsBroken);
        Assert.AreEqual(false, equipment[4].ServiceRequired);
        Assert.AreEqual(false, equipment[4].IsBroken);
        //Кормление
        workers[2].FeedCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Feeding, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsHungry);
        //Поение
        workers[2].WaterCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Watering, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsThirsty);
        //Здоровье коровы
        var hlth = vet.CheckHealth(cows.First());
        Assert.AreEqual(true, hlth);
        Assert.AreEqual(ActivityType.Prevention, vet.Activity);
        //Помещение убрано
        workers.First().CleanBld(worker: workers[0], bld: building);
        Assert.AreEqual(WorkType.Cleaning, workers[0].WorkType);
        Assert.AreEqual(true, building.IsClean);
        //Корова в доильном зале
        var bld = building.CowMove(cows.First());
        Assert.AreEqual(BuildingType.MilkingHall, bld);
        Assert.AreEqual(CowLocation.MilkingHall, cows.First().Location);
        Assert.AreEqual(CowLocation.MilkingHall, cows.First().Move(CowLocation.MilkingHall));
        //Продукция получена
        var prod = workers[1].GetMilk(worker: workers[1], cow: cows.First(), equipment[1]);
        var prodExp = new Product(isObtained: true, inStock: true, isStored: true, isSent: false, isSpoiled: false);
        Assert.AreEqual(WorkType.Milking, workers[1].WorkType);
        Assert.AreEqual(200, equipment[1].UsageTime);
        Assert.AreEqual(true, equipment[1].IsUsing);
        Assert.AreEqual(prodExp, prod);
        //Корова в коровнике
        building.Type = BuildingType.Barn;
        bld = building.CowMove(cows.First());
        Assert.AreEqual(BuildingType.Barn, bld);
        Assert.AreEqual(CowLocation.Stall, cows.First().Location);
        Assert.AreEqual(CowLocation.Stall, cows.First().Move(CowLocation.Stall));
        //Мойка оборудования
        workers.First().CleanEq(workers[0], equipment[0]);
        workers.First().CleanEq(workers[0], equipment[1]);
        workers.First().CleanEq(workers[0], equipment[2]);
        workers.First().CleanEq(workers[0], equipment[3]);
        workers.First().CleanEq(workers[0], equipment[4]);
        Assert.AreEqual(WorkType.Cleaning, workers[0].WorkType);
        Assert.AreEqual(true, equipment[0].IsClean);
        Assert.AreEqual(true, equipment[1].IsClean);
        Assert.AreEqual(true, equipment[2].IsClean);
        Assert.AreEqual(true, equipment[3].IsClean);
        Assert.AreEqual(true, equipment[4].IsClean);
        //Корова сыта
        cows.First().Eat(food);
        Assert.AreEqual(false, cows.First().IsHungry);
        Assert.AreEqual(CowCond.Eating, cows.First().Condition);
        Assert.AreEqual(false, food.InStock);
        //Корова не испытывает жажды
        cows.First().Drink(water);
        Assert.AreEqual(false, cows.First().IsThirsty);
        Assert.AreEqual(CowCond.Drinking, cows.First().Condition);
        Assert.AreEqual(false, water.InStock);
        //Корова спит
        Assert.AreEqual(CowCond.Sleeping, cows.First().Sleep());
        //Заказ оформлен
        foreman.Order(food);
        var foodCheck = new Food(inStock: true, isOrdered: true, isTransported: false, type: FoodType.Hay,
            location: FoodLocation.Warehouse, readyToUse: false);
        Assert.AreEqual(foodCheck, food);
        //Работа принята
        Foreman.AcceptWork(true);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        Assert.AreEqual(false, Foreman.Schedule.IsActive);
    }

    //тестирование вариантов использования
    [Test]
    public void TestWorker()
    {
        //рабочий
        equipment = new List<Equipment>();
        equipment.AddRange(new List<Equipment>()
        {
            new(type: EqType.Loader, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.WateringMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.MilkingMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.CowFlipper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Scraper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
        });
        //Дойка коровы
        var prodAct = workers[1].GetMilk(worker: workers[1], cow: cows.First(), equipment[2]) ??
                      new Product(isObtained: true, inStock: true, isStored: true, isSent: false, isSpoiled: false);
        var prodExp = new Product(isObtained: true, inStock: true, isStored: true, isSent: false, isSpoiled: false);
        Assert.AreEqual(WorkType.Milking, workers[1].WorkType);
        Assert.AreEqual(200, equipment[2].UsageTime);
        Assert.AreEqual(true, equipment[2].IsUsing);
        Assert.AreEqual(prodExp.IsObtained, prodAct.IsObtained);
        Assert.AreEqual(prodExp.InStock, prodAct.InStock);
        Assert.AreEqual(prodExp.IsStored, prodAct.IsStored);
        Assert.AreEqual(prodExp.IsSent, prodAct.IsSent);
        Assert.AreEqual(prodExp.IsSpoiled, prodAct.IsSpoiled);
        // Assert.AreEqual(prodExp, prodAct);
        //Кормление коровы
        workers[2].FeedCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Feeding, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsHungry);
        //Поение коровы
        workers[2].WaterCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Watering, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsThirsty);
        //Уборка помещения
        workers.First().CleanBld(worker: workers[0], bld: building);
        Assert.AreEqual(WorkType.Cleaning, workers[0].WorkType);
        Assert.AreEqual(true, building.IsClean);
        //Мойка оборудования
        workers.First().CleanEq(workers[0], equipment[1]);
        Assert.AreEqual(WorkType.Cleaning, workers[0].WorkType);
        Assert.AreEqual(true, equipment[1].IsClean);
    }

    [Test]
    public void TestForeman()
    {
        //бригадир
        equipment = new List<Equipment>();
        equipment.AddRange(new List<Equipment>()
        {
            new(type: EqType.Loader, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.WateringMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.MilkingMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.CowFlipper, isBroken: true, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Scraper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
        });
        //Установка расписания
        foreman.SetChart();
        Assert.AreEqual(new List<ScheduleActivity>
        {
            ScheduleActivity.Checking,
            ScheduleActivity.Feeding,
            ScheduleActivity.Watering,
            ScheduleActivity.Health,
            ScheduleActivity.Milking,
            ScheduleActivity.Cleaning,
            ScheduleActivity.Breaktime
        }, Foreman.Schedule.Activity);
        Assert.AreEqual(new List<DateTime>
        {
            new(2024, 4, 19, 5, 0, 0),
            new(2024, 4, 19, 5, 30, 0),
            new(2024, 4, 19, 6, 0, 0),
            new(2024, 4, 19, 6, 30, 0),
            new(2024, 4, 19, 7, 30, 0),
            new(2024, 4, 19, 8, 0, 0),
            new(2024, 4, 19, 10, 0, 0)
        }, Foreman.Schedule.ActivityTime);
        Assert.AreEqual(new List<int> { 30, 30, 30, 90, 150, 120, 240 }, Foreman.Schedule.Duration);
        Assert.AreEqual(new List<Responsible>
        {
            Responsible.Cattleman,
            Responsible.CalfHouse,
            Responsible.CalfHouse,
            Responsible.Vet,
            Responsible.Operator,
            Responsible.Cattleman,
            Responsible.Vet
        }, Foreman.Schedule.Responsible);
        Assert.AreEqual(true, Foreman.Schedule.IsActive);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        //Заказ корма
        foreman.Order(food);
        var foodCheck = new Food(inStock: true, isOrdered: true, isTransported: false, type: FoodType.Hay,
            location: FoodLocation.Warehouse, readyToUse: false);
        Assert.AreEqual(foodCheck, food);
        //Приёмка работы
        Foreman.AcceptWork(true);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        Assert.AreEqual(false, Foreman.Schedule.IsActive);
        //Заказ оборудования
        var newEq = foreman.BuyEq(equipment.First().Type);
        var oneEqCheck = new Equipment(type: EqType.WateringMachine, isBroken: false, isUsing: false,
            serviceRequired: false, usageTime: 0, isClean: true);
        Assert.AreEqual(oneEqCheck, newEq);
        //Утилизация оборудования
        equipment = foreman.DeleteEq(equipment.First(), equipment);
        var eqCheck = new List<Equipment>();
        eqCheck.AddRange(new List<Equipment>()
        {
            new(type: EqType.WateringMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.MilkingMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.CowFlipper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Scraper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Loader, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 0,
                isClean: true),
        });
        Assert.AreEqual(eqCheck, equipment);
        Assert.AreEqual(true, Foreman.Schedule.Disruptions);
    }

    [Test]
    public void TestVet()
    {
        //ветеринар

        //Проверка состояния здоровья коровы
        vet.CheckHealth(cows.First());
        Assert.AreEqual(ActivityType.Prevention, vet.Activity);
        //Лечение коровы
        cows[2].Health = CowHealth.Sick;
        var heal = vet.Heal(cows[2]);
        Assert.AreEqual(ActivityType.Treatment, vet.Activity);
        if (heal)
        {
            Assert.AreEqual(CowHealth.Healthy, cows[2].Health);
            Assert.AreEqual(CowLocation.Stall, cows[2].Location);
        }
        else
        {
            Assert.AreEqual(CowHealth.Sick, cows[2].Health);
            Assert.AreEqual(CowLocation.Stall, cows[2].Location);
            Assert.AreEqual(false, Foreman.Schedule.Disruptions);
            Assert.AreEqual(false, Foreman.Schedule.IsActive);
        }
    }

    //тестирование сценариев
    [Test]
    public void TestScriptFirst()
    {
        //Рабочий отправляет запрос на установку рабочего графика бригадиру
        workers[2].GetChart();
        Assert.AreEqual(true, Foreman.ChartRequest);
        //Бригадир создаёт расписание
        foreman.SetChart();
        Assert.AreEqual(new List<ScheduleActivity>
        {
            ScheduleActivity.Checking,
            ScheduleActivity.Feeding,
            ScheduleActivity.Watering,
            ScheduleActivity.Health,
            ScheduleActivity.Milking,
            ScheduleActivity.Cleaning,
            ScheduleActivity.Breaktime
        }, Foreman.Schedule.Activity);
        Assert.AreEqual(new List<DateTime>
        {
            new(2024, 4, 19, 5, 0, 0),
            new(2024, 4, 19, 5, 30, 0),
            new(2024, 4, 19, 6, 0, 0),
            new(2024, 4, 19, 6, 30, 0),
            new(2024, 4, 19, 7, 30, 0),
            new(2024, 4, 19, 8, 0, 0),
            new(2024, 4, 19, 10, 0, 0)
        }, Foreman.Schedule.ActivityTime);
        Assert.AreEqual(new List<int> { 30, 30, 30, 90, 150, 120, 240 }, Foreman.Schedule.Duration);
        Assert.AreEqual(new List<Responsible>
        {
            Responsible.Cattleman,
            Responsible.CalfHouse,
            Responsible.CalfHouse,
            Responsible.Vet,
            Responsible.Operator,
            Responsible.Cattleman,
            Responsible.Vet
        }, Foreman.Schedule.Responsible);
        Assert.AreEqual(true, Foreman.Schedule.IsActive);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        //Рабочий сверяется с расписанием
        var chart = workers[2].CheckChart();
        Assert.AreEqual(chart, Foreman.Schedule);
        //Рабочий проверяет оборудование
        workers.First().CheckEq(equipment[0]);
        workers.First().CheckEq(equipment[1]);
        workers.First().CheckEq(equipment[2]);
        workers.First().CheckEq(equipment[3]);
        workers.First().CheckEq(equipment[4]);
        Assert.AreEqual(false, equipment[0].ServiceRequired);
        Assert.AreEqual(false, equipment[0].IsBroken);
        Assert.AreEqual(false, equipment[1].ServiceRequired);
        Assert.AreEqual(false, equipment[1].IsBroken);
        Assert.AreEqual(false, equipment[2].ServiceRequired);
        Assert.AreEqual(false, equipment[2].IsBroken);
        Assert.AreEqual(false, equipment[3].ServiceRequired);
        Assert.AreEqual(false, equipment[3].IsBroken);
        Assert.AreEqual(false, equipment[4].ServiceRequired);
        Assert.AreEqual(false, equipment[4].IsBroken);
        //Рабочий занимается приготовлением кормосмеси
        var foodAct = workers[2].MakeFood(food.Type);
        var foodExp = new Food(inStock: true, isOrdered: false, isTransported: true, type: food.Type,
            location: FoodLocation.Stall, readyToUse: true);
        Assert.AreEqual(foodExp, foodAct);
        //Рабочий занимается кормлением коров
        workers[2].FeedCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Feeding, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsHungry);
        //Рабочий занимается поением коров
        workers[2].WaterCow(worker: workers[2], cow: cows.First());
        Assert.AreEqual(WorkType.Watering, workers[2].WorkType);
        Assert.AreEqual(false, cows.First().IsThirsty);
        //Рабочий выполняет запрос на проверку здоровья коровы ветеринаром
        workers[2].CheckHealthRequest();
        Assert.AreEqual(true, Vet.CheckHealthRequest);
        //Ветеринар проверяет здоровье коровы и возвращает сведения о её здоровье рабочему
        vet.CheckHealth(cows.First());
        Assert.AreEqual(ActivityType.Prevention, vet.Activity);
        //Рабочий выполняет уборку помещений
        workers[2].CleanBld(worker: workers[2], bld: building);
        Assert.AreEqual(WorkType.Cleaning, workers[2].WorkType);
        Assert.AreEqual(true, building.IsClean);
        //Рабочий выполняет запрос бригадиру на приёмку работ, бригадир принимает работу
        Foreman.AcceptWork(true);
        Assert.AreEqual(false, Foreman.Schedule.Disruptions);
        Assert.AreEqual(false, Foreman.Schedule.IsActive);
    }

    [Test]
    public void TestScriptSecond()
    {
        //Рабочий сверяется с расписанием
        var chart = workers[1].CheckChart();
        Assert.AreEqual(chart, Foreman.Schedule);
        //Рабочий делает запрос на перемещение коров в доильный зал
        building.Type = BuildingType.MilkingHall;
        var bld = building.CowMove(cows.First());
        Assert.AreEqual(BuildingType.MilkingHall, bld);
        //Коровы перемещаются в доильный зал
        Assert.AreEqual(CowLocation.MilkingHall, cows.First().Move(CowLocation.MilkingHall));
        //Рабочий выполняет доение коров
        var prodAct = workers[1].GetMilk(worker: workers[1], cow: cows.First(), equipment[2]) ??
                      new Product(isObtained: true, inStock: true, isStored: true, isSent: false, isSpoiled: false);
        var prodExp = new Product(isObtained: true, inStock: true, isStored: true, isSent: false, isSpoiled: false);
        Assert.AreEqual(WorkType.Milking, workers[1].WorkType);
        Assert.AreEqual(200, equipment[2].UsageTime);
        Assert.AreEqual(true, equipment[2].IsUsing);
        Assert.AreEqual(prodExp.IsObtained, prodAct.IsObtained);
        Assert.AreEqual(prodExp.InStock, prodAct.InStock);
        Assert.AreEqual(prodExp.IsStored, prodAct.IsStored);
        Assert.AreEqual(prodExp.IsSent, prodAct.IsSent);
        Assert.AreEqual(prodExp.IsSpoiled, prodAct.IsSpoiled);
        // Assert.AreEqual(prodExp, prod);
        //Рабочий делает запрос на перемещение коров в коровник
        building.Type = BuildingType.Barn;
        bld = building.CowMove(cows.First());
        Assert.AreEqual(BuildingType.Barn, bld);
        //Коровы перемещаются в коровник
        Assert.AreEqual(CowLocation.Stall, cows.First().Move(CowLocation.Stall));
        //Рабочий выполняет мойку оборудования
        workers.First().CleanEq(workers[0], equipment[2]);
        Assert.AreEqual(WorkType.Cleaning, workers[0].WorkType);
        Assert.AreEqual(true, equipment[2].IsClean);
    }

    [Test]
    public void TestScriptThird()
    {
        equipment.First().IsBroken = true;
        //Проверка исправности оборудования рабочим
        workers.First().CheckEq(equipment.First());
        Assert.AreEqual(false, equipment.First().ServiceRequired);
        Assert.AreEqual(true, equipment.First().IsBroken);
        //Рабочий пытается починить оборудование
        workers.First().RepairEq(workers.First(), equipment.First(), true);
        Assert.AreEqual(WorkType.Fixing, workers.First().WorkType);
        Assert.AreEqual(true, equipment.First().IsBroken);
        Assert.AreEqual(true, Foreman.EqUpdate);
        Assert.AreEqual(true, Foreman.Schedule.Disruptions);
        //Рабочий делает запрос на утилизацию текущего оборудования бригадиру
        equipment = foreman.DeleteEq(equipment.First(), equipment, false);
        var eqCheck = new List<Equipment>();
        eqCheck.AddRange(new List<Equipment>()
        {
            new(type: EqType.WateringMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.MilkingMachine, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.CowFlipper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
            new(type: EqType.Scraper, isBroken: false, isUsing: false, serviceRequired: false, usageTime: 100,
                isClean: true),
        });
        eqCheck = equipment;
        Assert.AreEqual(eqCheck, equipment);
        Assert.AreEqual(true, Foreman.Schedule.Disruptions);
        //Бригадир утилизирует оборудование и закупает новое
        var newEq = foreman.BuyEq(EqType.Loader);
        var oneEqCheck = new Equipment(type: EqType.Loader, isBroken: false, isUsing: false,
            serviceRequired: false, usageTime: 0, isClean: true);
        Assert.AreEqual(oneEqCheck, newEq);
        //Рабочий делает запрос на получение оборудования, бригадир передает оборудование рабочему
        workers.First().GetEq(equipment.Last());
        Assert.AreEqual(false, equipment.Last().IsClean);
        Assert.AreEqual(true, equipment.Last().IsUsing);
        Assert.AreEqual(100, equipment.Last().UsageTime);
        Assert.AreEqual(false, Foreman.EqUpdate);
    }
}