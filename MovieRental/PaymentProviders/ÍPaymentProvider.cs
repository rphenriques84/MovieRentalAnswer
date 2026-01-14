namespace MovieRental.PaymentProviders
{
    public interface IPaymentProvider
    {
        /// <summary>
        /// Logical provider name used to match against Rental.PaymentMethod (case-insensitive).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Attempts to pay the specified amount. Return true on success, false on declined payment.
        /// Throw on infrastructure/transport errors.
        /// </summary>
        Task<bool> Pay(double amount);
    }
}
