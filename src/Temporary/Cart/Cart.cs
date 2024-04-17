﻿using System.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ServiceComposer.AspNetCore;
using Temporary;

namespace OmNomNom.Website.ViewModelComposition;

public class Cart : ICompositionRequestsHandler
{
    private readonly OrderStorage storage;

    public Cart(OrderStorage storage)
    {
        this.storage = storage;
    }

    [HttpGet("/cart/{orderId}")]
    public Task Handle(HttpRequest request)
    {
        var vm = request.GetComposedResponseModel();
        var orderIdString = (string)request.HttpContext.GetRouteData().Values["orderId"]!;

        // Using the orderIdString we'll create a proper Guid and try to retrieve a potentially used address 
        if (!Guid.TryParse(orderIdString, out var orderId))
            orderId = Guid.NewGuid();

        vm.OrderId = orderId;
        
        //var cartItems = new Dictionary<int, dynamic>();
        //cartItems[0] = CreateCartItem(Guid.Parse("ff899e9d-4033-48d4-b189-e6ef4a3dc25b"), "Fremont Stout", 10, 3);;
        //cartItems[1] = CreateCartItem(Guid.Parse("0b3dcc85-110b-4491-9946-d20c0a51917b"), "Heineken", 0.5m, 24);;
        var order = storage.GetOrder(orderId);
        vm.CartItems = order.Cart.Items;//cartItems.Values.ToList();
        vm.TotalCartPrice = order.TotalCartPrice;

        return Task.CompletedTask;
    }
}