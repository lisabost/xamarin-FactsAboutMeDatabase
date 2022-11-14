using FactsAboutMeDatabase.Models;
using SQLite;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FactsAboutMeDatabase.Data
{
    public class FactsDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public FactsDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(FactsAboutMe).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(FactsAboutMe)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public Task<List<FactsAboutMe>> GetItemsAsync()
        {
            return Database.Table<FactsAboutMe>().ToListAsync();
        }

        public Task<FactsAboutMe> GetItemAsync(int id)
        {
            return Database.Table<FactsAboutMe>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(FactsAboutMe item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> InsertList(IEnumerable<FactsAboutMe> items)
        {
            return Database.InsertAllAsync(items);
        }

        public Task<int> DeleteItemAsync(FactsAboutMe item)
        {
            return Database.DeleteAsync(item);
        }
        public Task<int> ClearAllAsync()
        {
            return Database.DeleteAllAsync<FactsAboutMe>();
        }
    }
}
