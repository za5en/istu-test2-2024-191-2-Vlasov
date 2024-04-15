using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    public class Schedule
    {
        public Schedule()
        {
            throw new System.NotImplementedException();
        }

        public string activity
        {
            get => default;
            set
            {
            }
        }

        public System.DateTime activity_time
        {
            get => default;
            set
            {
            }
        }

        public int duration
        {
            get => default;
            set
            {
            }
        }

        public string responsible
        {
            get => default;
            set
            {
            }
        }

        public bool is_active
        {
            get => default;
            set
            {
            }
        }

        public bool disruptions
        {
            get => default;
            set
            {
            }
        }

        public bool activeChange()
        {
            throw new System.NotImplementedException();
        }

        public bool disrupted()
        {
            throw new System.NotImplementedException();
        }

        public bool start()
        {
            throw new System.NotImplementedException();
        }

        public bool stop()
        {
            throw new System.NotImplementedException();
        }
    }
}