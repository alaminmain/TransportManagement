using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using TransportManagerModern.Shared.Models;

namespace TransportManagerModern.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public VehiclesController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleDto>> GetAll()
        {
            // Hardcoded ComCode = 1 for initial migration phase
            int comCode = 1; 
            
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            
            var sql = @"
                SELECT 
                    ComCode, StoreCode, VehicleID, VehicleNo, EngineNo, ChesisNo, ModelNo, 
                    EngineVolume, PurchaseDate, VehicleDesc, MobileNo, Capacity, CapacityUnit, 
                    KmPerLiter, FuelCode, IsHired, Remarks, VehicleStatus 
                FROM VehicleInfo 
                WHERE ComCode = @ComCode";

            return await connection.QueryAsync<VehicleDto>(sql, new { ComCode = comCode });
        }
    }
}
