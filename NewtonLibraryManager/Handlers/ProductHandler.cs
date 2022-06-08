using Microsoft.EntityFrameworkCore;
using NewtonLibraryManager.Models;

namespace NewtonLibraryManager.Handlers;

public static class ProductHandler
{
    /// <summary>
    /// Method to borrow a product again.
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool ReBorrowProduct(int userID, int productId)
    {
        //int userId = AccountHandler.CurrentIdLoggedIn;
        try
        {
            ReturnProduct(productId);
            BorrowProduct(userID, productId);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Should be called when a user returns a product. It sets the return date in the lending details database.
    /// </summary>
    /// <param name="product">Product</param>
    /// <param name="authors">List of authors.</param>
    /// <returns>Returns true if everything went ok. Error otherwise.</returns>
    public static bool UpdateProduct(Product product, List<Author> authors)
    {
        Console.WriteLine();
        Console.WriteLine("---Product inside UpdateProduct---");
        Console.WriteLine("product.Title: " + product.Title);
        Console.WriteLine("product.LanguageId: " + product.LanguageId);
        Console.WriteLine("product.CategoryId: " + product.CategoryId);
        Console.WriteLine("product.NrOfCopies: " + product.NrOfCopies);
        Console.WriteLine("product.Dewey: " + product.Dewey);
        Console.WriteLine("product.Description: " + product.Description);
        Console.WriteLine("product.Isbn: " + product.Isbn);
        Console.WriteLine("product.ProductType: " + product.ProductType);
        Console.WriteLine();

        //Prepare list of authorIds to be used when creating Authordettails.
        List<int> authorIds = new();

        //Save authors from database.
        var authorList = EntityFramework.Read.ReadHandler.GetAuthors();

        //Go through authors from input.
        //Check if they exists by comparing every author from database.
        //If author exists, use author id from "list of authors from database" and add it to id-list.
        //If it doesnt exist, create new author and save that new new Id and add to id-list.
        authors.ForEach(x =>
        {
            var authorExists = false;
            authorList.ForEach(d =>
            {
                if (x.FirstName == d.FirstName && x.LastName == d.LastName)
                {
                    authorIds.Add(d.Id);
                    authorExists = true;
                }
            });
            if (!authorExists)
            {
                var authorId = EntityFramework.Create.CreateHandler.CreateAuthor(x.FirstName, x.LastName);
                authorIds.Add(authorId);
            }
        });

        Console.WriteLine("---authorIds in UpdateProduct---");
        foreach (var i in authorIds)
        {
            Console.WriteLine("Id: " + i);
        }
        Console.WriteLine();


        //If everything is ok and no exceptions has been thrown,
        //proceed with updating product and authordetail
        try
        {

            try
            {
                EntityFramework.Delete.DeleteHandler.DeleteAuthorDetail(product.Id);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed EntityFramework.Delete.DeleteHandler.DeleteAuthorDetail");
                return false;
            }

            try
            {
                EntityFramework.Update.UpdateHandler.UpdateProduct(product);
            }
            catch(Exception)
            {
                Console.WriteLine("Failed EntityFramework.Update.UpdateHandler.UpdateProduct");
                return false;
            }

            foreach(var a in authorIds)
            {
                try
                {
                    EntityFramework.Create.CreateHandler.CreateAuthorDetail(a, product.Id);
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed EntityFramework.Create.CreateHandler.CreateAuthorDetail for Author id " + a + " and product.Id " + product.Id);
                    return false;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Should be called when a user returns a product. It sets the return date in the lending details database.
    /// </summary>
    /// <param name="prodId">Product Id</param>
    /// <param name="userId">User Id, optional.</param>
    /// <returns>Returns true if everything went ok. False with an error otherwise.</returns>
    public static bool ReturnProduct(int prodId)
    {
        try
        {
            var lendingDetails = EntityFramework.Read.ReadHandler.GetLendingDetails();
            var ld = lendingDetails.Where(x =>
                x.ProductId == prodId && x.UserId == AccountHandler.CurrentIdLoggedIn).ToList();

            ld.ForEach(x =>
            {
                if (x.ReturnDate == null)
                {
                    x.ReturnDate = DateTime.Now;
                    EntityFramework.Update.UpdateHandler.UpdateLendingDetails(x);
                }
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Error~{ex.Message}");
        }

        return true;
    }

    /// <summary>
    /// A method to cancel a reservation
    /// </summary>
    /// <param name="prodId"></param>
    /// <returns></returns>
    public static bool CancelReservation(int prodId)
    {
        var reservationDetails = EntityFramework.Read.ReadHandler.GetReservationDetails();
        var rd = reservationDetails.FirstOrDefault(x => x.ProductId == prodId && x.UserId
        == AccountHandler.CurrentIdLoggedIn);
        if (rd == null) return false;
        
        try
        {
            EntityFramework.Delete.DeleteHandler.DeleteReservationDetail(rd.Id);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;

    }

    /// <summary>
    /// Deletes a product from the database, based on the product ID. Returns true if successful, false otherwise.
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static bool DeleteProduct(int productId)
    {
        if (AccountHandler.AdminLoggedIn)
        {
            try
            {
                EntityFramework.Delete.DeleteHandler.DeleteAuthorDetail(productId);
                EntityFramework.Delete.DeleteHandler.DeleteProduct(productId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } else
        {
            throw new Exception("Admin not logged in");
        }
        return false;
    }

    /// <summary>
    /// Should be called when a product is borrowed. Creates a lending detail based on user and product ID, with the
    /// current date and time. Returns true if all is ok, false otherwise.
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static bool BorrowProduct(int userid, int productId)
    {
        try
        {
            EntityFramework.Create.CreateHandler.CreateLendingDetail(userid, DateTime.Now, DateTime.Now.AddMonths(1), productId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
    /// <summary>
    /// Creates a reservation detail based on user and product ID. Returns true if all is ok, throws exception and
    /// returns false otherwise.
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public static bool ReserveProduct(int userid, int productId)
    {
        try
        {
            EntityFramework.Create.CreateHandler.CreateReservationDetail(userid, DateTime.Now, productId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    /// <summary>
    /// Gets all products in the database, along with their languages, categories, authors, and types.
    /// </summary>
    /// <returns>Returns a List<DisplayProductModel> with all products in the database.</returns>

    public static List<DisplayProductModel> ListAllProducts()
    {
        using (var db = new NewtonLibraryContext())
        {
            var queryable = from product in db.Products
                            join ad in db.AuthorDetails on product.Id equals ad.ProductId
                            join language in db.Languages on product.LanguageId equals language.Id
                            join category in db.Categories on product.CategoryId equals category.Id
                            join author in db.Authors on ad.AuthorId equals author.Id
                            join type in db.Types on product.ProductType equals type.Id
                            select new DisplayProductModel
                            {
                                Id = product.Id,
                                Title = product.Title,
                                FirstName = author.FirstName,
                                LastName = author.LastName,
                                Language = language.Language1,
                                Category = category.Category1,
                                NrOfCopies = product.NrOfCopies,
                                Dewey = product.Dewey,
                                Description = product.Description,
                                Isbn = product.Isbn,
                                ProductType = type.Type1
                            };
            queryable = queryable.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
            var list = queryable.ToList();
            List<DisplayProductModel> filteredList = new();

            list.ForEach(x =>
            {

                int found = 0;
                filteredList.ForEach(v =>
                {
                    if (x.Id == v.Id)
                    {
                        found = 1;
                    }
                });

                if (found == 0)
                {
                    filteredList.Add(x);
                }
            });
            return filteredList;
        }
    }

    /// <summary>
    /// Creates a model for a display product on the front end. The model can be used as a normal class.
    /// </summary>
    /// <param name="urlId"></param>
    /// <returns></returns>
    public static List<DisplayProductModel> ShowProduct(string urlId)
    {
        using (var db = new NewtonLibraryContext())
        {
            var id = int.Parse(urlId);
            var queryable = from product in db.Products
                            join ad in db.AuthorDetails on product.Id equals ad.ProductId
                            join language in db.Languages on product.LanguageId equals language.Id
                            join category in db.Categories on product.CategoryId equals category.Id
                            join author in db.Authors on ad.AuthorId equals author.Id
                            join type in db.Types on product.ProductType equals type.Id
                            where product.Id == id
                            select new DisplayProductModel
                            {
                                Id = id,
                                Title = product.Title,
                                FirstName = author.FirstName,
                                LastName = author.LastName,
                                Language = language.Language1,
                                Category = category.Category1,
                                NrOfCopies = product.NrOfCopies,
                                Dewey = product.Dewey,
                                Description = product.Description,
                                Isbn = product.Isbn,
                                ProductType = type.Type1

                            };

            return queryable.ToList();
        }
    }

    /// <summary>
    /// Returns a product ID based off of a product ISBN. Checks for nulls before returning.
    /// </summary>
    /// <param name="isbn"></param>
    /// <returns>Id of the product. Returns 0 if the product doesn't exist.</returns>
    public static int GetProductIdFromIsbn(string isbn)
    {
        var list = EntityFramework.Read.ReadHandler.GetProducts()
            .Where(x => x.Isbn == isbn).ToList();
        var prod = list.FirstOrDefault();

        if (prod != null)
            return prod.Id;

        Console.WriteLine("Did not find that isbn in the database.");
        return 0;
    }

    /// <summary>
    /// Check if a LendingDetail exist between a specific user and a specific product
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="productId"></param>
    /// <returns>True if a LendingDetail exists, False if not.</returns>
    public static bool HasLendingDetail(int userId, int productId)
    {
        var list = EntityFramework.Read.ReadHandler.GetLendingDetails()
            .Where(ld => (ld.UserId == userId && ld.ProductId == productId && ld.ReturnDate == null))
            .ToList();

        return (list.Count > 0);
    }

    /// <summary>
    /// Check if a ReservationDetail exist between a specific user and a specific product
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="productId"></param>
    /// <returns>True if a ReservationDetail exists, False if not.</returns>
    public static bool HasCommonReservationDetail(int userId, int productId)
    {
        var list = EntityFramework.Read.ReadHandler.GetReservationDetails()
            .Where(ld => (ld.UserId == userId && ld.ProductId == productId))
            .ToList();

        return (list.Count > 0);
    }

    /// <summary>
    /// Check if any ReservationDetail exist for a specific product
    /// </summary>
    /// <param name="productId"></param>
    /// <returns>True if a ReservationDetail exists, False if not.</returns>
    public static bool HasAnyReservationDetail(int productId)
    {
        var list = EntityFramework.Read.ReadHandler.GetReservationDetails()
            .Where(ld => ld.ProductId == productId)
            .ToList();

        return (list.Count > 0);
    }

    /// <summary>
    /// Checks which user is first in line among everyone who is reserving a product
    /// </summary>
    /// <param name="productId">Id of the product for which reservations are checked</param>
    /// <returns>Id of the user first in line, or 0 if there is no queue.</returns>
    public static int GetFirstReservationIdInQueue(int productId)
    {
        var list = EntityFramework.Read.ReadHandler.GetReservationDetails()
        .Where(ld => ld.ProductId == productId)
        .OrderBy(ld => ld.ReservationDate)
        .ToList();

        return list[0].UserId ?? 0;
    }


    /// <summary>
    /// Checks whether a user is in the reservation queue for a product. 
    /// </summary>
    /// <param name="productId">Id of the product being checked</param>
    /// <param name="userId">Id of the user being checked</param>
    /// <param name="queuePosition">Out parameter for position in queue</param>
    /// <returns>Boolean for if the user is queued for the product or not</returns>
    public static bool HasReservationForProduct(int productId, int userId, out int queuePosition)
    {
        var list = EntityFramework.Read.ReadHandler.GetReservationDetails()
        .Where(ld => ld.ProductId == productId)
        .OrderBy(ld => ld.ReservationDate)
        .ToList();

        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].UserId == userId)
            {
                queuePosition = i + 1;
                return true;
            }
        }
        queuePosition = 0;
        return false;
    }

    /// <summary>
    /// Adds a Product/Author/AuthorDetail if it doesnt exist already.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="authors"></param>
    /// <returns></returns>
    public static bool InsertProduct(Product product, List<Author> authors)
    {
        //Check if ISBN exists in DB.
        var productList = EntityFramework.Read.ReadHandler.GetProducts()
            .Where(x => x.Isbn == product.Isbn).ToList();
        if (productList.Count > 1)
            throw new Exception("Product already exist");

        //Prepare list of authorIds to be used when creating Authordettails.
        List<int> authorIds = new();

        //Save authors from database.
        var authorList = EntityFramework.Read.ReadHandler.GetAuthors();

        //Go through authors from input.
        //Check if they exists by comparing every author from database.
        //If author exists, use author id from "list of authors from database" and add it to id-list.
        //If it doesnt exist, create new author and save that new new Id and add to id-list.
        authors.ForEach(x =>
        {
            var authorExists = false;
            authorList.ForEach(d =>
            {
                if (x.FirstName == d.FirstName && x.LastName == d.LastName)
                {
                    authorIds.Add(d.Id);
                    authorExists = true;
                }
            });
            if (!authorExists)
            {
                var authorId = EntityFramework.Create.CreateHandler.CreateAuthor(x.FirstName, x.LastName);
                authorIds.Add(authorId);
            }
        });

        //If everything is ok and no exceptions has been thrown,
        //proceed with creating product and authordetail
        var prodId = EntityFramework.Create.CreateHandler.CreateProduct(
            product.Title,
            product.LanguageId,
            product.CategoryId,
            product.NrOfCopies,
            product.Dewey,
            product.Description,
            product.Isbn,
            product.ProductType
        );

        authorIds.ForEach(x =>
        {
            EntityFramework.Create.CreateHandler.CreateAuthorDetail(x, prodId);
        });

        return true;
    }
}