namespace Farm.Models
{
    public class FarmField
    {
        public int? Id { get; set; }
	  public string? Tally_Field_Id { get; set; }
	  public int? Farmer_Id { get; set; }
	  public int? Village_Id { get; set; }
	  public string? Survey_Nos { get; set; }
	  public string? Mapping_Data_Id { get; set; }
	  public string? Gate_GPS_Lattitude { get; set; }
	  public string? Gate_GPS_Longitude { get; set; }
	  public int? Farmer_Supervisor_Id { get; set; }
	  public bool? IsActive { get; set; }
	  

	  
    }
}
