namespace DrivingSchoolWebApp.Services.Models
{
    using Account;
    using AutoMapper;
    using Car;
    using Data.Models;


    public class AppProfile:Profile
    {
        public AppProfile()
		{
            this.CreateMap<RegisterViewModel, AppUser>();

		    this.CreateMap<Data.Models.Car,CarDetailsViewModel>().ForMember(dest=>dest.Id,opt=>opt.MapFrom(s=>s.Id));
		}
    }
}
