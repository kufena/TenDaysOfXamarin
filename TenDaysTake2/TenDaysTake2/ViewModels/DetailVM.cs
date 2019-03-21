using System;
using System.Collections.Generic;
using System.Text;
using TenDaysTake2.Models;

namespace TenDaysTake2.ViewModels
{
    class DetailVM
    {

        public Experience selectedExperience { get; set; }

        public DetailVM()
        {
            Console.WriteLine("detail vm create");
        }
    }
}
