using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.PaymentProviders;

namespace MovieRental.Rental
{
    public class RentalFeatures(
        MovieRentalDbContext movieRentalDb,
        IEnumerable<IPaymentProvider> paymentProviders) : IRentalFeatures
    {
        public async Task<Rental> SaveAsync(Rental rental)
        {
            await using var transaction = await movieRentalDb.Database.BeginTransactionAsync();

            try
            {
                const double FeePerDay = 25.55; // value read from configuration or database
                double amountToPay = FeePerDay * rental.DaysRented;

                IPaymentProvider? payProvider = paymentProviders.FirstOrDefault(pp => pp.Name == rental.PaymentMethod);

                bool sucess =
                    payProvider is not null &&
                    amountToPay > 0 &&
                    await payProvider.Pay(amountToPay);

                if (sucess)
                {
                    await movieRentalDb.Rentals.AddAsync(rental);
                    await movieRentalDb.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else await transaction.RollbackAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;  // Always rethrow so the caller knows something failed
            }

            return rental;
        }

        public async Task<IEnumerable<Rental>> GetRentalsByCustomerNameAsync(string customerName)
        {
            // Preferably, when supported by the database collation we could use TSQL LIKE:
            // EF.Functions.Like(r.Customer.Name, $"%{customerName}%")
            // bellow solution is not ideal for performance, but works everywhere
            var query = movieRentalDb.Rentals
                .AsNoTracking()
                .Where(r => r.Customer != null &&
                            r.Customer.Name.ToLower().Contains(customerName.ToLower()))
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                ;

            return await query.ToListAsync();
        }
    }
}
