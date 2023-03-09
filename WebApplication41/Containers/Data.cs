using System.Text.Json;
namespace WebApplication41.Containers
{
    public interface Saver
    {
        public Task<IDictionary<int, IEnumerable<string>>?> GetAll(string file);
        public int GetNumber(IDictionary<int, IEnumerable<string>> dict);


        public IEnumerable<string>? GetGame(IDictionary<int, IEnumerable<string>> dict, int number);
        public Task<int> Change(string file, IDictionary<int, IEnumerable<string>> dict, IEnumerable<string> board, int number);

        public Task<bool> Remove(string file, IDictionary<int, IEnumerable<string>> dict, int number);

        public Task<int> Add(string file, IDictionary<int, IEnumerable<string>> dict, IEnumerable<string> board, int number);
       
    }
    public class Data: Saver
    {
        public async Task<IDictionary<int, IEnumerable<string>>?> GetAll(string file)
        {
            IDictionary<int, IEnumerable<string>>? person;
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            person = await JsonSerializer.DeserializeAsync<IDictionary<int, IEnumerable<string>>>(fs);

            return person;
        }
        public  int GetNumber(IDictionary<int, IEnumerable<string>> dict)
        {
            if (dict.Keys.Count == 0) return 1;
            return dict.Keys.Max()+1;
        }
        public IEnumerable<string>? GetGame(IDictionary<int, IEnumerable<string>> dict , int number)
        {
            if (!dict.ContainsKey(number))
            return null;
            
            return dict[number];
        }
        public async Task<int> Add(string file, IDictionary<int, IEnumerable<string>> dict, IEnumerable<string> board, int number )
        {

            dict.Add(number, board);
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))          
            await JsonSerializer.SerializeAsync<IDictionary<int, IEnumerable<string>>>(fs, dict);
            return number;
            
        }
        public async Task<int> Change(string file, IDictionary<int, IEnumerable<string>> dict, IEnumerable<string> board, int number)
        {

            dict[number] = board;
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            await JsonSerializer.SerializeAsync<IDictionary<int, IEnumerable<string>>>(fs, dict);
            return number;

        }
        public async Task<bool> Remove(string file, IDictionary<int, IEnumerable<string>> dict,  int number)
        {

            var check = dict.Remove(number);
            if (!check) return false;
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            await JsonSerializer.SerializeAsync<IDictionary<int, IEnumerable<string>>>(fs, dict);
            return true;

        }
       

    }
}
