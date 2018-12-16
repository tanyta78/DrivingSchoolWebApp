namespace DrivingSchoolWebApp.Data.Models.Enums
{
    public enum OrderStatus
    {
        AwaitingPayment = 1,
        PaymentReceived = 2,
        PaymentUpdated = 3,
        Completed = 4,
        Cancelled = 5,
        Refunded = 6,
        Expired = 7
    }
}