using HRDirector.Controller;

namespace HRDirector.Service
{
    public class HRDirectorService
    {
        private readonly IHRDirectorApi _hrDirectorApi;

        public HRDirectorService(IHRDirectorApi hrDirectorApi)
        {
            _hrDirectorApi = hrDirectorApi;
        }

        public async Task<DirectorInfo> GetDirectorInfoAsync()
        {
            return await _hrDirectorApi.GetDirectorInfo();
        }
    }
}
