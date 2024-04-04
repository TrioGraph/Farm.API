namespace Farm.Models
{
    public class Field_Visit
    {
        public int? Id { get; set; }
	  public int? Field_Id { get; set; }
	  public int? Employee_Id { get; set; }
	  public string? Notes { get; set; }
	  public DateTime? Field_Visit_Date { get; set; }
	  public int? Photo_Id { get; set; }
	  public string? Status_Complete { get; set; }
	  public DateTime? Visist_Schedule_Date { get; set; }
	  public int? Prescription_Photo_Id { get; set; }
	  public bool? IsActive { get; set; }
	  

	  
    }
}
