using System.ComponentModel.DataAnnotations;

namespace Northwind.Models;

public class CustomerWithPassword
{
    public Customer Customer { get; set; }
    // ReSharper disable once Mvc.TemplateNotResolved
    //Fixes an error in rider, ignore the above comment
    [UIHint("password"), Required]
    public string Password { get; set; }
}

public class LoginModel
{
    // ReSharper disable once Mvc.TemplateNotResolved
    //Fixes an error in rider, ignore the above comment
    [Required, UIHint("email")]
    public string Email { get; set; }

    // ReSharper disable once Mvc.TemplateNotResolved
    //Fixes an error in rider, ignore the above comment
    [Required, UIHint("password")]
    public string Password { get; set; }
}