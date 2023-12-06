using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MCMicroservice.Repository;
using MCMicroservice.Repository.Models;

namespace MCMicroservice.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : ControllerBase
    {
        private DS_MeasurementDbContext _context;

        public MeasurementController(DS_MeasurementDbContext context)
        {
            _context = context;
        }

        //Methods for CRUD operations
        [HttpGet]
        [Route("GetAllMeasurements")]
        public List<Measurement> GetAllMeasurements()
        {
            return _context.Measurements.ToList();
        }

        [HttpPost]
        [Route("AddMeasurement")]
        public void AddMeasurement(Measurement measurement)
        {
            _context.Measurements.Add(measurement);
            _context.SaveChanges();
        }

        [HttpDelete]
        [Route("DeleteMeasurementById")]
        public void DeleteMeasurementById(int id)
        {
            var measurement = _context.Measurements.FirstOrDefault(x => x.Id == id);
            _context.Measurements.Remove(measurement);
            _context.SaveChanges();
        }

    }
}
