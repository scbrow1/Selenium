using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class User
{
    public int ID { get; set; }
    public string Password { get; set; }
    public string IPAddress { get; set; }
    public int IsActive { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string EmailAddress { get; set; }
    public string ZipCode { get; set; }
    public DateTime BirthDate { get; set; }
    public int CountryID { get; set; }
    public string CountryName { get; set; }
    public int LanguageID { get; set; }
    public string LanguageName { get; set; }
    public string Gender { get; set; }
    public string PrimaryImage { get; set; }
    public int Verified { get; set; }
    public int SYTUser { get; set; }
    public int ViewExplicitPosts { get; set; }
    public int EmailOnComment { get; set; }
    public int EmailFavorites { get; set; }
    public int EmailOnFollow { get; set; }
    public int HasImage { get; set; }
    public int IsAdmin { get; set; }
    public int WhoCanMessage { get; set; }
    public int RememberMe { get; set; }

    public void Delete()
    {
        string query = "DELETE FROM Bubs WHERE ID = " + EmailAddress;
        Database.DeleteQuery(query);
    }
}

public class Comment
{
    public int iD { get; set; }
    public int nubID { get; set; }
    public int bubID { get; set; }
    public int parent { get; set; }
    public int numReplies { get; set; }
    public string sortIndex { get; set; }
    public string text { get; set; }
    public string elapsedTime { get; set; }
    public int anonymous { get; set; }

    public void Delete()
    {
        int commentId = Database.GetIdOfLastRecord("comments");
        string query1 = "DELETE FROM comments WHERE ID = " + commentId.ToString();
        Database.DeleteQuery(query1);
        string query2 = "UPDATE nubs set numcomments=0 where id = 1670";
        Database.UpdateQuery(query2);
    }
}