using LazyCache;

namespace ProductApi.Services
{
    public class CacheService
    {
        private readonly IAppCache _cache;

        public CacheService(IAppCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetStatusNameAsync(int statusKey)
        {
            var statuses = await _cache.GetOrAddAsync("productStatuses", () =>
            {
                return Task.FromResult(new Dictionary<int, string>
            {
                { 1, "Active" },
                { 0, "Inactive" }
            });
            }, TimeSpan.FromMinutes(5));

            return statuses.TryGetValue(statusKey, out var statusName) ? statusName : "Unknown";
        }
    }

}
