using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.EntityFramework.Update
{
    public static class UpdateHandler
    {
        //Update author
        public static void updateAuthor(int AuthorId, Author updatedAuthor)
        {
            using (NewtonLibraryContext db = new NewtonLibraryContext())
            {
                //Retrieve author object form DB with help from id input
                Author author = db.Authors.Find(AuthorId);

                //Throw exception if author object returned NULL
                if (author == null)
                {
                    throw new ArgumentNullException("Could not find author");
                }


                //Set old authorobject to new authorobject
                author = updatedAuthor;

                //Save changes, return true if properties are changed else return false
                try
                {
                    db.SaveChanges();
                } catch (Exception ex)
                {
                    throw new Exception("Could not update database");
                }
            }
        }
    }
}
