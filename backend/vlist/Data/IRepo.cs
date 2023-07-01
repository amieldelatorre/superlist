using vlist.Models.VList;

namespace vlist.Data
{
    public interface IRepo
    {
        public Task<VList> GetAsync(string id);
        public Task CreateAsync(VList vList);
        public Task UpdateAsync(string id, VList vList); 
    }
}
