using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using SimpleLoans.DataAccess.Models;

namespace SimpleLoans.DataAccess.BogusRepository
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        Task<User> Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly FirestoreDb _firebase;
        public UserRepository(FirestoreDb firebase)
        {
            _firebase = firebase;
        }
        public async Task<User> Authenticate(string username, string password)
        {
             if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            DocumentReference docRef = _firebase.Collection("users").Document(username);
            var snapshopt = await docRef.GetSnapshotAsync();
          
            if(!snapshopt.Exists) return null;
            
             var user = snapshopt.ConvertTo<User>();

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public async Task<User> Create(User user, string password)
        {
             // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            DocumentReference docRef = _firebase.Collection("users").Document(user.Username);
            var snapshopt = await docRef.GetSnapshotAsync();
           
            if(snapshopt.Exists)
            {
                throw new Exception("Username \"" + user.Username + "\" is already taken");
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await docRef.SetAsync(user);
           
            return user;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async void Update(User user, string password = null)
        {

            DocumentReference docRef = _firebase.Collection("users").Document(user.Username);
            var snapshopt = await docRef.GetSnapshotAsync();
           
            if(!snapshopt.Exists)
                throw new Exception("User not found");
            
            var userParam = snapshopt.ConvertTo<User>();
            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
               // if (_context.Users.Any(x => x.Username == userParam.Username))
              //      throw new Exception("Username " + userParam.Username + " is already taken");
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
         
            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            Dictionary<FieldPath, object> updates = new Dictionary<FieldPath, object>
            {
                { new FieldPath("FirstName"), userParam.FirstName },
                { new FieldPath("LastName"), userParam.LastName },
                { new FieldPath("PasswordHash"), userParam.PasswordHash },
                { new FieldPath("PasswordSalt"), userParam.PasswordSalt },
            };

            await docRef.UpdateAsync(updates);
        }

         private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}