using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Models;
using NutritionAPI.Models.Client_Requests;
using NutritionAPI.Models.ClientRequests;

namespace NutritionAPI.Controllers
{
    [Route("api/exercises")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly DataContext _context;
        public Task<List<Exercise>> _latestList { get; set; }

        public ExerciseController( DataContext context)
        {
            _context = context;
            _latestList = _context.Exercises.ToListAsync();
        }


        [HttpGet]
        public async Task<ActionResult<List<Exercise>>> Get()
        {
            return Ok(await _latestList );
        }

        [HttpPost]
        public async Task<ActionResult<List<Exercise>>> Create( Exercise_CR payload )
        {
            //  Create
            Exercise item = new Exercise( Guid.NewGuid() );
            
            //  Apply
            item.ApplyFromClientRequest( payload );
            
            //  Queue
            _context.Exercises.Add( item );
            
            //  Save
            await _context.SaveChangesAsync();
            
            //  List
            return Ok( await _latestList );
        }


        [HttpPut]
        public async Task<ActionResult<List<Exercise>>> Update( Exercise_CR payload )
        {

            //  Exist Check
            var item = await _context.Exercises.FindAsync( Guid.Parse(payload.id) );
            if ( item == null)
            {
                return BadRequest("Exercise not found");
            }

            //  Merge Updates
            item.ApplyFromClientRequest(payload);

            //  Save
            await _context.SaveChangesAsync();

            //  Return Full List
            return Ok( await _latestList );
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Exercise>>> Delete( string id )
        {
            //  Exist Check
            var item = await _context.Exercises.FindAsync( Guid.Parse(id) );
            if ( item == null)
            {
                return BadRequest( "Exercise not found" );
            }

            //  Queue
            _context.Exercises.Remove( item );
            
            //  Save
            await _context.SaveChangesAsync();

            //  List
            return Ok( await _latestList );
        }

    }
}
