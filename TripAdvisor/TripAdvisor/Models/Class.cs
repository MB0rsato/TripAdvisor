﻿namespace TripAdvisor.Models
{
    public class Class
    {
        public string classid { get; set; }
        public string specialization { get; set; }

        public override string ToString()
        {
            return classid + " " + specialization;
        }
    }
}
