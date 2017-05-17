using MijnLijn.Models;
using System.Threading.Tasks;

namespace MijnLijn.Data.Remote
{
    public class LineManager
    {
        readonly IRestService _service;

        public LineManager(IRestService service)
        {
            this._service = service;
        }
        public Task<ApiResponse> GetLines(int[] stopNumbers)
        {
            return _service.PostToGetLines(stopNumbers);
        }
    }
}
