# MovieRental Exercise

This is a dummy representation of a movie rental system.
Can you help us fix some issues and implement missing features?

 * The app is throwing an error when we start, please help us. Also, tell us what caused the issue.
 + Changed line 15 in Program.cs from AddSingleton to AddScoped because DbContext should have a scoped lifetime.
   A Scoped service can depend on a singleton, the opposite cannot happen.

 * The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
 + An async method allows the operation to run without blocking the calling thread while it waits for I/O (like saving to a database).
   This improves scalability because the thread can handle other requests during the wait.

 * Please finish the method to filter rentals by customer name, and add the new endpoint.
 + The solution applied is not ideal, but it works everywhere.
   Preferably, when supported by the database collation we could use TSQL LIKE: EF.Functions.Like(r.Customer.Name, $"%{customerName}%")

 * We noticed we do not have a table for customers, it is not good to have just the customer name in the rental.
   Can you help us add a new entity for this? Don't forget to change the customer name field to a foreign key, and fix your previous method!
 + Created a Customer class.

 * In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
 + A few observations:
    1. Performance issues and scalability concerns:
       If the Movies table is large, loading all records into memory could lead to performance degradation.
       Including some filtering criteria would be more practical. Like pagination or specific search parameters.
    2. Little value in returning all records at once. Can't think of a real worl example where this is useful. At least not without pagination.
       Instead of having a method that retrieves all movies, consider implementing multiple methods that allow for querying based on certain criteria.
    3. Returning a List<Movie> directly may not be the best practice. Consider returning an IEnumerable<Movie> or IQueryable<Movie> for better flexibility.

 * No exceptions are being caught in this api, how would you deal with these exceptions?
 + Usually I add a global exception handler in Program.cs (ErrorController.cs)
   Also, depending on the project, I might create some custom exceptions and/or add try catch blocks on methods that execute/handle outside systems (ex APIs, databases, etc).


## Challenge (Nice to have)
We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
    * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
* RentalFeatures Class:
    * Within the RentalFeatures class, you are required to implement the payment processing functionality.
* Payment Provider Designation:
    * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
* Extensibility:
    * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
* Payment Failure Handling:
    * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.

+ Please see IPaymentProvider interface and its usage on RentalFeatures.cs


