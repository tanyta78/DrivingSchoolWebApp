﻿namespace DrivingSchoolWebApp.Services.Models.Feedback
{
    using Data.Models;
    using Mapping;

    public class CreateFeedbackInputModel:IMapFrom<Feedback>
    {
        public int CustomerId { get; set; }

        public int CourseId { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}
