﻿using System;

namespace Mofleet.Domain.TimeWorks.Dtos
{
    public class TimeOfWorkDto
    {
        public DayOfWeek Day { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
    }
}
