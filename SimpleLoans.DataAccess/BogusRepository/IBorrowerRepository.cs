using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using SimpleLoans.DataAccess.Models;

namespace SimpleLoans.DataAccess.BogusRepository
{
    public interface IBorrowerRepository
    {
         IEnumerable<Borrower> GetBorrowers();
         void Add();
         Task<Borrower> GetBorrowerById(string id);
    }

    public class BorrowerRepository : IBorrowerRepository
    {   
        private readonly FirestoreDb _firestoreDb;   
        public BorrowerRepository(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }            
        public void Add()
        {
            DocumentReference docRef = _firestoreDb.Collection("borrowers").Document("alovelace");
            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "First", "Ada" },
                { "Last", "Lovelace" },
                { "Born", 1815 }
            };
            var k = docRef.SetAsync(user).Result;
        }

        public async Task<Borrower> GetBorrowerById(string id)
        {
            DocumentReference docRef = _firestoreDb.Collection("borrowers").Document(id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            return snapshot.Exists ? snapshot.ConvertTo<Borrower>() : null;
        }

        public IEnumerable<Borrower> GetBorrowers()
        {
            var address = new Faker<Address>()
            .RuleFor(add => add.City, (f, u) => f.Address.City())
            .RuleFor(add => add.Street, (f,u) => f.Address.StreetName())
            .RuleFor(add => add.ApartmentNumber, (f, u) => f.Random.Number(1, 300))
            .RuleFor(add => add.Province, (f, u) => f.Address.County());
           
            
            var borrowers = new Faker<Borrower>()
            .RuleFor(bw => bw.Name, (f, u) => f.Name.FirstName())
            .RuleFor(bw => bw.Identification, (f, u) => f.Vehicle.ToString())
            .RuleFor(bw => bw.Address, (f, u) => address);
        
             return borrowers.Generate(10);
        }
    }
}