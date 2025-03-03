﻿using Messegify.Application.Dtos;
using Messegify.Application.Services;
using Messegify.Application.Services.MessageRequests;
using Microsoft.AspNetCore.Mvc;

namespace Messegify.Application.Controllers;

[ApiController]
[Route("api/chatRoom/{chatRoomId:guid}/message")]
public class MessageController : Controller
{
    private readonly IMessageRequestHandler _messageRequestHandler;
    
    public MessageController(IMessageRequestHandler messageRequestHandler)
    {
        _messageRequestHandler = messageRequestHandler;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromRoute] Guid chatRoomId, SendMessageDto dto, CancellationToken ct)
    {
        var request = new SendMessageRequest(dto, chatRoomId);

        await _messageRequestHandler.Handle(request, ct);

        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetChatroomMessages([FromRoute] Guid chatRoomId, 
        [FromQuery]int? pageSize, [FromQuery]int? pageNumber, CancellationToken ct)
    {
        object messagesDto;

        if (pageSize != null && pageNumber != null)
        {
            var request = new GetPagedMessagesRequest(chatRoomId, (int)pageSize, (int)pageNumber);
            messagesDto = await _messageRequestHandler.Handle(request, ct);
        }
        else
        {
            var request = new GetMessagesRequest(chatRoomId);
            messagesDto = await _messageRequestHandler.Handle(request, ct);
        }
        
        return Ok(messagesDto);
    }
}