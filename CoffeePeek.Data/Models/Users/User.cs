﻿namespace CoffeePeek.Data.Models.Users;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string Email { get; set; }
}