namespace DrivingSchoolWebApp.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum AgeGroup
    {
        [Display(Name = "Teenager (13 - 17)")]
        Teenager = 1,
        [Display(Name = "YoungAdult (18 - 20)")]
        YoungAdult = 2,
        [Display(Name = "Adult(21 - 39)")]
        Adult = 3,
        [Display(Name = "Young Middle-Aged Adult (40 - 49)")]
        YoungMAdult = 4,
        [Display(Name = "Middle-Aged Adult (50 - 54)")]
        MiddleAgedAdult = 5,
        [Display(Name = "Very Young Senior Citizen (55 - 64)")]
        VSeniorCitizen = 6,
        [Display(Name = "Young Senior Citizen (65 - 74)")]
        YoungSeniorCitizen = 7,
        [Display(Name = "Senior Citizen (75 - 84)")]
        SeniorCitizen = 8,
        [Display(Name = "Old Senior Citizen (85+)")]
        OldSeniorCitizen = 9
    }
}
