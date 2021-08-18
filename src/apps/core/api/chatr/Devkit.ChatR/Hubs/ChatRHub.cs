// -----------------------------------------------------------------------
// <copyright file="ChatRHub.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Hubs
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.ChatR.Business.Sessions.Commands.PostMessage;
    using MediatR;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// The session hub.
    /// </summary>
    /// <seealso cref="Hub" />
    public class ChatRHub : Hub
    {
        /// <summary>
        /// The mediator.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatRHub" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public ChatRHub(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task SendMessage(PostMessageCommand command)
        {
            var postedMessage = await this._mediator.Send(command);
            await this.Clients.Group(command.SessionId).SendAsync("ReceiveMessage", new { sessionId = command.SessionId, postedMessage });
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="message">The message.</param>
        /// <param name="replyTo">The reply to.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task SendMessageAlt(string sessionId, string userName, string message, string replyTo = default)
        {
            var postedMessage = await this._mediator.Send(new PostMessageCommand
            {
                UserName = userName,
                Message = message,
                SessionId = sessionId,
                ReplyTo = replyTo
            });

            await this.Clients.Group(sessionId).SendAsync("ReceiveMessage", postedMessage);
        }

        /// <summary>
        /// Joins the session.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task SubscribeToChatSession(string sessionId, string userName)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, sessionId, CancellationToken.None);
            await this.Clients.Group(sessionId).SendAsync("SubscribedToChatSession", sessionId, userName);
        }

        /// <summary>
        /// Leaves the session.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task UnsubscribeFromChatSession(string sessionId, string userName)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, sessionId, CancellationToken.None);
            await this.Clients.Group(sessionId).SendAsync("UnsubscribedFromChatSession", sessionId, userName);
        }
    }
}