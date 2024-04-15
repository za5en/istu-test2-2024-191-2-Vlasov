using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    public class Worker
    {
        public Worker()
        {
            throw new System.NotImplementedException();
        }

        public bool is_working
        {
            get => default;
            set
            {
            }
        }

        public bool is_working_day
        {
            get => default;
            set
            {
            }
        }

        public bool is_healthy
        {
            get => default;
            set
            {
            }
        }

        public string work_type
        {
            get => default;
            set
            {
            }
        }

        public string location
        {
            get => default;
            set
            {
            }
        }

        public string post
        {
            get => default;
            set
            {
            }
        }

        public bool workCondChange()
        {
            throw new System.NotImplementedException();
        }

        public bool healthChange()
        {
            throw new System.NotImplementedException();
        }

        public void getMilk(Cow cow)
        {
            throw new System.NotImplementedException();
        }

        public bool feedCow(string cow, Food food)
        {
            throw new System.NotImplementedException();
        }

        public bool waterCow(Cow cow, string water)
        {
            throw new System.NotImplementedException();
        }

        public bool cleanBld(Building bld, Water water)
        {
            throw new System.NotImplementedException();
        }

        public bool cleanEq(Equipment eq, string water)
        {
            throw new System.NotImplementedException();
        }

        public bool useEq(string eq)
        {
            throw new System.NotImplementedException();
        }

        public void checkEq(string eq)
        {
            throw new System.NotImplementedException();
        }

        public void repairEq(string eq)
        {
            throw new System.NotImplementedException();
        }
    }
}