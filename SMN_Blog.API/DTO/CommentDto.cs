﻿namespace SMN_Blog.API.DTO
{
    public class CommentDto
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int PostId { get; set; }
    }
}
