namespace Farm.Models
{
    public class Broadcast_Message
    {
        public int? Id { get; set; }
	  public string? Message_Text { get; set; }
	  public int? Employee_Id { get; set; }
	  public int? Farmer_id { get; set; }
	  public int? Farmer_Supervisor_Id { get; set; }
	  public DateTime? Message_Sent_Date { get; set; }
	  public bool? IsActive { get; set; }
	  

	  
    }
}
