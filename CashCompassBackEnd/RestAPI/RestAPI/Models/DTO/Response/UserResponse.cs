﻿namespace RestAPI.Models.DTO.Response;

public class UserResponse
{
    public UserResponse(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}