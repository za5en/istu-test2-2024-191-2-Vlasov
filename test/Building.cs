using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    public class Building
    {
        public Building()
        {
            throw new System.NotImplementedException();
        }

        public string type
        {
            get => default;
            set
            {
            }
        }

        public bool service_required
        {
            get => default;
            set
            {
            }
        }

        public bool is_clean
        {
            get => default;
            set
            {
            }
        }

        public bool servicePointCheck()
        {
            throw new System.NotImplementedException();
        }

        public string cowEnter(Cow cow)
        {
            throw new System.NotImplementedException();
        }

        public string cowOut(string cow)
        {
            throw new System.NotImplementedException();
        }
    }
}