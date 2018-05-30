using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SkillsCub.Core;

namespace SkillsCub.MVC.ViewModels
{
    public class ExerciseModelRequest
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public DateTime TimeToOpen { get; set; }
        public DateTime TimeToClose { get; set; }
        public string Detail { get; set; }
        public IEnumerable<AttachedFile> AttachedFiles { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
  