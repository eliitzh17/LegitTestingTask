﻿namespace PlaywrightTests.Contract;

public class Credentials(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}