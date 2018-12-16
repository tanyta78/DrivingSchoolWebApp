namespace DrivingSchoolWebApp.Services.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Contracts;
    using Data.Common;
    using Data.Models;
    using Data.Models.Enums;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Models.Payment;

    public class PaymentService : BaseService, IPaymentService
    {
        private readonly IRepository<Payment> paymentRepository;

        public PaymentService(IRepository<Payment> paymentRepository,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper) : base(userManager, signInManager, mapper)
        {
            this.paymentRepository = paymentRepository;
        }

        public IEnumerable<Payment> All()
        {
            return this.paymentRepository.All().ToList();
        }

        public TViewModel GetPaymentById<TViewModel>(int paymentId)
        {
            var payment = this.paymentRepository.All().Where(x => x.Id == paymentId)
                             .To<TViewModel>().FirstOrDefault();

            if (payment == null)
            {
                throw new ArgumentException("No course with id in db");
            }

            return payment;
        }

        public IEnumerable<TViewModel> GetPaymentsByCustomerId<TViewModel>(int customerId)
        {
            var payments = this.paymentRepository.All().Where(x => x.Order.CustomerId== customerId).ProjectTo<TViewModel>().ToList();
            
            return payments;
        }

        public IEnumerable<TViewModel> GetPaymentsByOrderId<TViewModel>(int orderId)
        {
            var payments = this.paymentRepository.All().Where(x => x.OrderId== orderId).ProjectTo<TViewModel>().ToList();
            
            return payments;
        }

        public IEnumerable<TViewModel> GetPaymentsBySchoolId<TViewModel>(int schoolId)
        {
            var payments = this.paymentRepository.All().Where(x => x.Order.Course.SchoolId== schoolId).ProjectTo<TViewModel>().ToList();
            
            return payments;
        }

        public IEnumerable<TViewModel> GetPaymentsByMethod<TViewModel>(PaymentMethod method)
        {
            var payments = this.paymentRepository.All().Where(x => x.PaymentMethod== method).ProjectTo<TViewModel>().ToList();
            
            return payments;
        }

        public async Task<Payment> Create(CreatePaymentInputModel model)
        {
            var payment = this.Mapper.Map<Payment>(model);
            
            await this.paymentRepository.AddAsync(payment);
            await this.paymentRepository.SaveChangesAsync();

            return payment;
        }
    }
}
