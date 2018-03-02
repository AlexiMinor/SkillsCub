using System;
using System.Collections.Generic;

namespace SkillsCub.DataLibrary.Entities.Implementation
{
    public class Excercise
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string ConditionOfProblem { get; set; }

        public DateTime CreationDate { get; set; }


        public IEnumerable<CourseExcercise> CourseExcercises { get; set; }


    }
}