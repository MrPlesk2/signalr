using System;
using System.Linq;
using BadNews.Models.Comments;
using BadNews.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsRepository commentsRepository;

        public CommentsController(CommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        [HttpGet("api/news/{id}/comments")]
        public ActionResult<CommentsDto> GetCommentsForNews(Guid id)
        {
            var comments = commentsRepository.GetComments(id);

            var commentDtos = comments.Select(comment => new CommentDto
            {
                User = comment.User,
                Value = comment.Value
            }).ToArray();

            var commentsDto = new CommentsDto
            {
                NewsId = id,
                Comments = commentDtos
            };

            return Ok(commentsDto);
        }
    }
}