using Firebase.Database;

namespace portifolio_lucas_vilarim_api_rest.Data
{
    public class FirebaseContext
    {
        private readonly FirebaseClient _client;

        public FirebaseContext(string databaseUrl)
        {
            _client = new FirebaseClient(databaseUrl);
        }

        public FirebaseClient Client => _client;
    }
}
