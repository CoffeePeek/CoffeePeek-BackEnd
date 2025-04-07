﻿using CoffeePeek.Moderation.Application.Responses;
using CoffeePeek.Moderation.Contract.Abstract;
using MediatR;

namespace CoffeePeek.Moderation.Application.Requests;

public class RegisterUserRequest : IRequest<Response<RegisterUserResponse>>
{
    
}