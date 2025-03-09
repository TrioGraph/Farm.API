using Farm.Models;

public class SessionData
{
    private static SessionData _instance;    

    private SessionData() { }

    public static SessionData Instance
    {
        get
        {
            if(_instance == null)
                _instance = new SessionData();

            return _instance;
        }
    }

    // Your properties can then be whatever you want
    public static int? UserId { get; set; }

}