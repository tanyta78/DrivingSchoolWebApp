﻿namespace DrivingSchoolWebApp.Services.Models.Lesson
{
    using System;
    using Data.Models;
    using Mapping;

    public class DetailsLessonViewModel : IMapFrom<Lesson>
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime StartTime { get; set; }

        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }

        public string Description { get; set; }

    }
}