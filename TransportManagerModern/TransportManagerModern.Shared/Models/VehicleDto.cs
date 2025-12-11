using System;

namespace TransportManagerModern.Shared.Models
{
    public class VehicleDto
    {
        public int ComCode { get; set; }
        public int? StoreCode { get; set; }
        public string VehicleID { get; set; }
        public string VehicleNo { get; set; }
        public string EngineNo { get; set; }
        public string ChesisNo { get; set; }
        public string ModelNo { get; set; }
        public string EngineVolume { get; set; }
        public string PurchaseDate { get; set; } // Kept as string to match legacy for now, or DateTime? Legacy used generic GetString usually. Gateway param says string.
        public string VehicleDesc { get; set; }
        public string MobileNo { get; set; }
        public string Capacity { get; set; }
        public string CapacityUnit { get; set; }
        public decimal? KmPerLiter { get; set; }
        public int? FuelCode { get; set; }
        public bool IsHired { get; set; } // Legacy IsHired_12 is Decimal? In SQL it might be bit or int. Gateway param is Decimal.
        public string Remarks { get; set; }
        public int VehicleStatus { get; set; }
    }
}
