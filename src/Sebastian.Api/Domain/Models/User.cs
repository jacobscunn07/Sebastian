﻿using System;
using System.Collections.Generic;

namespace Sebastian.Api.Domain.Models
{
    public class User
    {
        public User()
        {
            Workouts = new List<Workout>();
        }

        public Guid Id { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public List<Workout> Workouts { get; set; }
    }
}