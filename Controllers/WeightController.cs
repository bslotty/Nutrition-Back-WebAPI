using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Models;
using NutritionAPI.Models.ClientRequests;

namespace NutritionAPI.Controllers
{
    [Route("api/weight")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        private readonly DataContext _context;
        public Task<List<Weight>> _latestList { get; set; }

        public WeightController(DataContext context)
        {
            _context = context;
            _latestList = _context.Weights.ToListAsync();
        }



        [HttpGet]
        public async Task<ActionResult<List<Weight>>> Get()
        {
            return Ok(await _latestList );
        }

        [HttpPost]
        public async Task<ActionResult<List<Weight>>> Create( Weight_CR payload )
        {
            //  Create
            Weight item = new Weight( Guid.NewGuid() );
            
            //  Apply
            item.ApplyFromClientRequest(payload);

            //  Queue
            _context.Weights.Add( item );

            //  Save
            await _context.SaveChangesAsync();

            //  List
            return Ok(await _latestList );
        }


        [HttpPut]
        public async Task<ActionResult<List<Weight>>> Update( Weight_CR payload )
        {

            //  Exist Check
            var item = await _context.Weights.FindAsync( new Guid(payload.id) );
            if ( item == null)
            {
                return BadRequest("Weight not found");
            }

            //  Apply
            item.ApplyFromClientRequest(payload );

            //  Save
            await _context.SaveChangesAsync();

            //  Return Full List
            return Ok(await _latestList );
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Weight>>> Delete(string id)
        {
            //  Exist
            var dbWeight = await _context.Weights.FindAsync( new Guid(id) );
            if (dbWeight == null)
            {
                return BadRequest("Weight not found");
            }

            //  Queue
            _context.Weights.Remove(dbWeight);
            
            //  Save
            await _context.SaveChangesAsync();

            //  List
            return Ok(await _latestList );
        }

    }
}
