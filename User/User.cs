
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;


namespace Model;

public partial class User
{
    [Required(ErrorMessage = "UserId is required")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Gmail is required")]
    [EmailAddress(ErrorMessage = "Invalid email")]
    public string Gmail { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastname { get; set; }


    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}