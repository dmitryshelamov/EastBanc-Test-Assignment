using System.Threading.Tasks;
using System.Web.Http;
using EastBancTestAssignment.KnapsackProblem.BLL.Services;
using EastBancTestAssignment.KnapsackProblem.DAL;
using EastBancTestAssignment.KnapsackProblem.DAL.Repositories;

namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.Controllers.Api
{
    public class BackpackController : ApiController
    {
        private BackpackTaskService _service;

        public BackpackController()
        {
            _service = new BackpackTaskService(new UnitOfWork(new AppDbContext()));
        }

        // DELETE: api/Backpack/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            await _service.DelelteBackpackTask(id);
            return Ok();
        }
    }
}
