// -----------------------------------------------------------------------
// <copyright file="RatingsController.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Controllers
{
    using System.Threading.Tasks;
    using Devkit.Ratings.Business.Ratings.Commands.SubmitRating;
    using Devkit.Ratings.Business.ViewModels;
    using Devkit.WebAPI;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The ratings controller.
    /// </summary>
    public class RatingsController : DevkitControllerBase
    {
        /// <summary>
        /// Submits the rating.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A submitted rating response object.</returns>
        [HttpPost]
        public async Task<RatingVM> SubmitRating(SubmitRatingCommand command) => await this.Mediator.Send(command);
    }
}