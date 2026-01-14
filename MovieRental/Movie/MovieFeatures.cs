using MovieRental.Data;

namespace MovieRental.Movie
{
	public class MovieFeatures : IMovieFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		public MovieFeatures(MovieRentalDbContext movieRentalDb)
		{
			_movieRentalDb = movieRentalDb;
		}
		
		public Movie Save(Movie movie)
		{
			_movieRentalDb.Movies.Add(movie);
			_movieRentalDb.SaveChanges();
			return movie;
		}

        // TODO: tell us what is wrong in this method? Forget about the async, what other concerns do you have?
        // 1. Lack of error handling: The method does not handle potential exceptions that may arise during database operations.
        // 2. Performance issues and Scalability concerns:
        //		If the Movies table is large, loading all records into memory could lead to performance degradation.
        //		Including some filtering criteria would be more practical. Like pagination or specific search parameters.
        // 3. Little value in returning all records at once. Can't think of a real worl example where this is useful. At least not without pagination.
        //	  Instead of having a method that retrieves all movies, consider implementing multiple methods that allow for querying based on certain criteria.
        // 4. Returning a List<Movie> directly may not be the best practice. Consider returning an IEnumerable<Movie> or IQueryable<Movie> for better flexibility.
        public IEnumerable<Movie> GetAll()
		{
			return _movieRentalDb.Movies.ToList();
		}
	}
}
